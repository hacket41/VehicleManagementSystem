namespace backend.Data.Entities;
using System.ComponentModel.DataAnnotations;

public class PartRequest
{
    [Key]
    public int Id { get; set; }

    public Guid CustomerId { get; set; }
    public User Customer { get; set; } = null!;

    public int? PartId { get; set; }
    public Part? Part { get; set; }

    [Required, MaxLength(200)]
    public string PartName { get; set; } = string.Empty;

    [MaxLength(500)]
    public string Description { get; set; } = string.Empty;

    public bool IsFulfilled { get; set; } = false;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}