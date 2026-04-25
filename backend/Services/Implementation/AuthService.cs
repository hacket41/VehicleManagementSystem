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

        var newUser = new User
        {
            UserName = (user.FirstName + user.LastName).ToLower(),
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Phone = user.Phone,
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
        throw new NotImplementedException();
    }

    public async Task<AuthResponseDto> RegisterCustomer(RegisterUserDto user)
    {
        throw new NotImplementedException();
    }
}