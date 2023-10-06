using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GaHealthcareNurses.Entity.ViewModels
{
  public class AddTaskListViewModel
    {
        public AddTaskListViewModel()
        {
            TaskListAddTemplates = new List<TaskListAddTemplateViewModel>();
        }
        [Range(1, int.MaxValue, ErrorMessage = "Select a service request")]
        public int JobId { get; set; }
        public string EmployerId { get; set; }
        public List<TaskListAddTemplateViewModel> TaskListAddTemplates { get; set; }
   
    }
}
