using backend.Data.DTO.Request;
using backend.Data.DTO.Response;

namespace backend.Services.Interfaces
{
    public interface IVendorService
    {
        Task<List<VendorResponse>> GetAllVendors();
        Task<VendorResponse?> GetVendorById(int id);
        Task<VendorResponse> CreateVendor(VendorRequestDto request);
        Task<VendorResponse?> EditVendor(int id, VendorRequestDto request);
        Task<bool> DeleteVendor(int id);
    }
}
