using backend.Data.DTO.Request;
using backend.Data.DTO.Response;
using backend.Data.Entities;

namespace backend.Services.Interfaces;

public interface IPartsService
{
    Task <List<PartsWithDetailsResponse>> GetAllParts();

    Task<PartsWithDetailsResponse?> GetPartWithDetails(int id);

    Task<PartsWithDetailsResponse> PurchasePart(PartsPurchaseRequest part);

    Task<PartsWithDetailsResponse?> EditPart(int id, PartsPurchaseRequest part);

    Task<bool> DeletePart(int id);

    Task<List<PartCategory>> GetPartsCategories();

    Task<string> AddPartsCategory(PartCategoryRequest categoryName);

    Task<bool> DeletePartsCategory(int id);

    Task<string> EditPartsCategory(PartCategory partCategory);

}