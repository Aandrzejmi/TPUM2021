using System;
using System.Collections.Generic;
using System.Text;
using Client.LogicAPI.DTOs;
using CommunicationAPI.Models;

namespace Client.LogicAPI.Interfaces
{
    public interface IEvidenceEntryService
    {
        public bool ValidateModel(EvidenceEntryDTO evidenceEntry);
        public bool ValidateModel(CEvidenceEntry _model);
        public bool AddEvidenceEntryDTO(EvidenceEntryDTO evidenceEntry);
        public bool ChangeEvidenceEntryDTO(int evidenceEntryID, EvidenceEntryDTO evidenceEntryDTO);
        public List<EvidenceEntryDTO> GetAllEvidenceEntryDTOs();
        public EvidenceEntryDTO GetEvidenceEntryDTOByID(int id);
    }
}
