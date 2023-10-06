using Microsoft.EntityFrameworkCore.Migrations;

namespace GaHealthcareNurses.DbMigration.Migrations
{
    public partial class ForeignKeyClientIdInJobTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Job_City_CityId",
                table: "Job");

            migrationBuilder.DropIndex(
                name: "IX_Job_CityId",
                table: "Job");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "Job");

            migrationBuilder.DropColumn(
                name: "ServiceListId",
                table: "Job");

            migrationBuilder.AddColumn<string>(
                name: "ClientId",
                table: "Job",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Job_ClientId",
                table: "Job",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Job_Client_ClientId",
                table: "Job",
                column: "ClientId",
                principalTable: "Client",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Job_Client_ClientId",
                table: "Job");

            migrationBuilder.DropIndex(
                name: "IX_Job_ClientId",
                table: "Job");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Job");

            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "Job",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ServiceListId",
                table: "Job",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Job_CityId",
                table: "Job",
                column: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Job_City_CityId",
                table: "Job",
                column: "CityId",
                principalTable: "City",
                principalColumn: "CityId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
