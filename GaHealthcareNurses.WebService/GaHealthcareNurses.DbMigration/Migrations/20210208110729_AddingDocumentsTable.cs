using Microsoft.EntityFrameworkCore.Migrations;

namespace GaHealthcareNurses.DbMigration.Migrations
{
    public partial class AddingDocumentsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CnaOrProfessionalLicense = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CprProvideNewLicense = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ssn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DrivingLicense = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TbTestResults = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StaffPortalInfo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PcaTest = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HiringDisclosures = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HiringPreScreening = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OfficeWillPulled = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SexOffenderReportOfficeWillPulled = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EVerifyWillPulled = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Documents_Nurse_Id",
                        column: x => x.Id,
                        principalTable: "Nurse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Documents");
        }
    }
}
