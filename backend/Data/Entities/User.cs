using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace backend.Data.Entities;

public class User : IdentityUser<Guid>
{
    [MaxLength(50)]
    public string FirstName { get; set; } = string.Empty;

    [MaxLength(50)]
    public string LastName { get; set; } = string.Empty;

    [MaxLength(100)]
    public string Address { get; set; } = string.Empty;

    [MaxLength(255)]
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiryDate { get; set; }
     // public string Email { get; set; } = string.Empty;
     // public string Phone { get; set; } = string.Empty;

}