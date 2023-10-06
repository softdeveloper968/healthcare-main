using GaHealthcareNurses.Entity.Custom_Validators;

namespace GaHealthcareNurses.Entity.ViewModels
{
    public class AcceptOrRejectDischargeSummaryRequestModel
    {
        public int Id { get; set; }
        public int Status { get; set; }
        [RequiredIf(nameof(Status), "2", ErrorMessage = "Reason is required")]
        public string RejectionReason { get; set; }
    }
}
