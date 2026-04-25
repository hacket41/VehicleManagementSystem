using backend.Data.DTO.Request;
using backend.Data.DTO.Response;
using backend.Data.Entities;
using backend.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace backend.Services.Implementation;

public class AuthService(UserManager<User> userManager, SignInManager<User> signInManager,
    RoleManager<IdentityRole<Guid>> roleManager) :IAuthService
{

    public async Task<AuthResponseDto> RegisterAdmin(RegisterUserDto user)
    {
        throw new NotImplementedException();
    }

    public async Task<AuthResponseDto> RegisterStaff(RegisterUserDto user)
    {
        throw new NotImplementedException();
    }

    public async Task<AuthResponseDto> RegisterCustomer(RegisterUserDto user)
    {
        throw new NotImplementedException();
    }
}