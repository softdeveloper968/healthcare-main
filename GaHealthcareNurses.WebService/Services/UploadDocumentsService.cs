using Contracts;
using Contracts.RepositoryContracts;
using Contracts.ServiceContracts;
using GaHealthcareNurses.Entity.Models;
using GaHealthcareNurses.Entity.ViewModels;
using Repository;
using Services.Helper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class UploadDocumentsService : IUploadDocumentsService
    {
        private IUploadDocumentsRepository _uploadDocumentsRepository;

        #region Constructor for UploadDocumentsService
        public UploadDocumentsService(IUploadDocumentsRepository uploadDocumentsRepository)
        {
            _uploadDocumentsRepository = uploadDocumentsRepository;
        }
        #endregion

        #region Implementing Interface
        public async Task AddDocument(UploadDocuments documents)
        {
            await _uploadDocumentsRepository.AddDocument(documents);
        }

        public async Task UpdateDocument(UploadDocuments documents)
        {
            await _uploadDocumentsRepository.UpdateDocument(documents);
        }

        public async Task<IEnumerable<UploadDocuments>> Get()
        {
            return await _uploadDocumentsRepository.Get();
        }

        public async Task<UploadDocuments> GetById(string id)
        {
            return await _uploadDocumentsRepository.GetById(id);
        }

        public async Task<UploadDocuments> UploadDocuments(UploadDocumentsViewModel documentsViewModel)
        {
            UploadDocuments documents = new UploadDocuments();
            documents.Id = documentsViewModel.Id;
            if (documentsViewModel.CnaOrProfessionalLicense != null)
            {
                var cnaOrProfessionalLicensePath = UploadAndDownloadFileAzure.UploadDocument(documentsViewModel.CnaOrProfessionalLicense, documentsViewModel.Id);
                documents.CnaOrProfessionalLicense = cnaOrProfessionalLicensePath;
            }
            if (documentsViewModel.CprProvideNewLicense != null)
            {
                var CprProvideNewLicensePath = UploadAndDownloadFileAzure.UploadDocument(documentsViewModel.CprProvideNewLicense, documentsViewModel.Id);
                documents.CprProvideNewLicense = CprProvideNewLicensePath;
            }
            if (documentsViewModel.Ssn != null)
            {
                var ssnPath = UploadAndDownloadFileAzure.UploadDocument(documentsViewModel.Ssn, documentsViewModel.Id);
                documents.Ssn = ssnPath;
            }
            if (documentsViewModel.DrivingLicense != null)
            {
                var drivingLicensePath = UploadAndDownloadFileAzure.UploadDocument(documentsViewModel.DrivingLicense, documentsViewModel.Id);
                documents.DrivingLicense = drivingLicensePath;
            }
            if (documentsViewModel.TbTestResults != null)
            {
                var tbTestResultsPath = UploadAndDownloadFileAzure.UploadDocument(documentsViewModel.TbTestResults, documentsViewModel.Id);
                documents.TbTestResults = tbTestResultsPath;
            }
            if (documentsViewModel.StaffPortalInfo != null)
            {
                var staffPortalInfoPath = UploadAndDownloadFileAzure.UploadDocument(documentsViewModel.StaffPortalInfo, documentsViewModel.Id);
                documents.StaffPortalInfo = staffPortalInfoPath;
            }
            if (documentsViewModel.PcaTest != null)
            {
                var pcaTestPath = UploadAndDownloadFileAzure.UploadDocument(documentsViewModel.PcaTest, documentsViewModel.Id);
                documents.PcaTest = pcaTestPath;
            }
            if (documentsViewModel.HiringDisclosures != null)
            {
                var hiringDisclosuresPath = UploadAndDownloadFileAzure.UploadDocument(documentsViewModel.HiringDisclosures, documentsViewModel.Id);
                documents.HiringDisclosures = hiringDisclosuresPath;
            }
            if (documentsViewModel.HiringPreScreening != null)
            {
                var hiringPreScreeningPath = UploadAndDownloadFileAzure.UploadDocument(documentsViewModel.HiringPreScreening, documentsViewModel.Id);
                documents.HiringPreScreening = hiringPreScreeningPath;
            }
            if (documentsViewModel.OfficeWillPulled != null)
            {
                var officeWillPulledPath = UploadAndDownloadFileAzure.UploadDocument(documentsViewModel.OfficeWillPulled, documentsViewModel.Id);
                documents.OfficeWillPulled = officeWillPulledPath;
            }
            if (documentsViewModel.SexOffenderReportOfficeWillPulled != null)
            {
                var sexOffenderReportOfficeWillPulledPath = UploadAndDownloadFileAzure.UploadDocument(documentsViewModel.SexOffenderReportOfficeWillPulled, documentsViewModel.Id);
                documents.SexOffenderReportOfficeWillPulled = sexOffenderReportOfficeWillPulledPath;
            }

            if (documentsViewModel.EVerifyWillPulled != null)
            {
                var eVerifyWillPulledPath = UploadAndDownloadFileAzure.UploadDocument(documentsViewModel.EVerifyWillPulled, documentsViewModel.Id);
                documents.EVerifyWillPulled = eVerifyWillPulledPath;
            }

            if (documentsViewModel.BackgroundCheck != null)
            {
                var backgroundCheckPath = UploadAndDownloadFileAzure.UploadDocument(documentsViewModel.BackgroundCheck, documentsViewModel.Id);
                documents.BackgroundCheck = backgroundCheckPath;
            }

            if (!string.IsNullOrEmpty(documentsViewModel.CnaExpiryDate))
            {
                documents.CnaExpiryDate = Convert.ToDateTime(documentsViewModel.CnaExpiryDate);
            }

            if (!string.IsNullOrEmpty(documentsViewModel.CprExpiryDate))
            {
                documents.CprExpiryDate = Convert.ToDateTime(documentsViewModel.CprExpiryDate);
            }

            if (!string.IsNullOrEmpty(documentsViewModel.SsnExpiryDate))
            {
                documents.SsnExpiryDate = Convert.ToDateTime(documentsViewModel.SsnExpiryDate);
            }

            if (!string.IsNullOrEmpty(documentsViewModel.DrivingLicenseExpiryDate))
            {
                documents.DrivingLicenseExpiryDate = Convert.ToDateTime(documentsViewModel.DrivingLicenseExpiryDate);
            }

            if (!string.IsNullOrEmpty(documentsViewModel.TbTestResultsExpiryDate))
            {
                documents.TbTestResultsExpiryDate = Convert.ToDateTime(documentsViewModel.TbTestResultsExpiryDate);
            }

            if (!string.IsNullOrEmpty(documentsViewModel.StaffPortalExpiryDate))
            {
                documents.StaffPortalExpiryDate = Convert.ToDateTime(documentsViewModel.StaffPortalExpiryDate);
            }

            if (!string.IsNullOrEmpty(documentsViewModel.PcaTestExpiryDate))
            {
                documents.PcaTestExpiryDate = Convert.ToDateTime(documentsViewModel.PcaTestExpiryDate);
            }

            if (!string.IsNullOrEmpty(documentsViewModel.HiringDisclosuresExpiryDate))
            {
                documents.HiringDisclosuresExpiryDate = Convert.ToDateTime(documentsViewModel.HiringDisclosuresExpiryDate);
            }

            if (!string.IsNullOrEmpty(documentsViewModel.HiringPreScreeningExpiryDate))
            {
                documents.HiringPreScreeningExpiryDate = Convert.ToDateTime(documentsViewModel.HiringPreScreeningExpiryDate);
            }

            if (!string.IsNullOrEmpty(documentsViewModel.OfficeWillPulledExpiryDate))
            {
                documents.OfficeWillPulledExpiryDate = Convert.ToDateTime(documentsViewModel.OfficeWillPulledExpiryDate);
            }

            if (!string.IsNullOrEmpty(documentsViewModel.SexOffenderReportOfficeWillPulledExpiryDate))
            {
                documents.SexOffenderReportOfficeWillPulledExpiryDate = Convert.ToDateTime(documentsViewModel.SexOffenderReportOfficeWillPulledExpiryDate);
            }

            if (!string.IsNullOrEmpty(documentsViewModel.EVerifyWillPulledExpiryDate))
            {
                documents.EVerifyWillPulledExpiryDate = Convert.ToDateTime(documentsViewModel.EVerifyWillPulledExpiryDate);
            }

            if (!string.IsNullOrEmpty(documentsViewModel.BackgroundCheckExpiryDate))
            {
                documents.BackgroundCheckExpiryDate = Convert.ToDateTime(documentsViewModel.BackgroundCheckExpiryDate);
            }

            await _uploadDocumentsRepository.AddDocument(documents);
            return documents;
        }

        public async Task<UploadDocuments> UpdateDocuments(UploadDocumentsViewModel documentsViewModel)
        {
            var documents = await _uploadDocumentsRepository.GetById(documentsViewModel.Id);
            if (documentsViewModel.CnaOrProfessionalLicense != null)
            {
                var cnaOrProfessionalLicensePath = UploadAndDownloadFileAzure.UploadDocument(documentsViewModel.CnaOrProfessionalLicense, documentsViewModel.Id);
                documents.CnaOrProfessionalLicense = cnaOrProfessionalLicensePath;
            }
            if (documentsViewModel.CprProvideNewLicense != null)
            {
                var CprProvideNewLicensePath = UploadAndDownloadFileAzure.UploadDocument(documentsViewModel.CprProvideNewLicense, documentsViewModel.Id);
                documents.CprProvideNewLicense = CprProvideNewLicensePath;
            }
            if (documentsViewModel.Ssn != null)
            {
                var ssnPath = UploadAndDownloadFileAzure.UploadDocument(documentsViewModel.Ssn, documentsViewModel.Id);
                documents.Ssn = ssnPath;
            }
            if (documentsViewModel.DrivingLicense != null)
            {
                var drivingLicensePath = UploadAndDownloadFileAzure.UploadDocument(documentsViewModel.DrivingLicense, documentsViewModel.Id);
                documents.DrivingLicense = drivingLicensePath;
            }
            if (documentsViewModel.TbTestResults != null)
            {
                var tbTestResultsPath = UploadAndDownloadFileAzure.UploadDocument(documentsViewModel.TbTestResults, documentsViewModel.Id);
                documents.TbTestResults = tbTestResultsPath;
            }
            if (documentsViewModel.StaffPortalInfo != null)
            {
                var staffPortalInfoPath = UploadAndDownloadFileAzure.UploadDocument(documentsViewModel.StaffPortalInfo, documentsViewModel.Id);
                documents.StaffPortalInfo = staffPortalInfoPath;
            }
            if (documentsViewModel.PcaTest != null)
            {
                var pcaTestPath = UploadAndDownloadFileAzure.UploadDocument(documentsViewModel.PcaTest, documentsViewModel.Id);
                documents.PcaTest = pcaTestPath;
            }
            if (documentsViewModel.HiringDisclosures != null)
            {
                var hiringDisclosuresPath = UploadAndDownloadFileAzure.UploadDocument(documentsViewModel.HiringDisclosures, documentsViewModel.Id);
                documents.HiringDisclosures = hiringDisclosuresPath;
            }
            if (documentsViewModel.HiringPreScreening != null)
            {
                var hiringPreScreeningPath = UploadAndDownloadFileAzure.UploadDocument(documentsViewModel.HiringPreScreening, documentsViewModel.Id);
                documents.HiringPreScreening = hiringPreScreeningPath;
            }
            if (documentsViewModel.OfficeWillPulled != null)
            {
                var officeWillPulledPath = UploadAndDownloadFileAzure.UploadDocument(documentsViewModel.OfficeWillPulled, documentsViewModel.Id);
                documents.OfficeWillPulled = officeWillPulledPath;
            }
            if (documentsViewModel.SexOffenderReportOfficeWillPulled != null)
            {
                var sexOffenderReportOfficeWillPulledPath = UploadAndDownloadFileAzure.UploadDocument(documentsViewModel.SexOffenderReportOfficeWillPulled, documentsViewModel.Id);
                documents.SexOffenderReportOfficeWillPulled = sexOffenderReportOfficeWillPulledPath;
            }
            if (documentsViewModel.EVerifyWillPulled != null)
            {
                var eVerifyWillPulledPath = UploadAndDownloadFileAzure.UploadDocument(documentsViewModel.EVerifyWillPulled, documentsViewModel.Id);
                documents.EVerifyWillPulled = eVerifyWillPulledPath;
            }
            if (documentsViewModel.BackgroundCheck != null)
            {
                var backgroundCheckPath = UploadAndDownloadFileAzure.UploadDocument(documentsViewModel.BackgroundCheck, documentsViewModel.Id);
                documents.BackgroundCheck = backgroundCheckPath;
            }

            if (!string.IsNullOrEmpty(documentsViewModel.CnaExpiryDate))
            {
                documents.CnaExpiryDate = Convert.ToDateTime(documentsViewModel.CnaExpiryDate);
            }

            if (!string.IsNullOrEmpty(documentsViewModel.CprExpiryDate))
            {
                documents.CprExpiryDate = Convert.ToDateTime(documentsViewModel.CprExpiryDate);
            }

            if (!string.IsNullOrEmpty(documentsViewModel.SsnExpiryDate))
            {
                documents.SsnExpiryDate = Convert.ToDateTime(documentsViewModel.SsnExpiryDate);
            }

            if (!string.IsNullOrEmpty(documentsViewModel.DrivingLicenseExpiryDate))
            {
                documents.DrivingLicenseExpiryDate = Convert.ToDateTime(documentsViewModel.DrivingLicenseExpiryDate);
            }

            if (!string.IsNullOrEmpty(documentsViewModel.TbTestResultsExpiryDate))
            {
                documents.TbTestResultsExpiryDate = Convert.ToDateTime(documentsViewModel.TbTestResultsExpiryDate);
            }

            if (!string.IsNullOrEmpty(documentsViewModel.StaffPortalExpiryDate))
            {
                documents.StaffPortalExpiryDate = Convert.ToDateTime(documentsViewModel.StaffPortalExpiryDate);
            }

            if (!string.IsNullOrEmpty(documentsViewModel.PcaTestExpiryDate))
            {
                documents.PcaTestExpiryDate = Convert.ToDateTime(documentsViewModel.PcaTestExpiryDate);
            }

            if (!string.IsNullOrEmpty(documentsViewModel.HiringDisclosuresExpiryDate))
            {
                documents.HiringDisclosuresExpiryDate = Convert.ToDateTime(documentsViewModel.HiringDisclosuresExpiryDate);
            }

            if (!string.IsNullOrEmpty(documentsViewModel.HiringPreScreeningExpiryDate))
            {
                documents.HiringPreScreeningExpiryDate = Convert.ToDateTime(documentsViewModel.HiringPreScreeningExpiryDate);
            }

            if (!string.IsNullOrEmpty(documentsViewModel.OfficeWillPulledExpiryDate))
            {
                documents.OfficeWillPulledExpiryDate = Convert.ToDateTime(documentsViewModel.OfficeWillPulledExpiryDate);
            }

            if (!string.IsNullOrEmpty(documentsViewModel.SexOffenderReportOfficeWillPulledExpiryDate))
            {
                documents.SexOffenderReportOfficeWillPulledExpiryDate = Convert.ToDateTime(documentsViewModel.SexOffenderReportOfficeWillPulledExpiryDate);
            }

            if (!string.IsNullOrEmpty(documentsViewModel.EVerifyWillPulledExpiryDate))
            {
                documents.EVerifyWillPulledExpiryDate = Convert.ToDateTime(documentsViewModel.EVerifyWillPulledExpiryDate);
            }

            if (!string.IsNullOrEmpty(documentsViewModel.BackgroundCheckExpiryDate))
            {
                documents.BackgroundCheckExpiryDate = Convert.ToDateTime(documentsViewModel.BackgroundCheckExpiryDate);
            }

            await _uploadDocumentsRepository.UpdateDocument(documents);
            return documents;
        }

        #endregion
    }
}
