using Microsoft.EntityFrameworkCore.Migrations;

namespace GaHealthcareNurses.DbMigration.Migrations
{
    public partial class JobTableRelationships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RequiredServiceId",
                table: "Job",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ResourceId",
                table: "Job",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Resource",
                columns: table => new
                {
                    ResourceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Abbreviation = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resource", x => x.ResourceId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Job_RequiredServiceId",
                table: "Job",
                column: "RequiredServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Job_ResourceId",
                table: "Job",
                column: "ResourceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Job_RequiredService_RequiredServiceId",
                table: "Job",
                column: "RequiredServiceId",
                principalTable: "RequiredService",
                principalColumn: "RequiredServiceId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Job_Resource_ResourceId",
                table: "Job",
                column: "ResourceId",
                principalTable: "Resource",
                principalColumn: "ResourceId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Job_RequiredService_RequiredServiceId",
                table: "Job");

            migrationBuilder.DropForeignKey(
                name: "FK_Job_Resource_ResourceId",
                table: "Job");

            migrationBuilder.DropTable(
                name: "Resource");

            migrationBuilder.DropIndex(
                name: "IX_Job_RequiredServiceId",
                table: "Job");

            migrationBuilder.DropIndex(
                name: "IX_Job_ResourceId",
                table: "Job");

            migrationBuilder.DropColumn(
                name: "RequiredServiceId",
                table: "Job");

            migrationBuilder.DropColumn(
                name: "ResourceId",
                table: "Job");
        }
    }
}
