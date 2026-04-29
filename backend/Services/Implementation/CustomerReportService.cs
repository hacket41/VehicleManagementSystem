using System.Text.Json;
using backend.Data;
using backend.Data.DTO.Request;
using backend.Data.Entities;
using backend.Data.Enums;
using backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backend.Services.Implementation;

public class CustomerReportService(AppDbContext db) : ICustomerReportService
{
    public async Task<CustomerReportDto> GenerateReportAsync(GenerateCustomerReportRequest request, Guid staffId)
    {
        List<CustomerReportRowDto> rows = request.Type switch
        {
            CustomerReportType.TopSpenders      => await GetTopSpenders(request),
            CustomerReportType.RegularCustomers => await GetRegularCustomers(request),
            CustomerReportType.PendingCredits   => await GetPendingCredits(),
            _ => throw new ArgumentOutOfRangeException()
        };

        var report = new CustomerReport
        {
            Type               = request.Type,
            PeriodStart        = request.PeriodStart ?? DateTime.MinValue,
            PeriodEnd          = request.PeriodEnd   ?? DateTime.UtcNow,
            ResultJson         = JsonSerializer.Serialize(rows),
            GeneratedByStaffId = staffId,
        };

        db.CustomerReports.Add(report);
        await db.SaveChangesAsync();

        await db.Entry(report).Reference(r => r.User).LoadAsync();

        return MapToDto(report, rows);
    }

    public async Task<CustomerReportDto?> GetReportByIdAsync(int reportId)
    {
        var report = await db.CustomerReports
            .Include(r => r.User)
            .FirstOrDefaultAsync(r => r.Id == reportId);

        if (report is null) return null;

        var rows = JsonSerializer.Deserialize<List<CustomerReportRowDto>>(report.ResultJson) ?? [];
        return MapToDto(report, rows);
    }

    public async Task<List<CustomerReportDto>> GetAllReportsAsync(CustomerReportType? type = null)
    {
        var query = db.CustomerReports
            .Include(r => r.User)
            .AsQueryable();

        if (type.HasValue) query = query.Where(r => r.Type == type.Value);

        var reports = await query.OrderByDescending(r => r.GeneratedAt).ToListAsync();

        return reports.Select(r =>
        {
            var rows = JsonSerializer.Deserialize<List<CustomerReportRowDto>>(r.ResultJson) ?? [];
            return MapToDto(r, rows);
        }).ToList();
    }


    private async Task<List<CustomerReportRowDto>> GetTopSpenders(GenerateCustomerReportRequest req)
    {
        var q = db.Sales.Where(s => s.PaymentStatus == PaymentStatus.Paid);

        if (req.PeriodStart.HasValue) q = q.Where(s => s.SaleDate >= req.PeriodStart.Value);
        if (req.PeriodEnd.HasValue)   q = q.Where(s => s.SaleDate <= req.PeriodEnd.Value);

        return await q
            .GroupBy(s => s.Customer)
            .Select(g => new CustomerReportRowDto
            {
                CustomerId     = g.Key.Id,
                FullName       = g.Key.FirstName + " " + g.Key.LastName,
                Email          = g.Key.Email ?? "",
                Phone          = g.Key.PhoneNumber ?? "",
                TotalPurchases = g.Count(),
                TotalSpent     = g.Sum(s => s.TotalAmount),
            })
            .OrderByDescending(r => r.TotalSpent)
            .Take(20)
            .ToListAsync();
    }

    private async Task<List<CustomerReportRowDto>> GetRegularCustomers(GenerateCustomerReportRequest req)
    {
        var q = db.Sales.AsQueryable();

        if (req.PeriodStart.HasValue) q = q.Where(s => s.SaleDate >= req.PeriodStart.Value);
        if (req.PeriodEnd.HasValue)   q = q.Where(s => s.SaleDate <= req.PeriodEnd.Value);

        return await q
            .GroupBy(s => s.Customer)
            .Where(g => g.Count() >= 2)
            .Select(g => new CustomerReportRowDto
            {
                CustomerId     = g.Key.Id,
                FullName       = g.Key.FirstName + " " + g.Key.LastName,
                Email          = g.Key.Email ?? "",
                Phone          = g.Key.PhoneNumber ?? "",
                TotalPurchases = g.Count(),
                TotalSpent     = g.Sum(s => s.TotalAmount),
            })
            .OrderByDescending(r => r.TotalPurchases)
            .ToListAsync();
    }

    private async Task<List<CustomerReportRowDto>> GetPendingCredits()
    {
        var overdueCutoff = DateTime.UtcNow.AddMonths(-1);

        return await db.Sales
            .Where(s => s.PaymentMethod == PaymentMethod.Credit
                     && s.PaymentStatus != PaymentStatus.Paid
                     && s.SaleDate <= overdueCutoff)
            .GroupBy(s => s.Customer)
            .Select(g => new CustomerReportRowDto
            {
                CustomerId             = g.Key.Id,
                FullName               = g.Key.FirstName + " " + g.Key.LastName,
                Email                  = g.Key.Email ?? "",
                Phone                  = g.Key.PhoneNumber ?? "",
                TotalPurchases         = g.Count(),
                OutstandingCredit      = g.Sum(s => s.TotalAmount),
                OldestUnpaidCreditDate = g.Min(s => s.SaleDate),
            })
            .OrderByDescending(r => r.OutstandingCredit)
            .ToListAsync();
    }

    private static CustomerReportDto MapToDto(CustomerReport r, List<CustomerReportRowDto> rows) => new()
    {
        Id          = r.Id,
        Type        = r.Type.ToString(),
        PeriodStart = r.PeriodStart == DateTime.MinValue ? null : r.PeriodStart,
        PeriodEnd   = r.PeriodEnd,
        GeneratedAt = r.GeneratedAt,
        GeneratedBy = $"{r.User?.FirstName} {r.User?.LastName}".Trim(),
        Rows        = rows,
    };
}
