using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GaHealthcareNurses.DbMigration.Migrations
{
    public partial class AddedDischargeSummaryTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DischargeSummary",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobId = table.Column<int>(type: "int", nullable: false),
                    NurseId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    EmployerId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoFurtherCareNeeded = table.Column<bool>(type: "bit", nullable: false),
                    MovedOutOfServiceArea = table.Column<bool>(type: "bit", nullable: false),
                    TransferredToAnotherAgency = table.Column<bool>(type: "bit", nullable: false),
                    PhysicianRequest = table.Column<bool>(type: "bit", nullable: false),
                    FamilyAssumeResponsibility = table.Column<bool>(type: "bit", nullable: false),
                    AdmittedToHospital = table.Column<bool>(type: "bit", nullable: false),
                    Death = table.Column<bool>(type: "bit", nullable: false),
                    OtherReason = table.Column<bool>(type: "bit", nullable: false),
                    PatientOrFamilyRefusedServices = table.Column<bool>(type: "bit", nullable: false),
                    AdmittedToSkilledNursingFacility = table.Column<bool>(type: "bit", nullable: false),
                    Alert = table.Column<bool>(type: "bit", nullable: false),
                    Forgetful = table.Column<bool>(type: "bit", nullable: false),
                    Confused = table.Column<bool>(type: "bit", nullable: false),
                    Agitated = table.Column<bool>(type: "bit", nullable: false),
                    Oriented = table.Column<bool>(type: "bit", nullable: false),
                    Disoriented = table.Column<bool>(type: "bit", nullable: false),
                    Depressed = table.Column<bool>(type: "bit", nullable: false),
                    Other = table.Column<bool>(type: "bit", nullable: false),
                    Comatose = table.Column<bool>(type: "bit", nullable: false),
                    Independent = table.Column<bool>(type: "bit", nullable: false),
                    PartiallyDependent = table.Column<bool>(type: "bit", nullable: false),
                    TotallyDependent = table.Column<bool>(type: "bit", nullable: false),
                    DiscussionWithPatientOrFamily = table.Column<bool>(type: "bit", nullable: false),
                    CompletedViaTelephone = table.Column<bool>(type: "bit", nullable: false),
                    MutualAgreementForDischarge = table.Column<bool>(type: "bit", nullable: false),
                    Goals = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Interventions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResponseToInterventions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateCompleted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RejectionReason = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DischargeSummary", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DischargeSummary_Employer_EmployerId",
                        column: x => x.EmployerId,
                        principalTable: "Employer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DischargeSummary_Job_JobId",
                        column: x => x.JobId,
                        principalTable: "Job",
                        principalColumn: "JobId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DischargeSummary_Nurse_NurseId",
                        column: x => x.NurseId,
                        principalTable: "Nurse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DischargeSummary_EmployerId",
                table: "DischargeSummary",
                column: "EmployerId");

            migrationBuilder.CreateIndex(
                name: "IX_DischargeSummary_JobId",
                table: "DischargeSummary",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_DischargeSummary_NurseId",
                table: "DischargeSummary",
                column: "NurseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DischargeSummary");
        }
    }
}
