using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECARE.Migrations
{
    /// <inheritdoc />
    public partial class MyMigrationujuny : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsVerified",
                table: "PatientRegistrations");

            migrationBuilder.DropColumn(
                name: "OTP",
                table: "PatientRegistrations");

            migrationBuilder.DropColumn(
                name: "OTPExpiration",
                table: "PatientRegistrations");

            migrationBuilder.AddColumn<bool>(
                name: "IsVerified",
                table: "ServiceRequests",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OTP",
                table: "ServiceRequests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "OTPExpiration",
                table: "ServiceRequests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsVerified",
                table: "ServiceRequests");

            migrationBuilder.DropColumn(
                name: "OTP",
                table: "ServiceRequests");

            migrationBuilder.DropColumn(
                name: "OTPExpiration",
                table: "ServiceRequests");

            migrationBuilder.AddColumn<bool>(
                name: "IsVerified",
                table: "PatientRegistrations",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OTP",
                table: "PatientRegistrations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "OTPExpiration",
                table: "PatientRegistrations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
