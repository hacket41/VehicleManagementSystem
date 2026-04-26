using backend.Data;
using backend.Data.DTO.Request;
using backend.Data.Entities;
using backend.Data.Enums;
using backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backend.Services.Implementation;

public class FinancialReportService(AppDbContext db) : IFinancialReportService
{
    public async Task<FinancialReportDto> GenerateReportAsync(
        GenerateFinancialReportRequest request, Guid adminId)
    {
        var (start, end) = GetPeriodBounds(request.Type, request.Date);

        var sales = await db.Sales
            .Where(s => s.SaleDate >= start && s.SaleDate <= end)
            .Include(s => s.Items)
            .ToListAsync();

     
        var purchases = await db.PurchaseOrders
            .Where(po => po.OrderDate >= start && po.OrderDate <= end
                         && po.Status == PurchaseStatus.Received)
            .ToListAsync();

        
        var outstandingCredit = await db.Sales
            .Where(s => s.PaymentMethod == PaymentMethod.Credit
                        && s.PaymentStatus != PaymentStatus.Paid)
            .SumAsync(s => s.TotalAmount);

        var report = new FinancialReport
        {
            Type = request.Type,
            PeriodStart = start,
            PeriodEnd = end,
            TotalSalesRevenue = sales.Sum(s => s.TotalAmount),
            TotalPurchaseCost = purchases.Sum(p => p.TotalCost),
            TotalDiscountsGiven = sales.Sum(s => s.DiscountAmount),
            GrossProfit = sales.Sum(s => s.TotalAmount) - purchases.Sum(p => p.TotalCost),
            TotalCreditOutstanding = outstandingCredit,
            TotalTransactions = sales.Count,
            TotalUnitsSold = sales.SelectMany(s => s.Items).Sum(i => i.Quantity),
            GeneratedByUserId = adminId,
        };

        db.FinancialReports.Add(report);
        await db.SaveChangesAsync();

        return await MapToDto(report);
    }

    public async Task<FinancialReportDto?> GetReportByIdAsync(int reportId)
    {
        var report = await db.FinancialReports
            .Include(r => r.GeneratedByUser)
            .FirstOrDefaultAsync(r => r.Id == reportId);

        return report is null ? null : await MapToDto(report);
    }

    public async Task<List<FinancialReportDto>> GetAllReportsAsync(ReportType? type = null)
    {
        var query = db.FinancialReports
            .Include(r => r.GeneratedByUser)
            .AsQueryable();

        if (type.HasValue)
            query = query.Where(r => r.Type == type.Value);

        var reports = await query.OrderByDescending(r => r.GeneratedAt).ToListAsync();
        var dtos = new List<FinancialReportDto>();

        foreach (var r in reports)
            dtos.Add(await MapToDto(r));

        return dtos;
    }
    

    private static (DateTime start, DateTime end) GetPeriodBounds(ReportType type, DateTime date)
    {
        return type switch
        {
            ReportType.Daily => (
                date.Date,
                date.Date.AddDays(1).AddTicks(-1)
            ),
            ReportType.Monthly => (
                new DateTime(date.Year, date.Month, 1),
                new DateTime(date.Year, date.Month, 1).AddMonths(1).AddTicks(-1)
            ),
            ReportType.Yearly => (
                new DateTime(date.Year, 1, 1),
                new DateTime(date.Year, 12, 31, 23, 59, 59, 999)
            ),
            _ => throw new ArgumentOutOfRangeException(nameof(type))
        };
    }

    private static Task<FinancialReportDto> MapToDto(FinancialReport r)
    {
        return Task.FromResult(new FinancialReportDto
        {
            Id = r.Id,
            Type = r.Type.ToString(),
            PeriodStart = r.PeriodStart,
            PeriodEnd = r.PeriodEnd,
            TotalSalesRevenue = r.TotalSalesRevenue,
            TotalPurchaseCost = r.TotalPurchaseCost,
            TotalDiscountsGiven = r.TotalDiscountsGiven,
            GrossProfit = r.GrossProfit,
            TotalCreditOutstanding = r.TotalCreditOutstanding,
            TotalTransactions = r.TotalTransactions,
            TotalUnitsSold = r.TotalUnitsSold,
            GeneratedAt = r.GeneratedAt,
            GeneratedBy = r.GeneratedByUser is null
                ? "Unknown"
                : $"{r.GeneratedByUser.FirstName} {r.GeneratedByUser.LastName}",
        });
    }
}