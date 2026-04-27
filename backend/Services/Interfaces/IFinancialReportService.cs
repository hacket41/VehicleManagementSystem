using backend.Data.DTO.Request;
using backend.Data.Enums;

namespace backend.Services.Interfaces;

public interface IFinancialReportService
{
    Task<FinancialReportDto> GenerateReportAsync(GenerateFinancialReportRequest request, Guid adminId);
    Task<FinancialReportDto?> GetReportByIdAsync(int reportId);
    Task<List<FinancialReportDto>> GetAllReportsAsync(ReportType? type = null);
}