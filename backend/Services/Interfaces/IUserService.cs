using backend.Data.DTO.Response;

namespace backend.Services.Interfaces;

public interface IUserService
{
    Task<UserAuthResponse> Me();
}