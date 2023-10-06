using Microsoft.EntityFrameworkCore.Migrations;

namespace GaHealthcareNurses.DbMigration.Migrations
{
    public partial class ReferralCount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReferralCount",
                table: "Nurse",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TotalRewards",
                table: "Nurse",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReferralCount",
                table: "Nurse");

            migrationBuilder.DropColumn(
                name: "TotalRewards",
                table: "Nurse");
        }
    }
}
