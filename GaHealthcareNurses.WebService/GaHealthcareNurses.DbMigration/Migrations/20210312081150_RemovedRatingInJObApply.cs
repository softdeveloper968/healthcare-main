using Microsoft.EntityFrameworkCore.Migrations;

namespace GaHealthcareNurses.DbMigration.Migrations
{
    public partial class RemovedRatingInJObApply : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClientRating",
                table: "JobApply");

            migrationBuilder.DropColumn(
                name: "NurseRating",
                table: "JobApply");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClientRating",
                table: "JobApply",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NurseRating",
                table: "JobApply",
                type: "int",
                nullable: true);
        }
    }
}
