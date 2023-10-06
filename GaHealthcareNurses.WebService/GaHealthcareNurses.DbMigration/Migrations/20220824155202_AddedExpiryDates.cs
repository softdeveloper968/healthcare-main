using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GaHealthcareNurses.DbMigration.Migrations
{
    public partial class AddedExpiryDates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "BackgroundCheckExpiryDate",
                table: "Documents",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CnaExpiryDate",
                table: "Documents",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CprExpiryDate",
                table: "Documents",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DrivingLicenseExpiryDate",
                table: "Documents",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EVerifyWillPulledExpiryDate",
                table: "Documents",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "HiringDisclosuresExpiryDate",
                table: "Documents",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "HiringPreScreeningExpiryDate",
                table: "Documents",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "OfficeWillPulledExpiryDate",
                table: "Documents",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PcaTestExpiryDate",
                table: "Documents",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SexOffenderReportOfficeWillPulledExpiryDate",
                table: "Documents",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SsnExpiryDate",
                table: "Documents",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StaffPortalExpiryDate",
                table: "Documents",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TbTestResultsExpiryDate",
                table: "Documents",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BackgroundCheckExpiryDate",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "CnaExpiryDate",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "CprExpiryDate",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "DrivingLicenseExpiryDate",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "EVerifyWillPulledExpiryDate",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "HiringDisclosuresExpiryDate",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "HiringPreScreeningExpiryDate",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "OfficeWillPulledExpiryDate",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "PcaTestExpiryDate",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "SexOffenderReportOfficeWillPulledExpiryDate",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "SsnExpiryDate",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "StaffPortalExpiryDate",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "TbTestResultsExpiryDate",
                table: "Documents");
        }
    }
}
