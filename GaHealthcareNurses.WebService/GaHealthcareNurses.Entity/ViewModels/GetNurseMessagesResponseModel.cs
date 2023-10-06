using System;
using System.Collections.Generic;
using System.Text;

namespace GaHealthcareNurses.Entity.ViewModels
{
    public class GetNurseMessagesResponseModel
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string CareRecipient { get; set; }
        public DateTime SentDate { get; set; }
        public string Agency { get; set; }
        public bool IsRead { get; set; }
        public string Message { get; set; }
    }
}
