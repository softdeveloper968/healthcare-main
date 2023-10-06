using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GaHealthcareNurses.DbMigration.Migrations
{
    public partial class AddedFieldsToHiringAggrements : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AlreadyHapititisBSeriesDone",
                table: "HiringAgreements",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CnaAndPcaDutiesAndResponsibilities",
                table: "HiringAgreements",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CommissionExpiresOn",
                table: "HiringAgreements",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CredibleEvidenceFillingDate",
                table: "HiringAgreements",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeclinationOfInfluenzaVaccinationFillingDate",
                table: "HiringAgreements",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "DocumentationOfTrainingAndOrientation",
                table: "HiringAgreements",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HepatitisBSeries",
                table: "HiringAgreements",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "NotaryPublic",
                table: "HiringAgreements",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PartnershipForHealth",
                table: "HiringAgreements",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "RefusedToTakeHepatitisBSeriesInPast",
                table: "HiringAgreements",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "WantToHaveHepatitisBSeries",
                table: "HiringAgreements",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Witness",
                table: "HiringAgreements",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AlreadyHapititisBSeriesDone",
                table: "HiringAgreements");

            migrationBuilder.DropColumn(
                name: "CnaAndPcaDutiesAndResponsibilities",
                table: "HiringAgreements");

            migrationBuilder.DropColumn(
                name: "CommissionExpiresOn",
                table: "HiringAgreements");

            migrationBuilder.DropColumn(
                name: "CredibleEvidenceFillingDate",
                table: "HiringAgreements");

            migrationBuilder.DropColumn(
                name: "DeclinationOfInfluenzaVaccinationFillingDate",
                table: "HiringAgreements");

            migrationBuilder.DropColumn(
                name: "DocumentationOfTrainingAndOrientation",
                table: "HiringAgreements");

            migrationBuilder.DropColumn(
                name: "HepatitisBSeries",
                table: "HiringAgreements");

            migrationBuilder.DropColumn(
                name: "NotaryPublic",
                table: "HiringAgreements");

            migrationBuilder.DropColumn(
                name: "PartnershipForHealth",
                table: "HiringAgreements");

            migrationBuilder.DropColumn(
                name: "RefusedToTakeHepatitisBSeriesInPast",
                table: "HiringAgreements");

            migrationBuilder.DropColumn(
                name: "WantToHaveHepatitisBSeries",
                table: "HiringAgreements");

            migrationBuilder.DropColumn(
                name: "Witness",
                table: "HiringAgreements");
        }
    }
}
