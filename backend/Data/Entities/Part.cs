using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Data.Entities;

public class Part
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(50)]
    public string PartNumber { get; set; } = string.Empty;

    [MaxLength(500)]
    public string Description { get; set; } = string.Empty;

    [MaxLength(100)]
    public string Category { get; set; } = string.Empty;
    
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


    public ICollection<SaleItem> SaleItems { get; set; } = new List<SaleItem>();
    public ICollection<PurchaseItem> PurchaseItems { get; set; } = new List<PurchaseItem>();
    public ICollection<PartRequest> PartRequests { get; set; } = new List<PartRequest>();
    public ICollection<LowStockNotification> LowStockNotifications { get; set; } = new List<LowStockNotification>();
}