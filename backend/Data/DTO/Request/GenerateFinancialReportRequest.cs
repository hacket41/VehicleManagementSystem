namespace backend.Data.DTO.Request;
using backend.Data.Enums;
public class GenerateFinancialReportRequest
{
    public ReportType Type { get; set; }
    public DateTime Date { get; set; }
}