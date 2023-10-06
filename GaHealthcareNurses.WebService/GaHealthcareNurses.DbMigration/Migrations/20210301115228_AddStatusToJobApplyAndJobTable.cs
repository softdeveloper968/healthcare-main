using Microsoft.EntityFrameworkCore.Migrations;

namespace GaHealthcareNurses.DbMigration.Migrations
{
    public partial class AddStatusToJobApplyAndJobTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "JobApply",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "Job",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_JobApply_StatusId",
                table: "JobApply",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Job_StatusId",
                table: "Job",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Job_Status_StatusId",
                table: "Job",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "StatusId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_JobApply_Status_StatusId",
                table: "JobApply",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "StatusId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Job_Status_StatusId",
                table: "Job");

            migrationBuilder.DropForeignKey(
                name: "FK_JobApply_Status_StatusId",
                table: "JobApply");

            migrationBuilder.DropIndex(
                name: "IX_JobApply_StatusId",
                table: "JobApply");

            migrationBuilder.DropIndex(
                name: "IX_Job_StatusId",
                table: "Job");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "JobApply");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "Job");
        }
    }
}
