using System;
using System.Collections.Generic;
using System.Text;
using LogicAPI.DTOs;

namespace LogicAPI.Interfaces
{
    public interface IEvidenceEntryService : IService
    {
        public bool ValidateModel(EvidenceEntryDTO evidenceEntry);
        public bool AddEvidenceEntryDTO(EvidenceEntryDTO evidenceEntry);
        public bool ChangeEvidenceEntryDTO(int evidenceEntryID, EvidenceEntryDTO evidenceEntryDTO);
        public List<EvidenceEntryDTO> GetAllEvidenceEntryDTOs();
        public EvidenceEntryDTO GetEvidenceEntryDTOByID(int id);
    }
}
