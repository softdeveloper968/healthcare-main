using Contracts;
using Contracts.ServiceContracts;
using GaHealthcareNurses.Entity.Models;
using GaHealthcareNurses.Entity.ViewModels;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GaHealthcareNurses.Entity.Common;
using AutoMapper;
using Services.Helper;

namespace Services
{
    public class CarePlanService : ICarePlanService
    {
        private ICarePlanRepository _carePlanRepository;
        private INurseService _nurseService;
        private IMapper _mapper;
        private IJobService _jobService;
        private INurseInboxService _nurseInboxService;

        #region Constructor for CarePlanService
        public CarePlanService(ICarePlanRepository carePlanRepository, INurseService nurseService, IJobService jobService, INurseInboxService nurseInboxService, IMapper mapper)
        {
            _carePlanRepository = carePlanRepository;
            _nurseService = nurseService;
            _jobService = jobService;
            _nurseInboxService = nurseInboxService;
            _mapper = mapper;
        }
        #endregion

        #region Implementing Interface
        public async Task<bool> CreatePlan(CarePlan model)
        {
            return await _carePlanRepository.AddCarePlan(model);
        }

        public async Task<List<GetCarePlanListResponseModel>> GetCarePlanByNurseId(string nurseId)
        {
            var carePlans = await _carePlanRepository.GetCarePlanByNurseId(nurseId);
            return _mapper.Map<List<GetCarePlanListResponseModel>>(carePlans);
        }

        public async Task<List<GetCarePlanListResponseModel>> GetCarePlanByEmployerId(string employerId)
        {
            var carePlans = await _carePlanRepository.GetCarePlanByEmployerId(employerId);
            return _mapper.Map<List<GetCarePlanListResponseModel>>(carePlans);
        }

        public async Task<CarePlanRequestModel> GetCarePlanById(int id)
        {
            var carePlan = await _carePlanRepository.GetCarePlanById(id);
            return _mapper.Map<CarePlanRequestModel>(carePlan);
        }

        public async Task<CarePlanRequestModel> GetCarePlanByJobId(int jobId)
        {
            var carePlan = await _carePlanRepository.GetCarePlanByJobId(jobId);
            return _mapper.Map<CarePlanRequestModel>(carePlan);
        }

        public async Task<bool> UpdateCarePlan(CarePlanRequestModel model)
        {
            CarePlan carePlan = _mapper.Map<CarePlan>(model);
            return await _carePlanRepository.UpdateCarePlan(carePlan);
        }

        public async Task<bool> DeleteCarePlan(int id)
        {
            var carePlan = await _carePlanRepository.GetCarePlanById(id);
            if (carePlan == null)
                return false;
            return await _carePlanRepository.DeleteCarePlan(carePlan);
        }

        public async Task<bool> AssignCarePlan(AssignCarePlanViewModel model)
        {
            var carePlan = await _carePlanRepository.GetCarePlanById(model.Id);
            if (carePlan == null)
                return false;
            carePlan.NurseId = model.NurseId;
            await _carePlanRepository.UpdateCarePlan(carePlan);
            NurseInbox message = new NurseInbox
            {
                NurseId = model.NurseId,
                EmployerId = carePlan.EmployerId,
                JobId = carePlan.JobId,
                Subject = EnumHelper.GetDescription(NurseInboxSubject.CarePlanCreated),
                SentDate = DateTime.UtcNow,
                Message = $"The Care Plan for the service request ({carePlan.Job.JobTitle.Title}) for Care Recipient - {carePlan.Job.CareRecipient.FirstName} {carePlan.Job.CareRecipient.LastName} has been generated."
            };
            await _nurseInboxService.AddMessage(message);
            var nurseData = await _nurseService.GetById(model.NurseId);
            Utility.SendMailToUser(message.Subject, nurseData.EmailAddress, message.Message);
            return true;
        }

