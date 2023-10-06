using GaHealthcareNurses.Entity.Models;
using GaHealthcareNurses.Entity.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.ServiceContracts
{
     public interface IServiceAgreementService
    {
        Task<bool> AddServiceAgreement(ServiceAgreement model);
        Task<bool> UpdateServiceAgreement(ServiceAgreementRequestModel model);
        Task <ServiceAgreementRequestModel> GetById(int id);
        Task<List<ServiceAgreementListModel>> GetByEmployerId(string employerId);
        Task<bool> ArchieveServiceAgreement(ArchieveServiceAgreementViewModel model);
        //Task<object> GetServiceAgreementPDFBytes(int id);
        Task<Response<ServiceAgreementPDFBytes>> GetServiceAgreementPDFBytes(int id);
        Task<Response<string>> SendServiceAgreementPDF(int id);
    }
}
