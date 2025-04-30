using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECARE.Migrations
{
    /// <inheritdoc />
    public partial class AddSignedEvoucherColumnToPharmacyServiceRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2b2e7b7f-1e19-4de9-9f7e-1b89cd83ea7e");

            migrationBuilder.AlterColumn<string>(
                name: "EVoucherPDF",
                table: "PharmacyServiceRequests",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "SignedEVoucher",
                table: "PharmacyServiceRequests",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e16a941c-ab2c-4146-8e81-175bcbddf95e", null, "PharmacyAdmin", "PHARMACYADMIN" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e16a941c-ab2c-4146-8e81-175bcbddf95e");

            migrationBuilder.DropColumn(
                name: "SignedEVoucher",
                table: "PharmacyServiceRequests");

            migrationBuilder.AlterColumn<string>(
                name: "EVoucherPDF",
                table: "PharmacyServiceRequests",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2b2e7b7f-1e19-4de9-9f7e-1b89cd83ea7e", null, "PharmacyAdmin", "PHARMACYADMIN" });
        }
    }
}
