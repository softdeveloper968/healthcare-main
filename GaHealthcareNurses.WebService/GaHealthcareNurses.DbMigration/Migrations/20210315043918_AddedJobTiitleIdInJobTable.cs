using Microsoft.EntityFrameworkCore.Migrations;

namespace GaHealthcareNurses.DbMigration.Migrations
{
    public partial class AddedJobTiitleIdInJobTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "Job");

            migrationBuilder.AddColumn<int>(
                name: "JobTitleId",
                table: "Job",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Job_JobTitleId",
                table: "Job",
                column: "JobTitleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Job_JobTitle_JobTitleId",
                table: "Job",
                column: "JobTitleId",
                principalTable: "JobTitle",
                principalColumn: "JobTitleId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Job_JobTitle_JobTitleId",
                table: "Job");

            migrationBuilder.DropIndex(
                name: "IX_Job_JobTitleId",
                table: "Job");

            migrationBuilder.DropColumn(
                name: "JobTitleId",
                table: "Job");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Job",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
