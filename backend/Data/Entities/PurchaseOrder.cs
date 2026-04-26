using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using backend.Data.Enums;

namespace backend.Data.Entities;

public class PurchaseOrder
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(30)]
    public string PurchaseOrderNumber { get; set; } = string.Empty;

    public int VendorId { get; set; }
    public Vendor Vendor { get; set; } = null!;

    public Guid CreatedByAdminId { get; set; }
    public User CreatedByAdmin { get; set; } = null!;

    public DateTime OrderDate { get; set; } = DateTime.UtcNow;
    public DateTime? ReceivedDate { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalCost { get; set; }

    public PurchaseStatus Status { get; set; } = PurchaseStatus.Draft;

    [MaxLength(500)]
    public string Notes { get; set; } = string.Empty;

    public ICollection<PurchaseItem> Items { get; set; } = new List<PurchaseItem>();
}

public class PurchaseItem
{
    [Key]
    public int Id { get; set; }

    public int PurchaseOrderId { get; set; }
    public PurchaseOrder PurchaseOrder { get; set; } = null!;

    public int PartId { get; set; }
    // public Part Part { get; set; } = null!;

    public int Quantity { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal UnitCost { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal LineTotal { get; set; }
}