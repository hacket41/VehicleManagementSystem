namespace backend.Services.Interfaces;
using backend.Data.DTO.Request;
public interface ISaleService
{
    Task<SaleResponseDto> CreateSaleAsync(CreateSaleRequest request, Guid staffId);
    Task<SaleResponseDto?> GetSaleByIdAsync(int saleId);
    Task<List<SaleResponseDto>> GetSalesByCustomerAsync(Guid customerId);

}