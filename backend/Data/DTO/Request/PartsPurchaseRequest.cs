using System.ComponentModel.DataAnnotations;

namespace backend.Data.DTO.Request;

public class PartsPurchaseRequest
{
    [Required, MaxLength(100)]
    public required string Name { get; set; }

    [Required, MaxLength(50)]
    public required string PartNumber { get; set; }

    [Required, MaxLength(500)]
    public required string Description { get; set; }

    [Required, MaxLength(50)]
    public required string Category { get; set; }


    // [Required, MaxLength(50)]
    // public required

}