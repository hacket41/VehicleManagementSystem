namespace backend.Data.Entities;

public class SalesInvoice
{
    public int InvoiceNumber { get; set; }
    public DateTime IssueDate { get; set; }

    public required User Customer { get; set; }
}