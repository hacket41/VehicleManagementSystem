using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using backend.Data.Enums;

namespace backend.Data.Entities;

public class Sale
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(30)]
    public string InvoiceNumber { get; set; } = string.Empty;

    public Guid CustomerId { get; set; }
    public User Customer { get; set; } = null!;

    public Guid StaffId { get; set; }
    public User Staff { get; set; } = null!;

    public int? VehicleId { get; set; }
    public Vehicle? Vehicle { get; set; }

    public DateTime SaleDate { get; set; } = DateTime.UtcNow;

    [Column(TypeName = "decimal(18,2)")]
    public decimal SubTotal { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal DiscountPercent { get; set; } = 0;

    [Column(TypeName = "decimal(18,2)")]
    public decimal DiscountAmount { get; set; } = 0;

    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalAmount { get; set; }

    public PaymentMethod PaymentMethod { get; set; }
    public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Pending;

    public DateTime? CreditDueDate { get; set; }

    public bool LoyaltyDiscountApplied { get; set; } = false;

    [MaxLength(500)]
    public string Notes { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<SaleItem> Items { get; set; } = new List<SaleItem>();
    public ICollection<CreditReminder> CreditReminders { get; set; } = new List<CreditReminder>();
}

