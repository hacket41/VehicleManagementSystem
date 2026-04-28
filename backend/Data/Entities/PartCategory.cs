using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace backend.Data.Entities;

[PrimaryKey(nameof(Id))]
public class PartCategory
{
    public int  Id { get; set; }

    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
}