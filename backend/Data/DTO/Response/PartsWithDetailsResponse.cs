namespace backend.Data.DTO.Response;

public class PartsWithDetailsResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string PartNumber { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int VendorId { get; set; }
    public string VendorName { get; set; } = string.Empty;
    public int CategoryId { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public string CompatibleVehicle { get; set; } = string.Empty;

    public decimal CostPrice { get; set; }
    public decimal SellingPrice { get; set; }
    public int StockQuantity { get; set; }

    public string ImageUrl { get; set; } = string.Empty;

    public bool IsActive { get; set; }
    public DateTime UpdatedAt { get; set; }
}