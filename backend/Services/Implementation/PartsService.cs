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
            .OrderByDescending(p => p.UpdatedAt)
            .Select(p => new PartsWithDetailsResponse
            {
                Id = p.Id,
                Name = p.Name,
                PartNumber = p.PartNumber,
                Description = p.Description,

                VendorId = p.VendorId,
                VendorName = p.Vendor!.Name,

                CategoryId = p.CategoryId,
                CategoryName = p.Category!.Name,

                CostPrice = p.CostPrice,
                SellingPrice = p.SellingPrice,
                StockQuantity = p.StockQuantity,

                ImageUrl = p.ImageUrl,
                IsActive = p.IsActive,
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

                ImageUrl = p.ImageUrl,
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
            ImageUrl = request.ImageUrl,
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
        updatedPart.ImageUrl = part.ImageUrl;
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

    public async Task<int> RestockPart(RestockPartDto request)
    {
        if (request.Id <= 0) return 0;

        var affectedRows =await db.Parts
            .Where(p => p.Id == request.Id)
            .ExecuteUpdateAsync(setters  => setters
                .SetProperty(
                    p  => p.StockQuantity,
                    p => p.StockQuantity + request.StockQuantity)
            );

        if(affectedRows == 0) return 0;

        var updatedStockQuantity = await db.Parts
            .Where(p => p.Id == request.Id)
            .Select(p => p.StockQuantity)
            .FirstAsync();

        return updatedStockQuantity;
    }

    public async Task<List<PartCategory>> GetPartsCategories()
    {
        var partsCategories = await db.PartCategories.ToListAsync();
        return partsCategories;
    }

    public async Task<string> AddPartsCategory(PartCategoryRequest categoryName)
    {
        var category = new PartCategory
        {
            Name = categoryName.Name
        };
        await db.PartCategories.AddAsync(category);
        await db.SaveChangesAsync();
        return categoryName.Name;
    }




    public async Task<bool> DeletePartsCategory(int id)
    {
        var result = await db.PartCategories.Where(pc => pc.Id == id).ExecuteDeleteAsync();
        return result > 0;
    }

    public async Task<string> EditPartsCategory(PartCategory partCategory)
    {
        var category = await db.PartCategories.FirstOrDefaultAsync(p => p.Id == partCategory.Id);
        if (category == null) return null!;

        category.Name = partCategory.Name;
        await db.SaveChangesAsync();
        return partCategory.Name;
    }

}