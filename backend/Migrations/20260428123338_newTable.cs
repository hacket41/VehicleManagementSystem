using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class newTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VendorId",
                table: "Parts",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Parts_VendorId",
                table: "Parts",
                column: "VendorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Parts_Vendors_VendorId",
                table: "Parts",
                column: "VendorId",
                principalTable: "Vendors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parts_Vendors_VendorId",
                table: "Parts");

            migrationBuilder.DropIndex(
                name: "IX_Parts_VendorId",
                table: "Parts");

            migrationBuilder.DropColumn(
                name: "VendorId",
                table: "Parts");
        }
    }
}
