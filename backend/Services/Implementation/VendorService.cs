using backend.Data;
using backend.Data.DTO.Request;
using backend.Data.DTO.Response;
using backend.Data.Entities;
using backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backend.Services.Implementation
{
    public class VendorService(AppDbContext db) : IVendorService
    {
        public async Task<List<VendorResponse>> GetAllVendors()
        {
            return await db.Vendors
                .Select(v => new VendorResponse
                {
                    Id = v.Id,
                    Name = v.Name,
                    ContactPerson = v.ContactPerson,
                    Phone = v.Phone,
                    Email = v.Email,
                    Address = v.Address,
                    IsActive = v.IsActive,
                    CreatedAt = v.CreatedAt
                }).ToListAsync();
        }

        public async Task<VendorResponse?> GetVendorById(int id)
        {
            return await db.Vendors
                 .Where(v => v.Id == id)
                  .Select(v => new VendorResponse
                  {
                      Id = v.Id,
                      Name = v.Name,
                      ContactPerson = v.ContactPerson,
                      Phone = v.Phone,
                      Email = v.Email,
                      Address = v.Address,
                      IsActive = v.IsActive,
                      CreatedAt = v.CreatedAt
                  }).FirstOrDefaultAsync();
        }

        public async Task<VendorResponse> CreateVendor(VendorRequestDto request)
        {
            var vendor = new Vendor
            {
                Name = request.Name,
                ContactPerson = request.ContactPerson,
                Phone = request.Phone,
                Email = request.Email,
                Address = request.Address,
                IsActive = request.IsActive,
                CreatedAt = DateTime.UtcNow
            };

            db.Vendors.Add(vendor);
            await db.SaveChangesAsync();

            return new VendorResponse
            {
                //Id = vendor.Id,
                Name = request.Name,
                ContactPerson = request.ContactPerson,
                Phone = request.Phone,
                Email = request.Email,
                Address = request.Address,
                IsActive = request.IsActive,
                //CreatedAt = request.CreatedAt
            };
        }

        public async Task<VendorResponse?> EditVendor(int id, VendorRequestDto request)
        {
            var vendor = await db.Vendors.FirstOrDefaultAsync(v => v.Id == id);

            if (vendor == null) return null;

            vendor.Name = request.Name;
            vendor.ContactPerson = request.ContactPerson;
            vendor.Phone = request.Phone;
            vendor.Email = request.Email;
            vendor.Address = request.Address;
            vendor.IsActive = request.IsActive;

            await db.SaveChangesAsync();

            return new VendorResponse
            {
                Id = vendor.Id,
                Name = vendor.Name,
                ContactPerson = vendor.ContactPerson,
                Phone = vendor.Phone,
                Email = vendor.Email,
                Address = vendor.Address,
                IsActive = vendor.IsActive,
                CreatedAt = vendor.CreatedAt
            };
        }

        public async Task<bool> DeleteVendor(int id)
        {
            var result = await db.Vendors
           .Where(v => v.Id == id)
           .ExecuteDeleteAsync();

            return result > 0;
        }
    }
}
