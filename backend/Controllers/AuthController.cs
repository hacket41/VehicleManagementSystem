using System.Security.Claims;
using backend.Data.DTO.Request;
using backend.Data.DTO.Response;
using backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(
    IAuthService authService,
    IJwtTokenService jwtTokenService
    ) : ControllerBase
{
    [HttpPost("register-customer")]
    public async Task<IActionResult> RegisterCustomer(RegisterUserDto user)
    {
        var result = await authService.RegisterCustomer(user);
        return result.Success? Ok(result) : BadRequest(result);
    }

    [HttpPost("register-staff")]
    public async Task<IActionResult> RegisterStaff(RegisterUserDto user)
    {
        var result = await authService.RegisterStaff(user);
        return result.Success? Ok(result) : BadRequest(result);
    }

    [HttpPost("register-admin")]
    public async Task<IActionResult> RegisterAdmin(RegisterUserDto user)
    {
        var result = await authService.RegisterAdmin(user);
        return result.Success? Ok(result) : BadRequest(result);

    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest user)
    {
        var result = await authService.Login(user);

        if (!result.Success)
            return Unauthorized(ApiResponse<AuthResponseDto>.Fail(
                "Login failed",
                result.Errors
            ));

        authService.SetTokenInsideCookies(HttpContext,  result.Token!, result.RefreshToken! );
        return Ok();
    }

    [HttpPost("logout")]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if(!Guid.TryParse(userId, out var userGuid)) return Unauthorized("Invalid Token");

        await authService.Logout(userGuid, HttpContext);
        return Ok();
    }

    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken()
    {
        HttpContext.Request.Cookies.TryGetValue("accessToken", out var accessToken);
        HttpContext.Request.Cookies.TryGetValue("refreshToken", out var refreshToken);
        if(accessToken is null || refreshToken is null) return Unauthorized("Missing or Invalid Tokens");

        var principal = jwtTokenService.GetPrincipalFromToken(accessToken);
        if(principal is null) return Unauthorized("Invalid Token");

        var userId = principal?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if(!Guid.TryParse(userId, out var userGuid))  return Unauthorized("Invalid Token, Cannot convert to GUID");

        var result = await authService.RefreshTokens(new RequestRefreshTokenDto
        {
            UserId = userGuid,
            RefreshToken = refreshToken,
        });

        if (result is null || result.Token is null || result.RefreshToken is null) return Unauthorized("Invalid Or Expired Refresh Token");
        authService.SetTokenInsideCookies(HttpContext, result.Token, result.RefreshToken);
        return Ok();
    }

}