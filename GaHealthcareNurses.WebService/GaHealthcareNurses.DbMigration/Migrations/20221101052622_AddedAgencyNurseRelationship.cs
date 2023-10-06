using Microsoft.EntityFrameworkCore.Migrations;

namespace GaHealthcareNurses.DbMigration.Migrations
{
    public partial class AddedAgencyNurseRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmployerNurse",
                columns: table => new
                {
                    EmployersId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NursesId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployerNurse", x => new { x.EmployersId, x.NursesId });
                    table.ForeignKey(
                        name: "FK_EmployerNurse_Employer_EmployersId",
                        column: x => x.EmployersId,
                        principalTable: "Employer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployerNurse_Nurse_NursesId",
                        column: x => x.NursesId,
                        principalTable: "Nurse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployerNurse_NursesId",
                table: "EmployerNurse",
                column: "NursesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployerNurse");
        }
    }
}
