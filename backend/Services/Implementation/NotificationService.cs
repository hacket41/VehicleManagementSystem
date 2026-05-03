using backend.Data;
using backend.Data.DTO.Request;
using backend.Data.Entities;
using backend.Data.Enums;
using backend.Services.Interfaces;
using backend.Services.Implementation.Template;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace backend.Services.Implementation;

public class NotificationService(
    AppDbContext db,
    IEmailService emailService,
    UserManager<User> userManager,
    ILogger<NotificationService> logger) : INotificationService
{
    public async Task<NotificationSummaryDto> CheckLowStockAsync()
    {
        var summary = new NotificationSummaryDto();

        var lowParts = await db.Parts
            .Where(p => p.IsActive && p.StockQuantity < p.LowStockThreshold)
            .ToListAsync();

        var alreadyPending = await db.LowStockNotifications
            .Where(n => n.Status == NotificationStatus.Pending)
            .Select(n => n.PartId)
            .ToListAsync();

        var newlyAlertedParts = new List<Part>();

        foreach (var part in lowParts)
        {
            if (alreadyPending.Contains(part.Id))
            {
                summary.AlreadyNotified++;
                continue;
            }

            db.LowStockNotifications.Add(new LowStockNotification
            {
                PartId             = part.Id,
                StockAtTimeOfAlert = part.StockQuantity,
                Status             = NotificationStatus.Sent,
                SentAt             = DateTime.UtcNow,
            });

            newlyAlertedParts.Add(part);
            summary.Processed++;
            summary.Details.Add(
                $"LOW STOCK ALERT: Part '{part.Name}' (#{part.PartNumber}) — " +
                $"only {part.StockQuantity} units left (threshold: {part.LowStockThreshold}).");

            logger.LogWarning("Low stock alert: Part {Name} has {Qty} units.", part.Name, part.StockQuantity);
        }

        if (newlyAlertedParts.Count > 0)
        {
            await db.SaveChangesAsync();

            var admins = await userManager.GetUsersInRoleAsync("Admin");
            var adminList = admins.Where(a => !string.IsNullOrEmpty(a.Email)).ToList();

            foreach (var admin in adminList)
            {
                try
                {
                    await emailService.SendAsync(
                        toEmail:  admin.Email!,
                        toName:   admin.UserName ?? "Admin",
                        subject:  $"[AutoParts] Low Stock Alert — {newlyAlertedParts.Count} part(s) need attention",
                        htmlBody: EmailTemplates.LowStockAlert(newlyAlertedParts));
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Failed to send low-stock alert to admin {Email}.", admin.Email);
                }
            }
        }

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

            db.CreditReminders.Add(new CreditReminder
            {
                SaleId        = sale.Id,
                CustomerId    = sale.CustomerId,
                InvoiceNumber = sale.InvoiceNumber,
                AmountDue     = sale.TotalAmount,
                DueDate       = sale.CreditDueDate ?? sale.SaleDate.AddMonths(1),
                DaysOverdue   = daysOverdue,
                Status        = NotificationStatus.Sent,
                SentAt        = DateTime.UtcNow,
            });

            summary.Processed++;
            summary.Details.Add(
                $"CREDIT REMINDER: Customer '{sale.Customer.FirstName} {sale.Customer.LastName}' " +
                $"({sale.Customer.Email}) — Invoice {sale.InvoiceNumber}, " +
                $"Amount: {sale.TotalAmount:C}, {daysOverdue} days overdue.");

            try
            {
                await emailService.SendAsync(
                    toEmail:  sale.Customer.Email,
                    toName:   $"{sale.Customer.FirstName} {sale.Customer.LastName}",
                    subject:  $"Payment Reminder — Invoice {sale.InvoiceNumber}",
                    htmlBody: EmailTemplates.CreditReminder(sale, daysOverdue));
            }
            catch (Exception ex)
            {
                logger.LogError(ex,
                    "Failed to send credit reminder for invoice {Invoice} to {Email}.",
                    sale.InvoiceNumber, sale.Customer.Email);
            }

            logger.LogInformation(
                "Credit reminder: Invoice {Invoice} for {Email} is {Days} days overdue.",
                sale.InvoiceNumber, sale.Customer.Email, daysOverdue);
        }

        if (summary.Processed > 0)
            await db.SaveChangesAsync();

        return summary;
    }
}