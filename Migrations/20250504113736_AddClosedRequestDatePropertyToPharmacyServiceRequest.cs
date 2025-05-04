using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECARE.Migrations
{
    /// <inheritdoc />
    public partial class AddClosedRequestDatePropertyToPharmacyServiceRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e16a941c-ab2c-4146-8e81-175bcbddf95e");

            migrationBuilder.AddColumn<DateTime>(
                name: "RequestClosedDate",
                table: "PharmacyServiceRequests",
                type: "datetime2",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "775226cf-a592-44a0-a0ea-540b517248da", null, "PharmacyAdmin", "PHARMACYADMIN" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "775226cf-a592-44a0-a0ea-540b517248da");

            migrationBuilder.DropColumn(
                name: "RequestClosedDate",
                table: "PharmacyServiceRequests");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e16a941c-ab2c-4146-8e81-175bcbddf95e", null, "PharmacyAdmin", "PHARMACYADMIN" });
        }
    }
}
