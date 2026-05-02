using System.Security.Claims;
using backend.Data.DTO.Response;
using backend.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UserController(
    UserManager<User> userManager
    ) : ControllerBase
{
    [HttpGet("me")]
    public async Task<ActionResult<UserAuthResponse>> Me()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId is null) return Unauthorized();

        var user= await userManager.FindByIdAsync(userId);
        if(user is null) return NotFound("User does not exist");

        var roles = await userManager.GetRolesAsync(user);

        return Ok(new UserAuthResponse
        {
            Id = user.Id,
            Name = user.FirstName + " " + user.LastName,
            Email = user.Email ?? "",
            PhoneNumber = user.PhoneNumber ?? "",
            Address = user.Address,
            Roles = roles
        });
    }
}