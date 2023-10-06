using GaHealthcareNurses.Entity.Custom_Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GaHealthcareNurses.Entity.ViewModels
{
    public class CreateServiceRequestViewModel
    {
        public CreateServiceRequestViewModel()
        {
            Days = new WeekDays();
            TaskListTemplates = new List<TaskListAddTemplateViewModel>();
        }
        public int JobId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Select a  Service")]
        public int ServiceListId { get; set; }
        public bool IsDischargeSummaryRequired { get; set; }
        public string DischargeSummaryAssignedNurse { get; set; }
        public bool IsInitialAssesstmentRequired { get; set; }
        public string InitialAssessmentAssignedNurse { get; set; }
        public bool IsSupervisoryVisitsRequired { get; set; }
        public string SupervisoryVisitAssignedNurse { get; set; }
        [RequiredIf(nameof(IsInitialAssesstmentRequired), "True", ErrorMessage = "Please enter assessment frequency")]
        public int? ReAssessmentFrequency { get; set; }
        [RequiredIf(nameof(IsSupervisoryVisitsRequired), "True", ErrorMessage = "Please enter visit frequency")]
        public int? ReVisitFrequency { get; set; }

        [Required(ErrorMessage = "Start date is required")]
        public DateTime? Date { get; set; }

        [Required(ErrorMessage = "Start Time is required")]
        public DateTime? Time { get; set; }

        [Required(ErrorMessage = "End date is required")]
        public DateTime? EndDate { get; set; }
        [Required(ErrorMessage = "End Time is required")]
        public DateTime? EndTime { get; set; }
        public WeekDays Days { get; set; }
        public string Frequency { get; set; }
        public int TotalHours { get; set; }

        [Required]
        public int? AgencyRate { get; set; }

        [Required]
        public int? CareGiverRate { get; set; }

        [RegularExpression(@"^(?<firstchar>(?=[A-Za-z]))((?<alphachars>[A-Za-z])|(?<specialchars>[A-Za-z]['-](?=[A-Za-z]))|(?<spaces> (?=[A-Za-z])))*$", ErrorMessage = "Enter valid first name")]
        [StringLength(32, ErrorMessage = "First name cannot be longer than 32 characters.")]
        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }
        public string MiddleInitial { get; set; }

        [RegularExpression(@"^(?<firstchar>(?=[A-Za-z]))((?<alphachars>[A-Za-z])|(?<specialchars>[A-Za-z]['-](?=[A-Za-z]))|(?<spaces> (?=[A-Za-z])))*$", ErrorMessage = "Enter valid last name")]
        [StringLength(32, ErrorMessage = "Last name cannot be longer than 32 characters.")]
        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Enter valid age")]
        [Required(ErrorMessage = "Enter age")]
        public int? Age { get; set; }

        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Enter valid email")]
        [Required(ErrorMessage = "Email is required")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [StringLength(32, ErrorMessage = "Address cannot be longer than 32 characters.")]
        public string AddressLine1 { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Select a  State")]
        public int StateId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Select a  City")]
        public int CityId { get; set; }
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "Select a gender")]
        public string GenderPreference { get; set; }

        [Required(ErrorMessage = "Select client payment method")]
        public string ClientPaymentMethod { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Select a  job title")]
        public int JobTitleId { get; set; }

        [Required(ErrorMessage = "Job description is required")]
        public string MoreInformation { get; set; }
        public string ResponsiblePartyName { get; set; }
        public string ResponsiblePartyEmail { get; set; }
        public string ResponsiblePartyTelephoneNumber { get; set; }
        public string ResponsiblePartyRelation { get; set; }
        public bool IsResponsiblePartySameAsCareRecipient { get; set; } = false;
        public string EmployerId { get; set; }
        public string CareGiver { get; set; }
        public List<TaskListAddTemplateViewModel> TaskListTemplates { get; set; }
    }
}