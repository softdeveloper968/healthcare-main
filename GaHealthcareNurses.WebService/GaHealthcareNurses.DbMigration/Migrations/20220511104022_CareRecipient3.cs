using Microsoft.EntityFrameworkCore.Migrations;

namespace GaHealthcareNurses.DbMigration.Migrations
{
    public partial class CareRecipient3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsResponsiblePatyInputsVisible",
                table: "CareRecipient",
                newName: "IsResponsiblePartySameAsCareRecipient");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsResponsiblePartySameAsCareRecipient",
                table: "CareRecipient",
                newName: "IsResponsiblePatyInputsVisible");
        }
    }
}
