using backend.Data;
using backend.Data.DTO.Request;
using backend.Data.Entities;
using backend.Data.Enums;
using backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backend.Services.Implementation;

public class SaleService(AppDbContext db, INotificationService notificationService) : ISaleService
{
    private const decimal LoyaltyThreshold   = 5000m;
    private const decimal LoyaltyDiscountPct = 10m;

    public async Task<SaleResponseDto> CreateSaleAsync(CreateSaleRequest request, Guid staffId)
    {
        // Validate parts exist and have enough stock
        var partIds = request.Items.Select(i => i.PartId).ToList();
        var parts   = await db.Parts.Where(p => partIds.Contains(p.Id)).ToListAsync();

        if (parts.Count != partIds.Count)
            throw new InvalidOperationException("One or more parts not found.");

        // Build sale items & subtotal
        var saleItems = new List<SaleItem>();
        decimal subTotal = 0;

        foreach (var itemReq in request.Items)
        {
            var part = parts.First(p => p.Id == itemReq.PartId);
            if (part.StockQuantity < itemReq.Quantity)
                throw new InvalidOperationException($"Insufficient stock for '{part.Name}'. Available: {part.StockQuantity}.");

            var lineTotal = part.SellingPrice * itemReq.Quantity;
            subTotal += lineTotal;

            saleItems.Add(new SaleItem
            {
                PartId    = part.Id,
                Quantity  = itemReq.Quantity,
                UnitPrice = part.SellingPrice,
                LineTotal = lineTotal,
            });
        }

        // Loyalty discount: 10% if subtotal > 5000
        bool loyaltyApplied    = subTotal > LoyaltyThreshold;
        decimal discountPct    = loyaltyApplied ? LoyaltyDiscountPct : 0m;
        decimal discountAmount = loyaltyApplied ? Math.Round(subTotal * discountPct / 100, 2) : 0m;
        decimal totalAmount    = subTotal - discountAmount;

        var invoiceNumber = $"INV-{DateTime.UtcNow:yyyyMMddHHmmss}-{Random.Shared.Next(100, 999)}";

        var sale = new Sale
        {
            InvoiceNumber         = invoiceNumber,
            CustomerId            = request.CustomerId,
            StaffId               = staffId,
            VehicleId             = request.VehicleId,
            PaymentMethod         = request.PaymentMethod,
            PaymentStatus         = request.PaymentMethod == PaymentMethod.Credit
                                        ? PaymentStatus.Pending
                                        : PaymentStatus.Paid,
            CreditDueDate         = request.CreditDueDate,
            SubTotal              = subTotal,
            DiscountPercent       = discountPct,
            DiscountAmount        = discountAmount,
            TotalAmount           = totalAmount,
            LoyaltyDiscountApplied = loyaltyApplied,
            Notes                 = request.Notes,
            Items                 = saleItems,
        };

        // Deduct stock
        foreach (var itemReq in request.Items)
        {
            var part = parts.First(p => p.Id == itemReq.PartId);
            part.StockQuantity -= itemReq.Quantity;
            part.UpdatedAt      = DateTime.UtcNow;
        }

        db.Sales.Add(sale);
        await db.SaveChangesAsync();

        // Trigger low-stock check after sale (non-blocking, best-effort)
        try { await notificationService.CheckLowStockAsync(); }
        catch { /* log but don't fail the sale */ }

        return await MapToDto(sale, saleItems, parts);
    }

    public async Task<SaleResponseDto?> GetSaleByIdAsync(int saleId)
    {
        var sale = await db.Sales
            .Include(s => s.Items).ThenInclude(i => i.Part)
            .FirstOrDefaultAsync(s => s.Id == saleId);

        if (sale is null) return null;
        return await MapToDto(sale, sale.Items.ToList(), sale.Items.Select(i => i.Part).ToList());
    }

    public async Task<List<SaleResponseDto>> GetSalesByCustomerAsync(Guid customerId)
    {
        var sales = await db.Sales
            .Include(s => s.Items).ThenInclude(i => i.Part)
            .Where(s => s.CustomerId == customerId)
            .OrderByDescending(s => s.SaleDate)
            .ToListAsync();

        var result = new List<SaleResponseDto>();
        foreach (var s in sales)
            result.Add(await MapToDto(s, s.Items.ToList(), s.Items.Select(i => i.Part).ToList()));
        return result;
    }

    private static Task<SaleResponseDto> MapToDto(Sale sale, List<SaleItem> items, List<Part> parts)
    {
        return Task.FromResult(new SaleResponseDto
        {
            Id                     = sale.Id,
            InvoiceNumber          = sale.InvoiceNumber,
            SubTotal               = sale.SubTotal,
            DiscountPercent        = sale.DiscountPercent,
            DiscountAmount         = sale.DiscountAmount,
            TotalAmount            = sale.TotalAmount,
            LoyaltyDiscountApplied = sale.LoyaltyDiscountApplied,
            PaymentMethod          = sale.PaymentMethod.ToString(),
            PaymentStatus          = sale.PaymentStatus.ToString(),
            SaleDate               = sale.SaleDate,
            Items = items.Select(i =>
            {
                var part = parts.FirstOrDefault(p => p.Id == i.PartId);
                return new SaleItemResponseDto
                {
                    PartId    = i.PartId,
                    PartName  = part?.Name ?? "Unknown",
                    Quantity  = i.Quantity,
                    UnitPrice = i.UnitPrice,
                    LineTotal = i.LineTotal,
                };
            }).ToList(),
        });
    }
}
