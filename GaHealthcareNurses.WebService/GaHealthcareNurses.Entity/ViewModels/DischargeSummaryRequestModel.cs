using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GaHealthcareNurses.Entity.ViewModels
{
    public class DischargeSummaryRequestModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please select one option")]
        public string Type { get; set; }
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
        public bool Alert { get; set; }
        public bool Forgetful { get; set; }
        public bool Confused { get; set; }
        public bool Agitated { get; set; }
        public bool Oriented { get; set; }
        public bool Disoriented { get; set; }
        public bool Depressed { get; set; }
        public bool Other { get; set; }
        public bool Comatose { get; set; }
        public bool Independent { get; set; }
        public bool PartiallyDependent { get; set; }
        public bool TotallyDependent { get; set; }
        public bool DiscussionWithPatientOrFamily { get; set; }
        public bool CompletedViaTelephone { get; set; }
        public bool MutualAgreementForDischarge { get; set; }
        public string Goals { get; set; }
        public string Comments { get; set; }
        public string Interventions { get; set; }
        public string ResponseToInterventions { get; set; }
        public string PatientName { get; set; }
    }

    public class DischargeSummaryPDFBytes
    {
        public byte[] Bytes { get; set; }
    }
}
