using backend.Data.DTO.Request;
using backend.Data.Enums;

namespace backend.Services.Interfaces;

public interface ICustomerReportService
{
    Task<CustomerReportDto> GenerateReportAsync(GenerateCustomerReportRequest request, Guid staffId);
    Task<CustomerReportDto?> GetReportByIdAsync(int reportId);
    Task<List<CustomerReportDto>> GetAllReportsAsync(CustomerReportType? type = null);
}