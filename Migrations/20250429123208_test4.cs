using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECARE.Migrations
{
    /// <inheritdoc />
    public partial class test4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "27075fa0-3418-4356-a719-4105f06bd70e");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "119f8a43-0d05-4bee-b6a0-5920c3dd3b84", null, "PharmacyAdmin", "PHARMACYADMIN" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "119f8a43-0d05-4bee-b6a0-5920c3dd3b84");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "27075fa0-3418-4356-a719-4105f06bd70e", null, "PharmacyAdmin", "PHARMACYADMIN" });
        }
    }
}
