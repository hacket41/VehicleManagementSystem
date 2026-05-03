using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using backend.Data.Entities;
using backend.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace backend.Services.Implementation;

public class JwtTokenService(
    UserManager<User> userManager,
    IConfiguration configuration
    ) :IJwtTokenService
{

    public async Task<string> GenerateUserToken(User user)
    {
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
        };
        var roles = await userManager.GetRolesAsync(user);
        claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));
        return GenerateToken(claims);
    }

    private string GenerateToken(IEnumerable<Claim> claims)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("Jwt:Secret")!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: configuration.GetValue<string>("Jwt:Issuer"),
            audience: configuration.GetValue<string>("Jwt:Audience"),
            claims: claims,
            expires: DateTime.UtcNow.AddHours(6),
            signingCredentials: creds
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private static string GenerateRefreshToken()
    {
        var random = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(random);
        return Convert.ToBase64String(random);
    }

    public async Task<string> GenerateAndSaveRefreshToken(User user)
    {
        var refreshToken = GenerateRefreshToken();
        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryDate = DateTime.UtcNow.AddDays(14);
        await userManager.UpdateAsync(user);
        return refreshToken;
    }

    public async Task<User?> ValidateRefreshToken(Guid userId, string  refreshToken)
    {
        var user = await userManager.FindByIdAsync(userId.ToString());

        if(user == null || refreshToken != user.RefreshToken || user.RefreshTokenExpiryDate <= DateTime.UtcNow)
            return null;

        return user;
    }

    public ClaimsPrincipal? GetPrincipalFromToken(string token)
    {
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(configuration.GetValue<string>("Jwt:Secret")!));
        var validationParams = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = configuration.GetValue<string>("Jwt:Issuer"),
            ValidateAudience = true,
            ValidAudience = configuration.GetValue<string>("Jwt:Audience"),
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = key,
            ValidateLifetime = false,
        };

        var handler = new JwtSecurityTokenHandler();
        try
        {
            return handler.ValidateToken(token, validationParams, out _);
        }
        catch
        {
            return null;
        }
    }

    public async Task RevokeRefreshToken(Guid userId)
    {
        var user = await userManager.FindByIdAsync(userId.ToString());
        if (user is null) return;

        user.RefreshToken = null;
        user.RefreshTokenExpiryDate = null;
        await userManager.UpdateAsync(user);
    }
}