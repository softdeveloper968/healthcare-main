using Microsoft.EntityFrameworkCore.Migrations;

namespace GaHealthcareNurses.DbMigration.Migrations
{
    public partial class PreferredRateChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HourlyRateStart",
                table: "Nurse",
                newName: "PreferredHourlyRateUpto");

            migrationBuilder.RenameColumn(
                name: "HourlyRateEnd",
                table: "Nurse",
                newName: "PreferredHourlyRate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PreferredHourlyRateUpto",
                table: "Nurse",
                newName: "HourlyRateStart");

            migrationBuilder.RenameColumn(
                name: "PreferredHourlyRate",
                table: "Nurse",
                newName: "HourlyRateEnd");
        }
    }
}
