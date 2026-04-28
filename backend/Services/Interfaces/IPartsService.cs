using backend.Data.DTO.Request;
using backend.Data.DTO.Response;
using Microsoft.AspNetCore.Mvc;

namespace backend.Services.Interfaces;

public interface IPartsService
{
    public Task <List<PartsWithDetailsResponse>> GetAllParts();
    public Task<PartsWithDetailsResponse?> GetPartWithDetails(int id);

    public Task<PartsWithDetailsResponse> PurchasePart(PartsPurchaseRequest part);

    public Task<PartsWithDetailsResponse?> EditPart(int id, PartsPurchaseRequest part);

    public Task<IActionResult> DeletePart(int id);

}