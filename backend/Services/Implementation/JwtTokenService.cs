using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using backend.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace backend.Services.Implementation;

public class JwtTokenService(IOptions<JwtOptions> jwtoptions, UserManager<User> userManager)
{
    public readonly JwtOptions JwtOptions = jwtoptions.Value;

    public async Task<string> GenerateToken(User user)
    {
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Email, user.Email),
        };

        var roles = await userManager.GetRolesAsync(user);
        foreach (var r in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, r));
        }

        return GenerateToken(claims);
    }

    private string GenerateToken(IEnumerable<Claim> claims)
    {
        var cred = new SigningCredentials(JwtOptions.SymmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: JwtOptions.Issuer,
            audience: JwtOptions.Audience,
            claims: claims,
            expires: JwtOptions.ExpiryDate,
            signingCredentials: cred
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }


}