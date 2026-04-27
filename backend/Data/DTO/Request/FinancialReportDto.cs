
using backend.Data.Enums;

namespace backend.Data.DTO.Request;

public class FinancialReportDto
{
    public int Id { get; set; }
    public string Type { get; set; } = string.Empty;
    public DateTime PeriodStart { get; set; }
    public DateTime PeriodEnd { get; set; }
    public decimal TotalSalesRevenue { get; set; }
    public decimal TotalPurchaseCost { get; set; }
    public decimal TotalDiscountsGiven { get; set; }
    public decimal GrossProfit { get; set; }
    public decimal TotalCreditOutstanding { get; set; }
    public int TotalTransactions { get; set; }
    public int TotalUnitsSold { get; set; }
    public DateTime GeneratedAt { get; set; }
    public string GeneratedBy { get; set; } = string.Empty;
}