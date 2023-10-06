using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;
using System.Text;

namespace GaHealthcareNurses.Entity.Models
{
    public class ClientProfile
    {
        [Key]
        public int Id { get; set; }

        //Personal Info
        public string  Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string GoesBy { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string SSN { get; set; }

        //Account Details
        public int StatusId { get; set; }
        public DateTime? LeadCreatedDate { get; set; }
        public DateTime? InitAssessmentDate { get; set; }
        public DateTime? ConversionDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EffectiveEndDate { get; set; }

        //Mailing Info
        public string MailingName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public int StateId { get; set; }
        public string ZipCode { get; set; }
        public string Geocode { get; set; }
        public string MobAppGeofence { get; set; }
        public int OverrideGeofenceRadiusMet { get; set; }

        //Characteristics
        public decimal Weight { get; set; }
        public int HeightInFeet { get; set; }
        public decimal HeightInInch { get; set; }
        public int LanguageId { get; set; }
        public string Occupation { get; set; }
        public string JobTitle { get; set; }
        public string Hobbies { get; set; }
        public int TriageLevelId { get;set; }
        public string AdvanceDir { get; set; }
        public string Allergies { get; set; }
        public string DNR { get; set; }
        public string Will { get; set; }
        public string PetOne { get; set; }
        public string PetTwo { get; set; }
        public string PetThree { get; set; }

        //Contact Info
        public string PhoneHome { get; set; }
        public string PhoneMobile { get; set; }
        public string PhoneOther { get; set; }
        public string Mobile { get; set; }
        public string TelephonyPhone { get; set; }
        public string EmailAddr { get; set; }
        //public string EmailOptional { get; set; }
        //public string From { get; set; }
    }
}