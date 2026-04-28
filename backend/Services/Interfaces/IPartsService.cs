using backend.Data.DTO.Request;
using Microsoft.AspNetCore.Mvc;

namespace backend.Services.Interfaces;

public interface IPartsService
{
    public Task<IActionResult> PurchasePart(PartsPurchase part);

    public Task<IActionResult> EditPart(PartsPurchase part);

    public Task<IActionResult> DeletePart(int id);

}