using backend.Data.DTO.Request;
using backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace backend.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Staff,Admin")]
public class SaleController(ISaleService saleService) : ControllerBase
{
    /// <summary>
    /// Create a sale. Automatically applies 10% loyalty discount when SubTotal &gt; 5000.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> CreateSale([FromBody] CreateSaleRequest request)
    {
        try
        {
            var staffId = GetCurrentUserId();
            var result  = await saleService.CreateSaleAsync(request, staffId);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await saleService.GetSaleByIdAsync(id);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpGet("customer/{customerId:guid}")]
    [Authorize(Roles = "Staff,Admin,Customer")]
    public async Task<IActionResult> GetByCustomer(Guid customerId)
    {
        var result = await saleService.GetSalesByCustomerAsync(customerId);
        return Ok(result);
    }

    private Guid GetCurrentUserId()
        => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)
                      ?? throw new InvalidOperationException("User ID claim missing."));
}
