using backend.Data;
using backend.Data.DTO.Request;
using backend.Data.DTO.Response;
using backend.Data.Entities;
using backend.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
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

    public async Task<IActionResult> EditPart(PartsPurchaseRequest part)
    {
        throw new NotImplementedException();
    }

    public async Task<IActionResult> DeletePart(int id)
    {
        throw new NotImplementedException();
    }
}