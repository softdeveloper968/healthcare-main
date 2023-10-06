using Microsoft.EntityFrameworkCore.Migrations;

namespace GaHealthcareNurses.DbMigration.Migrations
{
    public partial class CareRecipient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ResponsiblePartyEmail",
                table: "CareRecipient",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ResponsiblePartyName",
                table: "CareRecipient",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ResponsiblePartyRelation",
                table: "CareRecipient",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ResponsiblePartyTelephoneNumber",
                table: "CareRecipient",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResponsiblePartyEmail",
                table: "CareRecipient");

            migrationBuilder.DropColumn(
                name: "ResponsiblePartyName",
                table: "CareRecipient");

            migrationBuilder.DropColumn(
                name: "ResponsiblePartyRelation",
                table: "CareRecipient");

            migrationBuilder.DropColumn(
                name: "ResponsiblePartyTelephoneNumber",
                table: "CareRecipient");
        }
    }
}
