using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using backend.Data.Enums;

namespace backend.Data.Entities;

public class PurchaseOrder
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(30)]
    public string PurchaseOrderNumber { get; set; } = string.Empty;

    [ForeignKey(nameof(Vendor))]
    public int VendorId { get; set; }
    public Vendor? Vendor { get; set; }

    [ForeignKey(nameof(User))]
    public Guid CreatedByAdminId { get; set; }
    public User? User { get; set; }

    public DateTime OrderDate { get; set; } = DateTime.UtcNow;
    public DateTime? ReceivedDate { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalCost { get; set; }

    public PurchaseStatus Status { get; set; } = PurchaseStatus.Draft;

    [MaxLength(500)]
    public string Notes { get; set; } = string.Empty;

    [JsonIgnore]
    public ICollection<PurchaseItem>? Items { get; set; }
}