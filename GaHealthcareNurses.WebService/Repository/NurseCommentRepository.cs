using Contracts;
using GaHealthcareNurses.Entity;
using GaHealthcareNurses.Entity.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class NurseCommentRepository : INurseCommentRepository
    {
        private GaHealthcareNursesContext _gaHealthcareNursesContext;

        #region Contructor For NurseCommentRepository
        public NurseCommentRepository(GaHealthcareNursesContext context)
        {
            _gaHealthcareNursesContext = context;
        }
        #endregion

        #region Implementing Interface
        public async Task<NurseComment> GetNurseCommentById(int id)
        {
            return await _gaHealthcareNursesContext.NurseComment.FirstOrDefaultAsync(x => x.NurseCommentId == id);
        }
        public async Task<bool> AddComment(NurseComment model)
        {
            await _gaHealthcareNursesContext.NurseComment.AddAsync(model);
            await _gaHealthcareNursesContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdateComment(NurseComment model)
        {
            _gaHealthcareNursesContext.NurseComment.Update(model);
            await _gaHealthcareNursesContext.SaveChangesAsync();
            return true;
        }
        public async Task<List<NurseComment>> GetCommentsForNurse(string nurseId, int jobId)
        {
            return await _gaHealthcareNursesContext.NurseComment.Include(x => x.Job).ThenInclude(x => x.CareRecipient).Where(x => x.NurseId == nurseId && x.JobId == jobId).ToListAsync();
        }
        public async Task<List<NurseComment>> GetCommentsForAgency(string employerId)
        {
            return await _gaHealthcareNursesContext.NurseComment.Include(x => x.Job).ThenInclude(x => x.CareRecipient).Include(x => x.Nurse).Where(x => x.EmployerId == employerId).ToListAsync();
        }
        #endregion
    }
}
