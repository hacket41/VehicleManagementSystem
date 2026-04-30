namespace backend.Data.DTO.Request;

public class RequestRefreshTokenDto
{
    public Guid UserId { get; set; }
    public required string RefreshToken { get; set; }
}