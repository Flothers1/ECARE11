using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ECARE.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Medication",
                table: "PatientRegistrations");

            migrationBuilder.DropColumn(
                name: "ProgramName",
                table: "PatientRegistrations");

            migrationBuilder.DropColumn(
                name: "[Program Sponsor]",
                table: "PatientRegistrations");

            migrationBuilder.AddColumn<int>(
                name: "CareProgramId",
                table: "PatientRegistrations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PharmacyBranchId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PharmacyId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CarePrograms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProductManager = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SponsorCompany = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MedicationName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    MedicationPackSize = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    MedicationPackConsumptionDuration = table.Column<int>(type: "int", nullable: false),
                    OriginalPrice = table.Column<decimal>(type: "decimal(18,3)", precision: 18, scale: 3, nullable: false),
                    PriceAfterDiscount = table.Column<decimal>(type: "decimal(18,3)", precision: 18, scale: 3, nullable: false),
                    HCPList = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarePrograms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Distributors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Distributors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pharmacies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pharmacies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProgramDistributors",
                columns: table => new
                {
                    CareProgramId = table.Column<int>(type: "int", nullable: false),
                    DistributorsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramDistributors", x => new { x.CareProgramId, x.DistributorsId });
                    table.ForeignKey(
                        name: "FK_ProgramDistributors_CarePrograms_CareProgramId",
                        column: x => x.CareProgramId,
                        principalTable: "CarePrograms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProgramDistributors_Distributors_DistributorsId",
                        column: x => x.DistributorsId,
                        principalTable: "Distributors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "pharmacyBranches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PharmacyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pharmacyBranches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_pharmacyBranches_Pharmacies_PharmacyId",
                        column: x => x.PharmacyId,
                        principalTable: "Pharmacies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProgramPharmacies",
                columns: table => new
                {
                    CareProgramId = table.Column<int>(type: "int", nullable: false),
                    PharmaciesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramPharmacies", x => new { x.CareProgramId, x.PharmaciesId });
                    table.ForeignKey(
                        name: "FK_ProgramPharmacies_CarePrograms_CareProgramId",
                        column: x => x.CareProgramId,
                        principalTable: "CarePrograms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProgramPharmacies_Pharmacies_PharmaciesId",
                        column: x => x.PharmaciesId,
                        principalTable: "Pharmacies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PharmacyServiceRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Payment = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true),
                    EVoucherPDF = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OTP = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    OTPExpiration = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsVerified = table.Column<bool>(type: "bit", nullable: true),
                    CareProgramId = table.Column<int>(type: "int", nullable: false),
                    PRId = table.Column<int>(type: "int", nullable: false),
                    PharmacyBranchId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PharmacyServiceRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PharmacyServiceRequests_CarePrograms_CareProgramId",
                        column: x => x.CareProgramId,
                        principalTable: "CarePrograms",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PharmacyServiceRequests_PatientRegistrations_PRId",
                        column: x => x.PRId,
                        principalTable: "PatientRegistrations",
                        principalColumn: "PatientRegistrationsId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PharmacyServiceRequests_pharmacyBranches_PharmacyBranchId",
                        column: x => x.PharmacyBranchId,
                        principalTable: "pharmacyBranches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "33f18fcf-5faf-46eb-b704-411570d2cfc4", null, "PharmacyAdmin", "PHARMACYADMIN" });

            migrationBuilder.InsertData(
                table: "Distributors",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Sofico pharma" },
                    { 2, "pharmaoverseas" },
                    { 3, "Ibn sina pharma" }
                });

            migrationBuilder.InsertData(
                table: "Pharmacies",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Amr tarrad pharmacy" },
                    { 2, "al-abdellatif altarshoby pharmacy" },
                    { 3, "sally pharmacy" },
                    { 4, "nadia abdelsallam pharmacy" },
                    { 5, "mohamed mostafa kamel pharmacy" },
                    { 6, "ahmed ramadan pharmacy" },
                    { 7, "mohamed mahrous pharmacy" }
                });

            migrationBuilder.InsertData(
                table: "pharmacyBranches",
                columns: new[] { "Id", "Name", "PharmacyId" },
                values: new object[,]
                {
                    { 1, "maadi", 1 },
                    { 2, "mohandseen", 1 },
                    { 3, "Masr algadida", 2 },
                    { 4, "Zayed", 2 },
                    { 5, "Rehab", 2 },
                    { 6, "Nasr city", 2 },
                    { 7, "Madinty", 2 },
                    { 8, "Portsaid", 2 },
                    { 9, "Seuz", 2 },
                    { 10, "islamilia", 2 },
                    { 11, "zagazig", 2 },
                    { 12, "manzla", 2 },
                    { 13, "Damietta", 2 },
                    { 14, "mansoura", 2 },
                    { 15, "Masr algadida", 3 },
                    { 16, "Fifth settelment", 3 },
                    { 17, "mansoura", 3 },
                    { 18, "Alex", 4 },
                    { 19, "Alex", 5 },
                    { 20, "Alex", 6 },
                    { 21, "Alex", 7 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PatientRegistrations_CareProgramId",
                table: "PatientRegistrations",
                column: "CareProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PharmacyBranchId",
                table: "AspNetUsers",
                column: "PharmacyBranchId");

            migrationBuilder.CreateIndex(
                name: "IX_pharmacyBranches_PharmacyId",
                table: "pharmacyBranches",
                column: "PharmacyId");

            migrationBuilder.CreateIndex(
                name: "IX_PharmacyServiceRequests_CareProgramId",
                table: "PharmacyServiceRequests",
                column: "CareProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_PharmacyServiceRequests_PharmacyBranchId",
                table: "PharmacyServiceRequests",
                column: "PharmacyBranchId");

            migrationBuilder.CreateIndex(
                name: "IX_PharmacyServiceRequests_PRId",
                table: "PharmacyServiceRequests",
                column: "PRId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramDistributors_DistributorsId",
                table: "ProgramDistributors",
                column: "DistributorsId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramPharmacies_PharmaciesId",
                table: "ProgramPharmacies",
                column: "PharmaciesId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_pharmacyBranches_PharmacyBranchId",
                table: "AspNetUsers",
                column: "PharmacyBranchId",
                principalTable: "pharmacyBranches",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientRegistrations_CarePrograms_CareProgramId",
                table: "PatientRegistrations",
                column: "CareProgramId",
                principalTable: "CarePrograms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_pharmacyBranches_PharmacyBranchId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_PatientRegistrations_CarePrograms_CareProgramId",
                table: "PatientRegistrations");

            migrationBuilder.DropTable(
                name: "PharmacyServiceRequests");

            migrationBuilder.DropTable(
                name: "ProgramDistributors");

            migrationBuilder.DropTable(
                name: "ProgramPharmacies");

            migrationBuilder.DropTable(
                name: "pharmacyBranches");

            migrationBuilder.DropTable(
                name: "Distributors");

            migrationBuilder.DropTable(
                name: "CarePrograms");

            migrationBuilder.DropTable(
                name: "Pharmacies");

            migrationBuilder.DropIndex(
                name: "IX_PatientRegistrations_CareProgramId",
                table: "PatientRegistrations");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_PharmacyBranchId",
                table: "AspNetUsers");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "33f18fcf-5faf-46eb-b704-411570d2cfc4");

            migrationBuilder.DropColumn(
                name: "CareProgramId",
                table: "PatientRegistrations");

            migrationBuilder.DropColumn(
                name: "PharmacyBranchId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PharmacyId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "Medication",
                table: "PatientRegistrations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProgramName",
                table: "PatientRegistrations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "[Program Sponsor]",
                table: "PatientRegistrations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
