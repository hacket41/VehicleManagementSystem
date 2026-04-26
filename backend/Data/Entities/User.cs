using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace backend.Data.Entities;

public class User : IdentityUser<Guid>
{
    public string FirstName { get; set; } = string.Empty;
     public string LastName { get; set; } = string.Empty;
     // public string Email { get; set; } = string.Empty;
     // public string Phone { get; set; } = string.Empty;

}