using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace backend.Data.Entities;
using backend.Data.Enums;

public class SaleItem
{
    [Key]
    public int Id { get; set; }

    [ForeignKey(nameof(Sale))]
    public int SaleId { get; set; }
    public Sale? Sale { get; set; }

    [ForeignKey(nameof(Part))]
    public int PartId { get; set; }
    public Part? Part { get; set; }

    public int Quantity { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal UnitPrice { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal LineTotal { get; set; }
}