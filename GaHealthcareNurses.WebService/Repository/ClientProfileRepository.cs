using Contracts.RepositoryContracts;
using GaHealthcareNurses.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GaHealthcareNurses.Entity.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Repository
{
    public class ClientProfileRepository : IClientProfileRepository
    {
        private GaHealthcareNursesContext _gaHealthcareNursesContext;

        #region Contructor For ClientRepository
        public ClientProfileRepository(GaHealthcareNursesContext context)
        {
            _gaHealthcareNursesContext = context;
        }
        #endregion

        #region Implementing Interface
        public async Task Add(ClientProfile clientProfile)
        {
            await _gaHealthcareNursesContext.ClientProfile.AddAsync(clientProfile);
            await _gaHealthcareNursesContext.SaveChangesAsync();
        }

        public async Task Delete(ClientProfile clientProfile)
        {
            _gaHealthcareNursesContext.ClientProfile.Remove(clientProfile);
            await _gaHealthcareNursesContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<ClientProfile>> Get()
        {
            return await _gaHealthcareNursesContext.ClientProfile.ToListAsync();
        }

        public async Task<ClientProfile> GetById(string id)
        {
            return await _gaHealthcareNursesContext.ClientProfile.Where(x => x.Id == Convert.ToInt32(id)).FirstOrDefaultAsync();
        }

        public async Task<ClientProfile> Update(ClientProfile clientProfile)
        {
            _gaHealthcareNursesContext.ClientProfile.Update(clientProfile);
            await _gaHealthcareNursesContext.SaveChangesAsync();
            return clientProfile;
        }
        #endregion
    }
}
