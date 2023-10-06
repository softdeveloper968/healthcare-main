using GaHealthcareNurses.Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GaHealthcareNurses.Entity.ViewModels
{
    public class GetActiveServiceRequestsResponseModel
    {
        public int JobId { get; set; }
        public string ServiceRequest { get; set; }
        public string CareRecipientName { get; set; }
        public string CityName { get; set; }
        public List<GetTaskListResponseModel> TaskList { get; set; }
    }
}
