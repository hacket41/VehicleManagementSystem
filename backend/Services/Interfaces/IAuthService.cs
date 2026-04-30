using backend.Data.DTO.Request;
using backend.Data.DTO.Response;

namespace backend.Services.Interfaces;

public interface IAuthService
{
    public Task<AuthResponseDto> RegisterAdmin(RegisterUserDto user);
    public Task<AuthResponseDto> RegisterStaff(RegisterUserDto user);
    public Task<AuthResponseDto> RegisterCustomer(RegisterUserDto user);
    public Task<AuthResponseDto> Login(LoginRequest user);
    public Task<AuthResponseDto> RefreshTokens(RequestRefreshTokenDto request);

}