using backend.Data.Entities;
using backend.Data.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace backend.Data;


public static class DataSeeder
{

    private static readonly Guid AdminId    = new("aaaaaaaa-0000-0000-0000-000000000001");
    private static readonly Guid StaffId    = new("bbbbbbbb-0000-0000-0000-000000000001");
    private static readonly Guid Customer1Id = new("cccccccc-0000-0000-0000-000000000001");
    private static readonly Guid Customer2Id = new("cccccccc-0000-0000-0000-000000000002");
    private static readonly Guid Customer3Id = new("cccccccc-0000-0000-0000-000000000003");

    public static async Task SeedAsync(IServiceProvider services)
    {
        var userManager = services.GetRequiredService<UserManager<User>>();
        var db          = services.GetRequiredService<AppDbContext>();

        await SeedUsers(userManager);
        await SeedParts(db);
        await SeedSales(db);
    }

    // ── Users ──────────────────────────────────────────────────────────────────

    private static async Task SeedUsers(UserManager<User> userManager)
    {
        await EnsureUser(userManager, AdminId,    "admin@vehicle.com",    "Admin",   "User",    "Admin1234!", "Admin");
        await EnsureUser(userManager, StaffId,    "staff@vehicle.com",    "Staff",   "Member",  "Staff1234!", "Staff");
        await EnsureUser(userManager, Customer1Id,"alice@example.com",    "Alice",   "Johnson", "Pass1234!", "Customer");
        await EnsureUser(userManager, Customer2Id,"bob@example.com",      "Bob",     "Smith",   "Pass1234!", "Customer");
        await EnsureUser(userManager, Customer3Id,"charlie@example.com",  "Charlie", "Brown",   "Pass1234!", "Customer");
    }

    private static async Task EnsureUser(
        UserManager<User> userManager,
        Guid id, string email, string first, string last, string password, string role)
    {
        if (await userManager.FindByIdAsync(id.ToString()) is not null) return;

        var user = new User
        {
            Id          = id,
            UserName    = email,
            Email       = email,
            FirstName   = first,
            LastName    = last,
            PhoneNumber = "9800000000",
            EmailConfirmed = true,
        };

        var result = await userManager.CreateAsync(user, password);
        if (result.Succeeded)
            await userManager.AddToRoleAsync(user, role);
    }

    // ── Parts ──────────────────────────────────────────────────────────────────

    private static async Task SeedParts(AppDbContext db)
    {
        if (await db.Parts.AnyAsync()) return;

        var parts = new List<Part>
        {
            // Low stock (triggers Feature 3 - low stock alert)
            new() { Name="Brake Pad Set",      PartNumber="BP-001", Category="Brakes",   CostPrice=800,  SellingPrice=1200, StockQuantity=3,  LowStockThreshold=10, CompatibleVehicles="Toyota,Honda",     Description="Front brake pads" },
            new() { Name="Oil Filter",         PartNumber="OF-002", Category="Engine",   CostPrice=150,  SellingPrice=250,  StockQuantity=5,  LowStockThreshold=10, CompatibleVehicles="All",              Description="Standard oil filter" },
            // Normal stock
            new() { Name="Air Filter",         PartNumber="AF-003", Category="Engine",   CostPrice=200,  SellingPrice=400,  StockQuantity=50, LowStockThreshold=10, CompatibleVehicles="All",              Description="Engine air filter" },
            new() { Name="Spark Plug (set 4)", PartNumber="SP-004", Category="Engine",   CostPrice=500,  SellingPrice=900,  StockQuantity=30, LowStockThreshold=10, CompatibleVehicles="Petrol engines",   Description="NGK spark plugs" },
            new() { Name="Shock Absorber",     PartNumber="SA-005", Category="Suspension",CostPrice=2000, SellingPrice=3500, StockQuantity=20, LowStockThreshold=10, CompatibleVehicles="Sedans",           Description="Front shock absorber" },
            new() { Name="Timing Belt",        PartNumber="TB-006", Category="Engine",   CostPrice=600,  SellingPrice=1100, StockQuantity=15, LowStockThreshold=10, CompatibleVehicles="Honda,Suzuki",     Description="Timing belt kit" },
            new() { Name="Radiator",           PartNumber="RD-007", Category="Cooling",  CostPrice=3500, SellingPrice=5500, StockQuantity=8,  LowStockThreshold=10, CompatibleVehicles="Toyota",           Description="Aluminium radiator" },
            new() { Name="Battery 12V",        PartNumber="BT-008", Category="Electrical",CostPrice=2500, SellingPrice=4000, StockQuantity=12, LowStockThreshold=10, CompatibleVehicles="All",              Description="Maintenance-free battery" },
        };

        db.Parts.AddRange(parts);
        await db.SaveChangesAsync();
    }

