using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using backend.Data.Enums;

namespace backend.Data.Entities;

public class FinancialReport
{
    [Key]
    public int Id { get; set; }

    public ReportType Type { get; set; }

    public DateTime PeriodStart { get; set; }
    public DateTime PeriodEnd { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalSalesRevenue { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalPurchaseCost { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalDiscountsGiven { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal GrossProfit { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalCreditOutstanding { get; set; }

    public int TotalTransactions { get; set; }
    public int TotalUnitsSold { get; set; }

    public DateTime GeneratedAt { get; set; } = DateTime.UtcNow;

    public Guid GeneratedByUserId { get; set; }
    public User GeneratedByUser { get; set; } = null!;
}