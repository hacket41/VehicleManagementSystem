using System.ComponentModel.DataAnnotations;

namespace backend.Data.DTO.Request
{
    public class VendorRequestDto
    {
        [Required, MaxLength(150)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(100)]
        public string ContactPerson { get; set; } = string.Empty;

        [MaxLength(20)]
        public string Phone { get; set; } = string.Empty;

        [MaxLength(254)]
        public string Email { get; set; } = string.Empty;


        [MaxLength(500)]
        public string Address { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;
    }
}
