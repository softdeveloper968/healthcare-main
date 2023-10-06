using Microsoft.EntityFrameworkCore.Migrations;

namespace GaHealthcareNurses.DbMigration.Migrations
{
    public partial class SupervisoryVisit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SupervisoryVisit",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobId = table.Column<int>(type: "int", nullable: false),
                    NurseId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    EmployerId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    StaffPersonInHome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SupervisoryVisitInfo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FollowingPoc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PocChanged = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatientCompatible = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CommunicatesWithPt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompilesWithInfection = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HonorsPatientRights = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatientNotifiedOfChange = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupervisoryVisit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SupervisoryVisit_Employer_EmployerId",
                        column: x => x.EmployerId,
                        principalTable: "Employer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SupervisoryVisit_Job_JobId",
                        column: x => x.JobId,
                        principalTable: "Job",
                        principalColumn: "JobId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SupervisoryVisit_Nurse_NurseId",
                        column: x => x.NurseId,
                        principalTable: "Nurse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SupervisoryVisit_EmployerId",
                table: "SupervisoryVisit",
                column: "EmployerId");

            migrationBuilder.CreateIndex(
                name: "IX_SupervisoryVisit_JobId",
                table: "SupervisoryVisit",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_SupervisoryVisit_NurseId",
                table: "SupervisoryVisit",
                column: "NurseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SupervisoryVisit");
        }
    }
}
