using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GaHealthcareNurses.Entity.ViewModels
{
   public class EducationRequestViewModel
    {
        [RegularExpression(@"^(?<firstchar>(?=[A-Za-z]))((?<alphachars>[A-Za-z])|(?<specialchars>[A-Za-z]['-](?=[A-Za-z]))|(?<spaces> (?=[A-Za-z])))*$", ErrorMessage = "Enter valid first name")]
        [StringLength(32, ErrorMessage = "First name cannot be longer than 32 characters.")]
        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }

        [RegularExpression(@"^(?<firstchar>(?=[A-Za-z]))((?<alphachars>[A-Za-z])|(?<specialchars>[A-Za-z]['-](?=[A-Za-z]))|(?<spaces> (?=[A-Za-z])))*$", ErrorMessage = "Enter valid lastt name")]
        [StringLength(32, ErrorMessage = "Last name cannot be longer than 32 characters.")]
        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; }

        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Enter valid email")]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public string Qualification { get; set; }
    }
}
