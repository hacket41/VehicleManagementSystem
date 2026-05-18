using backend.Data.DTO.Response;
using backend.Data.Entities;

namespace backend.Services.Interfaces;

public interface IInvoiceService
{
    Task<VendorInfoForInvoice?> GetVendorInformation(int vendorId);
    Task<User?> GetUserInformation(Guid userId);
}