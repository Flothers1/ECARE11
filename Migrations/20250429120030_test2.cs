using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECARE.Migrations
{
    /// <inheritdoc />
    public partial class test2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientRegistrations_CarePrograms_CareProgramId",
                table: "PatientRegistrations");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "33f18fcf-5faf-46eb-b704-411570d2cfc4");

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

            migrationBuilder.AddForeignKey(
                name: "FK_PatientRegistrations_CarePrograms_CareProgramId",
                table: "PatientRegistrations",
                column: "CareProgramId",
                principalTable: "CarePrograms",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientRegistrations_CarePrograms_CareProgramId",
                table: "PatientRegistrations");

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
                values: new object[] { "33f18fcf-5faf-46eb-b704-411570d2cfc4", null, "PharmacyAdmin", "PHARMACYADMIN" });

            migrationBuilder.AddForeignKey(
                name: "FK_PatientRegistrations_CarePrograms_CareProgramId",
                table: "PatientRegistrations",
                column: "CareProgramId",
                principalTable: "CarePrograms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
