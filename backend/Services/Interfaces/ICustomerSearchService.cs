using backend.Data.DTO.Response;

namespace backend.Services.Interfaces;

public interface ICustomerSearchService
{
    Task<List<CustomerSearchResponse>> SearchCustomersAsync(string query);
}