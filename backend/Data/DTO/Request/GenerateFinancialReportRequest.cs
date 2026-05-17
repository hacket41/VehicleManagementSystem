namespace backend.Data.DTO.Request;
using backend.Data.Enums;
public class GenerateFinancialReportRequest
{
    public ReportType Type { get; set; }
    public DateOnly Date { get; set; }
}
