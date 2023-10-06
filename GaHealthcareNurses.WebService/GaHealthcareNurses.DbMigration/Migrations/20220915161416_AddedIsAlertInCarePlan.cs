using Microsoft.EntityFrameworkCore.Migrations;

namespace GaHealthcareNurses.DbMigration.Migrations
{
    public partial class AddedIsAlertInCarePlan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Alert",
                table: "CarePlan");

            migrationBuilder.AddColumn<bool>(
                name: "IsAlert",
                table: "CarePlan",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAlert",
                table: "CarePlan");

            migrationBuilder.AddColumn<string>(
                name: "Alert",
                table: "CarePlan",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
