using backend.Data.Entities;

namespace backend.Services.Interfaces;

public interface IJwtTokenService
{
    public Task<string> GenerateUserToken(User user);

}