using System.ComponentModel.DataAnnotations;
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace backend.Data.Entities;

[Index(nameof(Name), IsUnique = true)]
public class PartCategory
{
    [Key]
    public int  Id { get; set; }

    [MaxLength(100)]
    public  string Name { get; set; } = string.Empty;
}