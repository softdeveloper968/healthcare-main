using Microsoft.EntityFrameworkCore.Migrations;

namespace GaHealthcareNurses.DbMigration.Migrations
{
    public partial class AddedForeignKeyCityIdInNurseTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Nurse_CityId",
                table: "Nurse",
                column: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Nurse_City_CityId",
                table: "Nurse",
                column: "CityId",
                principalTable: "City",
                principalColumn: "CityId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Nurse_City_CityId",
                table: "Nurse");

            migrationBuilder.DropIndex(
                name: "IX_Nurse_CityId",
                table: "Nurse");
        }
    }
}
