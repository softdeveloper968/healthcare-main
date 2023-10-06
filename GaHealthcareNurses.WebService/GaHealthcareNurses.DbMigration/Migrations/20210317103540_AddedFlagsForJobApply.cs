using Microsoft.EntityFrameworkCore.Migrations;

namespace GaHealthcareNurses.DbMigration.Migrations
{
    public partial class AddedFlagsForJobApply : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AcceptJobDescriptionAndPolicies",
                table: "Job");

            migrationBuilder.AddColumn<bool>(
                name: "AcceptJobDescriptionAndPolicies",
                table: "JobApply",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CNACanBeShared",
                table: "JobApply",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CPRCanBeShared",
                table: "JobApply",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "DocumentsCanBeShared",
                table: "JobApply",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "DrivingLicenseCanBeShare",
                table: "JobApply",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "G4CanBeShared",
                table: "JobApply",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HiringDisclosuresCanBeShared",
                table: "JobApply",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HiringPreScreeningCanBeShared",
                table: "JobApply",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "SSNCanBeShared",
                table: "JobApply",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "TBResultsCanBeShared",
                table: "JobApply",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "W4CanBeShared",
                table: "JobApply",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AcceptJobDescriptionAndPolicies",
                table: "JobApply");

            migrationBuilder.DropColumn(
                name: "CNACanBeShared",
                table: "JobApply");

            migrationBuilder.DropColumn(
                name: "CPRCanBeShared",
                table: "JobApply");

            migrationBuilder.DropColumn(
                name: "DocumentsCanBeShared",
                table: "JobApply");

            migrationBuilder.DropColumn(
                name: "DrivingLicenseCanBeShare",
                table: "JobApply");

            migrationBuilder.DropColumn(
                name: "G4CanBeShared",
                table: "JobApply");

            migrationBuilder.DropColumn(
                name: "HiringDisclosuresCanBeShared",
                table: "JobApply");

            migrationBuilder.DropColumn(
                name: "HiringPreScreeningCanBeShared",
                table: "JobApply");

            migrationBuilder.DropColumn(
                name: "SSNCanBeShared",
                table: "JobApply");

            migrationBuilder.DropColumn(
                name: "TBResultsCanBeShared",
                table: "JobApply");

            migrationBuilder.DropColumn(
                name: "W4CanBeShared",
                table: "JobApply");

            migrationBuilder.AddColumn<bool>(
                name: "AcceptJobDescriptionAndPolicies",
                table: "Job",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
