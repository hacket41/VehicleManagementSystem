using backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin,Staff")]
public class CustomerSearchController(ICustomerSearchService customerSearchService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Search([FromQuery] string q)
    {
        if (string.IsNullOrWhiteSpace(q))
            return BadRequest("Search query cannot be empty.");

        var results = await customerSearchService.SearchCustomersAsync(q);
        return Ok(results);
    }
}