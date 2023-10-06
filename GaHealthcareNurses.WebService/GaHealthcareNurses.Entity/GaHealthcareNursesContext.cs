using GaHealthcareNurses.Entity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GaHealthcareNurses.Entity
{
    public class GaHealthcareNursesContext : IdentityDbContext<IdentityUser>
    {
        public GaHealthcareNursesContext(DbContextOptions<GaHealthcareNursesContext> options)
         : base(options)
        {
        }
        public DbSet<Nurse> Nurse { get; set; }
        public DbSet<Certification> Certification { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<Education> Education { get; set; }
        public DbSet<EducationType> EducationType { get; set; }
        public DbSet<Employer> Employer { get; set; }
        public DbSet<Reference> Reference { get; set; }
        public DbSet<State> State { get; set; }
        public DbSet<WorkExperience> WorkExperience { get; set; }
        public DbSet<CareCoordinationNote> CareCoordinationNote { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<Job> Job { get; set; }
        public DbSet<TaskList> TaskList { get; set; }
        public DbSet<VisitNote> VisitNote { get; set; }
        public DbSet<CareRecipient> CareRecipient { get; set; }
        public DbSet<CareRecipientLocation> CareRecipientLocation { get; set; }
        public DbSet<CareRecipientRelationship> CareRecipientRelationship { get; set; }
        public DbSet<Client> Client { get; set; }
        public DbSet<ClientProfile> ClientProfile { get; set; }
        public DbSet<HiringAgreements> HiringAgreements { get; set; }
        public DbSet<UploadDocuments> Documents { get; set; }
        public DbSet<ServiceList> ServiceList { get; set; }
        public DbSet<JobApply> JobApply { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<TaskListVerification> TaskListVerification { get; set; }
        public DbSet<JobTitle> JobTitle { get; set; }
        public DbSet<JobApplyForAgency> JobApplyForAgency { get; set; }
        public DbSet<TaskListTemplate> TaskListTemplate { get; set; }
        public DbSet<TaskListCategory> TaskListCategory { get; set; }
        public DbSet<AgencyTaskList> AgencyTaskList { get; set; }
        public DbSet<Subscription> Subscription { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Admin> Admin { get; set; }
        public DbSet<Referral> Referral { get; set; }
        public DbSet<BuyCourses> BuyCourses { get; set; }
        public DbSet<NurseCnaSkills> NurseCnaSkills { get; set; }
        public DbSet<NurseComment> NurseComment { get; set; }
        public DbSet<County> County { get; set; }
        public DbSet<AgencyServedCounties> AgencyServedCounties { get; set; }
        public DbSet<DischargeSummary> DischargeSummary { get; set; }
        public DbSet<SupervisoryVisit> SupervisoryVisit { get; set; }
        public DbSet<NurseInbox> NurseInbox { get; set; }
        public DbSet<CarePlan> CarePlan { get; set; }
        public DbSet<BlsAreaZipcode> BlsAreaZipcode { get; set; }
        public DbSet<BlsSalaryDetails> BlsSalaryDetails { get; set; }
        public DbSet<NotifyConfiguration> NotifyConfiguration { get; set; }
        public DbSet<ServiceAgreement> ServiceAgreement { get; set; }
        public DbSet<InOutTime> InOutTime { get; set; }
        public DbSet<AdultAssessment> AdultAssessment { get; set; }
        public DbSet<Diagnosis> Diagnosis { get; set; }
        public DbSet<Wound> Wound { get; set; }
    }
}
