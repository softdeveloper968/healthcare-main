using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GaHealthcareNurses.DbMigration.Migrations
{
    public partial class TaskLIstTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskList_VisitNote_VisitNoteId",
                table: "TaskList");

            migrationBuilder.DropColumn(
                name: "BloodPressureNotes",
                table: "TaskList");

            migrationBuilder.DropColumn(
                name: "IsBloodPressureChecked",
                table: "TaskList");

            migrationBuilder.DropColumn(
                name: "IsPainRatingChecked",
                table: "TaskList");

            migrationBuilder.DropColumn(
                name: "IsPulseChecked",
                table: "TaskList");

            migrationBuilder.DropColumn(
                name: "IsTemperatureChecked",
                table: "TaskList");

            migrationBuilder.DropColumn(
                name: "PainRatingNotes",
                table: "TaskList");

            migrationBuilder.RenameColumn(
                name: "WeightNotes",
                table: "TaskList",
                newName: "Verified");

            migrationBuilder.RenameColumn(
                name: "TemperatureNotes",
                table: "TaskList",
                newName: "TaskName");

            migrationBuilder.RenameColumn(
                name: "PulseNotes",
                table: "TaskList",
                newName: "TaskDescription");

            migrationBuilder.RenameColumn(
                name: "IsWeightChecked",
                table: "TaskList",
                newName: "TaskStatus");

            migrationBuilder.AlterColumn<int>(
                name: "VisitNoteId",
                table: "TaskList",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "TaskList",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "EmployerId",
                table: "TaskList",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "JobId",
                table: "TaskList",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "NurseId",
                table: "TaskList",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaskList_EmployerId",
                table: "TaskList",
                column: "EmployerId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskList_JobId",
                table: "TaskList",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskList_NurseId",
                table: "TaskList",
                column: "NurseId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskList_Employer_EmployerId",
                table: "TaskList",
                column: "EmployerId",
                principalTable: "Employer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskList_Job_JobId",
                table: "TaskList",
                column: "JobId",
                principalTable: "Job",
                principalColumn: "JobId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskList_Nurse_NurseId",
                table: "TaskList",
                column: "NurseId",
                principalTable: "Nurse",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskList_VisitNote_VisitNoteId",
                table: "TaskList",
                column: "VisitNoteId",
                principalTable: "VisitNote",
                principalColumn: "VisitNoteId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskList_Employer_EmployerId",
                table: "TaskList");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskList_Job_JobId",
                table: "TaskList");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskList_Nurse_NurseId",
                table: "TaskList");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskList_VisitNote_VisitNoteId",
                table: "TaskList");

            migrationBuilder.DropIndex(
                name: "IX_TaskList_EmployerId",
                table: "TaskList");

            migrationBuilder.DropIndex(
                name: "IX_TaskList_JobId",
                table: "TaskList");

            migrationBuilder.DropIndex(
                name: "IX_TaskList_NurseId",
                table: "TaskList");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "TaskList");

            migrationBuilder.DropColumn(
                name: "EmployerId",
                table: "TaskList");

            migrationBuilder.DropColumn(
                name: "JobId",
                table: "TaskList");

            migrationBuilder.DropColumn(
                name: "NurseId",
                table: "TaskList");

            migrationBuilder.RenameColumn(
                name: "Verified",
                table: "TaskList",
                newName: "WeightNotes");

            migrationBuilder.RenameColumn(
                name: "TaskStatus",
                table: "TaskList",
                newName: "IsWeightChecked");

            migrationBuilder.RenameColumn(
                name: "TaskName",
                table: "TaskList",
                newName: "TemperatureNotes");

            migrationBuilder.RenameColumn(
                name: "TaskDescription",
                table: "TaskList",
                newName: "PulseNotes");

            migrationBuilder.AlterColumn<int>(
                name: "VisitNoteId",
                table: "TaskList",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BloodPressureNotes",
                table: "TaskList",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsBloodPressureChecked",
                table: "TaskList",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPainRatingChecked",
                table: "TaskList",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPulseChecked",
                table: "TaskList",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsTemperatureChecked",
                table: "TaskList",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PainRatingNotes",
                table: "TaskList",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskList_VisitNote_VisitNoteId",
                table: "TaskList",
                column: "VisitNoteId",
                principalTable: "VisitNote",
                principalColumn: "VisitNoteId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
