
using System.ComponentModel.DataAnnotations;

namespace GaHealthcareNurses.Entity.ViewModels
{
    public class SendJobOfferToNurseViewModel
    {
        public int JobId { get; set; }
        public string NurseId { get; set; }
        [Required(ErrorMessage = "Please enter proposed rate")]
        public double? OfferedRate { get; set; }
    }
}
