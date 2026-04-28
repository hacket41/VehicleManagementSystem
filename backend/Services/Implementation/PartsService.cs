using backend.Data.DTO.Request;
using backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace backend.Services.Implementation;

public class PartsService : IPartsService
{

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