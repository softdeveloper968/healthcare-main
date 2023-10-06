using GaHealthcareNurses.Entity.Models;
using GaHealthcareNurses.Entity.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.RepositoryContracts
{
    public interface IServiceAgreementRepository
    {
        Task<bool> AddServiceAgreement(ServiceAgreement model);
        Task<bool> UpdateServiceAgreement(ServiceAgreement model);
        Task<ServiceAgreement> GetById(int id);
        Task<List<ServiceAgreement>> GetByEmployerId(string employerId);
    }
}
