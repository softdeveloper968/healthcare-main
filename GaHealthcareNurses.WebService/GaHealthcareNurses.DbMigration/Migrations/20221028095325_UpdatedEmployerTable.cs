using Microsoft.EntityFrameworkCore.Migrations;

namespace GaHealthcareNurses.DbMigration.Migrations
{
    public partial class UpdatedEmployerTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Calendar",
                table: "Employer",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CarePlan",
                table: "Employer",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CareRecipients",
                table: "Employer",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "DischargeSummary",
                table: "Employer",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Documents",
                table: "Employer",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HeatMap",
                table: "Employer",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "InitialAssessment",
                table: "Employer",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "NursesList",
                table: "Employer",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ServiceAgreements",
                table: "Employer",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "SupervisoryVisits",
                table: "Employer",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Tasks",
                table: "Employer",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Calendar",
                table: "Employer");

            migrationBuilder.DropColumn(
                name: "CarePlan",
                table: "Employer");

            migrationBuilder.DropColumn(
                name: "CareRecipients",
                table: "Employer");

            migrationBuilder.DropColumn(
                name: "DischargeSummary",
                table: "Employer");

            migrationBuilder.DropColumn(
                name: "Documents",
                table: "Employer");

            migrationBuilder.DropColumn(
                name: "HeatMap",
                table: "Employer");

            migrationBuilder.DropColumn(
                name: "InitialAssessment",
                table: "Employer");

            migrationBuilder.DropColumn(
                name: "NursesList",
                table: "Employer");

            migrationBuilder.DropColumn(
                name: "ServiceAgreements",
                table: "Employer");

            migrationBuilder.DropColumn(
                name: "SupervisoryVisits",
                table: "Employer");

            migrationBuilder.DropColumn(
                name: "Tasks",
                table: "Employer");
        }
    }
}
