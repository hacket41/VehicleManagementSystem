using backend.Data.Entities;

namespace backend.Services.Interfaces;

public interface IJwtTokenService
{
     Task<string> GenerateUserToken(User user);
     Task<string> GenerateAndSaveRefreshToken(User user);


}