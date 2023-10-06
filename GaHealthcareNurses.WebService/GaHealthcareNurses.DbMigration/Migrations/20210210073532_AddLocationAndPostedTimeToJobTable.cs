using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GaHealthcareNurses.DbMigration.Migrations
{
    public partial class AddLocationAndPostedTimeToJobTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "Job",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PostedTime",
                table: "Job",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "RequiredServiceId",
                table: "CareRecipient",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Job_CityId",
                table: "Job",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_CareRecipient_RequiredServiceId",
                table: "CareRecipient",
                column: "RequiredServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_CareRecipient_RequiredService_RequiredServiceId",
                table: "CareRecipient",
                column: "RequiredServiceId",
                principalTable: "RequiredService",
                principalColumn: "RequiredServiceId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Job_City_CityId",
                table: "Job",
                column: "CityId",
                principalTable: "City",
                principalColumn: "CityId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CareRecipient_RequiredService_RequiredServiceId",
                table: "CareRecipient");

            migrationBuilder.DropForeignKey(
                name: "FK_Job_City_CityId",
                table: "Job");

            migrationBuilder.DropIndex(
                name: "IX_Job_CityId",
                table: "Job");

            migrationBuilder.DropIndex(
                name: "IX_CareRecipient_RequiredServiceId",
                table: "CareRecipient");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "Job");

            migrationBuilder.DropColumn(
                name: "PostedTime",
                table: "Job");

            migrationBuilder.DropColumn(
                name: "RequiredServiceId",
                table: "CareRecipient");
        }
    }
}
