
using System.ComponentModel;

namespace GaHealthcareNurses.Entity.Common
{
    public enum EmailTemplateType
    {
        Register,
        Forgot,
        JobInvitation,
        JobApplied,
        JobOffer,
        JobOfferAccept,
        JobOfferReject,
        Referral,
        BuyCourses
    }

    public enum EmailSubjectRole
    {
        Invitation,
        InvitationAccept,
        JobOffer,
        JobOfferAccept,
        JobOfferReject,
        [Description("Reset Password")] ResetPassword,
        [Description("Confirmation Email")] ConfirmationEmail,     
        Reply,
        Refferal,
        BuyCourses ,
        Register,
        CEU,
        [Description("New Service Request")] NewServiceRequest,
        [Description("Service Request Awarded")] ServiceRequestAwarded,
        [Description("Job Offer : ")] NurseJobOffer
    }

    public enum DischargeSummaryStatus
    {
        Pending,
        Verified,
        Rejected,
        Incomplete
    }

    public enum UserRoles
    {
        Client,
        Nurse,
        Employer,
        Admin
    }

    public enum NurseInboxSubject
    {
        [Description("Discharge Summary Created.")] DischargeSummaryCreated,
        [Description("Supervisory Visit Created.")] SupervisoryVisitCreated,
        [Description("Discharge Summary Accepted.")] DischargeSummaryAccepted,
        [Description("Discharge Summary Rejected.")] DischargeSummaryRejected,
        [Description("Job Application Successful.")] JobApplicationSuccessful,
        [Description("Care Plan Created.")] CarePlanCreated,
        [Description("Adult Assessment Created.")] AssessmentCreated
    }

    public enum CareRecipientStatus
    {
        [Description("Pre-Admit")] PreAdmit,
        Admitted,
        Transfer,
        Discharged,
        [Description("Non-Admin")] NonAdmin
    }

    public enum CareRecipientVisibility
    {
        Active,
        Inactive,
        Deleted
    }
}
