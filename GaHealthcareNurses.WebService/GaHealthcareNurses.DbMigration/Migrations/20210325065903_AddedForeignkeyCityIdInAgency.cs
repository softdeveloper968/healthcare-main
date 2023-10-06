using Microsoft.EntityFrameworkCore.Migrations;

namespace GaHealthcareNurses.DbMigration.Migrations
{
    public partial class AddedForeignkeyCityIdInAgency : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Employer_CityId",
                table: "Employer",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Employer_StateId",
                table: "Employer",
                column: "StateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employer_City_CityId",
                table: "Employer",
                column: "CityId",
                principalTable: "City",
                principalColumn: "CityId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Employer_State_StateId",
                table: "Employer",
                column: "StateId",
                principalTable: "State",
                principalColumn: "StateId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employer_City_CityId",
                table: "Employer");

            migrationBuilder.DropForeignKey(
                name: "FK_Employer_State_StateId",
                table: "Employer");

            migrationBuilder.DropIndex(
                name: "IX_Employer_CityId",
                table: "Employer");

            migrationBuilder.DropIndex(
                name: "IX_Employer_StateId",
                table: "Employer");
        }
    }
}
