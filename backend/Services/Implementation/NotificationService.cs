using backend.Data;
using backend.Data.Entities;
using backend.Data.Enums;
using backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using backend.Data.DTO.Request;

namespace backend.Services.Implementation;

public class NotificationService(AppDbContext db, ILogger<NotificationService> logger) : INotificationService
{
    
    public async Task<NotificationSummaryDto> CheckLowStockAsync()
    {
        var summary = new NotificationSummaryDto();

        var lowParts = await db.Parts
            .Where(p => p.IsActive && p.StockQuantity < p.LowStockThreshold)
            .ToListAsync();

        // Only create a new notification if there isn't already a Pending one for this part
        var alreadyPending = await db.LowStockNotifications
            .Where(n => n.Status == NotificationStatus.Pending)
            .Select(n => n.PartId)
            .ToListAsync();

        foreach (var part in lowParts)
        {
            if (alreadyPending.Contains(part.Id))
            {
                summary.AlreadyNotified++;
                continue;
            }

            var notification = new LowStockNotification
            {
                PartId             = part.Id,
                StockAtTimeOfAlert = part.StockQuantity,
                Status             = NotificationStatus.Sent,  
                SentAt             = DateTime.UtcNow,
            };

            db.LowStockNotifications.Add(notification);
            summary.Processed++;
            summary.Details.Add($"LOW STOCK ALERT: Part '{part.Name}' (#{part.PartNumber}) — only {part.StockQuantity} units left (threshold: {part.LowStockThreshold}).");
            logger.LogWarning("Low stock alert: Part {Name} has {Qty} units.", part.Name, part.StockQuantity);
        }

        if (summary.Processed > 0)
            await db.SaveChangesAsync();

        return summary;
    }
    
    public async Task<NotificationSummaryDto> SendCreditRemindersAsync()
    {
        var summary = new NotificationSummaryDto();
        var cutoff  = DateTime.UtcNow.AddMonths(-1);

        var overdueSales = await db.Sales
            .Include(s => s.Customer)
            .Where(s => s.PaymentMethod == PaymentMethod.Credit
                     && s.PaymentStatus != PaymentStatus.Paid
                     && s.SaleDate <= cutoff)
            .ToListAsync();

        var alreadyReminded = await db.CreditReminders
            .Where(cr => cr.Status == NotificationStatus.Sent)
            .Select(cr => cr.SaleId)
            .ToListAsync();

        foreach (var sale in overdueSales)
        {
            if (alreadyReminded.Contains(sale.Id))
            {
                summary.AlreadyNotified++;
                continue;
            }

            var daysOverdue = (int)(DateTime.UtcNow - sale.SaleDate).TotalDays - 30;

            var reminder = new CreditReminder
            {
                SaleId        = sale.Id,
                CustomerId    = sale.CustomerId,
                InvoiceNumber = sale.InvoiceNumber,
                AmountDue     = sale.TotalAmount,
                DueDate       = sale.CreditDueDate ?? sale.SaleDate.AddMonths(1),
                DaysOverdue   = daysOverdue,
                Status        = NotificationStatus.Sent,
                SentAt        = DateTime.UtcNow,
            };

            db.CreditReminders.Add(reminder);
            summary.Processed++;
            summary.Details.Add(
                $"CREDIT REMINDER: Customer '{sale.Customer.FirstName} {sale.Customer.LastName}' " +
                $"({sale.Customer.Email}) — Invoice {sale.InvoiceNumber}, Amount: {sale.TotalAmount:C}, " +
                $"{daysOverdue} days overdue.");

            logger.LogInformation(
                "Credit reminder: Invoice {Invoice} for customer {Email} is {Days} days overdue.",
                sale.InvoiceNumber, sale.Customer.Email, daysOverdue);
        }

        if (summary.Processed > 0)
            await db.SaveChangesAsync();

        return summary;
    }
}
