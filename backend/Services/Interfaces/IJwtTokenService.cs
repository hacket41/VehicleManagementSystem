using System.Security.Claims;
using backend.Data.Entities;

namespace backend.Services.Interfaces;

public interface IJwtTokenService
{
     Task<string> GenerateUserToken(User user);
     Task<string> GenerateAndSaveRefreshToken(User user);
     Task<User?> ValidateRefreshToken(Guid userId,  string refreshToken);
     ClaimsPrincipal? GetPrincipalFromToken(string token);
     Task RevokeRefreshToken(Guid userId);




}