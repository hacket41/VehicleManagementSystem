using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class initialcreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CreditReminders_Sales_SaleId",
                table: "CreditReminders");

            migrationBuilder.DropForeignKey(
                name: "FK_CreditReminders_Users_CustomerId",
                table: "CreditReminders");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerReports_Users_GeneratedByStaffId",
                table: "CustomerReports");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerReviews_Users_CustomerId",
                table: "CustomerReviews");

            migrationBuilder.DropForeignKey(
                name: "FK_FinancialReports_Users_GeneratedByUserId",
                table: "FinancialReports");

            migrationBuilder.DropForeignKey(
                name: "FK_LowStockNotifications_Parts_PartId",
                table: "LowStockNotifications");

            migrationBuilder.DropForeignKey(
                name: "FK_PartRequests_Parts_PartId",
                table: "PartRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_PartRequests_Users_CustomerId",
                table: "PartRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseItems_Parts_PartId",
                table: "PurchaseItems");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrders_Users_CreatedByAdminId",
                table: "PurchaseOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrders_Vendors_VendorId",
                table: "PurchaseOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_SaleItems_Parts_PartId",
                table: "SaleItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Sales_Users_CustomerId",
                table: "Sales");

            migrationBuilder.DropForeignKey(
                name: "FK_Sales_Users_StaffId",
                table: "Sales");

            migrationBuilder.DropForeignKey(
                name: "FK_Sales_Vehicles_VehicleId",
                table: "Sales");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceAppointments_Users_CustomerId",
                table: "ServiceAppointments");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceAppointments_Vehicles_VehicleId",
                table: "ServiceAppointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_Users_CustomerId",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_Vehicles_VehicleNumber",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_Sales_InvoiceNumber",
                table: "Sales");

            migrationBuilder.DropIndex(
                name: "IX_Sales_PaymentStatus",
                table: "Sales");

            migrationBuilder.DropIndex(
                name: "IX_Sales_SaleDate",
                table: "Sales");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseOrders_PurchaseOrderNumber",
                table: "PurchaseOrders");

            migrationBuilder.DropIndex(
                name: "IX_Parts_PartNumber",
                table: "Parts");

            migrationBuilder.DropIndex(
                name: "IX_Parts_StockQuantity",
                table: "Parts");

            migrationBuilder.DropIndex(
                name: "IX_CustomerReviews_CustomerId",
                table: "CustomerReviews");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "Parts");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Users",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Users",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "Sales",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500);

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Parts",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "PartId",
                table: "PartRequests",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "CustomerReviews",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PartCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartCategories", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Parts_CategoryId",
                table: "Parts",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerReviews_UserId",
                table: "CustomerReviews",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CreditReminders_Sales_SaleId",
                table: "CreditReminders",
                column: "SaleId",
                principalTable: "Sales",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CreditReminders_Users_CustomerId",
                table: "CreditReminders",
                column: "CustomerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerReports_Users_GeneratedByStaffId",
                table: "CustomerReports",
                column: "GeneratedByStaffId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerReviews_Users_UserId",
                table: "CustomerReviews",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FinancialReports_Users_GeneratedByUserId",
                table: "FinancialReports",
                column: "GeneratedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LowStockNotifications_Parts_PartId",
                table: "LowStockNotifications",
                column: "PartId",
                principalTable: "Parts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PartRequests_Parts_PartId",
                table: "PartRequests",
                column: "PartId",
                principalTable: "Parts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PartRequests_Users_CustomerId",
                table: "PartRequests",
                column: "CustomerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Parts_PartCategories_CategoryId",
                table: "Parts",
                column: "CategoryId",
                principalTable: "PartCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseItems_Parts_PartId",
                table: "PurchaseItems",
                column: "PartId",
                principalTable: "Parts",
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

            migrationBuilder.AddForeignKey(
                name: "FK_SaleItems_Parts_PartId",
                table: "SaleItems",
                column: "PartId",
                principalTable: "Parts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_Users_CustomerId",
                table: "Sales",
                column: "CustomerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_Users_StaffId",
                table: "Sales",
                column: "StaffId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_Vehicles_VehicleId",
                table: "Sales",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceAppointments_Users_CustomerId",
                table: "ServiceAppointments",
                column: "CustomerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceAppointments_Vehicles_VehicleId",
                table: "ServiceAppointments",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_Users_CustomerId",
                table: "Vehicles",
                column: "CustomerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CreditReminders_Sales_SaleId",
                table: "CreditReminders");

            migrationBuilder.DropForeignKey(
                name: "FK_CreditReminders_Users_CustomerId",
                table: "CreditReminders");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerReports_Users_GeneratedByStaffId",
                table: "CustomerReports");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerReviews_Users_UserId",
                table: "CustomerReviews");

            migrationBuilder.DropForeignKey(
                name: "FK_FinancialReports_Users_GeneratedByUserId",
                table: "FinancialReports");

            migrationBuilder.DropForeignKey(
                name: "FK_LowStockNotifications_Parts_PartId",
                table: "LowStockNotifications");

            migrationBuilder.DropForeignKey(
                name: "FK_PartRequests_Parts_PartId",
                table: "PartRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_PartRequests_Users_CustomerId",
                table: "PartRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_Parts_PartCategories_CategoryId",
                table: "Parts");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseItems_Parts_PartId",
                table: "PurchaseItems");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrders_Users_CreatedByAdminId",
                table: "PurchaseOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrders_Vendors_VendorId",
                table: "PurchaseOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_SaleItems_Parts_PartId",
                table: "SaleItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Sales_Users_CustomerId",
                table: "Sales");

            migrationBuilder.DropForeignKey(
                name: "FK_Sales_Users_StaffId",
                table: "Sales");

            migrationBuilder.DropForeignKey(
                name: "FK_Sales_Vehicles_VehicleId",
                table: "Sales");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceAppointments_Users_CustomerId",
                table: "ServiceAppointments");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceAppointments_Vehicles_VehicleId",
                table: "ServiceAppointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_Users_CustomerId",
                table: "Vehicles");

            migrationBuilder.DropTable(
                name: "PartCategories");

            migrationBuilder.DropIndex(
                name: "IX_Parts_CategoryId",
                table: "Parts");

            migrationBuilder.DropIndex(
                name: "IX_CustomerReviews_UserId",
                table: "CustomerReviews");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Parts");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "CustomerReviews");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Users",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Users",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "Sales",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Sales",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Parts",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "PartId",
                table: "PartRequests",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_VehicleNumber",
                table: "Vehicles",
                column: "VehicleNumber");

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
                name: "IX_PurchaseOrders_PurchaseOrderNumber",
                table: "PurchaseOrders",
                column: "PurchaseOrderNumber",
                unique: true);

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
                name: "IX_CustomerReviews_CustomerId",
                table: "CustomerReviews",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_CreditReminders_Sales_SaleId",
                table: "CreditReminders",
                column: "SaleId",
                principalTable: "Sales",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CreditReminders_Users_CustomerId",
                table: "CreditReminders",
                column: "CustomerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerReports_Users_GeneratedByStaffId",
                table: "CustomerReports",
                column: "GeneratedByStaffId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerReviews_Users_CustomerId",
                table: "CustomerReviews",
                column: "CustomerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FinancialReports_Users_GeneratedByUserId",
                table: "FinancialReports",
                column: "GeneratedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LowStockNotifications_Parts_PartId",
                table: "LowStockNotifications",
                column: "PartId",
                principalTable: "Parts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PartRequests_Parts_PartId",
                table: "PartRequests",
                column: "PartId",
                principalTable: "Parts",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_PartRequests_Users_CustomerId",
                table: "PartRequests",
                column: "CustomerId",
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

            migrationBuilder.AddForeignKey(
                name: "FK_SaleItems_Parts_PartId",
                table: "SaleItems",
                column: "PartId",
                principalTable: "Parts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_Users_CustomerId",
                table: "Sales",
                column: "CustomerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_Users_StaffId",
                table: "Sales",
                column: "StaffId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_Vehicles_VehicleId",
                table: "Sales",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceAppointments_Users_CustomerId",
                table: "ServiceAppointments",
                column: "CustomerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceAppointments_Vehicles_VehicleId",
                table: "ServiceAppointments",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_Users_CustomerId",
                table: "Vehicles",
                column: "CustomerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
