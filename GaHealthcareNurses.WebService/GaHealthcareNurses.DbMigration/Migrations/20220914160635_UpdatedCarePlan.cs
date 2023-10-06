using Microsoft.EntityFrameworkCore.Migrations;

namespace GaHealthcareNurses.DbMigration.Migrations
{
    public partial class UpdatedCarePlan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsPersonalCareBathComplete",
                table: "CarePlan",
                newName: "IsPertientInformationOther");

            migrationBuilder.RenameColumn(
                name: "EnvironmentalOtherBotes",
                table: "CarePlan",
                newName: "PertientInformationOther");

            migrationBuilder.AlterColumn<string>(
                name: "IsPersonalCareBathPartial",
                table: "CarePlan",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<string>(
                name: "ActivityTransferAssistFrequency",
                table: "CarePlan",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ActivityTransferAssistNotes",
                table: "CarePlan",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EnvironmentalOtherNotes",
                table: "CarePlan",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsObserveRespiration",
                table: "CarePlan",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsObserveRespirationOver",
                table: "CarePlan",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsObserveRespirationUnder",
                table: "CarePlan",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ObserveRespirationOver",
                table: "CarePlan",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ObserveRespirationUnder",
                table: "CarePlan",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActivityTransferAssistFrequency",
                table: "CarePlan");

            migrationBuilder.DropColumn(
                name: "ActivityTransferAssistNotes",
                table: "CarePlan");

            migrationBuilder.DropColumn(
                name: "EnvironmentalOtherNotes",
                table: "CarePlan");

            migrationBuilder.DropColumn(
                name: "IsObserveRespiration",
                table: "CarePlan");

            migrationBuilder.DropColumn(
                name: "IsObserveRespirationOver",
                table: "CarePlan");

            migrationBuilder.DropColumn(
                name: "IsObserveRespirationUnder",
                table: "CarePlan");

            migrationBuilder.DropColumn(
                name: "ObserveRespirationOver",
                table: "CarePlan");

            migrationBuilder.DropColumn(
                name: "ObserveRespirationUnder",
                table: "CarePlan");

            migrationBuilder.RenameColumn(
                name: "PertientInformationOther",
                table: "CarePlan",
                newName: "EnvironmentalOtherBotes");

            migrationBuilder.RenameColumn(
                name: "IsPertientInformationOther",
                table: "CarePlan",
                newName: "IsPersonalCareBathComplete");

            migrationBuilder.AlterColumn<bool>(
                name: "IsPersonalCareBathPartial",
                table: "CarePlan",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
