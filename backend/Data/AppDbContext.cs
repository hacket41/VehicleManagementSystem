using backend.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace backend.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options)
    : IdentityDbContext<User, IdentityRole<Guid>, Guid>(options)
{
    public DbSet<PartCategory> PartCategories { get; set; }
    public DbSet<Part> Parts { get; set; }
    public DbSet<Vendor> Vendors { get; set; }
    public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
    public DbSet<PurchaseItem> PurchaseItems { get; set; }

  
    public DbSet<Sale> Sales { get; set; }
    public DbSet<SaleItem> SaleItems { get; set; }

  
    public DbSet<Vehicle> Vehicles { get; set; }
    public DbSet<ServiceAppointment> ServiceAppointments { get; set; }
    public DbSet<PartRequest> PartRequests { get; set; }
    public DbSet<CustomerReview> CustomerReviews { get; set; }

 
    public DbSet<FinancialReport> FinancialReports { get; set; }
    public DbSet<CustomerReport> CustomerReports { get; set; }

 
    public DbSet<LowStockNotification> LowStockNotifications { get; set; }
    public DbSet<CreditReminder> CreditReminders { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

      
        builder.Entity<User>().ToTable("Users");
        builder.Entity<IdentityRole<Guid>>().ToTable("Roles");

        builder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();


        SeedRoles(builder);
    }

    private static void SeedRoles(ModelBuilder builder)
    {
        List<IdentityRole<Guid>> identityRole =
        [
            new()
            {
                Id = new Guid("2c5e174e-3b0e-446f-86af-483d56fd7210"),
                Name = "Admin",
                NormalizedName = "ADMIN",
                ConcurrencyStamp = "a1b2c3d4-e5f6-7890-abcd-ef1234567890"
            },
            new()
            {
                Id = new Guid("1eac6b18-9381-47a6-a273-f3e52a0396db"),
                Name = "Customer",
                NormalizedName = "CUSTOMER",
                ConcurrencyStamp = "b2c3d4e5-f6a7-8901-bcde-f12345678901"
            },
            new()
            {
                Id = new Guid("8e445865-a24d-4543-a6c6-9443d048cdb9"),
                Name = "Staff",
                NormalizedName = "STAFF",
                ConcurrencyStamp = "c3d4e5f6-a7b8-9012-cdef-123456789012"
            }
        ];
        builder.Entity<IdentityRole<Guid>>().HasData(identityRole);
    }
}