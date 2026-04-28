using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using backend.Data.Enums;
namespace backend.Data.Entities;



public class CreditReminder
{
    [Key]
    public int Id { get; set; }

    [ForeignKey(nameof(Sale))]
    public int SaleId { get; set; }
    public Sale? Sale { get; set; }

    [ForeignKey(nameof(User))]
    public Guid CustomerId { get; set; }

    public User? User { get; set; }

    [MaxLength(50)]
    public string InvoiceNumber { get; set; } = string.Empty;

    public decimal AmountDue { get; set; }

    public DateTime DueDate { get; set; }

    public int DaysOverdue { get; set; }

    public NotificationStatus Status { get; set; } = NotificationStatus.Pending;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? SentAt { get; set; }

    [MaxLength(500)]
    public string ErrorMessage { get; set; } = string.Empty;
}