using backend.Data.DTO.Request;
using backend.Data.DTO.Response;
using backend.Data.Entities;
using backend.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace backend.Services.Implementation;

public class AuthService(
    UserManager<User> userManager,
    IJwtTokenService jwtTokenService
    ) :IAuthService
{

    public async Task<AuthResponseDto> RegisterAdmin(RegisterUserDto user)
    {
        var newUser = new User
        {
            UserName = (user.FirstName + user.LastName).ToLower(),
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            PhoneNumber = user.Phone,
        };

        var result = await userManager.CreateAsync(newUser, user.Password);

        if (!result.Succeeded)
        {
            return new AuthResponseDto
            {
                Errors = result.Errors.Select(x => x.Description).ToList(),
                Success = false,

            };
        }
        var addAdminRole = await userManager.AddToRoleAsync(newUser, "Admin");
        if (!addAdminRole.Succeeded)
        {
            return new AuthResponseDto
            {
                Errors = addAdminRole.Errors.Select(x => x.Description).ToList(),
                Success = false,
            };
        }

        return new AuthResponseDto
        {
            Success = true,
            Errors = null,

        };

    }

    public async Task<AuthResponseDto> RegisterStaff(RegisterUserDto user)
    {

        var newUser = new User
        {
            UserName = (user.FirstName + user.LastName).ToLower(),
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            PhoneNumber = user.Phone,
        };

        var result = await userManager.CreateAsync(newUser, user.Password);

        if (!result.Succeeded)
        {
            return new AuthResponseDto
            {
                Errors = result.Errors.Select(x => x.Description).ToList(),
                Success = false,

            };
        }
        var addStaffRole = await userManager.AddToRoleAsync(newUser, "Staff");
        if (!addStaffRole.Succeeded)
        {
            return new AuthResponseDto
            {
                Errors = addStaffRole.Errors.Select(x => x.Description).ToList(),
                Success = false,
            };
        }

        return new AuthResponseDto
        {
            Success = true,
            Errors = null,

        };

    }

    public async Task<AuthResponseDto> RegisterCustomer(RegisterUserDto user)
    {
        var newUser = new User
        {
            UserName = (user.FirstName + user.LastName).ToLower(),
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            PhoneNumber = user.Phone,
        };

        var result = await userManager.CreateAsync(newUser, user.Password);

        if (!result.Succeeded)
        {
            return new AuthResponseDto
            {
                Errors = result.Errors.Select(x => x.Description).ToList(),
                Success = false,

            };
        }
        var addCustomerRole = await userManager.AddToRoleAsync(newUser, "Customer");
        if (!addCustomerRole.Succeeded)
        {
            return new AuthResponseDto
            {
                Errors = addCustomerRole.Errors.Select(x => x.Description).ToList(),
                Success = false,
            };
        }

        return new AuthResponseDto
        {
            Success = true,
            Errors = null,
        };

    }

    public async Task<AuthResponseDto> Login(LoginRequest user)
    {
        var userEmail = await userManager.FindByEmailAsync(user.Email);
        if (userEmail == null)
            return new AuthResponseDto
            {
                Success = false,
                Errors = ["Invalid Credentials"],
            };

        var isPasswordValid = await userManager.CheckPasswordAsync(userEmail, user.Password);
        if (!isPasswordValid)
            return new AuthResponseDto
            {
                Success = false,
                Errors = ["Invalid Credentials"]
            };

        var token = await jwtTokenService.GenerateUserToken(userEmail);
        var refreshToken = await jwtTokenService.GenerateAndSaveRefreshToken(userEmail);

        return new AuthResponseDto
        {
            Success = true,
            Token = token,
            RefreshToken = refreshToken,
        };
    }

    public async Task<AuthResponseDto> RefreshTokens(RequestRefreshTokenDto request)
    {
        var user = await jwtTokenService.ValidateRefreshToken(request.UserId, request.RefreshToken);
        if (user is null) return null!;

        var token = await jwtTokenService.GenerateUserToken(user);
        var refreshToken = await jwtTokenService.GenerateAndSaveRefreshToken(user);
        return new AuthResponseDto
        {
            Success = true,
            Token = token,
            RefreshToken = refreshToken,
        };
    }

    public void SetTokenInsideCookies(HttpContext httpContext, string token, string refreshToken)
    {
        httpContext.Response.Cookies.Append("accessToken", token,
            new CookieOptions
            {
                Expires = DateTime.Now.AddHours(6),
                HttpOnly = true,
                IsEssential = true,
                Secure = true,
                SameSite = SameSiteMode.None,
            });
        httpContext.Response.Cookies.Append("refreshToken", refreshToken,
            new CookieOptions
            {
                Expires = DateTime.Now.AddDays(14),
                HttpOnly = true,
                IsEssential = true,
                Secure = true,
                SameSite = SameSiteMode.None,
            });
    }


}