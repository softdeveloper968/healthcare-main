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

namespace Services
{
    public class NurseInboxService : INurseInboxService
    {
        private INurseInboxRepository _nurseInboxRepository;
        private IMapper _mapper;

        #region Constructor for NurseInboxService
        public NurseInboxService(INurseInboxRepository nurseInboxRepository, IMapper mapper)
        {
            _nurseInboxRepository = nurseInboxRepository;
            _mapper = mapper;
        }
        #endregion

        #region Implementing Interface
        public async Task<bool> AddMessage(NurseInbox model)
        {
            return await _nurseInboxRepository.AddMessage(model);
        }

        public async Task<List<GetNurseMessagesResponseModel>> GetNurseMessages(string nurseId)
        {
            return _mapper.Map<List<GetNurseMessagesResponseModel>>(await _nurseInboxRepository.GetNurseMessages(nurseId));
        }

        public async Task<bool> ReadMessage(ReadMessageViewModel model)
        {
            var message = await _nurseInboxRepository.GetById(model.Id);
            if (message == null)
                return false;
            message.IsRead = model.IsRead;
            return await _nurseInboxRepository.UpdateMessage(message);
        }

        public async Task<bool> DeleteMessage(int id)
        {
            var message = await _nurseInboxRepository.GetById(id);
            if (message == null)
                return false;
            return await _nurseInboxRepository.DeleteMessage(message);
        }
        #endregion
    }
}
