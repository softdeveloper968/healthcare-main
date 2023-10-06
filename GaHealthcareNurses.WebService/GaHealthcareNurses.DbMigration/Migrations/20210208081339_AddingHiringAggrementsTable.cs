using Microsoft.EntityFrameworkCore.Migrations;

namespace GaHealthcareNurses.DbMigration.Migrations
{
    public partial class AddingHiringAggrementsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HiringAgreements",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AcknowledgementOfPoliciesAndProcedures = table.Column<bool>(type: "bit", nullable: false),
                    JobDescription = table.Column<bool>(type: "bit", nullable: false),
                    GahRegulations = table.Column<bool>(type: "bit", nullable: false),
                    ConfidientiallyStatement = table.Column<bool>(type: "bit", nullable: false),
                    DeclinationOfInfluenzaVaccination = table.Column<bool>(type: "bit", nullable: false),
                    FireOrSafetyProcedure = table.Column<bool>(type: "bit", nullable: false),
                    CodeOfEthics = table.Column<bool>(type: "bit", nullable: false),
                    AbuseNeglectAndExploitationPolicy = table.Column<bool>(type: "bit", nullable: false),
                    ClientCareInEmergency = table.Column<bool>(type: "bit", nullable: false),
                    CredibleEvidenceOfAbuseStatement = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HiringAgreements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HiringAgreements_Nurse_Id",
                        column: x => x.Id,
                        principalTable: "Nurse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HiringAgreements");
        }
    }
}
