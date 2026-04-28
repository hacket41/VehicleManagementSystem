using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Data.DTO.Request;

public class PartsPurchaseRequest
{
    [Required, MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(50)]
    public string PartNumber { get; set; } = string.Empty;

    [MaxLength(500)]
    public string Description { get; set; } = string.Empty;

    public int CategoryId { get; set; }

    public int VendorId { get; set; }

    [MaxLength(500)]
    public string CompatibleVehicles { get; set; } = string.Empty;

    [Range(0.01 , double.MaxValue, ErrorMessage =  "Price must be greater than 0.")]
    public decimal CostPrice { get; set; }

    [Range(0.01 , double.MaxValue, ErrorMessage =  "Price must be greater than 0.")]
    public decimal SellingPrice { get; set; }

    [Range(0 , int.MaxValue, ErrorMessage =  "Stock Quantity must be greater than 0.")]
    public int StockQuantity { get; set; }

    public bool IsActive { get; set; } = true;

}