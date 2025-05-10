using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECARE.Migrations
{
    /// <inheritdoc />
    public partial class AddDelivaryOTPDelivaryOTPExpirationIsDeliveredColumnsToPharmacyServiceRequestModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "NationalIDPhotoBack",
                table: "RegistrationForms",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AddColumn<string>(
                name: "DeliveryOTP",
                table: "PharmacyServiceRequests",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeliveryOTPExpiration",
                table: "PharmacyServiceRequests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDelivered",
                table: "PharmacyServiceRequests",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveryOTP",
                table: "PharmacyServiceRequests");

            migrationBuilder.DropColumn(
                name: "DeliveryOTPExpiration",
                table: "PharmacyServiceRequests");

            migrationBuilder.DropColumn(
                name: "IsDelivered",
                table: "PharmacyServiceRequests");

            migrationBuilder.AlterColumn<string>(
                name: "NationalIDPhotoBack",
                table: "RegistrationForms",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);
        }
    }
}
