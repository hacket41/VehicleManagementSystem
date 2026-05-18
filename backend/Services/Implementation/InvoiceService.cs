using backend.Data;
using backend.Data.DTO.Response;
using backend.Data.Entities;
using backend.Services.Interfaces;

namespace backend.Services.Implementation;

public class InvoiceService (
    AppDbContext db,
    IVendorService vendorService
    ): IInvoiceService
{

    public async Task<VendorInfoForInvoice?> GetVendorInformation(int vendorId)
    {
        var vendor = await vendorService.GetVendorById(vendorId);
        if (vendor is null) return null!;

        return new VendorInfoForInvoice
        {
            Name = vendor.Name,
            ContactPerson =  vendor.ContactPerson,
            Email = vendor.Email,
            Phone = vendor.Phone,
            Address = vendor.Address,
        };
    }

    public async Task<User?> GetUserInformation(Guid userId)
    {
        var user = await db.Users.FindAsync(userId);
        return user;
    }
}