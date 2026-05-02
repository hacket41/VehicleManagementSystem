namespace backend.Data.DTO.Response;

public class UserAuthResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public IList<string> Roles { get; set; } = [];

}