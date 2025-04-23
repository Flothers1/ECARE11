using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECARE.Migrations
{
    /// <inheritdoc />
    public partial class ModifyPatientRegisterationAndCareProgramProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Indication",
                table: "PatientRegistrations");

            migrationBuilder.DropColumn(
                name: "Medication",
                table: "PatientRegistrations");

            migrationBuilder.DropColumn(
                name: "ProgramName",
                table: "PatientRegistrations");

            migrationBuilder.AddColumn<string>(
                name: "SponsorCompany",
                table: "CarePrograms",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SponsorCompany",
                table: "CarePrograms");

            migrationBuilder.AddColumn<string>(
                name: "Indication",
                table: "PatientRegistrations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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
        }
    }
}
