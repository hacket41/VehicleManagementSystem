using backend.Data.DTO.Response;

namespace backend.Data.Entities;

public class PurchaseInvoice
{
    public int InvoiceNumber { get; set; }
    public DateTime IssueDate { get; set; }

    public VendorInfoForInvoice? Vendor { get; set; }
    public List<Part>? Parts { get; set; }

}