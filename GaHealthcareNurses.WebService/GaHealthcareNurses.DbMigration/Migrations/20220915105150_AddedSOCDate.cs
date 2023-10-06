using Microsoft.EntityFrameworkCore.Migrations;

namespace GaHealthcareNurses.DbMigration.Migrations
{
    public partial class AddedSOCDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ClinicianSignatureAndDate",
                table: "CarePlan",
                newName: "VerbalSOCDate");

            migrationBuilder.AddColumn<string>(
                name: "ClinicianSignature",
                table: "CarePlan",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClinicianSignature",
                table: "CarePlan");

            migrationBuilder.RenameColumn(
                name: "VerbalSOCDate",
                table: "CarePlan",
                newName: "ClinicianSignatureAndDate");
        }
    }
}
