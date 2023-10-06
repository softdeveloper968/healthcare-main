using Microsoft.EntityFrameworkCore.Migrations;

namespace GaHealthcareNurses.DbMigration.Migrations
{
    public partial class UpdatedAdultAssessment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmployerId",
                table: "AdultAssessment",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AdultAssessment_EmployerId",
                table: "AdultAssessment",
                column: "EmployerId");

            migrationBuilder.AddForeignKey(
                name: "FK_AdultAssessment_Employer_EmployerId",
                table: "AdultAssessment",
                column: "EmployerId",
                principalTable: "Employer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdultAssessment_Employer_EmployerId",
                table: "AdultAssessment");

            migrationBuilder.DropIndex(
                name: "IX_AdultAssessment_EmployerId",
                table: "AdultAssessment");

            migrationBuilder.DropColumn(
                name: "EmployerId",
                table: "AdultAssessment");
        }
    }
}
