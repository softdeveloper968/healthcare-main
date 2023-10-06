using GaHealthcareNurses.Entity.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts.RepositoryContracts
{
    public interface IClientProfileRepository
    {
        Task<IEnumerable<ClientProfile>> Get();
        Task<ClientProfile> GetById(string id);
        Task Add(ClientProfile clientProfile);
        Task<ClientProfile> Update(ClientProfile clientProfile);
        Task Delete(ClientProfile clientProfile);
    }
}
