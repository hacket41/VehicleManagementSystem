using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace backend.Data.Entities;

public class User : IdentityUser<Guid>
{
    [Required] [MaxLength(255)] public string FirstName { get; set; } = string.Empty;

    [Required] [MaxLength(255)] public string LastName { get; set; } = string.Empty;

    [Required] [Range(10, 10)] public int? Phone { get; set; }
}