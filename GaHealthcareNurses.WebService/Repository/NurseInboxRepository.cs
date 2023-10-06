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
    public class NurseInboxRepository : INurseInboxRepository
    {
        private GaHealthcareNursesContext _gaHealthcareNursesContext;

        #region Contructor For NurseInboxRepository
        public NurseInboxRepository(GaHealthcareNursesContext context)
        {
            _gaHealthcareNursesContext = context;
        }
        #endregion

        #region Implementing Interface
        public async Task<bool> AddMessage(NurseInbox model)
        {
            await _gaHealthcareNursesContext.NurseInbox.AddAsync(model);
            await _gaHealthcareNursesContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<NurseInbox>> GetNurseMessages(string nurseId)
        {
            return await _gaHealthcareNursesContext.NurseInbox.Include(x => x.Job).ThenInclude(x => x.CareRecipient).Include(x => x.Employer).Where(x => x.NurseId == nurseId).ToListAsync();
        }

        public async Task<NurseInbox> GetById(int id)
        {
            return await _gaHealthcareNursesContext.NurseInbox.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> UpdateMessage(NurseInbox model)
        {
            _gaHealthcareNursesContext.NurseInbox.Update(model);
            await _gaHealthcareNursesContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteMessage(NurseInbox model)
        {
            _gaHealthcareNursesContext.NurseInbox.Remove(model);
            await _gaHealthcareNursesContext.SaveChangesAsync();
            return true;
        }
        #endregion
    }
}
