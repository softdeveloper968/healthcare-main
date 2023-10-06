using Microsoft.EntityFrameworkCore.Migrations;

namespace GaHealthcareNurses.DbMigration.Migrations
{
    public partial class RemovedNullCheckInClientRecipient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CareRecipient_RequiredService_RequiredServiceId",
                table: "CareRecipient");

            migrationBuilder.DropForeignKey(
                name: "FK_Job_RequiredService_RequiredServiceId",
                table: "Job");

            migrationBuilder.DropColumn(
                name: "ZipCode",
                table: "CareRecipient");

            migrationBuilder.AlterColumn<int>(
                name: "RequiredServiceId",
                table: "Job",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "RequiredServiceId",
                table: "CareRecipient",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CareRecipient_RequiredService_RequiredServiceId",
                table: "CareRecipient",
                column: "RequiredServiceId",
                principalTable: "RequiredService",
                principalColumn: "RequiredServiceId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Job_RequiredService_RequiredServiceId",
                table: "Job",
                column: "RequiredServiceId",
                principalTable: "RequiredService",
                principalColumn: "RequiredServiceId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CareRecipient_RequiredService_RequiredServiceId",
                table: "CareRecipient");

            migrationBuilder.DropForeignKey(
                name: "FK_Job_RequiredService_RequiredServiceId",
                table: "Job");

            migrationBuilder.AlterColumn<int>(
                name: "RequiredServiceId",
                table: "Job",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "RequiredServiceId",
                table: "CareRecipient",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "ZipCode",
                table: "CareRecipient",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CareRecipient_RequiredService_RequiredServiceId",
                table: "CareRecipient",
                column: "RequiredServiceId",
                principalTable: "RequiredService",
                principalColumn: "RequiredServiceId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Job_RequiredService_RequiredServiceId",
                table: "Job",
                column: "RequiredServiceId",
                principalTable: "RequiredService",
                principalColumn: "RequiredServiceId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
