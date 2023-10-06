using AutoMapper;
using GaHealthcareNurses.Entity.Common;
using GaHealthcareNurses.Entity.Models;
using GaHealthcareNurses.Entity.ViewModels;
using MoreLinq;
using System;
using System.Linq;

namespace Services
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            CreateMap<SignUp, Nurse>();
            CreateMap<WorkExperienceDto, WorkExperience>();
            CreateMap<EducationDto, EducationType>();
            CreateMap<EducationDto, Education>();
            CreateMap<CertificationDto, Certification>();
            CreateMap<ReferenceDto, Reference>();
            CreateMap<EmployerViewModel, Employer>();
            CreateMap<HiringAggrementsViewModel, HiringAgreements>();
            CreateMap<ClientViewModel, Client>();
            CreateMap<JobViewModel, Job>();
            CreateMap<Nurse, SignUp>().ForMember(x => x.Password, opt => opt.Ignore());
            CreateMap<Nurse, BasicInfo>();
            CreateMap<WorkExperience, WorkExperienceDto>();
            CreateMap<Nurse, WorkExperienceViewModel>().ForMember(x => x.WorkExperienceDto, opt => opt.MapFrom(x => x.WorkExperiences));
            CreateMap<Education, EducationDto>().ForMember(x => x.Name, opt => opt.Ignore());
            CreateMap<Certification, CertificationDto>();
            CreateMap<Nurse, EducationViewModel>().ForMember(x => x.EducationDto, opt => opt.MapFrom(x => x.Educations)).ForMember(x => x.CertificationDto, opt => opt.MapFrom(x => x.Certifications));
            CreateMap<Reference, ReferenceDto>();
            CreateMap<Nurse, Finish>().ForMember(x => x.ReferenceDto, opt => opt.MapFrom(x => x.References));
            CreateMap<CareRecipientViewModel, CareRecipient>();
            CreateMap<JobApplyViewModel, JobApply>();
            CreateMap<Job, GetJobsViewModel>();
            CreateMap<TaskListViewModel, TaskList>();
            CreateMap<AddSignatureViewModel, GetTaskListByDate>();
            CreateMap<JobApplyForAgencyViewModel, JobApplyForAgency>();
            CreateMap<Job, GetJobsForAgencyViewModel>();
            CreateMap<TaskListTemplateViewModel, TaskListTemplate>();
            CreateMap<Employer, EmployerViewModel>().ForMember(x => x.Password, opt => opt.Ignore());
            CreateMap<ReferralSignUp, Nurse>();
            CreateMap<Job, JobResponseModel>();
            CreateMap<NurseCnaSkillsViewModel, NurseCnaSkills>();
            CreateMap<NurseCnaSkills, NurseCnaSkillsViewModel>();
            CreateMap<TaskList, GetTaskListResponseModel>();
            CreateMap<NurseComment, GetNurseCommentResponseModel>()
            .ForMember(pts => pts.ClientName, opt => opt.MapFrom(ps => $"{ps.Job.CareRecipient.FirstName} {ps.Job.CareRecipient.LastName}"));
            CreateMap<NurseComment, GetAgenyCommentResponseModel>()
            .ForMember(pts => pts.ClientName, opt => opt.MapFrom(ps => $"{ps.Job.CareRecipient.FirstName} {ps.Job.CareRecipient.LastName}"))
            .ForMember(pts => pts.NurseName, opt => opt.MapFrom(ps => $"{ps.Nurse.FirstName} {ps.Nurse.LastName}"));
            CreateMap<JobApplyForAgency, GetActiveServiceRequestsResponseModel>()
                .ForMember(pts => pts.ServiceRequest, opt => opt.MapFrom(ps => ps.Job.JobTitle.Title))
                .ForMember(pts => pts.CareRecipientName, opt => opt.MapFrom(ps => $"{ps.Job.CareRecipient.FirstName} {ps.Job.CareRecipient.LastName}"))
                .ForMember(pts => pts.CityName, opt => opt.MapFrom(ps => ps.Job.CareRecipient.City.Name))
                .ForMember(pts => pts.TaskList, opt => opt.MapFrom(ps => ps.Job.TaskList));
            CreateMap<Employer, GetAgencyListResponseModel>()
                .ForMember(pts => pts.Address, opt => opt.MapFrom(ps => $"{ps.AddressLine1}, {ps.City.Name}, {ps.State.ShortName}, {ps.City.ZipCode}"))
                .ForMember(pts => pts.CountiesServed, opt => opt.MapFrom(ps => string.Join(", ", ps.AgencyServedCounties.Select(x => x.County.CountyName).ToList())))
                .ForMember(pts => pts.Phone, opt => opt.MapFrom(ps => $"{ps.TelephoneNo.Substring(0, 3)}-{ps.TelephoneNo.Substring(3, 3)}-{ps.TelephoneNo.Substring(6, 4)}"));
            CreateMap<CreateServiceRequestViewModel, CareRecipient>()
                .ForMember(pts => pts.WhenToStart, opt => opt.MapFrom(ps => ps.Date.ToString()))
                .ForMember(pts => pts.EndDate, opt => opt.MapFrom(ps => ps.EndDate.ToString()));
            CreateMap<DischargeSummary, GetDischargeSummaryListResponseModel>()
                .ForMember(pts => pts.CareRecipient, opt => opt.MapFrom(ps => $"{ps.Job.CareRecipient.FirstName} {ps.Job.CareRecipient.LastName}"))
                .ForMember(pts => pts.ServiceProvided, opt => opt.MapFrom(ps => $"{ps.Job.CareRecipient.ServiceList.Name}"))
                .ForMember(pts => pts.StartDate, opt => opt.MapFrom(ps => $"{ps.Job.CareRecipient.WhenToStart}"))
                .ForMember(pts => pts.EndDate, opt => opt.MapFrom(ps => $"{ps.Job.CareRecipient.EndDate}"))
                .ForMember(pts => pts.CareGiver, opt => opt.MapFrom(ps => $"{ps.Nurse.FirstName} {ps.Nurse.LastName}"))
                .ForMember(pts => pts.Location, opt => opt.MapFrom(ps => $"{ps.Job.CareRecipient.AddressLine1}, {ps.Job.CareRecipient.City.Name}, {ps.Job.CareRecipient.State.ShortName}, {ps.Job.CareRecipient.City.ZipCode}"))
                .ForMember(pts => pts.Agency, opt => opt.MapFrom(ps => $"{ps.Employer.Name}"))
                .ForMember(pts => pts.Status, opt => opt.MapFrom(ps => ps.Status == (int)DischargeSummaryStatus.Verified ? DischargeSummaryStatus.Verified.ToString() : (ps.DateCompleted == null ? DischargeSummaryStatus.Incomplete.ToString() : DischargeSummaryStatus.Pending.ToString())));
            CreateMap<DischargeSummary, DischargeSummaryRequestModel>()
                .ForMember(pts => pts.PatientName, opt => opt.MapFrom(ps => $"{ps.Job.CareRecipient.FirstName} {ps.Job.CareRecipient.LastName}"));
            CreateMap<SupervisoryVisit, GetSupervisoryVisitListResponseModel>()
                .ForMember(pts => pts.CareRecipient, opt => opt.MapFrom(ps => $"{ps.Job.CareRecipient.FirstName} {ps.Job.CareRecipient.LastName}"))
                .ForMember(pts => pts.ServiceProvided, opt => opt.MapFrom(ps => $"{ps.Job.CareRecipient.ServiceList.Name}"))
                .ForMember(pts => pts.StartDate, opt => opt.MapFrom(ps => $"{ps.Job.CareRecipient.WhenToStart}"))
                .ForMember(pts => pts.EndDate, opt => opt.MapFrom(ps => $"{ps.Job.CareRecipient.EndDate}"))
                .ForMember(pts => pts.CareGiver, opt => opt.MapFrom(ps => $"{ps.Nurse.FirstName} {ps.Nurse.LastName}"))
                .ForMember(pts => pts.Location, opt => opt.MapFrom(ps => $"{ps.Job.CareRecipient.AddressLine1}, {ps.Job.CareRecipient.City.Name}, {ps.Job.CareRecipient.State.ShortName}, {ps.Job.CareRecipient.City.ZipCode}"))
                .ForMember(pts => pts.Agency, opt => opt.MapFrom(ps => $"{ps.Employer.Name}"));
            CreateMap<SupervisoryVisit, SupervisoryVisitRequestModel>()
                .ForMember(pts => pts.PatientName, opt => opt.MapFrom(ps => $"{ps.Job.CareRecipient.FirstName} {ps.Job.CareRecipient.LastName}"));
            CreateMap<NurseInbox, GetNurseMessagesResponseModel>()
                .ForMember(pts => pts.Agency, opt => opt.MapFrom(ps => ps.Employer.Name))
                .ForMember(pts => pts.CareRecipient, opt => opt.MapFrom(ps => $"{ps.Job.CareRecipient.FirstName} {ps.Job.CareRecipient.LastName}"));
            CreateMap<CarePlan, GetCarePlanListResponseModel>()
                .ForMember(pts => pts.CareRecipient, opt => opt.MapFrom(ps => $"{ps.Job.CareRecipient.FirstName} {ps.Job.CareRecipient.LastName}"))
                .ForMember(pts => pts.ServiceProvided, opt => opt.MapFrom(ps => $"{ps.Job.CareRecipient.ServiceList.Name}"))
                .ForMember(pts => pts.StartDate, opt => opt.MapFrom(ps => $"{ps.Job.CareRecipient.WhenToStart}"))
                .ForMember(pts => pts.EndDate, opt => opt.MapFrom(ps => $"{ps.Job.CareRecipient.EndDate}"))
                .ForMember(pts => pts.CareGiver, opt => opt.MapFrom(ps => $"{ps.Nurse.FirstName} {ps.Nurse.LastName}"))
                .ForMember(pts => pts.Location, opt => opt.MapFrom(ps => $"{ps.Job.CareRecipient.AddressLine1}, {ps.Job.CareRecipient.City.Name}, {ps.Job.CareRecipient.State.ShortName}, {ps.Job.CareRecipient.City.ZipCode}"))
                .ForMember(pts => pts.Agency, opt => opt.MapFrom(ps => $"{ps.Employer.Name}"));
            CreateMap<CarePlan, CarePlanRequestModel>()
                .ForMember(pts => pts.PatientName, opt => opt.MapFrom(ps => $"{ps.Job.CareRecipient.FirstName} {ps.Job.CareRecipient.LastName}"))
                .ForMember(pts => pts.PatientAddress, opt => opt.MapFrom(ps => $"{ps.Job.CareRecipient.AddressLine1}, {ps.Job.CareRecipient.City.Name}, {ps.Job.CareRecipient.State.ShortName}, {ps.Job.CareRecipient.City.ZipCode}"))
                .ForMember(pts => pts.PatientAge, opt => opt.MapFrom(ps => ps.Job.CareRecipient.Age))
                .ForMember(pts => pts.AgencyName, opt => opt.MapFrom(ps => ps.Employer.Name))
                .ForMember(pts => pts.AgencyAddress, opt => opt.MapFrom(ps => $"{ps.Employer.AddressLine1}, {ps.Employer.City.Name}, {ps.Employer.State.ShortName}, {ps.Employer.City.ZipCode}"))
                .ForMember(pts => pts.PatientGender, opt => opt.MapFrom(ps => ps.Job.CareRecipient.GenderPreference));
            CreateMap<CarePlanRequestModel, CarePlan>();
            CreateMap<Job, GetCareRecipientListResponseModel>()
                .ForMember(pts => pts.FirstName, opt => opt.MapFrom(ps => ps.CareRecipient.FirstName))
                .ForMember(pts => pts.LastName, opt => opt.MapFrom(ps => ps.CareRecipient.LastName))
                .ForMember(pts => pts.Age, opt => opt.MapFrom(ps => ps.CareRecipient.Age))
                .ForMember(pts => pts.Gender, opt => opt.MapFrom(ps => ps.CareRecipient.GenderPreference))
                .ForMember(pts => pts.Address, opt => opt.MapFrom(ps => $"{ps.CareRecipient.AddressLine1}, {ps.CareRecipient.City.Name}, {ps.CareRecipient.State.ShortName}, {ps.CareRecipient.City.ZipCode}"))
                .ForMember(pts => pts.Status, opt => opt.MapFrom(ps => ps.CareRecipient.Status))
                .ForMember(pts => pts.Visibility, opt => opt.MapFrom(ps => ps.CareRecipient.Visibility))
                .ForMember(pts => pts.AssignedCareGiver, opt => opt.MapFrom(ps => ps.JobApplies != null && ps.JobApplies.Count > 0 && ps.JobApplies.FirstOrDefault(x => x.StatusId == 1) != null ? $"{ps.JobApplies.FirstOrDefault(x => x.StatusId == 1).Nurse.FirstName} {ps.JobApplies.FirstOrDefault(x => x.StatusId == 1).Nurse.LastName}" : "N/A"));
            CreateMap<Job, GetServiceRequestListResponseModel>()
                .ForMember(pts => pts.CareRecipient, opt => opt.MapFrom(ps => $"{ps.CareRecipient.FirstName} {ps.CareRecipient.LastName}"))
                .ForMember(pts => pts.Age, opt => opt.MapFrom(ps => ps.CareRecipient.Age))
                .ForMember(pts => pts.ServiceRequired, opt => opt.MapFrom(ps => ps.CareRecipient.ServiceList.Name))
                .ForMember(pts => pts.StartDate, opt => opt.MapFrom(ps => ps.CareRecipient.WhenToStart))
                .ForMember(pts => pts.EndDate, opt => opt.MapFrom(ps => ps.CareRecipient.EndDate))
                .ForMember(pts => pts.TotalHours, opt => opt.MapFrom(ps => ps.CareRecipient.TotalHours))
                .ForMember(pts => pts.Frequency, opt => opt.MapFrom(ps => ps.CareRecipient.Frequency))
                .ForMember(pts => pts.Location, opt => opt.MapFrom(ps => $"{ps.CareRecipient.AddressLine1}, {ps.CareRecipient.City.Name}, {ps.CareRecipient.State.ShortName}, {ps.CareRecipient.City.ZipCode}"))
                .ForMember(pts => pts.Agency, opt => opt.MapFrom(ps => ps.Employer != null ? ps.Employer.Name : "N/A"))
                .ForMember(pts => pts.CareGiver, opt => opt.MapFrom(ps => ps.JobApplies != null && ps.JobApplies.Count > 0 && ps.JobApplies.FirstOrDefault(x => x.StatusId == 1) != null ? $"{ps.JobApplies.FirstOrDefault(x => x.StatusId == 1).Nurse.FirstName} {ps.JobApplies.FirstOrDefault(x => x.StatusId == 1).Nurse.LastName}" : "N/A"))
                .ForMember(pts => pts.Skill, opt => opt.MapFrom(ps => ps.JobTitle.Title))
                .ForMember(pts => pts.Status, opt => opt.MapFrom(ps => ps.JobApplyForAgencies.Any(x => x.StatusId == 13) ? "Active" : (ps.JobApplyForAgencies.Any(x => x.StatusId == 14) ? "Completed" : (ps.JobApplyForAgencies.Any(x => x.StatusId == 11 || x.StatusId == 12) ? "Pending" : "Created"))))
                .ForMember(pts => pts.IsSupervisoryCreated, opt => opt.MapFrom(ps => ps.SupervisoryVisits != null && ps.SupervisoryVisits.Count > 0))
                .ForMember(pts => pts.IsDischargeSummaryCreated, opt => opt.MapFrom(ps => ps.DischargeSummaries != null && ps.DischargeSummaries.Count > 0))
                .ForMember(pts => pts.IsInitialAssessmentCreated, opt => opt.MapFrom(ps => ps.AdultAssessments != null && ps.AdultAssessments.Count > 0))
                .ForMember(pts => pts.IsCarePlanCreated, opt => opt.MapFrom(ps => ps.CarePlans != null && ps.CarePlans.Count > 0))
                .ForMember(pts => pts.PreferredRate, opt => opt.MapFrom(ps => ps.JobApplyForAgencies.FirstOrDefault().PrefferedRate))
                .ForMember(pts => pts.ClientRatingToAgency, opt => opt.MapFrom(ps => ps.ClientRatingToAgency))
                .ForMember(pts => pts.ClientRatingToNurse, opt => opt.MapFrom(ps => ps.ClientRatingToNurse));
            CreateMap<NotifyConfiguration, NotifyConfigurationRequestModel>();
            CreateMap<Job, CreateServiceRequestViewModel>()
                .ForMember(pts => pts.ServiceListId, opt => opt.MapFrom(ps => ps.CareRecipient.ServiceListId))
                .ForMember(pts => pts.DischargeSummaryAssignedNurse, opt => opt.MapFrom(ps => ps.DischargeSummaries != null && ps.DischargeSummaries.Count > 0 ? ps.DischargeSummaries.FirstOrDefault().NurseId : null))
                .ForMember(pts => pts.SupervisoryVisitAssignedNurse, opt => opt.MapFrom(ps => ps.SupervisoryVisits != null && ps.SupervisoryVisits.Count > 0 ? ps.SupervisoryVisits.FirstOrDefault().NurseId : null))
                //.ForMember(pts => pts.InitialAssessmentAssignedNurse, opt => opt.MapFrom(ps => ps.AdultAssessments != null && ps.AdultAssessments.Count > 0 ? ps.AdultAssessments.FirstOrDefault().NurseId : null))
                .ForMember(pts => pts.Date, opt => opt.MapFrom(ps => Convert.ToDateTime(ps.CareRecipient.WhenToStart)))
                .ForMember(pts => pts.Time, opt => opt.MapFrom(ps => Convert.ToDateTime(ps.CareRecipient.WhenToStart)))
                .ForMember(pts => pts.EndDate, opt => opt.MapFrom(ps => Convert.ToDateTime(ps.CareRecipient.EndDate)))
                .ForMember(pts => pts.EndTime, opt => opt.MapFrom(ps => Convert.ToDateTime(ps.CareRecipient.EndDate)))
                .ForMember(pts => pts.Frequency, opt => opt.MapFrom(ps => ps.CareRecipient.Frequency))
                .ForMember(pts => pts.TotalHours, opt => opt.MapFrom(ps => ps.CareRecipient.TotalHours))
                .ForMember(pts => pts.AgencyRate, opt => opt.MapFrom(ps => Convert.ToInt32(ps.JobApplyForAgencies.FirstOrDefault().PrefferedRate)))
                .ForMember(pts => pts.FirstName, opt => opt.MapFrom(ps => ps.CareRecipient.FirstName))
                .ForMember(pts => pts.MiddleInitial, opt => opt.MapFrom(ps => ps.CareRecipient.MiddleInitial))
                .ForMember(pts => pts.LastName, opt => opt.MapFrom(ps => ps.CareRecipient.LastName))
                .ForMember(pts => pts.Age, opt => opt.MapFrom(ps => ps.CareRecipient.Age))
                .ForMember(pts => pts.AddressLine1, opt => opt.MapFrom(ps => ps.CareRecipient.AddressLine1))
                .ForMember(pts => pts.StateId, opt => opt.MapFrom(ps => ps.CareRecipient.StateId))
                .ForMember(pts => pts.CityId, opt => opt.MapFrom(ps => ps.CareRecipient.CityId))
                .ForMember(pts => pts.PostalCode, opt => opt.MapFrom(ps => ps.CareRecipient.PostalCode))
                .ForMember(pts => pts.GenderPreference, opt => opt.MapFrom(ps => ps.CareRecipient.GenderPreference))
                .ForMember(pts => pts.MoreInformation, opt => opt.MapFrom(ps => ps.CareRecipient.MoreInformation))
                .ForMember(pts => pts.ResponsiblePartyName, opt => opt.MapFrom(ps => ps.CareRecipient.ResponsiblePartyName))
                .ForMember(pts => pts.ResponsiblePartyEmail, opt => opt.MapFrom(ps => ps.CareRecipient.ResponsiblePartyEmail))
                .ForMember(pts => pts.EmailAddress, opt => opt.MapFrom(ps => ps.CareRecipient.EmailAddress))
                .ForMember(pts => pts.ResponsiblePartyTelephoneNumber, opt => opt.MapFrom(ps => ps.CareRecipient.ResponsiblePartyTelephoneNumber))
                .ForMember(pts => pts.ResponsiblePartyRelation, opt => opt.MapFrom(ps => ps.CareRecipient.ResponsiblePartyRelation))
                .ForMember(pts => pts.IsResponsiblePartySameAsCareRecipient, opt => opt.MapFrom(ps => ps.CareRecipient.IsResponsiblePartySameAsCareRecipient))
                .ForMember(pts => pts.CareGiver, opt => opt.MapFrom(ps => ps.JobApplies != null && ps.JobApplies.Count > 0 ? ps.JobApplies.FirstOrDefault().NurseId : null))
                .ForMember(pts => pts.TaskListTemplates, opt => opt.MapFrom(ps => ps.TaskList.Select(x => new TaskListAddTemplateViewModel { TaskName = x.TaskName, TaskDescription = x.TaskDescription }).DistinctBy(x => x.TaskName)));
            CreateMap<ServiceAgreement, ServiceAgreementListModel>()
                .ForMember(pts => pts.ServiceRequired, opt => opt.MapFrom(ps => ps.Job.Description))
                .ForMember(pts => pts.ServiceProvide, opt => opt.MapFrom(ps => ps.Job.CareRecipient.ServiceList.Name))
                .ForMember(pts => pts.CareRecipient, opt => opt.MapFrom(ps => $"{ps.Job.CareRecipient.FirstName} {ps.Job.CareRecipient.LastName}"))
                .ForMember(pts => pts.CareStartsOn, opt => opt.MapFrom(ps => Convert.ToDateTime(ps.Job.CareRecipient.WhenToStart)));
            CreateMap<ServiceAgreement, ServiceAgreementRequestModel>()
                .ForMember(pts => pts.AgencyName, opt => opt.MapFrom(ps => ps.Employer.Name))
                .ForMember(pts => pts.AgencyPhoneNo, opt => opt.MapFrom(ps => $"{ps.Employer.TelephoneNo.Substring(0, 3)}-{ps.Employer.TelephoneNo.Substring(3, 3)}-{ps.Employer.TelephoneNo.Substring(6, 4)}"))
                .ForMember(pts => pts.DescriptionByClient, opt => opt.MapFrom(ps => ps.Job.Description))
                .ForMember(pts => pts.ClientName, opt => opt.MapFrom(ps => $"{ps.Job.CareRecipient.FirstName} {ps.Job.CareRecipient.LastName}"))
                .ForMember(pts => pts.ServiceProvide, opt => opt.MapFrom(ps => ps.Job.CareRecipient.ServiceList.Name))
                .ForMember(pts => pts.TotalHours, opt => opt.MapFrom(ps => ps.Job.CareRecipient.TotalHours))
                .ForMember(pts => pts.Frequency, opt => opt.MapFrom(ps => ps.Job.CareRecipient.Frequency))
                .ForMember(pts => pts.StartDate, opt => opt.MapFrom(ps => ps.Job.CareRecipient.WhenToStart))
                .ForMember(pts => pts.EndDate, opt => opt.MapFrom(ps => ps.Job.CareRecipient.EndDate))
                .ForMember(pts => pts.ChargesOfService, opt => opt.MapFrom(ps => ps.Job.JobApplyForAgencies.FirstOrDefault() != null ? ps.Job.JobApplyForAgencies.FirstOrDefault().PrefferedRate : string.Empty));
            CreateMap<AdultAssessment, GetAdultAssessmentListResponseModel>()
                .ForMember(pts => pts.Id, opt => opt.MapFrom(ps => ps.AdultAssessmentId))
                .ForMember(pts => pts.CareRecipient, opt => opt.MapFrom(ps => $"{ps.Job.CareRecipient.FirstName} {ps.Job.CareRecipient.LastName}"))
                .ForMember(pts => pts.ServiceProvided, opt => opt.MapFrom(ps => $"{ps.Job.CareRecipient.ServiceList.Name}"))
                .ForMember(pts => pts.StartDate, opt => opt.MapFrom(ps => $"{ps.Job.CareRecipient.WhenToStart}"))
                .ForMember(pts => pts.EndDate, opt => opt.MapFrom(ps => $"{ps.Job.CareRecipient.EndDate}"))
                .ForMember(pts => pts.CareGiver, opt => opt.MapFrom(ps => $"{ps.Nurse.FirstName} {ps.Nurse.LastName}"))
                .ForMember(pts => pts.Location, opt => opt.MapFrom(ps => $"{ps.Job.CareRecipient.AddressLine1}, {ps.Job.CareRecipient.City.Name}, {ps.Job.CareRecipient.State.ShortName}, {ps.Job.CareRecipient.City.ZipCode}"))
                .ForMember(pts => pts.Agency, opt => opt.MapFrom(ps => $"{ps.Employer.Name}"));
            CreateMap<JobApply, ActiveServiceRequestsResponseModel>()
                .ForMember(pts => pts.JobId, opt => opt.MapFrom(ps => ps.JobId))
                .ForMember(pts => pts.StartDate, opt => opt.MapFrom(ps => ps.Job.CareRecipient.WhenToStart))
                .ForMember(pts => pts.EndDate, opt => opt.MapFrom(ps => ps.Job.CareRecipient.EndDate))
                .ForMember(pts => pts.IsClockedIn, opt => opt.MapFrom(ps => ps.Job.InOutTimes.Any(x => x.ClockInTime != null && x.ClockInTime.Value.Date == DateTime.Now.Date)))
                .ForMember(pts => pts.IsClockedOut, opt => opt.MapFrom(ps => ps.Job.InOutTimes.Any(x => x.ClockOutTime != null && x.ClockOutTime.Value.Date == DateTime.Now.Date)));
            CreateMap<DiagnosisViewModel, Diagnosis>();
            CreateMap<Diagnosis, DiagnosisViewModel>()
                .ForMember(pts => pts.Id, opt => opt.MapFrom(ps => ps.DiagnosisId));
            CreateMap<WoundViewModel, Wound>().ReverseMap(); ;
            CreateMap<AdultAssessmentRequestModel, AdultAssessment>()
                .ForMember(pts => pts.Wound, opt => opt.Ignore())
                .ForMember(pts => pts.Diagnosis, opt => opt.Ignore());
            CreateMap<AdultAssessment, AdultAssessmentRequestModel>()
                .ForMember(pts => pts.FirstName, opt => opt.MapFrom(ps => ps.Job.CareRecipient.FirstName))
                .ForMember(pts => pts.LastName, opt => opt.MapFrom(ps => ps.Job.CareRecipient.LastName))
                .ForMember(pts => pts.Gender, opt => opt.MapFrom(ps => ps.Job.CareRecipient.GenderPreference))
                .ForMember(pts => pts.Age, opt => opt.MapFrom(ps => ps.Job.CareRecipient.Age));
            CreateMap<Nurse, NurseUpdateViewModel>()
                .ForMember(pts => pts.Employers, opt => opt.MapFrom(ps => ps.Employers.Select(x => x.Id)));
        }
    }
}
