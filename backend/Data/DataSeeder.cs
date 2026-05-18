using backend.Data.Entities;
using backend.Data.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace backend.Data;

//ALL SEEDED DATA BANAKO CHU MAILE MERO MA TEST GARNA TIMI HARULE EDIT GARHUNCHA HAI
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
        await SeedVendors(db);
        await SeedCategory(db);
        await SeedParts(db);
        await SeedSales(db);
    }
    
    private static async Task SeedUsers(UserManager<User> userManager)
    {
        await EnsureUser(userManager, AdminId,    "admin@vehicle.com",    "Admin",   "User",
            "Admin1234!",  "Anamnagar-10, Kathmandu", "Admin");
        await EnsureUser(userManager, StaffId,    "staff@vehicle.com",    "Staff",   "Member",
            "Staff1234!","Sukumbasi, Bagmati", "Staff");
        await EnsureUser(userManager, Customer1Id,"alice@example.com",    "Alice",   "Johnson",
            "Pass1234!", "Boudha-5, Kathmandu", "Customer");
        await EnsureUser(userManager, Customer2Id,"abhijit.singh.0106@gmail.com",      "Bob",
            "Smith",
            "Pass1234!", "Gothatar-9, Kathmandu", "Customer");
        await EnsureUser(userManager, Customer3Id,"charlie@example.com",  "Charlie", "Brown",
            "Pass1234!", "Pokhara", "Customer");
    }

    private static async Task EnsureUser(
        UserManager<User> userManager,
        Guid id, string email, string first, string last, string password, string address, string role)
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
            Address = address,
            EmailConfirmed = true,
        };

        var result = await userManager.CreateAsync(user, password);
        if (result.Succeeded)
            await userManager.AddToRoleAsync(user, role);
    }

    private static async Task SeedVendors(AppDbContext db)
    {
        if (await db.Vendors.AnyAsync()) return;

        var vendors = new List<Vendor>
        {
            new()
            {
                Id = 1,
                Name = "AutoParts Nepal Pvt. Ltd.",
                ContactPerson = "Ramesh Shrestha",
                Phone = "9801000001",
                Email = "autoparts@nepal.com",
                Address = "Kathmandu",
            },
            new()
            {
                Id = 2,
                Name = "Himalayan Spares",
                ContactPerson = "Sita Gurung",
                Phone = "9801000002",
                Email = "himalayan@spares.com",
                Address = "Pokhara",
            },
            new()
            {
                Id = 3,
                Name = "Everest Auto Supplies",
                ContactPerson = "Kiran Rai",
                Phone = "9801000003",
                Email = "everest@auto.com",
                Address = "Lalitpur",
            },
            new()
            {
                Id = 4,
                Name = "Global Vehicle Parts",
                ContactPerson = "Amit Shah",
                Phone = "9801000004",
                Email = "global@vehicle.com",
                Address = "Biratnagar",
            },
        };

        await db.Vendors.AddRangeAsync(vendors);
        await db.SaveChangesAsync();
    }


    private static async Task SeedCategory(AppDbContext db)
    {
        if (await db.PartCategories.AnyAsync()) return;

        var categories = new List<PartCategory>
        {
            new() { Name = "Engine" },
            new() { Name = "Cooling" },
            new() { Name = "Electrical" },
            new() { Name = "Suspension" },
            new() { Name = "Tyres" },
        };

        await db.PartCategories.AddRangeAsync(categories);
        await db.SaveChangesAsync();
    }

        private static async Task SeedParts(AppDbContext db)
        {
            if (await db.Parts.AnyAsync()) return;
            var engine     = await db.PartCategories.FirstAsync(c => c.Name == "Engine");
            var cooling    = await db.PartCategories.FirstAsync(c => c.Name == "Cooling");
            var electrical = await db.PartCategories.FirstAsync(c => c.Name == "Electrical");
            var suspension = await db.PartCategories.FirstAsync(c => c.Name == "Suspension");

            var vendor1 = await db.Vendors.FirstAsync(v => v.Name == "AutoParts Nepal Pvt. Ltd.");
            var vendor2 = await db.Vendors.FirstAsync(v => v.Name == "Himalayan Spares");
            var vendor3 = await db.Vendors.FirstAsync(v => v.Name == "Everest Auto Supplies");
            var vendor4 = await db.Vendors.FirstAsync(v => v.Name == "Global Vehicle Parts");

            var parts = new List<Part>
            {
                // Low stock
                new() {
                    Name="Brake Pad Set", PartNumber="BP-001",
                    CategoryId = engine.Id, VendorId = vendor1.Id,
                    CostPrice=800, SellingPrice=1200,
                    StockQuantity=3, LowStockThreshold=10,
                    CompatibleVehicles="Toyota,Honda",
                    Description="Front brake pads",
                    ImageUrl = "https://2azty9bk8m.ufs.sh/f/1shMHs8Plx65pgSpPRViI9qejw5NOZT3XnoEMPFBtdzWyc86"
                },

                new() {
                    Name="Oil Filter", PartNumber="OF-002",
                    CategoryId = cooling.Id, VendorId = vendor2.Id,
                    CostPrice=150, SellingPrice=250,
                    StockQuantity=5, LowStockThreshold=10,
                    CompatibleVehicles="All",
                    Description="Standard oil filter",
                    ImageUrl = "https://2azty9bk8m.ufs.sh/f/1shMHs8Plx65JdcGd4IATtOwbN5vcXLfEFZhlkSQyoMxJP8j"
                },

                // Normal stock
                new() {
                    Name="Air Filter", PartNumber="AF-003",
                    CategoryId = engine.Id, VendorId = vendor2.Id,
                    CostPrice=200, SellingPrice=400,
                    StockQuantity=50, LowStockThreshold=10,
                    CompatibleVehicles="All",
                    Description="Engine air filter",
                    ImageUrl = "https://2azty9bk8m.ufs.sh/f/1shMHs8Plx65OggR61TRmQZxdy0jV71LghPzUBYSWNpMcDut"
                },

                new() {
                    Name="Spark Plug (set 4)", PartNumber="SP-004",
                    CategoryId = electrical.Id, VendorId = vendor3.Id,
                    CostPrice=500, SellingPrice=900,
                    StockQuantity=30, LowStockThreshold=10,
                    CompatibleVehicles="Petrol engines",
                    Description="NGK spark plugs",
                    ImageUrl = "https://2azty9bk8m.ufs.sh/f/1shMHs8Plx65VockkCZRhIkHXUn9NE8QmcGLKSMtCJri0Ooj"
                },

                new() {
                    Name="Shock Absorber", PartNumber="SA-005",
                    CategoryId = suspension.Id, VendorId = vendor4.Id,
                    CostPrice=2000, SellingPrice=3500,
                    StockQuantity=20, LowStockThreshold=10,
                    CompatibleVehicles="Sedans",
                    Description="Front shock absorber",
                    ImageUrl = "https://2azty9bk8m.ufs.sh/f/1shMHs8Plx65alxBMjSG2kthzCmOKP5EWe41XcsfNFivo3gI"
                },

                new() {
                    Name="Timing Belt", PartNumber="TB-006",
                    CategoryId = engine.Id, VendorId = vendor1.Id,
                    CostPrice=600, SellingPrice=1100,
                    StockQuantity=15, LowStockThreshold=10,
                    CompatibleVehicles="Honda,Suzuki",
                    Description="Timing belt kit",
                    ImageUrl = "https://2azty9bk8m.ufs.sh/f/1shMHs8Plx65MaCaRdKA2T7dx4bFUvDlCtOiJE1ue6YgKPwX"
                },

                new() {
                    Name="Radiator", PartNumber="RD-007",
                    CategoryId = cooling.Id, VendorId = vendor3.Id,
                    CostPrice=3500, SellingPrice=5500,
                    StockQuantity=8, LowStockThreshold=10,
                    CompatibleVehicles="Toyota",
                    Description="Aluminium radiator",
                    ImageUrl = "https://2azty9bk8m.ufs.sh/f/1shMHs8Plx6515xsvd8Plx65FuyIvRCAG0JgNzXUW8rYanEp"
                },

                new() {
                    Name="Battery 12V", PartNumber="BT-008",
                    CategoryId = electrical.Id, VendorId = vendor4.Id,
                    CostPrice=2500, SellingPrice=4000,
                    StockQuantity=12, LowStockThreshold=10,
                    CompatibleVehicles="All",
                    Description="Maintenance-free battery",
                    ImageUrl = "https://2azty9bk8m.ufs.sh/f/1shMHs8Plx65gie6eTaVAekau52m7TW1KirwlbFcNp8ohPsD"
                },
            };

            await db.Parts.AddRangeAsync(parts);
            await db.SaveChangesAsync();
        }
   
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