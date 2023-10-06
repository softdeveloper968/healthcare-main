using Microsoft.EntityFrameworkCore.Migrations;

namespace GaHealthcareNurses.DbMigration.Migrations
{
    public partial class AddedServiceListTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Client_RequiredService_RequiredServiceId",
                table: "Client");

            migrationBuilder.DropIndex(
                name: "IX_Client_RequiredServiceId",
                table: "Client");

            migrationBuilder.DropColumn(
                name: "ServiceNeeded",
                table: "RequiredService");

            migrationBuilder.DropColumn(
                name: "RequiredServiceId",
                table: "Client");

            migrationBuilder.AddColumn<string>(
                name: "ClientId",
                table: "RequiredService",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ServiceListId",
                table: "RequiredService",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ServiceList",
                columns: table => new
                {
                    ServiceListId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceList", x => x.ServiceListId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RequiredService_ClientId",
                table: "RequiredService",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_RequiredService_ServiceListId",
                table: "RequiredService",
                column: "ServiceListId");

            migrationBuilder.AddForeignKey(
                name: "FK_RequiredService_Client_ClientId",
                table: "RequiredService",
                column: "ClientId",
                principalTable: "Client",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RequiredService_ServiceList_ServiceListId",
                table: "RequiredService",
                column: "ServiceListId",
                principalTable: "ServiceList",
                principalColumn: "ServiceListId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequiredService_Client_ClientId",
                table: "RequiredService");

            migrationBuilder.DropForeignKey(
                name: "FK_RequiredService_ServiceList_ServiceListId",
                table: "RequiredService");

            migrationBuilder.DropTable(
                name: "ServiceList");

            migrationBuilder.DropIndex(
                name: "IX_RequiredService_ClientId",
                table: "RequiredService");

            migrationBuilder.DropIndex(
                name: "IX_RequiredService_ServiceListId",
                table: "RequiredService");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "RequiredService");

            migrationBuilder.DropColumn(
                name: "ServiceListId",
                table: "RequiredService");

            migrationBuilder.AddColumn<string>(
                name: "ServiceNeeded",
                table: "RequiredService",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RequiredServiceId",
                table: "Client",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Client_RequiredServiceId",
                table: "Client",
                column: "RequiredServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Client_RequiredService_RequiredServiceId",
                table: "Client",
                column: "RequiredServiceId",
                principalTable: "RequiredService",
                principalColumn: "RequiredServiceId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
