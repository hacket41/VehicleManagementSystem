using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using backend.Data.Enums;

namespace backend.Data.Entities;

public class ServiceAppointment
{
    [Key]
    public int Id { get; set; }

    [ForeignKey(nameof(User))]
    public Guid CustomerId { get; set; }
    public User? User { get; set; }

    [ForeignKey(nameof(Vehicle))]
    public int VehicleId { get; set; }
    public Vehicle Vehicle { get; set; } = null!;

    public DateTime AppointmentDate { get; set; }

    [MaxLength(500)]
    public string ServiceDescription { get; set; } = string.Empty;

    public AppointmentStatus Status { get; set; } = AppointmentStatus.Booked;

    [MaxLength(500)]
    public string Notes { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}