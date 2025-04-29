using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECARE.Migrations
{
    /// <inheritdoc />
    public partial class test5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "119f8a43-0d05-4bee-b6a0-5920c3dd3b84");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2b2e7b7f-1e19-4de9-9f7e-1b89cd83ea7e", null, "PharmacyAdmin", "PHARMACYADMIN" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2b2e7b7f-1e19-4de9-9f7e-1b89cd83ea7e");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "119f8a43-0d05-4bee-b6a0-5920c3dd3b84", null, "PharmacyAdmin", "PHARMACYADMIN" });
        }
    }
}
