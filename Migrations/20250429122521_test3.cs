using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECARE.Migrations
{
    /// <inheritdoc />
    public partial class test3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "17fc75ab-d04f-459e-abc9-9a98e914a6cc");

            migrationBuilder.AlterColumn<int>(
                name: "CareProgramId",
                table: "PatientRegistrations",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "27075fa0-3418-4356-a719-4105f06bd70e", null, "PharmacyAdmin", "PHARMACYADMIN" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "27075fa0-3418-4356-a719-4105f06bd70e");

            migrationBuilder.AlterColumn<int>(
                name: "CareProgramId",
                table: "PatientRegistrations",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "17fc75ab-d04f-459e-abc9-9a98e914a6cc", null, "PharmacyAdmin", "PHARMACYADMIN" });
        }
    }
}
