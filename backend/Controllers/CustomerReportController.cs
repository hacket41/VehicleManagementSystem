using backend.Data.DTO.Request;
using backend.Data.Enums;
using backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace backend.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Staff,Admin")]
public class CustomerReportController(ICustomerReportService reportService) : ControllerBase
{
    [HttpPost("generate")]
    public async Task<IActionResult> Generate([FromBody] GenerateCustomerReportRequest request)
    {
        var staffId = GetCurrentUserId();
        var result = await reportService.GenerateReportAsync(request, staffId);
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await reportService.GetReportByIdAsync(id);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] CustomerReportType? type)
    {
        var result = await reportService.GetAllReportsAsync(type);
        return Ok(result);
    }

    private Guid GetCurrentUserId()
        => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)
                      ?? throw new InvalidOperationException("User ID claim missing."));
}
