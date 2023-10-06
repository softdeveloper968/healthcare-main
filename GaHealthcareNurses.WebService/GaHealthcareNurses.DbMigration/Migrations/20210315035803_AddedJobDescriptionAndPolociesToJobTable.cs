using Microsoft.EntityFrameworkCore.Migrations;

namespace GaHealthcareNurses.DbMigration.Migrations
{
    public partial class AddedJobDescriptionAndPolociesToJobTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AcceptJobDescriptionAndPolicies",
                table: "JobApply");

            migrationBuilder.AddColumn<bool>(
                name: "AcceptJobDescriptionAndPolicies",
                table: "Job",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AcceptJobDescriptionAndPolicies",
                table: "Job");

            migrationBuilder.AddColumn<bool>(
                name: "AcceptJobDescriptionAndPolicies",
                table: "JobApply",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
