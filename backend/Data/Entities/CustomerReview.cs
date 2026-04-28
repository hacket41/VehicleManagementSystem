using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace backend.Data.Entities;
using System.ComponentModel.DataAnnotations;

[Table("CustomerReviews")]
[PrimaryKey(nameof(Id))]
public class CustomerReview
{
    public int Id { get; set; }

    [ForeignKey(nameof(CustomerId))]
    public Guid CustomerId { get; set; }
    public User? User { get; set; }

    [Range(1, 5)]
    public int Rating { get; set; }

    [MaxLength(1000)]
    public string Comment { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}