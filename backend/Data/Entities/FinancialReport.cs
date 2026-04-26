using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace backend.Data.Entities;

public class FinancialReport
{
    [Key]
    public int Id { get; set; }
    
    public DateTime GenerateDate {get; set;}

    public ReportType Type { get; set;}

    public decimal TotalSales { get; set; }
    
    public decimal TotalPurchases { get; set; }
    
    public decimal Profit { get; set; }

    public Guid? GeneratedByUserId { get; set; }

    public User? GeneratedByUser { get; set; }
}
