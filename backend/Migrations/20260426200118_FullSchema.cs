using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class FullSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FinancialReports_Users_GeneratedByUserId",
                table: "FinancialReports");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseItem_PurchaseOrders_PurchaseOrderId",
                table: "PurchaseItem");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrders_Users_CreatedByAdminId",
                table: "PurchaseOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrders_Vendors_VendorId",
                table: "PurchaseOrders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PurchaseItem",
                table: "PurchaseItem");

            migrationBuilder.RenameTable(
                name: "PurchaseItem",
                newName: "PurchaseItems");

            migrationBuilder.RenameIndex(
                name: "IX_PurchaseItem_PurchaseOrderId",
                table: "PurchaseItems",
                newName: "IX_PurchaseItems_PurchaseOrderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PurchaseItems",
                table: "PurchaseItems",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "CustomerReports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    PeriodStart = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PeriodEnd = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ResultJson = table.Column<string>(type: "text", nullable: false),
                    GeneratedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    GeneratedByStaffId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerReports_Users_GeneratedByStaffId",
                        column: x => x.GeneratedByStaffId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomerReviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CustomerId = table.Column<Guid>(type: "uuid", nullable: false),
                    Rating = table.Column<int>(type: "integer", nullable: false),
                    Comment = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerReviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerReviews_Users_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Parts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    PartNumber = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Category = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CompatibleVehicles = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    CostPrice = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    SellingPrice = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    StockQuantity = table.Column<int>(type: "integer", nullable: false),
                    LowStockThreshold = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CustomerId = table.Column<Guid>(type: "uuid", nullable: false),
                    VehicleNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Make = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Model = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Year = table.Column<int>(type: "integer", nullable: false),
                    VIN = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Mileage = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vehicles_Users_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LowStockNotifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PartId = table.Column<int>(type: "integer", nullable: false),
                    StockAtTimeOfAlert = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    SentAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ErrorMessage = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LowStockNotifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LowStockNotifications_Parts_PartId",
                        column: x => x.PartId,
                        principalTable: "Parts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PartRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CustomerId = table.Column<Guid>(type: "uuid", nullable: false),
                    PartId = table.Column<int>(type: "integer", nullable: true),
                    PartName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    IsFulfilled = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PartRequests_Parts_PartId",
                        column: x => x.PartId,
                        principalTable: "Parts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_PartRequests_Users_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Sales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    InvoiceNumber = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    CustomerId = table.Column<Guid>(type: "uuid", nullable: false),
                    StaffId = table.Column<Guid>(type: "uuid", nullable: false),
                    VehicleId = table.Column<int>(type: "integer", nullable: true),
                    SaleDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    SubTotal = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    DiscountPercent = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    DiscountAmount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    PaymentMethod = table.Column<int>(type: "integer", nullable: false),
                    PaymentStatus = table.Column<int>(type: "integer", nullable: false),
                    CreditDueDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LoyaltyDiscountApplied = table.Column<bool>(type: "boolean", nullable: false),
                    Notes = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sales_Users_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sales_Users_StaffId",
                        column: x => x.StaffId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sales_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "ServiceAppointments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CustomerId = table.Column<Guid>(type: "uuid", nullable: false),
                    VehicleId = table.Column<int>(type: "integer", nullable: false),
                    AppointmentDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ServiceDescription = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    Notes = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceAppointments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceAppointments_Users_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServiceAppointments_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CreditReminders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SaleId = table.Column<int>(type: "integer", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uuid", nullable: false),
                    InvoiceNumber = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    AmountDue = table.Column<decimal>(type: "numeric", nullable: false),
                    DueDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DaysOverdue = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    SentAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ErrorMessage = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditReminders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CreditReminders_Sales_SaleId",
                        column: x => x.SaleId,
                        principalTable: "Sales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CreditReminders_Users_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SaleItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SaleId = table.Column<int>(type: "integer", nullable: false),
                    PartId = table.Column<int>(type: "integer", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    LineTotal = table.Column<decimal>(type: "numeric(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SaleItems_Parts_PartId",
                        column: x => x.PartId,
                        principalTable: "Parts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SaleItems_Sales_SaleId",
                        column: x => x.SaleId,
                        principalTable: "Sales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_PurchaseOrderNumber",
                table: "PurchaseOrders",
                column: "PurchaseOrderNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseItems_PartId",
                table: "PurchaseItems",
                column: "PartId");

            migrationBuilder.CreateIndex(
                name: "IX_CreditReminders_CustomerId",
                table: "CreditReminders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CreditReminders_SaleId",
                table: "CreditReminders",
                column: "SaleId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerReports_GeneratedByStaffId",
                table: "CustomerReports",
                column: "GeneratedByStaffId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerReviews_CustomerId",
                table: "CustomerReviews",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_LowStockNotifications_PartId",
                table: "LowStockNotifications",
                column: "PartId");

            migrationBuilder.CreateIndex(
                name: "IX_PartRequests_CustomerId",
                table: "PartRequests",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_PartRequests_PartId",
                table: "PartRequests",
                column: "PartId");

            migrationBuilder.CreateIndex(
                name: "IX_Parts_PartNumber",
                table: "Parts",
                column: "PartNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Parts_StockQuantity",
                table: "Parts",
                column: "StockQuantity");

            migrationBuilder.CreateIndex(
                name: "IX_SaleItems_PartId",
                table: "SaleItems",
                column: "PartId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleItems_SaleId",
                table: "SaleItems",
                column: "SaleId");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_CustomerId",
                table: "Sales",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_InvoiceNumber",
                table: "Sales",
                column: "InvoiceNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sales_PaymentStatus",
                table: "Sales",
                column: "PaymentStatus");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_SaleDate",
                table: "Sales",
                column: "SaleDate");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_StaffId",
                table: "Sales",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_VehicleId",
                table: "Sales",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceAppointments_CustomerId",
                table: "ServiceAppointments",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceAppointments_VehicleId",
                table: "ServiceAppointments",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_CustomerId",
                table: "Vehicles",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_VehicleNumber",
                table: "Vehicles",
                column: "VehicleNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_FinancialReports_Users_GeneratedByUserId",
                table: "FinancialReports",
                column: "GeneratedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseItems_Parts_PartId",
                table: "PurchaseItems",
                column: "PartId",
                principalTable: "Parts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseItems_PurchaseOrders_PurchaseOrderId",
                table: "PurchaseItems",
                column: "PurchaseOrderId",
                principalTable: "PurchaseOrders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrders_Users_CreatedByAdminId",
                table: "PurchaseOrders",
                column: "CreatedByAdminId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrders_Vendors_VendorId",
                table: "PurchaseOrders",
                column: "VendorId",
                principalTable: "Vendors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FinancialReports_Users_GeneratedByUserId",
                table: "FinancialReports");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseItems_Parts_PartId",
                table: "PurchaseItems");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseItems_PurchaseOrders_PurchaseOrderId",
                table: "PurchaseItems");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrders_Users_CreatedByAdminId",
                table: "PurchaseOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrders_Vendors_VendorId",
                table: "PurchaseOrders");

            migrationBuilder.DropTable(
                name: "CreditReminders");

            migrationBuilder.DropTable(
                name: "CustomerReports");

            migrationBuilder.DropTable(
                name: "CustomerReviews");

            migrationBuilder.DropTable(
                name: "LowStockNotifications");

            migrationBuilder.DropTable(
                name: "PartRequests");

            migrationBuilder.DropTable(
                name: "SaleItems");

            migrationBuilder.DropTable(
                name: "ServiceAppointments");

            migrationBuilder.DropTable(
                name: "Parts");

            migrationBuilder.DropTable(
                name: "Sales");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseOrders_PurchaseOrderNumber",
                table: "PurchaseOrders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PurchaseItems",
                table: "PurchaseItems");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseItems_PartId",
                table: "PurchaseItems");

            migrationBuilder.RenameTable(
                name: "PurchaseItems",
                newName: "PurchaseItem");

            migrationBuilder.RenameIndex(
                name: "IX_PurchaseItems_PurchaseOrderId",
                table: "PurchaseItem",
                newName: "IX_PurchaseItem_PurchaseOrderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PurchaseItem",
                table: "PurchaseItem",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FinancialReports_Users_GeneratedByUserId",
                table: "FinancialReports",
                column: "GeneratedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseItem_PurchaseOrders_PurchaseOrderId",
                table: "PurchaseItem",
                column: "PurchaseOrderId",
                principalTable: "PurchaseOrders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrders_Users_CreatedByAdminId",
                table: "PurchaseOrders",
                column: "CreatedByAdminId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrders_Vendors_VendorId",
                table: "PurchaseOrders",
                column: "VendorId",
                principalTable: "Vendors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
