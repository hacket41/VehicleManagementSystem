using backend.Data.Entities;

namespace backend.Services.Interfaces;

public interface IPdfService
{
    byte[] GeneratePurchaseInvoice(PurchaseInvoice invoice);
    byte[] GenerateSalesInvoice(SalesInvoice invoice);

}