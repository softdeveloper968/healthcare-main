using GaHealthcareNurses.Entity.Models;
using GaHealthcareNurses.Entity.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.ServiceContracts
{
    public interface INurseCommentService
    {
        Task<bool> AddComment(AddCommentViewModel model);
        Task<List<GetNurseCommentResponseModel>> GetCommentsForNurse(string nurseId, int jobId);
        Task<List<GetAgenyCommentResponseModel>> GetCommentsForAgency(string employerId);
        Task<bool> UpdateAgencyResponse(AgencyResponseRequestModel model);
        Task<bool> ReadNurseComment(int nurseCommentId);
    }
}
