using Microsoft.EntityFrameworkCore.Migrations;

namespace GaHealthcareNurses.DbMigration.Migrations
{
    public partial class AddedBoolFieldsToNurseTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AgreeToApplicantStatement",
                table: "Nurse",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "AgreeToNonDiscriminationDocument",
                table: "Nurse",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AgreeToApplicantStatement",
                table: "Nurse");

            migrationBuilder.DropColumn(
                name: "AgreeToNonDiscriminationDocument",
                table: "Nurse");
        }
    }
}