    // ── Sales ──────────────────────────────────────────────────────────────────

    private static async Task SeedSales(AppDbContext db)
    {
        if (await db.Sales.AnyAsync()) return;

        var parts = await db.Parts.ToListAsync();
        var brakePad    = parts.First(p => p.PartNumber == "BP-001");
        var oilFilter   = parts.First(p => p.PartNumber == "OF-002");
        var sparkPlug   = parts.First(p => p.PartNumber == "SP-004");
        var shockAbs    = parts.First(p => p.PartNumber == "SA-005");
        var radiator    = parts.First(p => p.PartNumber == "RD-007");
        var battery     = parts.First(p => p.PartNumber == "BT-008");

        var now = DateTime.UtcNow;

        var sales = new List<Sale>
        {
            MakeSale("INV-TODAY-001", Customer1Id, StaffId, now,
                PaymentMethod.Cash, PaymentStatus.Paid, false, new[]
                {
                    (brakePad, 2),
                    (sparkPlug, 1),
                }),

   
            MakeSale("INV-MONTH-001", Customer2Id, StaffId, now.AddDays(-10),
                PaymentMethod.Cash, PaymentStatus.Paid, true, new[]
                {
                    (shockAbs, 2),   // 2 × 3500 = 7000 → loyalty 10% → total 6300
                }),

            MakeSale("INV-YEAR-001", Customer3Id, StaffId, now.AddDays(-60),
                PaymentMethod.Cash, PaymentStatus.Paid, false, new[]
                {
                    (oilFilter, 3),
                    (battery, 1),
                }),

     

            MakeSale("INV-REG-001", Customer1Id, StaffId, now.AddDays(-5),
                PaymentMethod.Cash, PaymentStatus.Paid, false, new[]
                {
                    (oilFilter, 2),
                }),

            MakeSale("INV-REG-002", Customer1Id, StaffId, now.AddDays(-20),
                PaymentMethod.Cash, PaymentStatus.Paid, false, new[]
                {
                    (sparkPlug, 2),
                }),

       
            MakeSale("INV-CREDIT-001", Customer2Id, StaffId, now.AddMonths(-2),
                PaymentMethod.Credit, PaymentStatus.Pending, false, new[]
                {
                    (radiator, 1),
                }, creditDue: now.AddMonths(-1)),

            MakeSale("INV-CREDIT-002", Customer3Id, StaffId, now.AddMonths(-3),
                PaymentMethod.Credit, PaymentStatus.Pending, false, new[]
                {
                    (battery, 2),
                    (brakePad, 1),
                }, creditDue: now.AddMonths(-2)),
        };

        db.Sales.AddRange(sales);
        await db.SaveChangesAsync();
    }

    private static Sale MakeSale(
        string invoiceNumber,
        Guid customerId,
        Guid staffId,
        DateTime saleDate,
        PaymentMethod method,
        PaymentStatus status,
        bool loyaltyApplied,
        (Part Part, int Qty)[] lineItems,
        DateTime? creditDue = null)
    {
        decimal subTotal = lineItems.Sum(li => li.Part.SellingPrice * li.Qty);
        decimal discPct  = loyaltyApplied ? 10m : 0m;
        decimal discAmt  = loyaltyApplied ? Math.Round(subTotal * 0.10m, 2) : 0m;
        decimal total    = subTotal - discAmt;

        var sale = new Sale
        {
            InvoiceNumber          = invoiceNumber,
            CustomerId             = customerId,
            StaffId                = staffId,
            SaleDate               = saleDate,
            CreatedAt              = saleDate,
            PaymentMethod          = method,
            PaymentStatus          = status,
            CreditDueDate          = creditDue,
            SubTotal               = subTotal,
            DiscountPercent        = discPct,
            DiscountAmount         = discAmt,
            TotalAmount            = total,
            LoyaltyDiscountApplied = loyaltyApplied,
        };

        foreach (var (part, qty) in lineItems)
        {
            sale.Items.Add(new SaleItem
            {
                PartId    = part.Id,
                Quantity  = qty,
                UnitPrice = part.SellingPrice,
                LineTotal = part.SellingPrice * qty,
            });
        }

        return sale;
    }
}
