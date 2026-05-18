using backend.Data.Entities;
using backend.Services.Implementation.Template;
using backend.Services.Interfaces;
using QuestPDF.Fluent;

namespace backend.Services.Implementation;

public class PdfService: IPdfService
{

    public byte[] GeneratePurchaseInvoice(PurchaseInvoice invoice)
    {
        var document = new PurchaseInvoiceTemplate(invoice);
        return document.GeneratePdf();
    }

    public byte[] GenerateSalesInvoice(SalesInvoice invoice)
    {
        throw new NotImplementedException();
    }
}