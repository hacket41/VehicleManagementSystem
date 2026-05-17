using System.ComponentModel.DataAnnotations;

namespace backend.Data.DTO.Request;

public class PartCategoryRequest
{
    [MaxLength(255)]
    public required string Name { get; set; }
}