using backend.Data;
using backend.Data.DTO.Request;
using backend.Data.DTO.Response;
using backend.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Services.Implementation;

public class PartsService(AppDbContext db) : IPartsService
{


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

                VendorName = p.Vendor.Name,
                CategoryName = p.Category.Name,

                SellingPrice = p.SellingPrice,
                StockQuantity = p.StockQuantity,
                UpdatedAt = p.UpdatedAt
            }).FirstOrDefaultAsync();
    }

    public async Task<IActionResult> PurchasePart(PartsPurchase part)
    {
        throw new NotImplementedException();
    }

    public async Task<IActionResult> EditPart(PartsPurchase part)
    {
        throw new NotImplementedException();
    }

    public async Task<IActionResult> DeletePart(int id)
    {
        throw new NotImplementedException();
    }
}