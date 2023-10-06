using Microsoft.EntityFrameworkCore.Migrations;

namespace GaHealthcareNurses.DbMigration.Migrations
{
    public partial class AddedRatingFieldsInJobTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "ClientRatingToAgency",
                table: "Job",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "ClientRatingToNurse",
                table: "Job",
                type: "float",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClientRatingToAgency",
                table: "Job");

            migrationBuilder.DropColumn(
                name: "ClientRatingToNurse",
                table: "Job");
        }
    }
}
