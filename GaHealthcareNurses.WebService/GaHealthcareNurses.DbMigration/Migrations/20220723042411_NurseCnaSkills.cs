using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GaHealthcareNurses.DbMigration.Migrations
{
    public partial class NurseCnaSkills : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NurseCnaSkills",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NurseId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Baths = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Shampoo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NailCare = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OralCare = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoutineSkinCare = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Positioning = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BackRubs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AmbulanceAssistance = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OralAndNascalCare = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoutineCatheterCare = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TemperaturePulseAndRespirationMeasurements = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BloodPressureMeasurements = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MeasureHeightAndWeight = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccurateMaintainIntakeAndOutputAndDocuments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DistinguishesMeasurementReportedTo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AssistWithMealsAndSnacks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FeedsPatientWithNormalSwallowingAbilities = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FeedingTubesWithScopeOfCnaCertification = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdministersEnemasInstructedBySupervisingNurse = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AssistPatientPerformingTurnCoughAndDeepBreathingExercises = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PracticeOxygenSafety = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProperBodyMechanics = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransferFromBedToChair = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransferFromChairToBed = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProperUseOfHoyerLifts = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProperUseOfSpecialtyBeds = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Other = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdaptCareToIncorporateNormalGrowthAndDevelopment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdaptMethodAndTerminologyOfPatients = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EnsureSafeEnvironmentReflectingSpecificNeeds = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HomeCare = table.Column<double>(type: "float", nullable: true),
                    NursingHome = table.Column<double>(type: "float", nullable: true),
                    AssistedLiving = table.Column<double>(type: "float", nullable: true),
                    Medical = table.Column<double>(type: "float", nullable: true),
                    Surgical = table.Column<double>(type: "float", nullable: true),
                    Hospice = table.Column<double>(type: "float", nullable: true),
                    Alzheimers = table.Column<double>(type: "float", nullable: true),
                    Cancer = table.Column<double>(type: "float", nullable: true),
                    Dementia = table.Column<double>(type: "float", nullable: true),
                    Diabetes = table.Column<double>(type: "float", nullable: true),
                    Parkinsons = table.Column<double>(type: "float", nullable: true),
                    CnaSignature = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NurseCnaSkills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NurseCnaSkills_Nurse_NurseId",
                        column: x => x.NurseId,
                        principalTable: "Nurse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NurseCnaSkills_NurseId",
                table: "NurseCnaSkills",
                column: "NurseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NurseCnaSkills");
        }
    }
}
