using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GaHealthcareNurses.DbMigration.Migrations
{
    public partial class AddedLatLongToTaskListVerification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Latitude",
                table: "TaskListVerification",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Longitude",
                table: "TaskListVerification",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TaskVerifiedTime",
                table: "TaskListVerification",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "TaskListVerification");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "TaskListVerification");

            migrationBuilder.DropColumn(
                name: "TaskVerifiedTime",
                table: "TaskListVerification");
        }
    }
}
