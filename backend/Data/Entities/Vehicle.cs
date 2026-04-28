using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Data.Entities;

public class Vehicle
{
    [Key]
    public int Id { get; set; }

    [ForeignKey(nameof(Customer))]
    public Guid CustomerId { get; set; }
    public User? Customer { get; set; }

    [Required, MaxLength(20)]
    public string VehicleNumber { get; set; } = string.Empty;  

    [MaxLength(50)]
    public string Make { get; set; } = string.Empty;   

    [MaxLength(50)]
    public string Model { get; set; } = string.Empty;  

    public int Year { get; set; }

    [MaxLength(30)]
    public string VIN { get; set; } = string.Empty;

    public int Mileage { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<ServiceAppointment> ServiceAppointments { get; set; } = new List<ServiceAppointment>();
    public ICollection<Sale> Sales { get; set; } = new List<Sale>();
}