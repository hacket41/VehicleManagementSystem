using System.ComponentModel.DataAnnotations;
using backend.Data.Enums;

namespace backend.Data.Entities;

public class CustomerReport
{
    [Key]
    public int Id { get; set; }

    public CustomerReportType Type { get; set; }

    public DateTime PeriodStart { get; set; }
    public DateTime PeriodEnd { get; set; }

    public string ResultJson { get; set; } = string.Empty;

    public DateTime GeneratedAt { get; set; } = DateTime.UtcNow;

    public Guid GeneratedByStaffId { get; set; }
    public User GeneratedByStaff { get; set; } = null!;
}

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