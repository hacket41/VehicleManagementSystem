using System.ComponentModel.DataAnnotations;
using backend.Data.Enums;

namespace backend.Data.Entities;

public class ServiceAppointment
{
    [Key]
    public int Id { get; set; }

    public Guid CustomerId { get; set; }
    public User Customer { get; set; } = null!;

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


