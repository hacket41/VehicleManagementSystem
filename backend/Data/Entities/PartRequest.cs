using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Data.Entities;
using System.ComponentModel.DataAnnotations;

public class PartRequest
{
    [Key]
    public int Id { get; set; }

    [ForeignKey(nameof(Customer))]
    public Guid CustomerId { get; set; }
    public User? Customer { get; set; }

    [ForeignKey(nameof(Part))]
    public int PartId { get; set; }
    public Part? Part { get; set; }

    [Required, MaxLength(200)]
    public string PartName { get; set; } = string.Empty;

    [MaxLength(500)]
    public string Description { get; set; } = string.Empty;

    public bool IsFulfilled { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}