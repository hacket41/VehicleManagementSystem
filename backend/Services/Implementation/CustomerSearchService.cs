using backend.Data;
using backend.Data.DTO.Response;
using backend.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using backend.Data.Entities;

namespace backend.Services.Implementation;

public class CustomerSearchService(
    AppDbContext db,
    UserManager<User> userManager
) : ICustomerSearchService
{
    public async Task<List<CustomerSearchResponse>> SearchCustomersAsync(string query)
    {
        var normalizedQuery = query.Trim().ToLower();

        // Get all users in the "Customer" role
        var customers = await userManager.GetUsersInRoleAsync("Customer");

        // Filter by name, email, phone, or ID 
        var filtered = customers.Where(u =>
            u.FirstName.ToLower().Contains(normalizedQuery) ||
            u.LastName.ToLower().Contains(normalizedQuery) ||
            (u.FirstName + " " + u.LastName).ToLower().Contains(normalizedQuery) ||
            (u.Email ?? "").ToLower().Contains(normalizedQuery) ||
            (u.PhoneNumber ?? "").Contains(normalizedQuery) ||
            u.Id.ToString().ToLower().Contains(normalizedQuery)
        ).ToList();

        // Also search by vehicle number (join from Vehicles table)
        var vehicleMatchIds = await db.Vehicles
            .Where(v => v.VehicleNumber.ToLower().Contains(normalizedQuery))
            .Select(v => v.CustomerId)
            .Distinct()
            .ToListAsync();

        // Merge: customers matched by profile OR by vehicle number
        var allMatchedIds = filtered.Select(u => u.Id)
            .Union(vehicleMatchIds)
            .Distinct()
            .ToList();

        // Fetch vehicles for all matched customers
        var vehicles = await db.Vehicles
            .Where(v => allMatchedIds.Contains(v.CustomerId))
            .ToListAsync();

        // Build final response
        var result = new List<CustomerSearchResponse>();
        foreach (var id in allMatchedIds)
        {
            var user = customers.FirstOrDefault(u => u.Id == id);
            if (user is null) continue;

            result.Add(new CustomerSearchResponse
            {
                Id = user.Id,
                Name = user.FirstName + " " + user.LastName,
                Email = user.Email ?? "",
                PhoneNumber = user.PhoneNumber ?? "",
                Address = user.Address,
                Vehicles = vehicles
                    .Where(v => v.CustomerId == id)
                    .Select(v => new VehicleSummary
                    {
                        Id = v.Id,
                        VehicleNumber = v.VehicleNumber,
                        Make = v.Make,
                        Model = v.Model,
                        Year = v.Year
                    }).ToList()
            });
        }

        return result;
    }
}