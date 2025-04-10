using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECARE.Migrations
{
    /// <inheritdoc />
    public partial class MyMigrationooo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Copy Of National ID / Passport",
                table: "PatientRegistrations",
                newName: "CopyOfIDOrPassport");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CopyOfIDOrPassport",
                table: "PatientRegistrations",
                newName: "Copy Of National ID / Passport");
        }
    }
}
