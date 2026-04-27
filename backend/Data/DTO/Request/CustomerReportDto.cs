using backend.Data.Enums;

namespace backend.Data.DTO.Request;

public class GenerateCustomerReportRequest
{
    public CustomerReportType Type { get; set; }
    public DateTime? PeriodStart { get; set; }
    public DateTime? PeriodEnd { get; set; }
}

public class CustomerReportDto
{
    public int Id { get; set; }
    public string Type { get; set; } = string.Empty;
    public DateTime? PeriodStart { get; set; }
    public DateTime? PeriodEnd { get; set; }
    public DateTime GeneratedAt { get; set; }
    public string GeneratedBy { get; set; } = string.Empty;
    public List<CustomerReportRowDto> Rows { get; set; } = new();
}

public class CustomerReportRowDto
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
