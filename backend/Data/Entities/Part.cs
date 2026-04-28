using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace backend.Data.Entities;

public class Part
{
    [Key]
    public int Id { get; init; }

    [Required, MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(50)]
    public string PartNumber { get; set; } = string.Empty;

    [MaxLength(500)]
    public string Description { get; set; } = string.Empty;

    [ForeignKey(nameof(Vendor))]
    public int VendorId { get; set; }
    public Vendor? Vendor { get; set; }

    [ForeignKey(nameof(Category))]
    public int CategoryId { get; set; }
    public PartCategory? Category { get; set; }
    
    [MaxLength(500)]
    public string CompatibleVehicles { get; set; } = string.Empty;

    [Column(TypeName = "decimal(18,2)")]
    public decimal CostPrice { get; set; }       

    [Column(TypeName = "decimal(18,2)")]
    public decimal SellingPrice { get; set; }   

    public int StockQuantity { get; set; }

    public int LowStockThreshold { get; set; } = 10;

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;


    [JsonIgnore]
    public List<SaleItem>? SaleItems { get; set; }

    [JsonIgnore]
    public List<PurchaseItem>? PurchaseItems { get; set; }

    [JsonIgnore]
    public List<PartRequest>? PartRequests { get; set; }

    [JsonIgnore]
    public List<LowStockNotification>? LowStockNotifications { get; set; }
}