using Microsoft.EntityFrameworkCore.Migrations;

namespace GaHealthcareNurses.DbMigration.Migrations
{
    public partial class UpdatedJob : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "CareGiverRate",
                table: "Job",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsInitialAssesstmentRequired",
                table: "Job",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsSupervisoryVisitsRequired",
                table: "Job",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ReAssessmentFrequency",
                table: "Job",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReVisitFrequency",
                table: "Job",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CareGiverRate",
                table: "Job");

            migrationBuilder.DropColumn(
                name: "IsInitialAssesstmentRequired",
                table: "Job");

            migrationBuilder.DropColumn(
                name: "IsSupervisoryVisitsRequired",
                table: "Job");

            migrationBuilder.DropColumn(
                name: "ReAssessmentFrequency",
                table: "Job");

            migrationBuilder.DropColumn(
                name: "ReVisitFrequency",
                table: "Job");
        }
    }
}
