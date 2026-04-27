namespace backend.Data.Entities;
using System.ComponentModel.DataAnnotations;

public class CustomerReview
{
    [Key]
    public int Id { get; set; }

    public Guid CustomerId { get; set; }
    public User Customer { get; set; } = null!;

    [Range(1, 5)]
    public int Rating { get; set; }

    [MaxLength(1000)]
    public string Comment { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}