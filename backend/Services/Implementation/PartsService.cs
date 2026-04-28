using backend.Data;
using backend.Data.DTO.Request;
using backend.Data.DTO.Response;
using backend.Data.Entities;
using backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Services.Implementation;

public class PartsService(AppDbContext db) : IPartsService
{


    public async Task<List<PartsWithDetailsResponse>> GetAllParts()
    {
        return await db.Parts
            .Select(p => new PartsWithDetailsResponse
            {
                Id = p.Id,
                Name = p.Name,
                PartNumber = p.PartNumber,
                Description = p.Description,

                VendorName = p.Vendor!.Name,
                CategoryName = p.Category!.Name,

                SellingPrice = p.SellingPrice,
                StockQuantity = p.StockQuantity,
                UpdatedAt = p.UpdatedAt
            }).ToListAsync();
    }

    public async Task<PartsWithDetailsResponse?> GetPartWithDetails(int id)
    {
        return await db.Parts
            .Where(p => p.Id == id)
            .Select(p => new PartsWithDetailsResponse
            {
                Id = p.Id,
                Name = p.Name,
                PartNumber = p.PartNumber,
                Description = p.Description,

                VendorName = p.Vendor!.Name,
                CategoryName = p.Category!.Name,

                CompatibleVehicle = p.CompatibleVehicles,
                SellingPrice = p.SellingPrice,
                StockQuantity = p.StockQuantity,
                UpdatedAt = p.UpdatedAt
            }).FirstOrDefaultAsync();
    }

    public async Task<PartsWithDetailsResponse> PurchasePart(PartsPurchaseRequest request)
    {
        var part = new Part
        {
            Name = request.Name,
            PartNumber = request.PartNumber,
            Description = request.Description,
            VendorId = request.VendorId,
            CategoryId = request.CategoryId,
            CompatibleVehicles = request.CompatibleVehicles,
            CostPrice = request.CostPrice,
            SellingPrice = request.SellingPrice,
            StockQuantity = request.StockQuantity,
            IsActive = request.IsActive,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        db.Parts.Add(part);
        await db.SaveChangesAsync();

        await db.Entry(part).Reference(p => p.Vendor).LoadAsync();
        await db.Entry(part).Reference(p => p.Category).LoadAsync();

        return new PartsWithDetailsResponse
        {
            Id = part.Id,
            Name = part.Name,
            PartNumber = part.PartNumber,
            Description = part.Description,
            VendorName = part.Vendor != null ? part.Vendor.Name : "Unknown",
            CategoryName = part.Category != null ? part.Category.Name : "Unknown",
            SellingPrice = part.SellingPrice,
            StockQuantity = part.StockQuantity,
            UpdatedAt = part.UpdatedAt
        };

    }

    public async Task<PartsWithDetailsResponse?> EditPart(int id, PartsPurchaseRequest part)
    {

        var updatedPart = await db.Parts
            .Include(p => p.Vendor)
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (updatedPart == null) return null;

        updatedPart.Name = part.Name;
        updatedPart.PartNumber = part.PartNumber;
        updatedPart.Description = part.Description;
        updatedPart.VendorId = part.VendorId;
        updatedPart.CategoryId = part.CategoryId;
        updatedPart.CompatibleVehicles = part.CompatibleVehicles;
        updatedPart.SellingPrice = part.SellingPrice;
        updatedPart.StockQuantity = part.StockQuantity;
        updatedPart.StockQuantity = part.StockQuantity;
        updatedPart.IsActive = part.IsActive;
        updatedPart.UpdatedAt = DateTime.UtcNow;

        await db.SaveChangesAsync();

        return new PartsWithDetailsResponse
        {
            Id = id,
            Name = updatedPart.Name,
            PartNumber = updatedPart.PartNumber,
            Description = updatedPart.Description,
            VendorName = updatedPart.Vendor?.Name ?? "Unknown",
            CategoryName = updatedPart.Category?.Name ?? "Unknown",
            SellingPrice = updatedPart.SellingPrice,
            StockQuantity = updatedPart.StockQuantity,
            UpdatedAt = updatedPart.UpdatedAt
        };
    }

    public async Task<bool> DeletePart(int id)
    {
        var result = await db.Parts
            .Where(p => p.Id == id)
            .ExecuteDeleteAsync();

        return result > 0;
    }
}