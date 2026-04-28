using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using backend.Data.Enums;

namespace backend.Data.Entities;

public class LowStockNotification
{
    [Key]
    public int Id { get; set; }

    [ForeignKey(nameof(Part))]
    public int PartId { get; set; }
    public Part Part { get; set; } = null!;

    public int StockAtTimeOfAlert { get; set; }

    public NotificationStatus Status { get; set; } = NotificationStatus.Pending;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? SentAt { get; set; }

    [MaxLength(500)]
    public string ErrorMessage { get; set; } = string.Empty;
}