        public async Task<Response<CarePlanPDFBytes>> GetCarePlanPDFBytes(int id)
        {
            try
            {
                var carePlan = _mapper.Map<CarePlanRequestModel>(await _carePlanRepository.GetCarePlanById(id));
                if (carePlan == null)
                    return new Response<CarePlanPDFBytes> { Status = "Error", Message = "Please enter valid care plan id" };
                var htmlString = Resource.CarePlanPDF;

                #region parameters binding
                htmlString = htmlString.Replace("[[PatientClaimNo]]", string.IsNullOrEmpty(carePlan.PatientClaimNo) ? "" : carePlan.PatientClaimNo);
                htmlString = htmlString.Replace("[[CurrentDate]]", DateTime.Now.Date.ToString("MM/dd/yyyy"));
                htmlString = htmlString.Replace("[[CareStartDate]]", string.IsNullOrEmpty(carePlan.CareStartDate.ToString()) ? "" : carePlan.CareStartDate.ToString());
                htmlString = htmlString.Replace("[[CertificationFrom]]", string.IsNullOrEmpty(carePlan.CertificationFrom.ToString()) ? "" : carePlan.CertificationFrom.ToString());
                htmlString = htmlString.Replace("[[CertificationTo]]", string.IsNullOrEmpty(carePlan.CertificationTo.ToString()) ? "" : carePlan.CertificationTo.ToString());
                htmlString = htmlString.Replace("[[MedicalRecordNo]]", string.IsNullOrEmpty(carePlan.MedicalRecordNo) ? "" : carePlan.MedicalRecordNo);
                htmlString = htmlString.Replace("[[ProviderNo]]", string.IsNullOrEmpty(carePlan.ProviderNo) ? "" : carePlan.ProviderNo);
                htmlString = htmlString.Replace("[[Eligibility]]", string.IsNullOrEmpty(carePlan.Eligibility) ? "" : carePlan.Eligibility);
                htmlString = htmlString.Replace("[[DiagnosisC]]", string.IsNullOrEmpty(carePlan.DiagnosisC) ? "" : carePlan.DiagnosisC);
                htmlString = htmlString.Replace("[[Medicationsl]]", string.IsNullOrEmpty(carePlan.Medicationsl) ? "" : carePlan.Medicationsl);
                htmlString = htmlString.Replace("[[DmeSuppliesi]]", string.IsNullOrEmpty(carePlan.DmeSuppliesi) ? "" : carePlan.DmeSuppliesi);
                htmlString = htmlString.Replace("[[SafetyMeasures]]", string.IsNullOrEmpty(carePlan.SafetyMeasures) ? "" : carePlan.SafetyMeasures);
                htmlString = htmlString.Replace("[[VerbalSOCDate]]", string.IsNullOrEmpty(carePlan.VerbalSOCDate.ToString()) ? "" : carePlan.VerbalSOCDate.ToString());
                htmlString = htmlString.Replace("[[HHAReceivedDate]]", string.IsNullOrEmpty(carePlan.HHAReceivedDate.ToString()) ? "" : carePlan.HHAReceivedDate.ToString());
                htmlString = htmlString.Replace("[[PhysicianName]]", string.IsNullOrEmpty(carePlan.PhysicianName) ? "" : carePlan.PhysicianName);
                htmlString = htmlString.Replace("[[PhysicianAddress]]", string.IsNullOrEmpty(carePlan.PhysicianAddress) ? "" : carePlan.PhysicianAddress);
                htmlString = htmlString.Replace("[[NutritionalRequirements]]", string.IsNullOrEmpty(carePlan.NutritionalRequirements) ? "" : carePlan.NutritionalRequirements);
                htmlString = htmlString.Replace("[[Allergies]]", string.IsNullOrEmpty(carePlan.Allergies) ? "" : carePlan.Allergies);
                htmlString = htmlString.Replace("[[FunctionalLimitations]]", string.IsNullOrEmpty(carePlan.FunctionalLimitations) ? "" : carePlan.FunctionalLimitations);
                htmlString = htmlString.Replace("[[ActivitiesPermitted]]", string.IsNullOrEmpty(carePlan.ActivitiesPermitted) ? "" : carePlan.ActivitiesPermitted);
                htmlString = htmlString.Replace("[[MentalStatus]]", string.IsNullOrEmpty(carePlan.MentalStatus) ? "" : carePlan.MentalStatus);
                htmlString = htmlString.Replace("[[Prognosis]]", string.IsNullOrEmpty(carePlan.Prognosis) ? "" : carePlan.Prognosis);
                htmlString = htmlString.Replace("[[Interventions]]", string.IsNullOrEmpty(carePlan.Interventions) ? "" : carePlan.Interventions);
                htmlString = htmlString.Replace("[[Goals]]", string.IsNullOrEmpty(carePlan.Goals) ? "" : carePlan.Goals);
                htmlString = htmlString.Replace("[[AdvanceDirectives]]", string.IsNullOrEmpty(carePlan.AdvanceDirectives) ? "" : carePlan.AdvanceDirectives);
                htmlString = htmlString.Replace("[[RehabPotential]]", string.IsNullOrEmpty(carePlan.RehabPotential) ? "" : carePlan.RehabPotential);
                htmlString = htmlString.Replace("[[DischargePlanning]]", string.IsNullOrEmpty(carePlan.DischargePlanning) ? "" : carePlan.DischargePlanning);
                htmlString = htmlString.Replace("[[Frequencies]]", string.IsNullOrEmpty(carePlan.Frequencies) ? "" : carePlan.Frequencies);
                htmlString = htmlString.Replace("[[LocationName]]", string.IsNullOrEmpty(carePlan.LocationName) ? "" : carePlan.LocationName);
                htmlString = htmlString.Replace("[[LocationNumber]]", string.IsNullOrEmpty(carePlan.LocationNumber) ? "" : carePlan.LocationNumber);
                htmlString = htmlString.Replace("[[CodeStatus]]", string.IsNullOrEmpty(carePlan.CodeStatus) ? "" : carePlan.CodeStatus);
                htmlString = htmlString.Replace("[[CnaPlanOfCareAllergies]]", string.IsNullOrEmpty(carePlan.CnaPlanOfCareAllergies) ? "" : carePlan.CnaPlanOfCareAllergies);
                htmlString = htmlString.Replace("[[Diagnosis]]", string.IsNullOrEmpty(carePlan.Diagnosis) ? "" : carePlan.Diagnosis);
                htmlString = htmlString.Replace("[[Diet]]", string.IsNullOrEmpty(carePlan.Diet) ? "" : carePlan.Diet);
                htmlString = htmlString.Replace("[[IsAlertChecked]]", GetCheckboxAttribute(carePlan.IsAlert));
                htmlString = htmlString.Replace("[[IsOrientedPersonChecked]]", GetCheckboxAttribute(carePlan.IsOrientedPerson));
                htmlString = htmlString.Replace("[[IsOrientedPlaceChecked]]", GetCheckboxAttribute(carePlan.IsOrientedTime));
                htmlString = htmlString.Replace("[[IsOrientedTimeChecked]]", GetCheckboxAttribute(carePlan.IsOrientedTime));
                htmlString = htmlString.Replace("[[IsOrientedConfusedChecked]]", GetCheckboxAttribute(carePlan.IsOrientedConfused));
                htmlString = htmlString.Replace("[[IsDenturesChecked]]", GetCheckboxAttribute(carePlan.IsDentures));
                htmlString = htmlString.Replace("[[IsDentureUpperChecked]]", GetCheckboxAttribute(carePlan.IsDentureUpper));
                htmlString = htmlString.Replace("[[IsDentureLowerChecked]]", GetCheckboxAttribute(carePlan.IsDentureLower));
                htmlString = htmlString.Replace("[[IsDenturePartialChecked]]", GetCheckboxAttribute(carePlan.IsDenturePartial));
                htmlString = htmlString.Replace("[[IsBedBoundChecked]]", GetCheckboxAttribute(carePlan.IsBedBound));
                htmlString = htmlString.Replace("[[IsBedRestWithBrpChecked]]", GetCheckboxAttribute(carePlan.IsBedRestWithBrp));
                htmlString = htmlString.Replace("[[IsUpAsToleratedChecked]]", GetCheckboxAttribute(carePlan.IsUpAsTolerated));
                htmlString = htmlString.Replace("[[IsHearingDeficitChecked]]", GetCheckboxAttribute(carePlan.IsHearingDeficit));
                htmlString = htmlString.Replace("[[IsHearingDeficitAidChecked]]", GetCheckboxAttribute(carePlan.IsHearingDeficitAid));
                htmlString = htmlString.Replace("[[IsHearingDeficitSpeechChecked]]", GetCheckboxAttribute(carePlan.IsHearingDeficitSpeech));
                htmlString = htmlString.Replace("[[IsVisualDeficitChecked]]", GetCheckboxAttribute(carePlan.IsVisualDeficit));
                htmlString = htmlString.Replace("[[IsVisualDeficitGlassesChecked]]", GetCheckboxAttribute(carePlan.IsVisualDeficitGlasses));
                htmlString = htmlString.Replace("[[IsVisualDeficitContactsChecked]]", GetCheckboxAttribute(carePlan.IsVisualDeficitContacts));
                htmlString = htmlString.Replace("[[IsVisualDeficitOtherChecked]]", GetCheckboxAttribute(carePlan.IsVisualDeficitOther));
                htmlString = htmlString.Replace("[[IsAssistiveDevicesChecked]]", GetCheckboxAttribute(carePlan.IsAssistiveDevices));
                htmlString = htmlString.Replace("[[IsPertientInformationOtherChecked]]", GetCheckboxAttribute(carePlan.IsPertientInformationOther));
                htmlString = htmlString.Replace("[[PertientInformationOther]]", string.IsNullOrEmpty(carePlan.PertientInformationOther) ? "" : carePlan.PertientInformationOther);
                htmlString = htmlString.Replace("[[IsObserveTempChecked]]", GetCheckboxAttribute(carePlan.IsObserveTemp));
                htmlString = htmlString.Replace("[[IsObserveTempOverChecked]]", GetCheckboxAttribute(carePlan.IsObserveTempOver));
                htmlString = htmlString.Replace("[[ObserveTempOver]]", string.IsNullOrEmpty(carePlan.ObserveTempOver) ? "" : carePlan.ObserveTempOver);
                htmlString = htmlString.Replace("[[IsObserveTempUnderChecked]]", GetCheckboxAttribute(carePlan.IsObserveTempUnder));
                htmlString = htmlString.Replace("[[ObserveTempUnder]]", string.IsNullOrEmpty(carePlan.ObserveTempUnder) ? "" : carePlan.ObserveTempUnder);
                htmlString = htmlString.Replace("[[IsObservePulseChecked]]", GetCheckboxAttribute(carePlan.IsObservePulse));
                htmlString = htmlString.Replace("[[IsObservePulseOverChecked]]", GetCheckboxAttribute(carePlan.IsObservePulseOver));
                htmlString = htmlString.Replace("[[ObservePulseOver]]", string.IsNullOrEmpty(carePlan.ObservePulseOver) ? "" : carePlan.ObservePulseOver);
                htmlString = htmlString.Replace("[[IsObservePulseUnderChecked]]", GetCheckboxAttribute(carePlan.IsObservePulseUnder));
                htmlString = htmlString.Replace("[[ObservePulseUnder]]", string.IsNullOrEmpty(carePlan.ObservePulseUnder) ? "" : carePlan.ObservePulseUnder);
                htmlString = htmlString.Replace("[[IsObserveRespirationChecked]]", GetCheckboxAttribute(carePlan.IsObserveRespiration));
                htmlString = htmlString.Replace("[[IsObserveRespirationOverChecked]]", GetCheckboxAttribute(carePlan.IsObserveRespirationOver));
                htmlString = htmlString.Replace("[[ObserveRespirationOver]]", string.IsNullOrEmpty(carePlan.ObserveRespirationOver) ? "" : carePlan.ObserveRespirationOver);
                htmlString = htmlString.Replace("[[IsObserveRespirationUnderChecked]]", GetCheckboxAttribute(carePlan.IsObserveRespirationUnder));
                htmlString = htmlString.Replace("[[ObserveRespirationUnder]]", string.IsNullOrEmpty(carePlan.ObserveRespirationUnder) ? "" : carePlan.ObserveRespirationUnder);
                htmlString = htmlString.Replace("[[IsObserveBPChecked]]", GetCheckboxAttribute(carePlan.IsObserveBP));
                htmlString = htmlString.Replace("[[IsObserveBPOverChecked]]", GetCheckboxAttribute(carePlan.IsObserveBPOver));
                htmlString = htmlString.Replace("[[ObserveBPOver]]", string.IsNullOrEmpty(carePlan.ObserveBPOver) ? "" : carePlan.ObserveRespirationUnder);
                htmlString = htmlString.Replace("[[IsObserveBPUnderChecked]]", GetCheckboxAttribute(carePlan.IsObserveBPUnder));
                htmlString = htmlString.Replace("[[ObserveBPUnder]]", string.IsNullOrEmpty(carePlan.ObserveBPUnder) ? "" : carePlan.ObserveBPUnder);
                htmlString = htmlString.Replace("[[IsObserveIntakeChecked]]", GetCheckboxAttribute(carePlan.IsObserveIntake));
                htmlString = htmlString.Replace("[[IsObserveIntakeOverChecked]]", GetCheckboxAttribute(carePlan.IsObserveIntakeOver));
                htmlString = htmlString.Replace("[[ObserveIntakeOver]]", string.IsNullOrEmpty(carePlan.ObserveIntakeOver) ? "" : carePlan.ObserveIntakeOver);
                htmlString = htmlString.Replace("[[IsObserveIntakeUnderChecked]]", GetCheckboxAttribute(carePlan.IsObserveIntakeUnder));
                htmlString = htmlString.Replace("[[ObserveIntakeUnder]]", string.IsNullOrEmpty(carePlan.ObserveIntakeUnder) ? "" : carePlan.ObserveIntakeUnder);
                htmlString = htmlString.Replace("[[IsObserveOutputAmountChecked]]", GetCheckboxAttribute(carePlan.IsObserveOutputAmount));
                htmlString = htmlString.Replace("[[IsObserveOutputAmountOverChecked]]", GetCheckboxAttribute(carePlan.IsObserveOutputAmountOver));
                htmlString = htmlString.Replace("[[ObserveOutputAmountOver]]", string.IsNullOrEmpty(carePlan.ObserveOutputAmountOver) ? "" : carePlan.ObserveOutputAmountOver);
                htmlString = htmlString.Replace("[[IsObserveOutputAmountUnderChecked]]", GetCheckboxAttribute(carePlan.IsObserveOutputAmountUnder));
                htmlString = htmlString.Replace("[[ObserveOutputAmountUnder]]", string.IsNullOrEmpty(carePlan.ObserveOutputAmountUnder) ? "" : carePlan.ObserveOutputAmountUnder);
                htmlString = htmlString.Replace("[[IsObserveWeightChecked]]", GetCheckboxAttribute(carePlan.IsObserveWeight));
                htmlString = htmlString.Replace("[[IsObserveWeightGainChecked]]", GetCheckboxAttribute(carePlan.IsObserveWeightGain));
                htmlString = htmlString.Replace("[[IsObserveWeightLossChecked]]", GetCheckboxAttribute(carePlan.IsObserveWeightLoss));
                htmlString = htmlString.Replace("[[IsObserveOther1Checked]]", GetCheckboxAttribute(carePlan.IsObserveOther1));
                htmlString = htmlString.Replace("[[IsObserveOther2Checked]]", GetCheckboxAttribute(carePlan.IsObserveOther2));
                htmlString = htmlString.Replace("[[IsObserveNoBMInDaysChecked]]", GetCheckboxAttribute(carePlan.IsObserveNoBMInDays));
                htmlString = htmlString.Replace("[[IsObservePainAboveChecked]]", GetCheckboxAttribute(carePlan.IsObservePainAbove));
                htmlString = htmlString.Replace("[[IsObserveChangesInSkinChecked]]", GetCheckboxAttribute(carePlan.IsObserveChangesInSkin));
                htmlString = htmlString.Replace("[[IsObserveReddenedAreaChecked]]", GetCheckboxAttribute(carePlan.IsObserveReddenedArea));
                htmlString = htmlString.Replace("[[IsObserveReddenedOtherChecked]]", GetCheckboxAttribute(carePlan.IsObserveReddenedOther));
                htmlString = htmlString.Replace("[[IsSafetyFallPrecautionsChecked]]", GetCheckboxAttribute(carePlan.IsSafetyFallPrecautions));
                htmlString = htmlString.Replace("[[IsSafetyAmbulateOnlyChecked]]", GetCheckboxAttribute(carePlan.IsSafetyAmbulateOnly));
                htmlString = htmlString.Replace("[[IsSafetySideRailsUpChecked]]", GetCheckboxAttribute(carePlan.IsSafetySideRailsUp));
                htmlString = htmlString.Replace("[[IsSafetySwallowingChecked]]", GetCheckboxAttribute(carePlan.IsSafetySwallowing));
                htmlString = htmlString.Replace("[[IsSafetyHipChecked]]", GetCheckboxAttribute(carePlan.IsSafetyHip));
                htmlString = htmlString.Replace("[[IsSafetySeizureChecked]]", GetCheckboxAttribute(carePlan.IsSafetySeizure));
                htmlString = htmlString.Replace("[[IsSafetyBleedingChecked]]", GetCheckboxAttribute(carePlan.IsSafetyBleeding));
                htmlString = htmlString.Replace("[[IsSafetyProneToFracturesChecked]]", GetCheckboxAttribute(carePlan.IsSafetyProneToFractures));
                htmlString = htmlString.Replace("[[IsSafetyOther1Checked]]", GetCheckboxAttribute(carePlan.IsSafetyOther1));
                htmlString = htmlString.Replace("[[IsSafetyOther2Checked]]", GetCheckboxAttribute(carePlan.IsSafetyOther2));
                htmlString = htmlString.Replace("[[IsInstructionsAmputeeChecked]]", GetCheckboxAttribute(carePlan.IsInstructionsAmputee));
                htmlString = htmlString.Replace("[[IsInstructionsSpecialEquipChecked]]", GetCheckboxAttribute(carePlan.IsInstructionsSpecialEquip));
                htmlString = htmlString.Replace("[[IsInstructionsOxygenChecked]]", GetCheckboxAttribute(carePlan.IsInstructionsOxygen));
                htmlString = htmlString.Replace("[[IsInstructionsWeightChecked]]", GetCheckboxAttribute(carePlan.IsInstructionsWeight));
                htmlString = htmlString.Replace("[[IsInstructionsOtherChecked]]", GetCheckboxAttribute(carePlan.IsInstructionsOther));
                htmlString = htmlString.Replace("[[IsVitalSignsTempChecked]]", GetCheckboxAttribute(carePlan.IsVitalSignsTemp));
                htmlString = htmlString.Replace("[[IsVitalSignsPulseChecked]]", GetCheckboxAttribute(carePlan.IsVitalSignsPulse));
                htmlString = htmlString.Replace("[[IsVitalSignsRespirationsChecked]]", GetCheckboxAttribute(carePlan.IsVitalSignsRespirations));
                htmlString = htmlString.Replace("[[IsVitalSignsBPChecked]]", GetCheckboxAttribute(carePlan.IsVitalSignsBP));
                htmlString = htmlString.Replace("[[IsVitalSignsPainRatingChecked]]", GetCheckboxAttribute(carePlan.IsVitalSignsPainRating));
                htmlString = htmlString.Replace("[[IsVitalSignsWeightChecked]]", GetCheckboxAttribute(carePlan.IsVitalSignsWeight));
                htmlString = htmlString.Replace("[[IsVitalSignsIntakeChecked]]", GetCheckboxAttribute(carePlan.IsVitalSignsIntake));
                htmlString = htmlString.Replace("[[IsVitalSignsOutputChecked]]", GetCheckboxAttribute(carePlan.IsVitalSignsOutput));
                htmlString = htmlString.Replace("[[IsVitalSignsOtherChecked]]", GetCheckboxAttribute(carePlan.IsVitalSignsOther));
                htmlString = htmlString.Replace("[[IsEliminationLastBMChecked]]", GetCheckboxAttribute(carePlan.IsEliminationLastBM));
                htmlString = htmlString.Replace("[[IsEliminationAssistWithToiletChecked]]", GetCheckboxAttribute(carePlan.IsEliminationAssistWithToilet));
                htmlString = htmlString.Replace("[[IsEliminationBedPanChecked]]", GetCheckboxAttribute(carePlan.IsEliminationBedPan));
                htmlString = htmlString.Replace("[[IsEliminationCCareChecked]]", GetCheckboxAttribute(carePlan.IsEliminationCCare));
                htmlString = htmlString.Replace("[[IsEliminationCCareExternalChecked]]", GetCheckboxAttribute(carePlan.IsEliminationCCareExternal));
                htmlString = htmlString.Replace("[[IsEliminationCCareUrethralChecked]]", GetCheckboxAttribute(carePlan.IsEliminationCCareUrethral));
                htmlString = htmlString.Replace("[[IsEliminationCCareSuprapublicChecked]]", GetCheckboxAttribute(carePlan.IsEliminationCCareSuprapublic));
                htmlString = htmlString.Replace("[[IsEliminationUrineBagChecked]]", GetCheckboxAttribute(carePlan.IsEliminationUrineBag));
                htmlString = htmlString.Replace("[[IsEliminationOstomyChecked]]", GetCheckboxAttribute(carePlan.IsEliminationOstomy));
                htmlString = htmlString.Replace("[[IsEliminationOther1Checked]]", GetCheckboxAttribute(carePlan.IsEliminationOther1));
                htmlString = htmlString.Replace("[[IsEliminationOther2Checked]]", GetCheckboxAttribute(carePlan.IsEliminationOther2));
                htmlString = htmlString.Replace("[[IsPersonalCareChecked]]", GetCheckboxAttribute(carePlan.IsPersonalCare));
                htmlString = htmlString.Replace("[[IsPersonalCareBathChairChecked]]", GetCheckboxAttribute(carePlan.IsPersonalCareBathChair));
                htmlString = htmlString.Replace("[[IsPersonalCareBathTubChecked]]", GetCheckboxAttribute(carePlan.IsPersonalCareBathTub));
                htmlString = htmlString.Replace("[[IsPersonalCareBathBedChecked]]", GetCheckboxAttribute(carePlan.IsPersonalCareBathBed));
                htmlString = htmlString.Replace("[[IsPersonalCareShowerChecked]]", GetCheckboxAttribute(carePlan.IsPersonalCareShower));
                htmlString = htmlString.Replace("[[IsPersonalCareShowerUseChairChecked]]", GetCheckboxAttribute(carePlan.IsPersonalCareShowerUseChair));
                htmlString = htmlString.Replace("[[IsPersonalCareOralChecked]]", GetCheckboxAttribute(carePlan.IsPersonalCareOral));
                htmlString = htmlString.Replace("[[IsPersonalCareSkinCareChecked]]", GetCheckboxAttribute(carePlan.IsPersonalCareSkinCare));
                htmlString = htmlString.Replace("[[IsPersonalCareSkinCareMoisturizeChecked]]", GetCheckboxAttribute(carePlan.IsPersonalCareSkinCareMoisturize));
                htmlString = htmlString.Replace("[[IsPersonalCareCheckPressureChecked]]", GetCheckboxAttribute(carePlan.IsPersonalCareCheckPressure));
                htmlString = htmlString.Replace("[[IsPersonalCareHairCareChecked]]", GetCheckboxAttribute(carePlan.IsPersonalCareHairCare));
                htmlString = htmlString.Replace("[[IsPersonalCareShampooChecked]]", GetCheckboxAttribute(carePlan.IsPersonalCareShampoo));
                htmlString = htmlString.Replace("[[IsPersonalCarePeriCareChecked]]", GetCheckboxAttribute(carePlan.IsPersonalCarePeriCare));
                htmlString = htmlString.Replace("[[IsPersonalCareFootCareChecked]]", GetCheckboxAttribute(carePlan.IsPersonalCareFootCare));
                htmlString = htmlString.Replace("[[IsPersonalCareCleanNailsChecked]]", GetCheckboxAttribute(carePlan.IsPersonalCareCleanNails));
                htmlString = htmlString.Replace("[[IsPersonalCareDressingChecked]]", GetCheckboxAttribute(carePlan.IsPersonalCareDressing));
                htmlString = htmlString.Replace("[[IsPersonalCareStockingsChecked]]", GetCheckboxAttribute(carePlan.IsPersonalCareStockings));
                htmlString = htmlString.Replace("[[IsPersonalCareShaveChecked]]", GetCheckboxAttribute(carePlan.IsPersonalCareShave));
                htmlString = htmlString.Replace("[[IsPersonalCareOther1Checked]]", GetCheckboxAttribute(carePlan.IsPersonalCareOther1));
                htmlString = htmlString.Replace("[[IsPersonalCareOther2Checked]]", GetCheckboxAttribute(carePlan.IsPersonalCareOther2));
                htmlString = htmlString.Replace("[[IsNutritionPrepMealsChecked]]", GetCheckboxAttribute(carePlan.IsNutritionPrepMeals));
                htmlString = htmlString.Replace("[[IsNutritionPrepSnacksChecked]]", GetCheckboxAttribute(carePlan.IsNutritionPrepSnacks));
                htmlString = htmlString.Replace("[[IsNutritionFeedingChecked]]", GetCheckboxAttribute(carePlan.IsNutritionFeeding));
                htmlString = htmlString.Replace("[[IsNutritionFluidsChecked]]", GetCheckboxAttribute(carePlan.IsNutritionFluids));
                htmlString = htmlString.Replace("[[IsNutritionFluidsLimitChecked]]", GetCheckboxAttribute(carePlan.IsNutritionFluidsLimit));
                htmlString = htmlString.Replace("[[IsNutritionFluidsEncourageChecked]]", GetCheckboxAttribute(carePlan.IsNutritionFluidsEncourage));
                htmlString = htmlString.Replace("[[IsNutritionOtherChecked]]", GetCheckboxAttribute(carePlan.IsNutritionOther));
                htmlString = htmlString.Replace("[[IsEnviromentalCleanChecked]]", GetCheckboxAttribute(carePlan.IsEnviromentalClean));
                htmlString = htmlString.Replace("[[IsEnviromentalCleanBathroomChecked]]", GetCheckboxAttribute(carePlan.IsEnviromentalCleanBathroom));
                htmlString = htmlString.Replace("[[IsEnviromentalCleanBedroomChecked]]", GetCheckboxAttribute(carePlan.IsEnviromentalCleanBedroom));
                htmlString = htmlString.Replace("[[IsEnviromentalCleanLivingChecked]]", GetCheckboxAttribute(carePlan.IsEnviromentalCleanLiving));
                htmlString = htmlString.Replace("[[IsEnviromentalCleanKitchenChecked]]", GetCheckboxAttribute(carePlan.IsEnviromentalCleanKitchen));
                htmlString = htmlString.Replace("[[IsEnviromentalLaundryChecked]]", GetCheckboxAttribute(carePlan.IsEnviromentalLaundry));
                htmlString = htmlString.Replace("[[IsEnvironmentalBedLinenChecked]]", GetCheckboxAttribute(carePlan.IsEnvironmentalBedLinen));
                htmlString = htmlString.Replace("[[IsEnvironmentalEquipCareChecked]]", GetCheckboxAttribute(carePlan.IsEnvironmentalEquipCare));
                htmlString = htmlString.Replace("[[IsEnvironmentalOtherChecked]]", GetCheckboxAttribute(carePlan.IsEnvironmentalOther));
                htmlString = htmlString.Replace("[[IsMedicationSelfAssistChecked]]", GetCheckboxAttribute(carePlan.IsMedicationSelfAssist));
                htmlString = htmlString.Replace("[[IsMedicationReminderChecked]]", GetCheckboxAttribute(carePlan.IsMedicationReminder));
                htmlString = htmlString.Replace("[[IsActivityAmbulationChecked]]", GetCheckboxAttribute(carePlan.IsActivityAmbulation));
                htmlString = htmlString.Replace("[[IsActivityTransferAssistChecked]]", GetCheckboxAttribute(carePlan.IsActivityTransferAssist));
                htmlString = htmlString.Replace("[[IsActivityTransferAssistChairChecked]]", GetCheckboxAttribute(carePlan.IsActivityTransferAssistChair));
                htmlString = htmlString.Replace("[[IsActivityTransferAssistBedChecked]]", GetCheckboxAttribute(carePlan.IsActivityTransferAssistBed));
                htmlString = htmlString.Replace("[[IsActivityTransferAssistCommodeChecked]]", GetCheckboxAttribute(carePlan.IsActivityTransferAssistCommode));
                htmlString = htmlString.Replace("[[IsActivityTransferAssistShowerChecked]]", GetCheckboxAttribute(carePlan.IsActivityTransferAssistShower));
                htmlString = htmlString.Replace("[[IsActivityTransferAssistTubChecked]]", GetCheckboxAttribute(carePlan.IsActivityTransferAssistTub));
                htmlString = htmlString.Replace("[[IsActivityTransferAssistHoyerLiftChecked]]", GetCheckboxAttribute(carePlan.IsActivityTransferAssistHoyerLift));
                htmlString = htmlString.Replace("[[IsActivityExcerciseChecked]]", GetCheckboxAttribute(carePlan.IsActivityExcercise));
                htmlString = htmlString.Replace("[[IsActivityExcercisePTChecked]]", GetCheckboxAttribute(carePlan.IsActivityExcercisePT));
                htmlString = htmlString.Replace("[[IsActivityExcerciseOTChecked]]", GetCheckboxAttribute(carePlan.IsActivityExcerciseOT));
                htmlString = htmlString.Replace("[[IsActivityExcerciseSLPChecked]]", GetCheckboxAttribute(carePlan.IsActivityExcerciseSLP));
                htmlString = htmlString.Replace("[[IsActivityRepositionChecked]]", GetCheckboxAttribute(carePlan.IsActivityReposition));
                htmlString = htmlString.Replace("[[IsActivityROMChecked]]", GetCheckboxAttribute(carePlan.IsActivityROM));
                htmlString = htmlString.Replace("[[IsActivityROMActiveChecked]]", GetCheckboxAttribute(carePlan.IsActivityROMActive));
                htmlString = htmlString.Replace("[[IsActivityROMPassiveChecked]]", GetCheckboxAttribute(carePlan.IsActivityROMPassive));
                htmlString = htmlString.Replace("[[IsActivityArmRightChecked]]", GetCheckboxAttribute(carePlan.IsActivityArmRight));
                htmlString = htmlString.Replace("[[IsActivityArmLeftChecked]]", GetCheckboxAttribute(carePlan.IsActivityArmLeft));
                htmlString = htmlString.Replace("[[IsActivityLegRightChecked]]", GetCheckboxAttribute(carePlan.IsActivityLegRight));
                htmlString = htmlString.Replace("[[IsActivityLegLeftChecked]]", GetCheckboxAttribute(carePlan.IsActivityLegLeft));
                htmlString = htmlString.Replace("[[IsActivityOther1Checked]]", GetCheckboxAttribute(carePlan.IsActivityOther1));
                htmlString = htmlString.Replace("[[IsActivityOther2Checked]]", GetCheckboxAttribute(carePlan.IsActivityOther2));
                htmlString = htmlString.Replace("[[IsAssistiveDevicesWCChecked]]", GetCheckboxAttribute(carePlan.IsAssistiveDevicesWC));
                htmlString = htmlString.Replace("[[IsAssistiveDevicesWalkerChecked]]", GetCheckboxAttribute(carePlan.IsAssistiveDevicesWalker));
                htmlString = htmlString.Replace("[[IsAssistiveDevicesCaneChecked]]", GetCheckboxAttribute(carePlan.IsAssistiveDevicesCane));
                htmlString = htmlString.Replace("[[IsAssistiveDevicesCrutchesChecked]]", GetCheckboxAttribute(carePlan.IsAssistiveDevicesCrutches));
                htmlString = htmlString.Replace("[[IsAssistiveDevicesBraceSplintChecked]]", GetCheckboxAttribute(carePlan.IsAssistiveDevicesBraceSplint));
                htmlString = htmlString.Replace("[[IsAssistiveDevicesOtherChecked]]", GetCheckboxAttribute(carePlan.IsAssistiveDevicesOther));
                htmlString = htmlString.Replace("[[ObserveWeightLoss]]", string.IsNullOrEmpty(carePlan.ObserveWeightLoss) ? "" : carePlan.ObserveWeightLoss);
                htmlString = htmlString.Replace("[[ObserveOther1]]", string.IsNullOrEmpty(carePlan.ObserveOther1) ? "" : carePlan.ObserveOther1);
                htmlString = htmlString.Replace("[[ObserveOther2]]", string.IsNullOrEmpty(carePlan.ObserveOther2) ? "" : carePlan.ObserveOther2);
                htmlString = htmlString.Replace("[[ObservePainAbove]]", string.IsNullOrEmpty(carePlan.ObservePainAbove) ? "" : carePlan.ObservePainAbove);
                htmlString = htmlString.Replace("[[ObserveChangesInSkin]]", string.IsNullOrEmpty(carePlan.ObserveChangesInSkin) ? "" : carePlan.ObserveChangesInSkin);
                htmlString = htmlString.Replace("[[ObserveReddenedOther]]", string.IsNullOrEmpty(carePlan.ObserveReddenedOther) ? "" : carePlan.ObserveReddenedOther);
                htmlString = htmlString.Replace("[[SafetyOther1]]", string.IsNullOrEmpty(carePlan.SafetyOther1) ? "" : carePlan.SafetyOther1);
                htmlString = htmlString.Replace("[[SafetyOther2]]", string.IsNullOrEmpty(carePlan.SafetyOther2) ? "" : carePlan.SafetyOther2);
                htmlString = htmlString.Replace("[[InstructionsAmputee]]", string.IsNullOrEmpty(carePlan.InstructionsAmputee) ? "" : carePlan.InstructionsAmputee);
                htmlString = htmlString.Replace("[[InstructionsSpecialEquip]]", string.IsNullOrEmpty(carePlan.InstructionsSpecialEquip) ? "" : carePlan.InstructionsSpecialEquip);
                htmlString = htmlString.Replace("[[InstructionsOxygen]]", string.IsNullOrEmpty(carePlan.InstructionsOxygen) ? "" : carePlan.InstructionsOxygen);
                htmlString = htmlString.Replace("[[InstructionsWeight]]", string.IsNullOrEmpty(carePlan.InstructionsWeight) ? "" : carePlan.InstructionsWeight);
                htmlString = htmlString.Replace("[[InstructionsOther]]", string.IsNullOrEmpty(carePlan.InstructionsOther) ? "" : carePlan.InstructionsOther);
                htmlString = htmlString.Replace("[[VitalSignsTempFrequency]]", string.IsNullOrEmpty(carePlan.VitalSignsTempFrequency) ? "" : carePlan.VitalSignsTempFrequency);
                htmlString = htmlString.Replace("[[VitalSignsTempNotes]]", string.IsNullOrEmpty(carePlan.VitalSignsTempNotes) ? "" : carePlan.VitalSignsTempNotes);
                htmlString = htmlString.Replace("[[VitalSignsPulseFrequency]]", string.IsNullOrEmpty(carePlan.VitalSignsPulseFrequency) ? "" : carePlan.VitalSignsPulseFrequency);
                htmlString = htmlString.Replace("[[VitalSignsPulseNotes]]", string.IsNullOrEmpty(carePlan.VitalSignsPulseNotes) ? "" : carePlan.VitalSignsPulseNotes);
                htmlString = htmlString.Replace("[[VitalSignsRespirationsFrequency]]", string.IsNullOrEmpty(carePlan.VitalSignsRespirationsFrequency) ? "" : carePlan.VitalSignsRespirationsFrequency);
                htmlString = htmlString.Replace("[[VitalSignsRespirationsNotes]]", string.IsNullOrEmpty(carePlan.VitalSignsRespirationsNotes) ? "" : carePlan.VitalSignsRespirationsNotes);
                htmlString = htmlString.Replace("[[VitalSignsBPFrequency]]", string.IsNullOrEmpty(carePlan.VitalSignsBPFrequency) ? "" : carePlan.VitalSignsBPFrequency);
                htmlString = htmlString.Replace("[[VitalSignsBPNotes]]", string.IsNullOrEmpty(carePlan.VitalSignsBPNotes) ? "" : carePlan.VitalSignsBPNotes);
                htmlString = htmlString.Replace("[[VitalSignsPainRatingFrequency]]", string.IsNullOrEmpty(carePlan.VitalSignsPainRatingFrequency) ? "" : carePlan.VitalSignsPainRatingFrequency);
                htmlString = htmlString.Replace("[[VitalSignsPainRatingNotes]]", string.IsNullOrEmpty(carePlan.VitalSignsPainRatingNotes) ? "" : carePlan.VitalSignsPainRatingNotes);
                htmlString = htmlString.Replace("[[VitalSignsWeightFrequency]]", string.IsNullOrEmpty(carePlan.VitalSignsWeightFrequency) ? "" : carePlan.VitalSignsWeightFrequency);
                htmlString = htmlString.Replace("[[VitalSignsWeightNotes]]", string.IsNullOrEmpty(carePlan.VitalSignsWeightNotes) ? "" : carePlan.VitalSignsWeightNotes);
                htmlString = htmlString.Replace("[[VitalSignsIntakeFrequency]]", string.IsNullOrEmpty(carePlan.VitalSignsIntakeFrequency) ? "" : carePlan.VitalSignsIntakeFrequency);
                htmlString = htmlString.Replace("[[VitalSignsIntakeNotes]]", string.IsNullOrEmpty(carePlan.VitalSignsIntakeNotes) ? "" : carePlan.VitalSignsIntakeNotes);
                htmlString = htmlString.Replace("[[VitalSignsOutputFrequency]]", string.IsNullOrEmpty(carePlan.VitalSignsOutputFrequency) ? "" : carePlan.VitalSignsOutputFrequency);
                htmlString = htmlString.Replace("[[VitalSignsOutputNotes]]", string.IsNullOrEmpty(carePlan.VitalSignsOutputNotes) ? "" : carePlan.VitalSignsOutputNotes);
                htmlString = htmlString.Replace("[[VitalSignsOther]]", string.IsNullOrEmpty(carePlan.VitalSignsOther) ? "" : carePlan.VitalSignsOther);
                htmlString = htmlString.Replace("[[VitalSignsOtherFrequency]]", string.IsNullOrEmpty(carePlan.VitalSignsOtherFrequency) ? "" : carePlan.VitalSignsOtherFrequency);
                htmlString = htmlString.Replace("[[VitalSignsOtherNotes]]", string.IsNullOrEmpty(carePlan.VitalSignsOtherNotes) ? "" : carePlan.VitalSignsOtherNotes);
                htmlString = htmlString.Replace("[[EliminationLastBMFrequency]]", string.IsNullOrEmpty(carePlan.EliminationLastBMFrequency) ? "" : carePlan.EliminationLastBMFrequency);
                htmlString = htmlString.Replace("[[EliminationLastBMNotes]]", string.IsNullOrEmpty(carePlan.EliminationLastBMNotes) ? "" : carePlan.EliminationLastBMNotes);
                htmlString = htmlString.Replace("[[EliminationAssistWithToiletFrequency]]", string.IsNullOrEmpty(carePlan.EliminationAssistWithToiletFrequency) ? "" : carePlan.EliminationAssistWithToiletFrequency);
                htmlString = htmlString.Replace("[[EliminationAssistWithToiletNotes]]", string.IsNullOrEmpty(carePlan.EliminationAssistWithToiletNotes) ? "" : carePlan.EliminationAssistWithToiletNotes);
                htmlString = htmlString.Replace("[[EliminationBedPanFrequency]]", string.IsNullOrEmpty(carePlan.EliminationBedPanFrequency) ? "" : carePlan.EliminationBedPanFrequency);
                htmlString = htmlString.Replace("[[EliminationBedPanNotes]]", string.IsNullOrEmpty(carePlan.EliminationBedPanNotes) ? "" : carePlan.EliminationBedPanNotes);
                htmlString = htmlString.Replace("[[EliminationCCareFrequency]]", string.IsNullOrEmpty(carePlan.EliminationCCareFrequency) ? "" : carePlan.EliminationCCareFrequency);
                htmlString = htmlString.Replace("[[EliminationCCareNotes]]", string.IsNullOrEmpty(carePlan.EliminationCCareNotes) ? "" : carePlan.EliminationCCareNotes);
                htmlString = htmlString.Replace("[[EliminationUrineBagFrequency]]", string.IsNullOrEmpty(carePlan.EliminationUrineBagFrequency) ? "" : carePlan.EliminationUrineBagFrequency);
                htmlString = htmlString.Replace("[[EliminationUrineBagNotes]]", string.IsNullOrEmpty(carePlan.EliminationUrineBagNotes) ? "" : carePlan.EliminationUrineBagNotes);
                htmlString = htmlString.Replace("[[EliminationOstomyFrequency]]", string.IsNullOrEmpty(carePlan.EliminationOstomyFrequency) ? "" : carePlan.EliminationOstomyFrequency);
                htmlString = htmlString.Replace("[[EliminationOstomyNotes]]", string.IsNullOrEmpty(carePlan.EliminationOstomyNotes) ? "" : carePlan.EliminationOstomyNotes);
                htmlString = htmlString.Replace("[[EliminationOther1]]", string.IsNullOrEmpty(carePlan.EliminationOther1) ? "" : carePlan.EliminationOther1);
                htmlString = htmlString.Replace("[[EliminationOther1Frequency]]", string.IsNullOrEmpty(carePlan.EliminationOther1Frequency) ? "" : carePlan.EliminationOther1Frequency);
                htmlString = htmlString.Replace("[[EliminationOther1Notes]]", string.IsNullOrEmpty(carePlan.EliminationOther1Notes) ? "" : carePlan.EliminationOther1Notes);
                htmlString = htmlString.Replace("[[EliminationOther2]]", string.IsNullOrEmpty(carePlan.EliminationOther2) ? "" : carePlan.EliminationOther2);
                htmlString = htmlString.Replace("[[EliminationOther2Frequency]]", string.IsNullOrEmpty(carePlan.EliminationOther2Frequency) ? "" : carePlan.EliminationOther2Frequency);
                htmlString = htmlString.Replace("[[EliminationOther2Notes]]", string.IsNullOrEmpty(carePlan.EliminationOther2Notes) ? "" : carePlan.EliminationOther2Notes);
                htmlString = htmlString.Replace("[[PartialChecked]]", GetCheckboxAttribute(carePlan.IsPersonalCareBathPartial, "Partial"));
                htmlString = htmlString.Replace("[[CompleteChecked]]", GetCheckboxAttribute(carePlan.IsPersonalCareBathPartial, "Complete"));
                htmlString = htmlString.Replace("[[PersonalCareBathFrequency]]", string.IsNullOrEmpty(carePlan.PersonalCareBathFrequency) ? "" : carePlan.PersonalCareBathFrequency);
                htmlString = htmlString.Replace("[[PersonalCareBathNotes]]", string.IsNullOrEmpty(carePlan.PersonalCareBathNotes) ? "" : carePlan.PersonalCareBathNotes);
                htmlString = htmlString.Replace("[[PersonalCareShowerFrequency]]", string.IsNullOrEmpty(carePlan.PersonalCareShowerFrequency) ? "" : carePlan.PersonalCareShowerFrequency);
                htmlString = htmlString.Replace("[[PersonalCareShowerNotes]]", string.IsNullOrEmpty(carePlan.PersonalCareShowerNotes) ? "" : carePlan.PersonalCareShowerNotes);
                htmlString = htmlString.Replace("[[PersonalCareOralFrequency]]", string.IsNullOrEmpty(carePlan.PersonalCareOralFrequency) ? "" : carePlan.PersonalCareOralFrequency);
                htmlString = htmlString.Replace("[[PersonalCareOralNotes]]", string.IsNullOrEmpty(carePlan.PersonalCareOralNotes) ? "" : carePlan.PersonalCareOralNotes);
                htmlString = htmlString.Replace("[[PersonalCareSkinCareFrequency]]", string.IsNullOrEmpty(carePlan.PersonalCareSkinCareFrequency) ? "" : carePlan.PersonalCareSkinCareFrequency);
                htmlString = htmlString.Replace("[[PersonalCareSkinCareNotes]]", string.IsNullOrEmpty(carePlan.PersonalCareSkinCareNotes) ? "" : carePlan.PersonalCareSkinCareNotes);
                htmlString = htmlString.Replace("[[PersonalCareCheckPressureFrequency]]", string.IsNullOrEmpty(carePlan.PersonalCareCheckPressureFrequency) ? "" : carePlan.PersonalCareCheckPressureFrequency);
                htmlString = htmlString.Replace("[[PersonalCareCheckPressureNotes]]", string.IsNullOrEmpty(carePlan.PersonalCareCheckPressureNotes) ? "" : carePlan.PersonalCareCheckPressureNotes);
                htmlString = htmlString.Replace("[[PersonalCareHairCareFrequency]]", string.IsNullOrEmpty(carePlan.PersonalCareHairCareFrequency) ? "" : carePlan.PersonalCareHairCareFrequency);
                htmlString = htmlString.Replace("[[PersonalCareHairCareNotes]]", string.IsNullOrEmpty(carePlan.PersonalCareHairCareNotes) ? "" : carePlan.PersonalCareHairCareNotes);
                htmlString = htmlString.Replace("[[PersonalCareShampooFrequency]]", string.IsNullOrEmpty(carePlan.PersonalCareShampooFrequency) ? "" : carePlan.PersonalCareShampooFrequency);
                htmlString = htmlString.Replace("[[PersonalCareShampooNotes]]", string.IsNullOrEmpty(carePlan.PersonalCareShampooNotes) ? "" : carePlan.PersonalCareShampooNotes);
                htmlString = htmlString.Replace("[[PersonalCarePeriCareFrequency]]", string.IsNullOrEmpty(carePlan.PersonalCarePeriCareFrequency) ? "" : carePlan.PersonalCarePeriCareFrequency);
                htmlString = htmlString.Replace("[[PersonalCarePeriCareNotes]]", string.IsNullOrEmpty(carePlan.PersonalCarePeriCareNotes) ? "" : carePlan.PersonalCarePeriCareNotes);
                htmlString = htmlString.Replace("[[PersonalCareFootCareFrequency]]", string.IsNullOrEmpty(carePlan.PersonalCareFootCareFrequency) ? "" : carePlan.PersonalCareFootCareFrequency);
                htmlString = htmlString.Replace("[[PersonalCareFootCareNotes]]", string.IsNullOrEmpty(carePlan.PersonalCareFootCareNotes) ? "" : carePlan.PersonalCareFootCareNotes);
                htmlString = htmlString.Replace("[[PersonalCareCleanNailsFrequency]]", string.IsNullOrEmpty(carePlan.PersonalCareCleanNailsFrequency) ? "" : carePlan.PersonalCareCleanNailsFrequency);
                htmlString = htmlString.Replace("[[PersonalCareCleanNailsNotes]]", string.IsNullOrEmpty(carePlan.PersonalCareCleanNailsNotes) ? "" : carePlan.PersonalCareCleanNailsNotes);
                htmlString = htmlString.Replace("[[PersonalCareDressingFrequency]]", string.IsNullOrEmpty(carePlan.PersonalCareDressingFrequency) ? "" : carePlan.PersonalCareDressingFrequency);
                htmlString = htmlString.Replace("[[PersonalCareDressingNotes]]", string.IsNullOrEmpty(carePlan.PersonalCareDressingNotes) ? "" : carePlan.PersonalCareDressingNotes);
                htmlString = htmlString.Replace("[[PersonalCareStockingsFrequency]]", string.IsNullOrEmpty(carePlan.PersonalCareStockingsFrequency) ? "" : carePlan.PersonalCareStockingsFrequency);
                htmlString = htmlString.Replace("[[PersonalCareStockingsNotes]]", string.IsNullOrEmpty(carePlan.PersonalCareStockingsNotes) ? "" : carePlan.PersonalCareStockingsNotes);
                htmlString = htmlString.Replace("[[PersonalCareShaveFrequency]]", string.IsNullOrEmpty(carePlan.PersonalCareShaveFrequency) ? "" : carePlan.PersonalCareShaveFrequency);
                htmlString = htmlString.Replace("[[PersonalCareShaveNotes]]", string.IsNullOrEmpty(carePlan.PersonalCareShaveNotes) ? "" : carePlan.PersonalCareShaveNotes);
                htmlString = htmlString.Replace("[[PersonalCareOther1]]", string.IsNullOrEmpty(carePlan.PersonalCareOther1) ? "" : carePlan.PersonalCareOther1);
                htmlString = htmlString.Replace("[[PersonalCareOther1Frequency]]", string.IsNullOrEmpty(carePlan.PersonalCareOther1Frequency) ? "" : carePlan.PersonalCareOther1Frequency);
                htmlString = htmlString.Replace("[[PersonalCareOther1Notes]]", string.IsNullOrEmpty(carePlan.PersonalCareOther1Notes) ? "" : carePlan.PersonalCareOther1Notes);
                htmlString = htmlString.Replace("[[PersonalCareOther2]]", string.IsNullOrEmpty(carePlan.PersonalCareOther2) ? "" : carePlan.PersonalCareOther2);
                htmlString = htmlString.Replace("[[PersonalCareOther2Frequency]]", string.IsNullOrEmpty(carePlan.PersonalCareOther2Frequency) ? "" : carePlan.PersonalCareOther2Frequency);
                htmlString = htmlString.Replace("[[PersonalCareOther2Notes]]", string.IsNullOrEmpty(carePlan.PersonalCareOther2Notes) ? "" : carePlan.PersonalCareOther2Notes);
                htmlString = htmlString.Replace("[[NutritionPrepMealsFrequency]]", string.IsNullOrEmpty(carePlan.NutritionPrepMealsFrequency) ? "" : carePlan.NutritionPrepMealsFrequency);
                htmlString = htmlString.Replace("[[NutritionPrepMealsNotes]]", string.IsNullOrEmpty(carePlan.NutritionPrepMealsNotes) ? "" : carePlan.NutritionPrepMealsNotes);
                htmlString = htmlString.Replace("[[NutritionPrepSnacksFrequency]]", string.IsNullOrEmpty(carePlan.NutritionPrepSnacksFrequency) ? "" : carePlan.NutritionPrepSnacksFrequency);
                htmlString = htmlString.Replace("[[NutritionPrepSnacksNotes]]", string.IsNullOrEmpty(carePlan.NutritionPrepSnacksNotes) ? "" : carePlan.NutritionPrepSnacksNotes);
                htmlString = htmlString.Replace("[[NutritionFeedingFrequency]]", string.IsNullOrEmpty(carePlan.NutritionFeedingFrequency) ? "" : carePlan.NutritionFeedingFrequency);
                htmlString = htmlString.Replace("[[NutritionFeedingNotes]]", string.IsNullOrEmpty(carePlan.NutritionFeedingNotes) ? "" : carePlan.NutritionFeedingNotes);
                htmlString = htmlString.Replace("[[NutritionFluidsFrequency]]", string.IsNullOrEmpty(carePlan.NutritionFluidsFrequency) ? "" : carePlan.NutritionFluidsFrequency);
                htmlString = htmlString.Replace("[[NutritionFluidsNotes]]", string.IsNullOrEmpty(carePlan.NutritionFluidsNotes) ? "" : carePlan.NutritionFluidsNotes);
                htmlString = htmlString.Replace("[[NutritionOther]]", string.IsNullOrEmpty(carePlan.NutritionOther) ? "" : carePlan.NutritionOther);
                htmlString = htmlString.Replace("[[NutritionOtherFrequency]]", string.IsNullOrEmpty(carePlan.NutritionOtherFrequency) ? "" : carePlan.NutritionOtherFrequency);
                htmlString = htmlString.Replace("[[NutritionOtherNotes]]", string.IsNullOrEmpty(carePlan.NutritionOtherNotes) ? "" : carePlan.NutritionOtherNotes);
                htmlString = htmlString.Replace("[[EnviromentalCleanFrequency]]", string.IsNullOrEmpty(carePlan.EnviromentalCleanFrequency) ? "" : carePlan.EnviromentalCleanFrequency);
                htmlString = htmlString.Replace("[[EnviromentalCleanNotes]]", string.IsNullOrEmpty(carePlan.EnviromentalCleanNotes) ? "" : carePlan.EnviromentalCleanNotes);
                htmlString = htmlString.Replace("[[EnviromentalLaundryFrequency]]", string.IsNullOrEmpty(carePlan.EnviromentalLaundryFrequency) ? "" : carePlan.EnviromentalLaundryFrequency);
                htmlString = htmlString.Replace("[[EnviromentalLaundryNotes]]", string.IsNullOrEmpty(carePlan.EnviromentalLaundryNotes) ? "" : carePlan.EnviromentalLaundryNotes);
                htmlString = htmlString.Replace("[[EnvironmentalBedLinenFrequency]]", string.IsNullOrEmpty(carePlan.EnvironmentalBedLinenFrequency) ? "" : carePlan.EnvironmentalBedLinenFrequency);
                htmlString = htmlString.Replace("[[EnvironmentalBedLinenNotes]]", string.IsNullOrEmpty(carePlan.EnvironmentalBedLinenNotes) ? "" : carePlan.EnvironmentalBedLinenNotes);
                htmlString = htmlString.Replace("[[EnvironmentalEquipCareFrequency]]", string.IsNullOrEmpty(carePlan.EnvironmentalEquipCareFrequency) ? "" : carePlan.EnvironmentalEquipCareFrequency);
                htmlString = htmlString.Replace("[[EnvironmentalEquipCareNotes]]", string.IsNullOrEmpty(carePlan.EnvironmentalEquipCareNotes) ? "" : carePlan.EnvironmentalEquipCareNotes);
                htmlString = htmlString.Replace("[[EnvironmentalOther]]", string.IsNullOrEmpty(carePlan.EnvironmentalOther) ? "" : carePlan.EnvironmentalOther);
                htmlString = htmlString.Replace("[[EnvironmentalOtherFrequency]]", string.IsNullOrEmpty(carePlan.EnvironmentalOtherFrequency) ? "" : carePlan.EnvironmentalOtherFrequency);
                htmlString = htmlString.Replace("[[EnvironmentalOtherNotes]]", string.IsNullOrEmpty(carePlan.EnvironmentalOtherNotes) ? "" : carePlan.EnvironmentalOtherNotes);
                htmlString = htmlString.Replace("[[MedicationSelfAssistFrequency]]", string.IsNullOrEmpty(carePlan.MedicationSelfAssistFrequency) ? "" : carePlan.MedicationSelfAssistFrequency);
                htmlString = htmlString.Replace("[[MedicationSelfAssistNotes]]", string.IsNullOrEmpty(carePlan.MedicationSelfAssistNotes) ? "" : carePlan.MedicationSelfAssistNotes);
                htmlString = htmlString.Replace("[[MedicationReminderFrequency]]", string.IsNullOrEmpty(carePlan.MedicationReminderFrequency) ? "" : carePlan.MedicationReminderFrequency);
                htmlString = htmlString.Replace("[[MedicationReminderNotes]]", string.IsNullOrEmpty(carePlan.MedicationReminderNotes) ? "" : carePlan.MedicationReminderNotes);
                htmlString = htmlString.Replace("[[ActivityAmbulationFrequency]]", string.IsNullOrEmpty(carePlan.ActivityAmbulationFrequency) ? "" : carePlan.ActivityAmbulationFrequency);
                htmlString = htmlString.Replace("[[ActivityAmbulationNotes]]", string.IsNullOrEmpty(carePlan.ActivityAmbulationNotes) ? "" : carePlan.ActivityAmbulationNotes);
                htmlString = htmlString.Replace("[[ActivityTransferAssistFrequency]]", string.IsNullOrEmpty(carePlan.ActivityTransferAssistFrequency) ? "" : carePlan.ActivityTransferAssistFrequency);
                htmlString = htmlString.Replace("[[ActivityTransferAssistNotes]]", string.IsNullOrEmpty(carePlan.ActivityTransferAssistNotes) ? "" : carePlan.ActivityTransferAssistNotes);
                htmlString = htmlString.Replace("[[ActivityExcerciseFrequency]]", string.IsNullOrEmpty(carePlan.ActivityExcerciseFrequency) ? "" : carePlan.ActivityExcerciseFrequency);
                htmlString = htmlString.Replace("[[ActivityExcerciseNotes]]", string.IsNullOrEmpty(carePlan.ActivityExcerciseNotes) ? "" : carePlan.ActivityExcerciseNotes);
                htmlString = htmlString.Replace("[[ActivityRepositionFrequency]]", string.IsNullOrEmpty(carePlan.ActivityRepositionFrequency) ? "" : carePlan.ActivityRepositionFrequency);
                htmlString = htmlString.Replace("[[ActivityRepositionNotes]]", string.IsNullOrEmpty(carePlan.ActivityRepositionNotes) ? "" : carePlan.ActivityRepositionNotes);
                htmlString = htmlString.Replace("[[ActivityROMFrequency]]", string.IsNullOrEmpty(carePlan.ActivityROMFrequency) ? "" : carePlan.ActivityROMFrequency);
                htmlString = htmlString.Replace("[[ActivityROMNotes]]", string.IsNullOrEmpty(carePlan.ActivityROMNotes) ? "" : carePlan.ActivityROMNotes);
                htmlString = htmlString.Replace("[[ActivityOther1]]", string.IsNullOrEmpty(carePlan.ActivityOther1) ? "" : carePlan.ActivityOther1);
                htmlString = htmlString.Replace("[[ActivityOther1Frequency]]", string.IsNullOrEmpty(carePlan.ActivityOther1Frequency) ? "" : carePlan.ActivityOther1Frequency);
                htmlString = htmlString.Replace("[[ActivityOther1Notes]]", string.IsNullOrEmpty(carePlan.ActivityOther1Notes) ? "" : carePlan.ActivityOther1Notes);
                htmlString = htmlString.Replace("[[ActivityOther2]]", string.IsNullOrEmpty(carePlan.ActivityOther2) ? "" : carePlan.ActivityOther2);
                htmlString = htmlString.Replace("[[ActivityOther2Frequency]]", string.IsNullOrEmpty(carePlan.ActivityOther2Frequency) ? "" : carePlan.ActivityOther2Frequency);
                htmlString = htmlString.Replace("[[ActivityOther2Notes]]", string.IsNullOrEmpty(carePlan.ActivityOther2Notes) ? "" : carePlan.ActivityOther2Notes);
                htmlString = htmlString.Replace("[[AssistiveDevicesOther]]", string.IsNullOrEmpty(carePlan.AssistiveDevicesOther) ? "" : carePlan.AssistiveDevicesOther);
                htmlString = htmlString.Replace("[[PatientName]]", string.IsNullOrEmpty(carePlan.PatientName) ? "" : carePlan.PatientName);
                htmlString = htmlString.Replace("[[PatientAddress]]", string.IsNullOrEmpty(carePlan.PatientAddress) ? "" : carePlan.PatientAddress);
                htmlString = htmlString.Replace("[[PatientGender]]", string.IsNullOrEmpty(carePlan.PatientGender) ? "" : carePlan.PatientGender);
                htmlString = htmlString.Replace("[[AgencyName]]", string.IsNullOrEmpty(carePlan.AgencyName) ? "" : carePlan.AgencyName);
                htmlString = htmlString.Replace("[[AgencyAddress]]", string.IsNullOrEmpty(carePlan.AgencyAddress) ? "" : carePlan.AgencyAddress);
                #endregion parameters binding 

                var bytes = HtmlToByte.ConvertToPDFBytes(htmlString);
                return new Response<CarePlanPDFBytes> { Status = "Success", Data = new CarePlanPDFBytes { Bytes = bytes } };
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
        #endregion
    }
}
