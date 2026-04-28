using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using backend.Data.Enums;

namespace backend.Data.Entities;


public class Sale
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(30)]
    public string InvoiceNumber { get; set; } = string.Empty;

    [ForeignKey(nameof(Customer))]
    public Guid CustomerId { get; set; }
    public User? Customer { get; set; }

    [ForeignKey(nameof(Staff))]
    public Guid StaffId { get; set; }
    public User? Staff { get; set; }

    [ForeignKey(nameof(Vehicle))]
    public int? VehicleId { get; set; }
    public Vehicle? Vehicle { get; set; }

    public DateTime SaleDate { get; set; } = DateTime.UtcNow.Date;

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

    public bool LoyaltyDiscountApplied { get; set; }

    [MaxLength(500)]
    public string? Notes { get; set; }

    // public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [JsonIgnore]
    public List<SaleItem> Items { get; set; } = [];

    [JsonIgnore]
    public List<CreditReminder> CreditReminders { get; set; } = [];
}