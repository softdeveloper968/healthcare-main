using System;
using System.Collections.Generic;
using System.Text;

namespace GaHealthcareNurses.Entity.Models
{
   public class TaskListCalenderResponseModel
    {
        public List<TaskListCalender> Tasks { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
}
