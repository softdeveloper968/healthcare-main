using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GaHealthcareNurses.DbMigration.Migrations
{
    public partial class MergePatientIntoCareRecipient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CareCoordinationNote_Patient_PatientId",
                table: "CareCoordinationNote");

            migrationBuilder.DropForeignKey(
                name: "FK_VisitNote_Patient_PatientId",
                table: "VisitNote");

            migrationBuilder.DropTable(
                name: "Patient");

            migrationBuilder.DropIndex(
                name: "IX_VisitNote_PatientId",
                table: "VisitNote");

            migrationBuilder.DropColumn(
                name: "PatientId",
                table: "VisitNote");

            migrationBuilder.RenameColumn(
                name: "PatientId",
                table: "CareCoordinationNote",
                newName: "CareRecipientId");

            migrationBuilder.RenameIndex(
                name: "IX_CareCoordinationNote_PatientId",
                table: "CareCoordinationNote",
                newName: "IX_CareCoordinationNote_CareRecipientId");

            migrationBuilder.AddColumn<int>(
                name: "CareRecipientId",
                table: "VisitNote",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AddressLine1",
                table: "CareRecipient",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AddressLine2",
                table: "CareRecipient",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Alert",
                table: "CareRecipient",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Allergies",
                table: "CareRecipient",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CodeStatus",
                table: "CareRecipient",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Diagnosis",
                table: "CareRecipient",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Forgetful",
                table: "CareRecipient",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ZipCode",
                table: "CareRecipient",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VisitNote_CareRecipientId",
                table: "VisitNote",
                column: "CareRecipientId");

            migrationBuilder.AddForeignKey(
                name: "FK_CareCoordinationNote_CareRecipient_CareRecipientId",
                table: "CareCoordinationNote",
                column: "CareRecipientId",
                principalTable: "CareRecipient",
                principalColumn: "CareRecipientId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VisitNote_CareRecipient_CareRecipientId",
                table: "VisitNote",
                column: "CareRecipientId",
                principalTable: "CareRecipient",
                principalColumn: "CareRecipientId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CareCoordinationNote_CareRecipient_CareRecipientId",
                table: "CareCoordinationNote");

            migrationBuilder.DropForeignKey(
                name: "FK_VisitNote_CareRecipient_CareRecipientId",
                table: "VisitNote");

            migrationBuilder.DropIndex(
                name: "IX_VisitNote_CareRecipientId",
                table: "VisitNote");

            migrationBuilder.DropColumn(
                name: "CareRecipientId",
                table: "VisitNote");

            migrationBuilder.DropColumn(
                name: "AddressLine1",
                table: "CareRecipient");

            migrationBuilder.DropColumn(
                name: "AddressLine2",
                table: "CareRecipient");

            migrationBuilder.DropColumn(
                name: "Alert",
                table: "CareRecipient");

            migrationBuilder.DropColumn(
                name: "Allergies",
                table: "CareRecipient");

            migrationBuilder.DropColumn(
                name: "CodeStatus",
                table: "CareRecipient");

            migrationBuilder.DropColumn(
                name: "Diagnosis",
                table: "CareRecipient");

            migrationBuilder.DropColumn(
                name: "Forgetful",
                table: "CareRecipient");

            migrationBuilder.DropColumn(
                name: "ZipCode",
                table: "CareRecipient");

            migrationBuilder.RenameColumn(
                name: "CareRecipientId",
                table: "CareCoordinationNote",
                newName: "PatientId");

            migrationBuilder.RenameIndex(
                name: "IX_CareCoordinationNote_CareRecipientId",
                table: "CareCoordinationNote",
                newName: "IX_CareCoordinationNote_PatientId");

            migrationBuilder.AddColumn<int>(
                name: "PatientId",
                table: "VisitNote",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Patient",
                columns: table => new
                {
                    PatientId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AddressLine1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressLine2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Alert = table.Column<bool>(type: "bit", nullable: false),
                    Allergies = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CityId = table.Column<int>(type: "int", nullable: true),
                    CodeStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Diagnosis = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Forgetful = table.Column<bool>(type: "bit", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StateId = table.Column<int>(type: "int", nullable: true),
                    ZipCode = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patient", x => x.PatientId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VisitNote_PatientId",
                table: "VisitNote",
                column: "PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_CareCoordinationNote_Patient_PatientId",
                table: "CareCoordinationNote",
                column: "PatientId",
                principalTable: "Patient",
                principalColumn: "PatientId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VisitNote_Patient_PatientId",
                table: "VisitNote",
                column: "PatientId",
                principalTable: "Patient",
                principalColumn: "PatientId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
