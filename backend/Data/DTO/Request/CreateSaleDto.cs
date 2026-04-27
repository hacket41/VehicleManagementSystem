using backend.Data.Enums;

namespace backend.Data.DTO.Request;

public class CreateSaleRequest
{
    public Guid CustomerId { get; set; }
    public int? VehicleId { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public DateTime? CreditDueDate { get; set; }
    public string Notes { get; set; } = string.Empty;
    public List<SaleItemRequest> Items { get; set; } = new();
}

public class SaleItemRequest
{
    public int PartId { get; set; }
    public int Quantity { get; set; }
}



public class SaleItemResponseDto
{
    public int PartId { get; set; }
    public string PartName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal LineTotal { get; set; }
}
