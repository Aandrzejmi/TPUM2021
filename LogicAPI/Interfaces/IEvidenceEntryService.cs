using System;
using System.Collections.Generic;
using System.Text;
using DataAPI.DTOs;

namespace LogicAPI
{
    public interface IEvidenceEntryService : IService
    {
        public EvidenceEntryDTO GetEvidenceEntryDTOByID(int id);
    }
}
