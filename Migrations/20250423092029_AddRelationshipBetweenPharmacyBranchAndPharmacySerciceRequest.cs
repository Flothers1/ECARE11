using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECARE.Migrations
{
    /// <inheritdoc />
    public partial class AddRelationshipBetweenPharmacyBranchAndPharmacySerciceRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PharmacyBranchId",
                table: "PharmacyServiceRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PharmacyServiceRequestId",
                table: "pharmacyBranches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "pharmacyBranches",
                keyColumn: "Id",
                keyValue: 1,
                column: "PharmacyServiceRequestId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "pharmacyBranches",
                keyColumn: "Id",
                keyValue: 2,
                column: "PharmacyServiceRequestId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "pharmacyBranches",
                keyColumn: "Id",
                keyValue: 3,
                column: "PharmacyServiceRequestId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "pharmacyBranches",
                keyColumn: "Id",
                keyValue: 4,
                column: "PharmacyServiceRequestId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "pharmacyBranches",
                keyColumn: "Id",
                keyValue: 5,
                column: "PharmacyServiceRequestId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "pharmacyBranches",
                keyColumn: "Id",
                keyValue: 6,
                column: "PharmacyServiceRequestId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "pharmacyBranches",
                keyColumn: "Id",
                keyValue: 7,
                column: "PharmacyServiceRequestId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "pharmacyBranches",
                keyColumn: "Id",
                keyValue: 8,
                column: "PharmacyServiceRequestId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "pharmacyBranches",
                keyColumn: "Id",
                keyValue: 9,
                column: "PharmacyServiceRequestId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "pharmacyBranches",
                keyColumn: "Id",
                keyValue: 10,
                column: "PharmacyServiceRequestId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "pharmacyBranches",
                keyColumn: "Id",
                keyValue: 11,
                column: "PharmacyServiceRequestId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "pharmacyBranches",
                keyColumn: "Id",
                keyValue: 12,
                column: "PharmacyServiceRequestId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "pharmacyBranches",
                keyColumn: "Id",
                keyValue: 13,
                column: "PharmacyServiceRequestId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "pharmacyBranches",
                keyColumn: "Id",
                keyValue: 14,
                column: "PharmacyServiceRequestId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "pharmacyBranches",
                keyColumn: "Id",
                keyValue: 15,
                column: "PharmacyServiceRequestId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "pharmacyBranches",
                keyColumn: "Id",
                keyValue: 16,
                column: "PharmacyServiceRequestId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "pharmacyBranches",
                keyColumn: "Id",
                keyValue: 17,
                column: "PharmacyServiceRequestId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "pharmacyBranches",
                keyColumn: "Id",
                keyValue: 18,
                column: "PharmacyServiceRequestId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "pharmacyBranches",
                keyColumn: "Id",
                keyValue: 19,
                column: "PharmacyServiceRequestId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "pharmacyBranches",
                keyColumn: "Id",
                keyValue: 20,
                column: "PharmacyServiceRequestId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "pharmacyBranches",
                keyColumn: "Id",
                keyValue: 21,
                column: "PharmacyServiceRequestId",
                value: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PharmacyServiceRequests_PharmacyBranchId",
                table: "PharmacyServiceRequests",
                column: "PharmacyBranchId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PharmacyServiceRequests_pharmacyBranches_PharmacyBranchId",
                table: "PharmacyServiceRequests",
                column: "PharmacyBranchId",
                principalTable: "pharmacyBranches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PharmacyServiceRequests_pharmacyBranches_PharmacyBranchId",
                table: "PharmacyServiceRequests");

            migrationBuilder.DropIndex(
                name: "IX_PharmacyServiceRequests_PharmacyBranchId",
                table: "PharmacyServiceRequests");

            migrationBuilder.DropColumn(
                name: "PharmacyBranchId",
                table: "PharmacyServiceRequests");

            migrationBuilder.DropColumn(
                name: "PharmacyServiceRequestId",
                table: "pharmacyBranches");
        }
    }
}
