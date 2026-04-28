using backend.Data.DTO.Request;
using backend.Data.DTO.Response;
using Microsoft.AspNetCore.Mvc;

namespace backend.Services.Interfaces;

public interface IPartsService
{
    public Task <List<PartsWithDetailsResponse>> GetAllParts();
    public Task<PartsWithDetailsResponse?> GetPartWithDetails(int id);

    public Task<IActionResult> PurchasePart(PartsPurchaseRequest part);

    public Task<IActionResult> EditPart(PartsPurchaseRequest part);

    public Task<IActionResult> DeletePart(int id);

}