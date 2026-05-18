using backend.Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

public class VehicleController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetVehicle(Vehicle vehicle)
    {
        return Ok();
    }
}