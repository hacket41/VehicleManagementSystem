using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using backend.Data.Enums;
using Microsoft.EntityFrameworkCore;

namespace backend.Data.Entities;

[Table("CustomerReports")]
[PrimaryKey(nameof(Id))]
[Index(nameof(GeneratedByStaffId), Name = "IX_CustomerReports_GeneratedByStaffId")]
public class CustomerReport
{
    public int Id { get; set; }

    public CustomerReportType Type { get; set; }

    public DateTime PeriodStart { get; set; }
    public DateTime PeriodEnd { get; set; }

    public string ResultJson { get; set; } = string.Empty;

    public DateTime GeneratedAt { get; set; } = DateTime.UtcNow;

    [ForeignKey(nameof(User))]
    public Guid GeneratedByStaffId { get; set; }
    public User? User { get; set; }
}