using System;
using System.Collections.Generic;
using System.Text;

namespace GaHealthcareNurses.Entity.ViewModels
{
    public class GetTaskListResponseModel
    {
        public int JobId { get; set; }
        public string TaskName { get; set; }
        public DateTime Date { get; set; }
        public bool TaskStatus { get; set; }
    }
}
