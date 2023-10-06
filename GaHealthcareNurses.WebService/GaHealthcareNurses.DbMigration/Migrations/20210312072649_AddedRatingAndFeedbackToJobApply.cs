using Microsoft.EntityFrameworkCore.Migrations;

namespace GaHealthcareNurses.DbMigration.Migrations
{
    public partial class AddedRatingAndFeedbackToJobApply : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Feedback",
                table: "JobApply",
                newName: "NurseFeedback");

            migrationBuilder.AddColumn<string>(
                name: "ClientFeedback",
                table: "JobApply",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ClientRating",
                table: "JobApply",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NurseRating",
                table: "JobApply",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClientFeedback",
                table: "JobApply");

            migrationBuilder.DropColumn(
                name: "ClientRating",
                table: "JobApply");

            migrationBuilder.DropColumn(
                name: "NurseRating",
                table: "JobApply");

            migrationBuilder.RenameColumn(
                name: "NurseFeedback",
                table: "JobApply",
                newName: "Feedback");
        }
    }
}
