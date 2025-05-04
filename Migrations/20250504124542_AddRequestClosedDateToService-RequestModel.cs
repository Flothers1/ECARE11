using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECARE.Migrations
{
    /// <inheritdoc />
    public partial class AddRequestClosedDateToServiceRequestModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "775226cf-a592-44a0-a0ea-540b517248da");

            migrationBuilder.AddColumn<DateTime>(
                name: "RequestClosedDate",
                table: "ServiceRequests",
                type: "datetime2",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "bb69a29a-aae9-4ae4-bf30-17d57a39b21f", null, "PharmacyAdmin", "PHARMACYADMIN" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bb69a29a-aae9-4ae4-bf30-17d57a39b21f");

            migrationBuilder.DropColumn(
                name: "RequestClosedDate",
                table: "ServiceRequests");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "775226cf-a592-44a0-a0ea-540b517248da", null, "PharmacyAdmin", "PHARMACYADMIN" });
        }
    }
}
