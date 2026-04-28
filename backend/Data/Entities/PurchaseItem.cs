using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using backend.Data.Enums;
namespace backend.Data.Entities;

public class PurchaseItem
{
    [Key]
    public int Id { get; set; }

    [ForeignKey(nameof(PurchaseOrder))]
    public int PurchaseOrderId { get; set; }
    public PurchaseOrder PurchaseOrder { get; set; } = null!;

    [ForeignKey(nameof(Part))]
    public int PartId { get; set; }
    public Part? Part { get; set; }

    public int Quantity { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal UnitCost { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal LineTotal { get; set; }
}