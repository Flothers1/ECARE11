using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECARE.Migrations
{
    /// <inheritdoc />
    public partial class MyMigrationtvtrb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "[Test 2 Document ]",
                table: "PatientRegistrations",
                newName: "TestDocument2");

            migrationBuilder.RenameColumn(
                name: "[Test 1 Document ]",
                table: "PatientRegistrations",
                newName: "TestDocument1");

            migrationBuilder.AddColumn<string>(
                name: "PrescriptionPath",
                table: "PatientRegistrations",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrescriptionPath",
                table: "PatientRegistrations");

            migrationBuilder.RenameColumn(
                name: "TestDocument2",
                table: "PatientRegistrations",
                newName: "[Test 2 Document ]");

            migrationBuilder.RenameColumn(
                name: "TestDocument1",
                table: "PatientRegistrations",
                newName: "[Test 1 Document ]");
        }
    }
}
