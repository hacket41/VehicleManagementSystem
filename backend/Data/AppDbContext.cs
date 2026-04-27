using backend.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace backend.Data;

public class AppDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

   
    public DbSet<Part> Parts => Set<Part>();
    public DbSet<Vendor> Vendors => Set<Vendor>();
    public DbSet<PurchaseOrder> PurchaseOrders => Set<PurchaseOrder>();
    public DbSet<PurchaseItem> PurchaseItems => Set<PurchaseItem>();

  
    public DbSet<Sale> Sales => Set<Sale>();
    public DbSet<SaleItem> SaleItems => Set<SaleItem>();

  
    public DbSet<Vehicle> Vehicles => Set<Vehicle>();
    public DbSet<ServiceAppointment> ServiceAppointments => Set<ServiceAppointment>();
    public DbSet<PartRequest> PartRequests => Set<PartRequest>();
    public DbSet<CustomerReview> CustomerReviews => Set<CustomerReview>();

 
    public DbSet<FinancialReport> FinancialReports => Set<FinancialReport>();
    public DbSet<CustomerReport> CustomerReports => Set<CustomerReport>();

 
    public DbSet<LowStockNotification> LowStockNotifications => Set<LowStockNotification>();
    public DbSet<CreditReminder> CreditReminders => Set<CreditReminder>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

      
        builder.Entity<User>().ToTable("Users");
        builder.Entity<IdentityRole<Guid>>().ToTable("Roles");

     
        builder.Entity<Sale>()
            .HasOne(s => s.Customer)
            .WithMany()
            .HasForeignKey(s => s.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Sale>()
            .HasOne(s => s.Staff)
            .WithMany()
            .HasForeignKey(s => s.StaffId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<SaleItem>()
            .HasOne(si => si.Sale)
            .WithMany(s => s.Items)
            .HasForeignKey(si => si.SaleId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<SaleItem>()
            .HasOne(si => si.Part)
            .WithMany(p => p.SaleItems)
            .HasForeignKey(si => si.PartId)
            .OnDelete(DeleteBehavior.Restrict);

     
        builder.Entity<PurchaseOrder>()
            .HasOne(po => po.CreatedByAdmin)
            .WithMany()
            .HasForeignKey(po => po.CreatedByAdminId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<PurchaseOrder>()
            .HasOne(po => po.Vendor)
            .WithMany(v => v.PurchaseOrders)
            .HasForeignKey(po => po.VendorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<PurchaseItem>()
            .HasOne(pi => pi.PurchaseOrder)
            .WithMany(po => po.Items)
            .HasForeignKey(pi => pi.PurchaseOrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<PurchaseItem>()
            .HasOne(pi => pi.Part)
            .WithMany(p => p.PurchaseItems)
            .HasForeignKey(pi => pi.PartId)
            .OnDelete(DeleteBehavior.Restrict);

    
        builder.Entity<Vehicle>()
            .HasOne(v => v.Customer)
            .WithMany()
            .HasForeignKey(v => v.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Sale>()
            .HasOne(s => s.Vehicle)
            .WithMany(v => v.Sales)
            .HasForeignKey(s => s.VehicleId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.Entity<ServiceAppointment>()
            .HasOne(a => a.Customer)
            .WithMany()
            .HasForeignKey(a => a.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<ServiceAppointment>()
            .HasOne(a => a.Vehicle)
            .WithMany(v => v.ServiceAppointments)
            .HasForeignKey(a => a.VehicleId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.Entity<PartRequest>()
            .HasOne(pr => pr.Customer)
            .WithMany()
            .HasForeignKey(pr => pr.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<PartRequest>()
            .HasOne(pr => pr.Part)
            .WithMany(p => p.PartRequests)
            .HasForeignKey(pr => pr.PartId)
            .OnDelete(DeleteBehavior.SetNull);

    
        builder.Entity<CustomerReview>()
            .HasOne(r => r.Customer)
            .WithMany()
            .HasForeignKey(r => r.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

       
        builder.Entity<FinancialReport>()
            .HasOne(r => r.GeneratedByUser)
            .WithMany()
            .HasForeignKey(r => r.GeneratedByUserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<CustomerReport>()
            .HasOne(r => r.GeneratedByStaff)
            .WithMany()
            .HasForeignKey(r => r.GeneratedByStaffId)
            .OnDelete(DeleteBehavior.Restrict);

  
        builder.Entity<LowStockNotification>()
            .HasOne(n => n.Part)
            .WithMany(p => p.LowStockNotifications)
            .HasForeignKey(n => n.PartId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<CreditReminder>()
            .HasOne(cr => cr.Customer)
            .WithMany()
            .HasForeignKey(cr => cr.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<CreditReminder>()
            .HasOne(cr => cr.Sale)
            .WithMany(s => s.CreditReminders)
            .HasForeignKey(cr => cr.SaleId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Sale>().HasIndex(s => s.InvoiceNumber).IsUnique();
        builder.Entity<Sale>().HasIndex(s => s.PaymentStatus);
        builder.Entity<Sale>().HasIndex(s => s.SaleDate);
        builder.Entity<Part>().HasIndex(p => p.PartNumber).IsUnique();
        builder.Entity<Part>().HasIndex(p => p.StockQuantity);
        builder.Entity<Vehicle>().HasIndex(v => v.VehicleNumber);
        builder.Entity<PurchaseOrder>().HasIndex(po => po.PurchaseOrderNumber).IsUnique();

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