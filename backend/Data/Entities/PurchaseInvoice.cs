using backend.Data.DTO.Response;

namespace backend.Data.Entities;

public class PurchaseInvoice
{
    public string InvoiceNumber { get; set; }
    public DateTime IssueDate { get; set; }

    public VendorInfoForInvoice Vendor { get; set; } 

    public PartsWithDetailsResponse Part { get; set; }

}