using System;
using System.Collections.Generic;
using System.Text;

namespace GaHealthcareNurses.Entity.ViewModels
{
    public class GetAgenyCommentResponseModel
    {
        public int NurseCommentId { get; set; }
        public int JobId { get; set; }
        public string EmployerId { get; set; }
        public string NurseId { get; set; }
        public string Comment { get; set; }
        public string AgencyResponse { get; set; }
        public DateTime CommentDate { get; set; }
        public DateTime? ResponseDate { get; set; }
        public bool IsUrgent { get; set; }
        public bool IsRead { get; set; }
        public string ClientName { get; set; }
        public string NurseName { get; set; }
    }
}
