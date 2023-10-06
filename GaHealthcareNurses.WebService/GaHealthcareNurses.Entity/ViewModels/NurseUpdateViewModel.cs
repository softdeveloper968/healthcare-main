using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GaHealthcareNurses.Entity.ViewModels
{
    public class NurseUpdateViewModel
    {
        public string Id { get; set; }

        [RegularExpression(@"^(?<firstchar>(?=[A-Za-z]))((?<alphachars>[A-Za-z])|(?<specialchars>[A-Za-z]['-](?=[A-Za-z]))|(?<spaces> (?=[A-Za-z])))*$", ErrorMessage = "Enter valid first name")]
        [StringLength(32, ErrorMessage = "First name cannot be longer than 32 characters.")]
        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }

        [RegularExpression(@"^(?<firstchar>(?=[A-Za-z]))((?<alphachars>[A-Za-z])|(?<specialchars>[A-Za-z]['-](?=[A-Za-z]))|(?<spaces> (?=[A-Za-z])))*$", ErrorMessage = "Enter valid middle name")]
        [StringLength(32, ErrorMessage = "Middle initial cannot be longer than 32 characters.")]
        public string MiddleInitial { get; set; }

        [RegularExpression(@"^(?<firstchar>(?=[A-Za-z]))((?<alphachars>[A-Za-z])|(?<specialchars>[A-Za-z]['-](?=[A-Za-z]))|(?<spaces> (?=[A-Za-z])))*$", ErrorMessage = "Enter valid last name")]
        [StringLength(32, ErrorMessage = "Last name cannot be longer than 32 characters.")]
        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Date of birth is required")]
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }
        public string SocialSecurityNo { get; set; }

        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Enter valid email")]
        [Required(ErrorMessage = "Email is required")]
        public string EmailAddress { get; set; }

        [RegularExpression(@"^[0-9]{3}[0-9]{3}[0-9]{4}$", ErrorMessage = "Enter valid phone number")]
        [Required(ErrorMessage = "Telephone number is required")]
        public string TelephoneNo { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string AddressLine1 { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Select a state")]
        public int StateId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Select a  city")]
        public int CityId { get; set; }
        public string ZipCode { get; set; }
        public List<string> Employers { get; set; }
    }
}
