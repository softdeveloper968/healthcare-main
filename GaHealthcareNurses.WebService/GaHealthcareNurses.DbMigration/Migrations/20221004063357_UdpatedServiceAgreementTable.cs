using Microsoft.EntityFrameworkCore.Migrations;

namespace GaHealthcareNurses.DbMigration.Migrations
{
    public partial class UdpatedServiceAgreementTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceAgreement_Employer_EmployerId1",
                table: "ServiceAgreement");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceAgreement_Job_JobId",
                table: "ServiceAgreement");

            migrationBuilder.DropIndex(
                name: "IX_ServiceAgreement_EmployerId1",
                table: "ServiceAgreement");

            migrationBuilder.DropColumn(
                name: "EmployerId1",
                table: "ServiceAgreement");

            migrationBuilder.AlterColumn<int>(
                name: "JobId",
                table: "ServiceAgreement",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EmployerId",
                table: "ServiceAgreement",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceAgreement_EmployerId",
                table: "ServiceAgreement",
                column: "EmployerId");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceAgreement_Employer_EmployerId",
                table: "ServiceAgreement",
                column: "EmployerId",
                principalTable: "Employer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceAgreement_Job_JobId",
                table: "ServiceAgreement",
                column: "JobId",
                principalTable: "Job",
                principalColumn: "JobId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceAgreement_Employer_EmployerId",
                table: "ServiceAgreement");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceAgreement_Job_JobId",
                table: "ServiceAgreement");

            migrationBuilder.DropIndex(
                name: "IX_ServiceAgreement_EmployerId",
                table: "ServiceAgreement");

            migrationBuilder.AlterColumn<int>(
                name: "JobId",
                table: "ServiceAgreement",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "EmployerId",
                table: "ServiceAgreement",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployerId1",
                table: "ServiceAgreement",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceAgreement_EmployerId1",
                table: "ServiceAgreement",
                column: "EmployerId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceAgreement_Employer_EmployerId1",
                table: "ServiceAgreement",
                column: "EmployerId1",
                principalTable: "Employer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceAgreement_Job_JobId",
                table: "ServiceAgreement",
                column: "JobId",
                principalTable: "Job",
                principalColumn: "JobId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
