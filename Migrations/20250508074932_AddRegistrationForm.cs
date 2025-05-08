using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECARE.Migrations
{
    /// <inheritdoc />
    public partial class AddRegistrationForm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
         
            migrationBuilder.CreateTable(
                name: "RegistrationForms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    Prescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    NationalIDPhotoFront = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    NationalIDPhotoBack = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    PSATestImage = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    AtomicScanningImage = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistrationForms", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RegistrationForms");

        }
    }
}
