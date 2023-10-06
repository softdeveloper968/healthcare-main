using GaHealthcareNurses.Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.ServiceContracts
{
    public interface IClientProfileService
    {
        Task<IEnumerable<ClientProfile>> Get();
        Task<ClientProfile> GetById(string id);
        Task Add(ClientProfile clientProfile);
        Task Delete(ClientProfile clientProfile);
        Task<ClientProfile> Update(ClientProfile clientProfile);
    }
}
