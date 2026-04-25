using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace backend;

public class JwtOptions
{
    public const string SectionName = "Jwt";

    [Required]
    public string Issuer { get; set; }
    [Required]
    public string Audience { get; set; }

    [Required]
    [MinLength(32)]
    public string Secret { get; init; }
    public SymmetricSecurityKey SymmetricSecurityKey => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Secret));

    [Range(1, int.MaxValue)]
    public int ExpiryHours { get; set; }
    public DateTime ExpiryDate => DateTime.UtcNow.AddHours(ExpiryHours);
}