using System.ComponentModel.DataAnnotations;

namespace backend.Data.DTO.Request;

public class RegisterUserDto
{
    [Required] [MaxLength(50)] public string FirstName { get; set; } = string.Empty;

    [Required] [MaxLength(50)] public string LastName { get; set; } = string.Empty;

    [Required] [EmailAddress] public string Email { get; set; } = string.Empty;

    [Required] public string Password { get; set; } = string.Empty;

    [Required]
    [MinLength(1)]
    [MaxLength(10)]
    public string Phone { get; set; } = string.Empty;
}