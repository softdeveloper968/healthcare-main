using System;
using System.Collections.Generic;
using System.Text;

namespace GaHealthcareNurses.Entity.ViewModels
{
    public class ServiceAgreementListModel
    {
        public int ServiceAgreementId { get; set; }
        public string ServiceRequired { get; set; }
        public string CareRecipient { get; set; }
        public DateTime? CareStartsOn { get; set; }
        public string ServiceProvide { get; set; }
        public DateTime? SignedByClient { get; set; }
        public DateTime? SignedByAgency { get; set; }
    }
}
