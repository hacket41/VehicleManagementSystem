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
        var result = await authService.RegisterCustomer(user);
        return Ok(result);
    }

    [HttpPost("register-staff")]
    public async Task<IActionResult> RegisterStaff(RegisterUserDto user)
    {
        var result = await authService.RegisterStaff(user);
        return Ok(result);
    }

    [HttpPost("register-admin")]
    public async Task<IActionResult> RegisterAdmin(RegisterUserDto user)
    {
        var result = await authService.RegisterAdmin(user);
        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest user)
    {
        var result = await authService.Login(user);
        if (!result.Success) return BadRequest(result);
        authService.SetTokenInsideCookies(HttpContext,  result.Token!, result.RefreshToken! );
        return Ok(result);
    }

    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken(RequestRefreshTokenDto request)
    {
        var result = await authService.RefreshTokens(request);
        if (result is null || result.Token is null || result.RefreshToken is null) return Unauthorized("Invalid Refresh Token");
        authService.SetTokenInsideCookies(HttpContext, result.Token, result.RefreshToken);
        return Ok(result);
    }

}