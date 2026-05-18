using backend.Data.Entities;

namespace backend.Services.Interfaces;

public interface IVehicleService
{
    Task<bool> SaleVehicle(Vehicle vehicle);
}