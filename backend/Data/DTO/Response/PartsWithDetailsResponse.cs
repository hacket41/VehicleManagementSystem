namespace backend.Data.DTO.Response;

public class PartsWithDetailsResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string PartNumber { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string VendorName { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
    public decimal SellingPrice { get; set; }
    public int StockQuantity { get; set; }
    public DateTime UpdatedAt { get; set; }
}