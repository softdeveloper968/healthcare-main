using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GaHealthcareNurses.DbMigration.Migrations
{
    public partial class UpdatedAssessmetAndWoundTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PtInrIsFingerStick",
                table: "AdultAssessment",
                newName: "Urostomy");

            migrationBuilder.RenameColumn(
                name: "PatientOtherEL",
                table: "AdultAssessment",
                newName: "WhoAssistPatient");

            migrationBuilder.RenameColumn(
                name: "ObservedEvidence",
                table: "AdultAssessment",
                newName: "RightCalf");

            migrationBuilder.RenameColumn(
                name: "IsStrokeDeficits",
                table: "AdultAssessment",
                newName: "IsVomiting");

            migrationBuilder.RenameColumn(
                name: "IsPatientOtherEL",
                table: "AdultAssessment",
                newName: "IsVisionMacularDegen");

            migrationBuilder.RenameColumn(
                name: "IsPatientAngryEL",
                table: "AdultAssessment",
                newName: "IsVisionLegallyBlind");

            migrationBuilder.RenameColumn(
                name: "IsENTMNasalCongestionR",
                table: "AdultAssessment",
                newName: "IsVisionGlaucoma");

            migrationBuilder.RenameColumn(
                name: "IsENTMNasalCongestionL",
                table: "AdultAssessment",
                newName: "IsVisionContacts");

            migrationBuilder.RenameColumn(
                name: "IsAbuseVerbalEmotional",
                table: "AdultAssessment",
                newName: "IsVisionCataracts");

            migrationBuilder.RenameColumn(
                name: "IsAbusePotential",
                table: "AdultAssessment",
                newName: "IsVisionBlurredR");

            migrationBuilder.RenameColumn(
                name: "IsAbusePhysical",
                table: "AdultAssessment",
                newName: "IsVisionBlurredL");

            migrationBuilder.RenameColumn(
                name: "IsAbuseFinancial",
                table: "AdultAssessment",
                newName: "IsVisionBlurred");

            migrationBuilder.RenameColumn(
                name: "IsAbuseActual",
                table: "AdultAssessment",
                newName: "IsVertigo");

            migrationBuilder.AlterColumn<string>(
                name: "STIsOther",
                table: "Wound",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<string>(
                name: "BedTissue",
                table: "Wound",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsSTIsOther",
                table: "Wound",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "SorroundingTissue",
                table: "Wound",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Square",
                table: "Wound",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AlterColumn<string>(
                name: "WongBakerScale",
                table: "AdultAssessment",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<string>(
                name: "SuppliesIssued",
                table: "AdultAssessment",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "PtInrIsVenipuncture",
                table: "AdultAssessment",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "NutritionalScreeningTotal",
                table: "AdultAssessment",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "NextPhysicianVisitDate",
                table: "AdultAssessment",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IsObstaclesWithNew",
                table: "AdultAssessment",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "IsNeuroLocation",
                table: "AdultAssessment",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "IsMedicationManaged",
                table: "AdultAssessment",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "HasPatientAllMedications",
                table: "AdultAssessment",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<string>(
                name: "AbdGirth",
                table: "AdultAssessment",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AssistPatient",
                table: "AdultAssessment",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Capillary",
                table: "AdultAssessment",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ChangeInMood",
                table: "AdultAssessment",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DescribeReported",
                table: "AdultAssessment",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EnteralFeeding",
                table: "AdultAssessment",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EvidenceActual",
                table: "AdultAssessment",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "EvidenceEmotiona",
                table: "AdultAssessment",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "EvidenceFinancial",
                table: "AdultAssessment",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "EvidencePhysical",
                table: "AdultAssessment",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "EvidencePotential",
                table: "AdultAssessment",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Evidenceobserved",
                table: "AdultAssessment",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "GenitourinaryOther",
                table: "AdultAssessment",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasHistoryOfOsteoarthritis",
                table: "AdultAssessment",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasHistoryOfRotatorCuffTear",
                table: "AdultAssessment",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "HasPatientAllMedicationsNo",
                table: "AdultAssessment",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HistoryCommentery",
                table: "AdultAssessment",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HistoryOther",
                table: "AdultAssessment",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HypoOnSupplementalTreatment",
                table: "AdultAssessment",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Inches",
                table: "AdultAssessment",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IntegumentarySkin",
                table: "AdultAssessment",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsAbdGirth",
                table: "AdultAssessment",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsAbdominalDistention",
                table: "AdultAssessment",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsAbdominalPain",
                table: "AdultAssessment",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsAbnormalHeartSounds",
                table: "AdultAssessment",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsArrhythmia",
                table: "AdultAssessment",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsCapillary",
                table: "AdultAssessment",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsChestPain",
                table: "AdultAssessment",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsChronic",
                table: "AdultAssessment",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsColostomy",
                table: "AdultAssessment",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsCompression",
                table: "AdultAssessment",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsConstipation",
                table: "AdultAssessment",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDiaphoresis",
                table: "AdultAssessment",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDiarrhea",
                table: "AdultAssessment",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsENTMEarPain",
                table: "AdultAssessment",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsENTMHearingAids",
                table: "AdultAssessment",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsENTMHearingLoss",
                table: "AdultAssessment",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsENTMNasalCongestion",
                table: "AdultAssessment",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsEdema",
                table: "AdultAssessment",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsEdemaDependent",
                table: "AdultAssessment",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsEdemaL",
                table: "AdultAssessment",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsEdemaPedal",
                table: "AdultAssessment",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsEdemaR",
                table: "AdultAssessment",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsEnteralFeeding",
                table: "AdultAssessment",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFatigues",
                table: "AdultAssessment",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFrequency",
                table: "AdultAssessment",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFrequent",
                table: "AdultAssessment",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsGenitourinaryNoHistory",
                table: "AdultAssessment",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsGenitourinaryOther",
                table: "AdultAssessment",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsHistoryOther",
                table: "AdultAssessment",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsIleostomy",
                table: "AdultAssessment",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsIndigestion",
                table: "AdultAssessment",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsInfusionNA",
                table: "AdultAssessment",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsIntegumentaryColorOther",
                table: "AdultAssessment",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsKidneyStones",
                table: "AdultAssessment",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsLastBmIncontinence",
                table: "AdultAssessment",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsLastOther",
                table: "AdultAssessment",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsLeftAnkle",
                table: "AdultAssessment",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsLeftCalf",
                table: "AdultAssessment",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsMoist",
                table: "AdultAssessment",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsNausea",
                table: "AdultAssessment",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsNeckVein",
                table: "AdultAssessment",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsNeedsAssistance",
                table: "AdultAssessment",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsNormalHeartSounds",
                table: "AdultAssessment",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsOstomy",
                table: "AdultAssessment",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPaatientAngry",
                table: "AdultAssessment",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPatientOthers",
                table: "AdultAssessment",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPedalL",
                table: "AdultAssessment",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPedalPulses",
                table: "AdultAssessment",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPedalR",
                table: "AdultAssessment",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPink",
                table: "AdultAssessment",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPlacement",
                table: "AdultAssessment",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPolycysticDisease",
                table: "AdultAssessment",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsRed",
                table: "AdultAssessment",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsRenalDisease",
                table: "AdultAssessment",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsResidualAmount",
                table: "AdultAssessment",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsRightAnkle",
                table: "AdultAssessment",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsRightCalf",
                table: "AdultAssessment",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsSelfCare",
                table: "AdultAssessment",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsStoma",
                table: "AdultAssessment",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUrgency",
                table: "AdultAssessment",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastOther",
                table: "AdultAssessment",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LeftAnkle",
                table: "AdultAssessment",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LeftCalf",
                table: "AdultAssessment",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ostomy",
                table: "AdultAssessment",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PatientOthers",
                table: "AdultAssessment",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ResidualAmount",
                table: "AdultAssessment",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RightAnkle",
                table: "AdultAssessment",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BedTissue",
                table: "Wound");

            migrationBuilder.DropColumn(
                name: "IsSTIsOther",
                table: "Wound");

            migrationBuilder.DropColumn(
                name: "SorroundingTissue",
                table: "Wound");

            migrationBuilder.DropColumn(
                name: "Square",
                table: "Wound");

            migrationBuilder.DropColumn(
                name: "AbdGirth",
                table: "AdultAssessment");

            migrationBuilder.DropColumn(
                name: "AssistPatient",
                table: "AdultAssessment");

            migrationBuilder.DropColumn(
                name: "Capillary",
                table: "AdultAssessment");

            migrationBuilder.DropColumn(
                name: "ChangeInMood",
                table: "AdultAssessment");

            migrationBuilder.DropColumn(
                name: "DescribeReported",
                table: "AdultAssessment");

            migrationBuilder.DropColumn(
                name: "EnteralFeeding",
                table: "AdultAssessment");

            migrationBuilder.DropColumn(
                name: "EvidenceActual",
                table: "AdultAssessment");

            migrationBuilder.DropColumn(
                name: "EvidenceEmotiona",
                table: "AdultAssessment");

            migrationBuilder.DropColumn(
                name: "EvidenceFinancial",
                table: "AdultAssessment");

            migrationBuilder.DropColumn(
                name: "EvidencePhysical",
                table: "AdultAssessment");

            migrationBuilder.DropColumn(
                name: "EvidencePotential",
                table: "AdultAssessment");

            migrationBuilder.DropColumn(
                name: "Evidenceobserved",
                table: "AdultAssessment");

            migrationBuilder.DropColumn(
                name: "GenitourinaryOther",
                table: "AdultAssessment");

            migrationBuilder.DropColumn(
                name: "HasHistoryOfOsteoarthritis",
                table: "AdultAssessment");

            migrationBuilder.DropColumn(
                name: "HasHistoryOfRotatorCuffTear",
                table: "AdultAssessment");

            migrationBuilder.DropColumn(
                name: "HasPatientAllMedicationsNo",
                table: "AdultAssessment");

            migrationBuilder.DropColumn(
                name: "HistoryCommentery",
                table: "AdultAssessment");

            migrationBuilder.DropColumn(
                name: "HistoryOther",
                table: "AdultAssessment");

            migrationBuilder.DropColumn(
                name: "HypoOnSupplementalTreatment",
                table: "AdultAssessment");

            migrationBuilder.DropColumn(
                name: "Inches",
                table: "AdultAssessment");

            migrationBuilder.DropColumn(
                name: "IntegumentarySkin",
                table: "AdultAssessment");

            migrationBuilder.DropColumn(
                name: "IsAbdGirth",
                table: "AdultAssessment");

            migrationBuilder.DropColumn(
                name: "IsAbdominalDistention",
                table: "AdultAssessment");

            migrationBuilder.DropColumn(
                name: "IsAbdominalPain",
                table: "AdultAssessment");

            migrationBuilder.DropColumn(
                name: "IsAbnormalHeartSounds",
                table: "AdultAssessment");

            migrationBuilder.DropColumn(
                name: "IsArrhythmia",
                table: "AdultAssessment");

            migrationBuilder.DropColumn(
                name: "IsCapillary",
                table: "AdultAssessment");

            migrationBuilder.DropColumn(
                name: "IsChestPain",
                table: "AdultAssessment");

            migrationBuilder.DropColumn(
                name: "IsChronic",
                table: "AdultAssessment");

            migrationBuilder.DropColumn(
                name: "IsColostomy",
                table: "AdultAssessment");

            migrationBuilder.DropColumn(
                name: "IsCompression",
                table: "AdultAssessment");

            migrationBuilder.DropColumn(
                name: "IsConstipation",
                table: "AdultAssessment");

            migrationBuilder.DropColumn(
                name: "IsDiaphoresis",
                table: "AdultAssessment");

            migrationBuilder.DropColumn(
                name: "IsDiarrhea",
                table: "AdultAssessment");

            migrationBuilder.DropColumn(
                name: "IsENTMEarPain",
                table: "AdultAssessment");

            migrationBuilder.DropColumn(
                name: "IsENTMHearingAids",
                table: "AdultAssessment");

            migrationBuilder.DropColumn(
                name: "IsENTMHearingLoss",
                table: "AdultAssessment");

            migrationBuilder.DropColumn(
                name: "IsENTMNasalCongestion",
                table: "AdultAssessment");

            migrationBuilder.DropColumn(
                name: "IsEdema",
                table: "AdultAssessment");

            migrationBuilder.DropColumn(
                name: "IsEdemaDependent",
                table: "AdultAssessment");

            migrationBuilder.DropColumn(
                name: "IsEdemaL",
                table: "AdultAssessment");

            migrationBuilder.DropColumn(
                name: "IsEdemaPedal",
                table: "AdultAssessment");

            migrationBuilder.DropColumn(
                name: "IsEdemaR",
                table: "AdultAssessment");

            migrationBuilder.DropColumn(
                name: "IsEnteralFeeding",
                table: "AdultAssessment");

            migrationBuilder.DropColumn(
                name: "IsFatigues",
                table: "AdultAssessment");

            migrationBuilder.DropColumn(
                name: "IsFrequency",
                table: "AdultAssessment");

            migrationBuilder.DropColumn(
                name: "IsFrequent",
                table: "AdultAssessment");

            migrationBuilder.DropColumn(
                name: "IsGenitourinaryNoHistory",
                table: "AdultAssessment");

            migrationBuilder.DropColumn(
                name: "IsGenitourinaryOther",
                table: "AdultAssessment");

            migrationBuilder.DropColumn(
                name: "IsHistoryOther",
                table: "AdultAssessment");

            migrationBuilder.DropColumn(
                name: "IsIleostomy",
                table: "AdultAssessment");

            migrationBuilder.DropColumn(
                name: "IsIndigestion",
                table: "AdultAssessment");

            migrationBuilder.DropColumn(
                name: "IsInfusionNA",
                table: "AdultAssessment");

            migrationBuilder.DropColumn(
                name: "IsIntegumentaryColorOther",
                table: "AdultAssessment");

            migrationBuilder.DropColumn(
                name: "IsKidneyStones",
                table: "AdultAssessment");

            migrationBuilder.DropColumn(
                name: "IsLastBmIncontinence",
                table: "AdultAssessment");

            migrationBuilder.DropColumn(
                name: "IsLastOther",
                table: "AdultAssessment");

            migrationBuilder.DropColumn(
                name: "IsLeftAnkle",
                table: "AdultAssessment");

            migrationBuilder.DropColumn(
                name: "IsLeftCalf",
                table: "AdultAssessment");

            migrationBuilder.DropColumn(
                name: "IsMoist",
                table: "AdultAssessment");

            migrationBuilder.DropColumn(
                name: "IsNausea",
                table: "AdultAssessment");

            migrationBuilder.DropColumn(
                name: "IsNeckVein",
                table: "AdultAssessment");

            migrationBuilder.DropColumn(
                name: "IsNeedsAssistance",
                table: "AdultAssessment");

            migrationBuilder.DropColumn(
                name: "IsNormalHeartSounds",
                table: "AdultAssessment");

            migrationBuilder.DropColumn(
                name: "IsOstomy",
                table: "AdultAssessment");

            migrationBuilder.DropColumn(
                name: "IsPaatientAngry",
                table: "AdultAssessment");

            migrationBuilder.DropColumn(
                name: "IsPatientOthers",
                table: "AdultAssessment");

            migrationBuilder.DropColumn(
                name: "IsPedalL",
                table: "AdultAssessment");

            migrationBuilder.DropColumn(
                name: "IsPedalPulses",
                table: "AdultAssessment");

            migrationBuilder.DropColumn(
                name: "IsPedalR",
                table: "AdultAssessment");

            migrationBuilder.DropColumn(
                name: "IsPink",
                table: "AdultAssessment");

            migrationBuilder.DropColumn(
                name: "IsPlacement",
                table: "AdultAssessment");

            migrationBuilder.DropColumn(
                name: "IsPolycysticDisease",
                table: "AdultAssessment");

            migrationBuilder.DropColumn(
                name: "IsRed",
                table: "AdultAssessment");

            migrationBuilder.DropColumn(
                name: "IsRenalDisease",
                table: "AdultAssessment");

            migrationBuilder.DropColumn(
                name: "IsResidualAmount",
                table: "AdultAssessment");

            migrationBuilder.DropColumn(
                name: "IsRightAnkle",
                table: "AdultAssessment");

            migrationBuilder.DropColumn(
                name: "IsRightCalf",
                table: "AdultAssessment");

            migrationBuilder.DropColumn(
                name: "IsSelfCare",
                table: "AdultAssessment");

            migrationBuilder.DropColumn(
                name: "IsStoma",
                table: "AdultAssessment");

            migrationBuilder.DropColumn(
                name: "IsUrgency",
                table: "AdultAssessment");

            migrationBuilder.DropColumn(
                name: "LastOther",
                table: "AdultAssessment");

            migrationBuilder.DropColumn(
                name: "LeftAnkle",
                table: "AdultAssessment");

            migrationBuilder.DropColumn(
                name: "LeftCalf",
                table: "AdultAssessment");

            migrationBuilder.DropColumn(
                name: "Ostomy",
                table: "AdultAssessment");

            migrationBuilder.DropColumn(
                name: "PatientOthers",
                table: "AdultAssessment");

            migrationBuilder.DropColumn(
                name: "ResidualAmount",
                table: "AdultAssessment");

            migrationBuilder.DropColumn(
                name: "RightAnkle",
                table: "AdultAssessment");

            migrationBuilder.RenameColumn(
                name: "WhoAssistPatient",
                table: "AdultAssessment",
                newName: "PatientOtherEL");

            migrationBuilder.RenameColumn(
                name: "Urostomy",
                table: "AdultAssessment",
                newName: "PtInrIsFingerStick");

            migrationBuilder.RenameColumn(
                name: "RightCalf",
                table: "AdultAssessment",
                newName: "ObservedEvidence");

            migrationBuilder.RenameColumn(
                name: "IsVomiting",
                table: "AdultAssessment",
                newName: "IsStrokeDeficits");

            migrationBuilder.RenameColumn(
                name: "IsVisionMacularDegen",
                table: "AdultAssessment",
                newName: "IsPatientOtherEL");

            migrationBuilder.RenameColumn(
                name: "IsVisionLegallyBlind",
                table: "AdultAssessment",
                newName: "IsPatientAngryEL");

            migrationBuilder.RenameColumn(
                name: "IsVisionGlaucoma",
                table: "AdultAssessment",
                newName: "IsENTMNasalCongestionR");

            migrationBuilder.RenameColumn(
                name: "IsVisionContacts",
                table: "AdultAssessment",
                newName: "IsENTMNasalCongestionL");

            migrationBuilder.RenameColumn(
                name: "IsVisionCataracts",
                table: "AdultAssessment",
                newName: "IsAbuseVerbalEmotional");

            migrationBuilder.RenameColumn(
                name: "IsVisionBlurredR",
                table: "AdultAssessment",
                newName: "IsAbusePotential");

            migrationBuilder.RenameColumn(
                name: "IsVisionBlurredL",
                table: "AdultAssessment",
                newName: "IsAbusePhysical");

            migrationBuilder.RenameColumn(
                name: "IsVisionBlurred",
                table: "AdultAssessment",
                newName: "IsAbuseFinancial");

            migrationBuilder.RenameColumn(
                name: "IsVertigo",
                table: "AdultAssessment",
                newName: "IsAbuseActual");

            migrationBuilder.AlterColumn<bool>(
                name: "STIsOther",
                table: "Wound",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "WongBakerScale",
                table: "AdultAssessment",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "SuppliesIssued",
                table: "AdultAssessment",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "PtInrIsVenipuncture",
                table: "AdultAssessment",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "NutritionalScreeningTotal",
                table: "AdultAssessment",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "NextPhysicianVisitDate",
                table: "AdultAssessment",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsObstaclesWithNew",
                table: "AdultAssessment",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsNeuroLocation",
                table: "AdultAssessment",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsMedicationManaged",
                table: "AdultAssessment",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "HasPatientAllMedications",
                table: "AdultAssessment",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
