using AutoMapper;
using Contracts.RepositoryContracts;
using Contracts.ServiceContracts;
using GaHealthcareNurses.Entity.Common;
using GaHealthcareNurses.Entity.Models;
using GaHealthcareNurses.Entity.ViewModels;
using Services.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ServiceAgreementService : IServiceAgreementService
    {
        private IServiceAgreementRepository _serviceAgreementRepository;
        private IMapper _mapper;

        #region Constructor for ServiceAgreementService
        public ServiceAgreementService(IServiceAgreementRepository serviceAgreementRepository, IMapper mapper)
        {
            _serviceAgreementRepository = serviceAgreementRepository;
            _mapper = mapper;
        }
        #endregion

        #region Implementing Interface
        public async Task<bool> AddServiceAgreement(ServiceAgreement model)
        {
            return await _serviceAgreementRepository.AddServiceAgreement(model);
        }

        public async Task<List<ServiceAgreementListModel>> GetByEmployerId(string employerId)
        {
              var serviceAgreements = await _serviceAgreementRepository.GetByEmployerId(employerId);
              return _mapper.Map<List<ServiceAgreementListModel>>(serviceAgreements);
        }
        public async Task<ServiceAgreementRequestModel> GetById(int id)
        {
            var serviceAgreement = await _serviceAgreementRepository.GetById(id);
            return _mapper.Map<ServiceAgreementRequestModel>(serviceAgreement);
        }
        public async Task<bool> UpdateServiceAgreement(ServiceAgreementRequestModel model)
        {
            var serviceAgreement = await _serviceAgreementRepository.GetById(model.ServiceAgreementId);
            if (serviceAgreement == null)
                return false;
            serviceAgreement.HasReceivedBillOfRights = model.HasReceivedBillOfRights;
            serviceAgreement.InitialDateOfContact = model.InitialDateOfContact;
            serviceAgreement.IsInsurance = model.IsInsurance;
            serviceAgreement.IsMedicaid = model.IsMedicaid;
            serviceAgreement.IsPrivatePay = model.IsPrivatePay;
            serviceAgreement.ReferredBy = model.ReferredBy;
            serviceAgreement.ReferredDate = model.ReferredDate;
            serviceAgreement.DateBilledOn = model.DateBilledOn;
            serviceAgreement.DateDueOn = (DateTime)model.DateDueOn;
            serviceAgreement.NoOfMonthAgreed = model.NoOfMonthAgreed;
            serviceAgreement.Condition1 = model.Condition1;
            serviceAgreement.Condition2 = model.Condition2;
            serviceAgreement.CanAccessPersonalFunds = model.CanAccessPersonalFunds;
            serviceAgreement.CanAccessPersonalVichle = model.CanAccessPersonalVichle;
            serviceAgreement.SignedByClient = model.SignedByClient;
            serviceAgreement.IsSignedByClient = model.IsSignedByClient;
            serviceAgreement.IsSignedByAgency = model.IsSignedByAgency;
            serviceAgreement.SignedByAgency = model.SignedByAgency;
            return await _serviceAgreementRepository.UpdateServiceAgreement(serviceAgreement);
        }
        public async Task<bool> ArchieveServiceAgreement(ArchieveServiceAgreementViewModel model)
        {
            var serviceAgreement = await _serviceAgreementRepository.GetById(model.ServiceAgreementId);
            if (serviceAgreement == null)
                return false;
            serviceAgreement.IsArchived = model.IsArchieve;
           return await _serviceAgreementRepository.UpdateServiceAgreement(serviceAgreement);
        }
        public async Task<Response<ServiceAgreementPDFBytes>> GetServiceAgreementPDFBytes(int id)
        {
            try
            {
                var serviceAgreement = _mapper.Map<ServiceAgreementRequestModel>(await _serviceAgreementRepository.GetById(id));
                if (serviceAgreement == null)
                    return new Response<ServiceAgreementPDFBytes> { Status = "Error", Message = "Please enter valid Service Agreement id" };
               
               
                var bytes = CreatePDF(serviceAgreement);
                return new Response<ServiceAgreementPDFBytes> { Status = "Success", Data = new ServiceAgreementPDFBytes { Bytes = bytes } };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private string GetCheckboxAttribute(string propertyValue, string compareBy)
        {
            return propertyValue == compareBy ? "checked" : "";
        }

        private string GetCheckboxAttribute(bool isChecked)
        {
            return isChecked ? "checked" : "";
        }
        public async Task<Response<string>> SendServiceAgreementPDF(int id)
        {
            try
            {
                var serviceAgreement = await _serviceAgreementRepository.GetById(id);
                if (serviceAgreement == null)
                    return new Response<string> { Status = "Error", Message = "Service agreement not found" };

                if(string.IsNullOrEmpty(serviceAgreement.Job.CareRecipient.EmailAddress))
                    return new Response<string> { Status = "Error", Message = "Care recipient email not found" };

                var bytes = CreatePDF(_mapper.Map<ServiceAgreementRequestModel>(serviceAgreement));
                EmailViewModel emailViewModel = new EmailViewModel()
                {
                    Subject = $"Service Request Agreement for {serviceAgreement.Job.JobTitle.Title}",
                    EmailBody = $"<p>Dear <b>{serviceAgreement.Job.CareRecipient.FirstName}</b></p>" +
                                       $"<p>Please find attached the Service Request Agreement as required before we can commence this implementation of the service request.</p>" +
                                       $"<div><b>{serviceAgreement.Employer.Name}</b></div>" +
                                        $"<div><b>{serviceAgreement.Employer.TelephoneNo}</b></div>"+
                                         $"<div><b>{serviceAgreement.Employer.AddressLine1}</b></div>",
                    Receiver = serviceAgreement.Job.CareRecipient.EmailAddress,
                    Attachment = bytes
                };
                Utility.SendEmail(emailViewModel);
                return new Response<string> { Status = "Success", Message = "Email sent successfully." };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private byte[] CreatePDF(ServiceAgreementRequestModel serviceAgreement)
        {
            var htmlString = Resource.ServiceAgreementPDF;

            
            htmlString = htmlString.Replace("[[ClientName]]", string.IsNullOrEmpty(serviceAgreement.ClientName) ? "" : serviceAgreement.ClientName);
            htmlString = htmlString.Replace("[[ReferredBy]]", string.IsNullOrEmpty(serviceAgreement.ReferredBy) ? "" : serviceAgreement.ReferredBy);
            htmlString = htmlString.Replace("[[ReferredDate]]", DateTime.Now.Date.ToString("MM/dd/yyyy"));
            htmlString = htmlString.Replace("[[InitialDateOfContact]]", DateTime.Now.Date.ToString("MM/dd/yyyy"));
            htmlString = htmlString.Replace("[[StartDate]]", DateTime.Now.Date.ToString("MM/dd/yyyy"));
            htmlString = htmlString.Replace("[[DescriptionByClient]]", string.IsNullOrEmpty(serviceAgreement.DescriptionByClient) ? "" : serviceAgreement.DescriptionByClient);
            htmlString = htmlString.Replace("[[ServiceProvide]]", string.IsNullOrEmpty(serviceAgreement.ServiceProvide) ? "" : serviceAgreement.ServiceProvide);

            htmlString = htmlString.Replace("[[TotalHours]]", string.IsNullOrEmpty(serviceAgreement.TotalHours) ? "" : serviceAgreement.TotalHours);

            htmlString = htmlString.Replace("[[Frequency]]", string.IsNullOrEmpty(serviceAgreement.Frequency) ? "" : serviceAgreement.Frequency);
            htmlString = htmlString.Replace("[[StartDate]]", DateTime.Now.Date.ToString("MM/dd/yyyy"));
            htmlString = htmlString.Replace("[[StartDate]]", DateTime.Now.Date.ToString("MM/dd/yyyy"));
            htmlString = htmlString.Replace("[[EndDate]]", DateTime.Now.Date.ToString("MM/dd/yyyy"));
            htmlString = htmlString.Replace("[[EndDate]]", DateTime.Now.Date.ToString("MM/dd/yyyy"));
            htmlString = htmlString.Replace("[[IsMedicaidChecked]]", GetCheckboxAttribute(serviceAgreement.IsMedicaid));
            htmlString = htmlString.Replace("[[IsInsuranceChecked]]", GetCheckboxAttribute(serviceAgreement.IsInsurance));
            htmlString = htmlString.Replace("[[IsPrivatePayChecked]]", GetCheckboxAttribute(serviceAgreement.IsPrivatePay));
            htmlString = htmlString.Replace("[[ChargesOfService]]", string.IsNullOrEmpty(serviceAgreement.ChargesOfService) ? "" : serviceAgreement.ChargesOfService);
            htmlString = htmlString.Replace("[[DateBilledOn]]", DateTime.Now.Date.ToString("MM/dd/yyyy"));
            htmlString = htmlString.Replace("[[DateDueOn]]", DateTime.Now.Date.ToString("MM/dd/yyyy"));
            htmlString = htmlString.Replace("[[NoOfMonthAgreed]]", Convert.ToString(serviceAgreement.NoOfMonthAgreed));
            htmlString = htmlString.Replace("[[Condition1]]", string.IsNullOrEmpty(serviceAgreement.Condition1) ? "" : serviceAgreement.Condition1);
            htmlString = htmlString.Replace("[[LeastNoOfDays1]]", Convert.ToString(serviceAgreement.LeastNoOfDays1));
            htmlString = htmlString.Replace("[[LeastNoOfDays2]]", Convert.ToString(serviceAgreement.LeastNoOfDays2));
            htmlString = htmlString.Replace("[[Condition2]]", string.IsNullOrEmpty(serviceAgreement.Condition2) ? "" : serviceAgreement.Condition2);
            htmlString = htmlString.Replace("[[CanAccessPersonalFundsChecked]]", GetCheckboxAttribute(serviceAgreement.CanAccessPersonalFunds));
            htmlString = htmlString.Replace("[[CanAccessPersonalVichleChecked]]", GetCheckboxAttribute(serviceAgreement.CanAccessPersonalVichle));
            htmlString = htmlString.Replace("[[HasReceivedBillOfRightsChecked]]", GetCheckboxAttribute(serviceAgreement.HasReceivedBillOfRights));
            htmlString = htmlString.Replace("[[IsSignedByClientChecked]]", GetCheckboxAttribute(serviceAgreement.IsSignedByClient));
            htmlString = htmlString.Replace("[[SignedByClient]]", DateTime.Now.Date.ToString("MM/dd/yyyy"));
            htmlString = htmlString.Replace("[[IsSignedByAgencyChecked]]", GetCheckboxAttribute(serviceAgreement.IsSignedByAgency));
            htmlString = htmlString.Replace("[[SignedByAgency]]", DateTime.Now.Date.ToString("MM/dd/yyyy"));
            var bytes = HtmlToByte.ConvertToPDFBytes(htmlString);
            return bytes;
        }
        #endregion
    }
}
