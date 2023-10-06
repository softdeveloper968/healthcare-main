using Microsoft.EntityFrameworkCore.Migrations;

namespace GaHealthcareNurses.DbMigration.Migrations
{
    public partial class UpdatedAgencyTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CNAs",
                table: "Employer",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LPNs",
                table: "Employer",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PCAs",
                table: "Employer",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RNs",
                table: "Employer",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CNAs",
                table: "Employer");

            migrationBuilder.DropColumn(
                name: "LPNs",
                table: "Employer");

            migrationBuilder.DropColumn(
                name: "PCAs",
                table: "Employer");

            migrationBuilder.DropColumn(
                name: "RNs",
                table: "Employer");
        }
    }
}
