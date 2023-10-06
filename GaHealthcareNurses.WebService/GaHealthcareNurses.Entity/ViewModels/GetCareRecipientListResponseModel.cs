using System;
using System.Collections.Generic;
using System.Text;

namespace GaHealthcareNurses.Entity.ViewModels
{
    public class GetCareRecipientListResponseModel
    {
        public int CareRecipientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string Status { get; set; }
        public string Visibility { get; set; }
        public int InitialAssessmentId { get; set; }
        public DateTime? InitialAssessmentDate { get; set; }
        public string AssignedCareGiver { get; set; }
    }
}
