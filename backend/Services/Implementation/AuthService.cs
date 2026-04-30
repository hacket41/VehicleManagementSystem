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
        return new AuthResponseDto
        {
            Success = true,
            Token = token,
        };
    }

}