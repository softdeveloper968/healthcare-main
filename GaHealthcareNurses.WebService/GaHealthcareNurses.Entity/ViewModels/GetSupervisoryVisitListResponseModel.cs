using System;
using System.Collections.Generic;
using System.Text;

namespace GaHealthcareNurses.Entity.ViewModels
{
    public class GetSupervisoryVisitListResponseModel
    {
        public int Id { get; set; }
        public string CareRecipient { get; set; }
        public string ServiceProvided { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string CareGiver { get; set; }
        public string Request { get; set; }
        public string Location { get; set; }
        public string Agency { get; set; }
    }
}
