using System;
using System.Collections.Generic;
using System.Text;

namespace GaHealthcareNurses.Entity.Models
{
   public class NurseCnaSkills
    {
        public int Id { get; set; }
        public string NurseId { get; set; }

        //Provides Basic age appropriate physical care and comfort measures and assistance with activities of daily living
        public string Baths { get; set; }
        public string Shampoo { get; set; }
        public string NailCare { get; set; }
        public string OralCare { get; set; }
        public string RoutineSkinCare { get; set; }
        public string Positioning { get; set; }
        public string BackRubs { get; set; }
        public string AmbulanceAssistance { get; set; }
        public string OralAndNascalCare { get; set; }
        public string RoutineCatheterCare { get; set; }

       //Obtains basic age appropriate measurements consistent with established policy/procedure and practice and practice standards for assigned
        public string TemperaturePulseAndRespirationMeasurements { get; set; }
        public string BloodPressureMeasurements { get; set; }
        public string MeasureHeightAndWeight { get; set; }
        public string AccurateMaintainIntakeAndOutputAndDocuments { get; set; }
        public string DistinguishesMeasurementReportedTo { get; set; }

        //Obtains basic age appropriate measurements consistent with established policy/procedure and practice and practice standards for assigned
        public string AssistWithMealsAndSnacks { get; set; }
        public string FeedsPatientWithNormalSwallowingAbilities { get; set; }
        public string FeedingTubesWithScopeOfCnaCertification { get; set; }

        //Assist in elimination support needs of assigned patients consistent with established policy policy/procedure
        public string AdministersEnemasInstructedBySupervisingNurse { get; set; }

        //Assist in providing pulmonary support of assigned patients consistent with established policy policy/procedure.
        public string AssistPatientPerformingTurnCoughAndDeepBreathingExercises { get; set; }
        public string PracticeOxygenSafety { get; set; }

        //Familiar with various assistive devices/techniques
        public string ProperBodyMechanics { get; set; }
        public string TransferFromBedToChair { get; set; }
        public string TransferFromChairToBed { get; set; }
        public string ProperUseOfHoyerLifts { get; set; }
        public string ProperUseOfSpecialtyBeds { get; set; }
        public string Other { get; set; }

        //Experience with Age Groups:
        public string AdaptCareToIncorporateNormalGrowthAndDevelopment { get; set; }
        public string AdaptMethodAndTerminologyOfPatients { get; set; }
        public string EnsureSafeEnvironmentReflectingSpecificNeeds { get; set; }

        //My experience is primarily in:
        public double? HomeCare { get; set; }
        public double? NursingHome { get; set; }
        public double? AssistedLiving { get; set; }
        public double? Medical { get; set; }
        public double? Surgical { get; set; }
        public double? Hospice { get; set; }
        public double? Alzheimers { get; set; }
        public double? Cancer { get; set; }
        public double? Dementia { get; set; }
        public double? Diabetes { get; set; }
        public double? Parkinsons { get; set; }
        public string CnaSignature { get; set; }
        public DateTime Date { get; set; }
        public virtual Nurse Nurse { get; set; }

    }
}
