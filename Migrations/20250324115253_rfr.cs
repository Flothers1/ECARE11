using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECARE.Migrations
{
    /// <inheritdoc />
    public partial class rfr : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CopyOfIDOrPassportPath",
                table: "PatientRegistrations",
                newName: "Copy Of National ID / Passport");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Copy Of National ID / Passport",
                table: "PatientRegistrations",
                newName: "CopyOfIDOrPassportPath");
        }
    }
}
