using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECARE.Migrations
{
    /// <inheritdoc />
    public partial class uuu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TestName",
                table: "ServiceRequests");

            migrationBuilder.RenameColumn(
                name: "TestDocument2",
                table: "PatientRegistrations",
                newName: "[Test 2 Document ]");

            migrationBuilder.RenameColumn(
                name: "TestDocument1",
                table: "PatientRegistrations",
                newName: "[Test 1 Document ]");

            migrationBuilder.RenameColumn(
                name: "StartDateOfMedication",
                table: "PatientRegistrations",
                newName: "[Start Date Of Medication]");

            migrationBuilder.RenameColumn(
                name: "RegistrationDate",
                table: "PatientRegistrations",
                newName: "[Registration Date]");

            migrationBuilder.RenameColumn(
                name: "ProgramSponsor",
                table: "PatientRegistrations",
                newName: "[Program Sponsor]");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber2",
                table: "PatientRegistrations",
                newName: "[Phone Number2]");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber1",
                table: "PatientRegistrations",
                newName: "[Phone Number1]");

            migrationBuilder.RenameColumn(
                name: "PatientName",
                table: "PatientRegistrations",
                newName: "[Patient Name]");

            migrationBuilder.RenameColumn(
                name: "NationalID",
                table: "PatientRegistrations",
                newName: "[National ID]");

            migrationBuilder.RenameColumn(
                name: "MembershipNumber",
                table: "PatientRegistrations",
                newName: "[Membership Number]");

            migrationBuilder.RenameColumn(
                name: "HealthcareProviderAddress",
                table: "PatientRegistrations",
                newName: "[Healthcare Provider Address]");

            migrationBuilder.RenameColumn(
                name: "HealthcareProvider",
                table: "PatientRegistrations",
                newName: "[Healthcare Provider]");

            migrationBuilder.RenameColumn(
                name: "HCPTerritory",
                table: "PatientRegistrations",
                newName: "[HCP Territory ]");

            migrationBuilder.RenameColumn(
                name: "HCPGovernment",
                table: "PatientRegistrations",
                newName: "[HCP Government ]");

            migrationBuilder.RenameColumn(
                name: "CopyOfIDOrPassport",
                table: "PatientRegistrations",
                newName: "[Copy Of National ID / Passport]");

            migrationBuilder.RenameColumn(
                name: "CaregiverPhone",
                table: "PatientRegistrations",
                newName: "[Caregiver Phone]");

            migrationBuilder.RenameColumn(
                name: "CaregiverName",
                table: "PatientRegistrations",
                newName: "[Caregiver Name]");

            migrationBuilder.RenameColumn(
                name: "AgeGroup",
                table: "PatientRegistrations",
                newName: "[Age Group]");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "[Test 2 Document ]",
                table: "PatientRegistrations",
                newName: "TestDocument2");

            migrationBuilder.RenameColumn(
                name: "[Test 1 Document ]",
                table: "PatientRegistrations",
                newName: "TestDocument1");

            migrationBuilder.RenameColumn(
                name: "[Start Date Of Medication]",
                table: "PatientRegistrations",
                newName: "StartDateOfMedication");

            migrationBuilder.RenameColumn(
                name: "[Registration Date]",
                table: "PatientRegistrations",
                newName: "RegistrationDate");

            migrationBuilder.RenameColumn(
                name: "[Program Sponsor]",
                table: "PatientRegistrations",
                newName: "ProgramSponsor");

            migrationBuilder.RenameColumn(
                name: "[Phone Number2]",
                table: "PatientRegistrations",
                newName: "PhoneNumber2");

            migrationBuilder.RenameColumn(
                name: "[Phone Number1]",
                table: "PatientRegistrations",
                newName: "PhoneNumber1");

            migrationBuilder.RenameColumn(
                name: "[Patient Name]",
                table: "PatientRegistrations",
                newName: "PatientName");

            migrationBuilder.RenameColumn(
                name: "[National ID]",
                table: "PatientRegistrations",
                newName: "NationalID");

            migrationBuilder.RenameColumn(
                name: "[Membership Number]",
                table: "PatientRegistrations",
                newName: "MembershipNumber");

            migrationBuilder.RenameColumn(
                name: "[Healthcare Provider]",
                table: "PatientRegistrations",
                newName: "HealthcareProvider");

            migrationBuilder.RenameColumn(
                name: "[Healthcare Provider Address]",
                table: "PatientRegistrations",
                newName: "HealthcareProviderAddress");

            migrationBuilder.RenameColumn(
                name: "[HCP Territory ]",
                table: "PatientRegistrations",
                newName: "HCPTerritory");

            migrationBuilder.RenameColumn(
                name: "[HCP Government ]",
                table: "PatientRegistrations",
                newName: "HCPGovernment");

            migrationBuilder.RenameColumn(
                name: "[Copy Of National ID / Passport]",
                table: "PatientRegistrations",
                newName: "CopyOfIDOrPassport");

            migrationBuilder.RenameColumn(
                name: "[Caregiver Phone]",
                table: "PatientRegistrations",
                newName: "CaregiverPhone");

            migrationBuilder.RenameColumn(
                name: "[Caregiver Name]",
                table: "PatientRegistrations",
                newName: "CaregiverName");

            migrationBuilder.RenameColumn(
                name: "[Age Group]",
                table: "PatientRegistrations",
                newName: "AgeGroup");

            migrationBuilder.AddColumn<string>(
                name: "TestName",
                table: "ServiceRequests",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
