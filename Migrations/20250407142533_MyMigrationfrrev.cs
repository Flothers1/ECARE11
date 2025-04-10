using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECARE.Migrations
{
    /// <inheritdoc />
    public partial class MyMigrationfrrev : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Passport",
                table: "PatientRegistrations");

            migrationBuilder.RenameColumn(
                name: "CopyOfIDOrPassport",
                table: "PatientRegistrations",
                newName: "CopyOfIDOrPassportFileFront");

            migrationBuilder.AddColumn<string>(
                name: "CopyOfIDOrPassportFileBack",
                table: "PatientRegistrations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CopyOfIDOrPassportFileBack",
                table: "PatientRegistrations");

            migrationBuilder.RenameColumn(
                name: "CopyOfIDOrPassportFileFront",
                table: "PatientRegistrations",
                newName: "CopyOfIDOrPassport");

            migrationBuilder.AddColumn<string>(
                name: "Passport",
                table: "PatientRegistrations",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
