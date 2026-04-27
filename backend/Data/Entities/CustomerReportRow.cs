namespace backend.Data.Entities;

public class CustomerReportRow
{
    public Guid CustomerId { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public int TotalPurchases { get; set; }
    public decimal TotalSpent { get; set; }
    public decimal OutstandingCredit { get; set; }
    public DateTime? OldestUnpaidCreditDate { get; set; }
}