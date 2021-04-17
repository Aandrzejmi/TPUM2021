using System;
using System.Collections.Generic;
using System.Text;
using LogicAPI.DTOs;

namespace LogicAPI
{
    public interface IEvidenceEntryService : IService
    {
        public List<EvidenceEntryDTO> GetAllEvidenceEntryDTOs();
        public EvidenceEntryDTO GetEvidenceEntryDTOByID(int id);
    }
}
