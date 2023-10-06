using Microsoft.EntityFrameworkCore.Migrations;

namespace GaHealthcareNurses.DbMigration.Migrations
{
    public partial class addedLatitudeLangitudeToNurseAndCareRecipient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Job");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Job");

            migrationBuilder.AddColumn<string>(
                name: "Latitude",
                table: "CareRecipient",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Longitude",
                table: "CareRecipient",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "CareRecipient");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "CareRecipient");

            migrationBuilder.AddColumn<string>(
                name: "Latitude",
                table: "Job",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Longitude",
                table: "Job",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
