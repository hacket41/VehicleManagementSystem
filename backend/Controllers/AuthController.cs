using backend.Data.DTO.Request;
using backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(IAuthService authService) : ControllerBase
{
    [HttpPost("register-customer")]
    public async Task<IActionResult> RegisterCustomer(RegisterUserDto user)
    {
        var result  = await authService.RegisterCustomer(user);
        return Ok(result);
    }
}