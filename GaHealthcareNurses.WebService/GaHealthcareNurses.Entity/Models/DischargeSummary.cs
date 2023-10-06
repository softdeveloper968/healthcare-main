using System;
using System.Collections.Generic;
using System.Text;

namespace GaHealthcareNurses.Entity.Models
{
    public class DischargeSummary
    {
        public int Id { get; set; }
        public int JobId { get; set; }
        public string NurseId { get; set; }
        public string EmployerId { get; set; }
        public string Type { get; set; }
        //Reason
        public bool NoFurtherCareNeeded { get; set; }
        public bool MovedOutOfServiceArea { get; set; }
        public bool TransferredToAnotherAgency { get; set; }
        public bool PhysicianRequest { get; set; }
        public bool FamilyAssumeResponsibility { get; set; }
        public bool AdmittedToHospital { get; set; }
        public bool Death { get; set; }
        public bool OtherReason { get; set; }
        public bool PatientOrFamilyRefusedServices { get; set; }
        public bool AdmittedToSkilledNursingFacility { get; set; }
        //Mental Status
        public bool Alert { get; set; }
        public bool Forgetful { get; set; }
        public bool Confused { get; set; }
        public bool Agitated { get; set; }
        public bool Oriented { get; set; }
        public bool Disoriented { get; set; }
        public bool Depressed { get; set; }
        public bool Other { get; set; }
        public bool Comatose { get; set; }
        //Functional Ability
        public bool Independent { get; set; }
        public bool PartiallyDependent { get; set; }
        public bool TotallyDependent { get; set; }
        //Comments/Recommendations
        public bool DiscussionWithPatientOrFamily { get; set; }
        public bool CompletedViaTelephone { get; set; }
        public bool MutualAgreementForDischarge { get; set; }
        public string Goals { get; set; }
        public string Comments { get; set; }
        public string Interventions { get; set; }
        public string ResponseToInterventions { get; set; }
        public int Status { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateCompleted { get; set; }
        public string RejectionReason { get; set; }
        public virtual Job Job { get; set; }
        public virtual Nurse Nurse { get; set; }
        public virtual Employer Employer { get; set; }

    }
}
