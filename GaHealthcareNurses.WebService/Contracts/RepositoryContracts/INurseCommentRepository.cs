using GaHealthcareNurses.Entity.Models;
using GaHealthcareNurses.Entity.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface INurseCommentRepository
    {
        Task<NurseComment> GetNurseCommentById(int id);
        Task<bool> AddComment(NurseComment model);
        Task<List<NurseComment>> GetCommentsForNurse(string nurseId, int jobId);
        Task<List<NurseComment>> GetCommentsForAgency(string employerId);
        Task<bool> UpdateComment(NurseComment model);
    }
}
