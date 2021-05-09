using System;
using System.Collections.Generic;
using System.Text;
using CommunicationAPI.Models;

namespace Server.LogicAPI.Interfaces
{
    public interface IEvidenceEntryService : IService
    {
        public bool ValidateModel(CEvidenceEntry evidenceEntry);
        public bool AddEvidenceEntry(CEvidenceEntry evidenceEntry);
        public bool ChangeEvidenceEntry(int evidenceEntryID, CEvidenceEntry evidenceEntryDTO);
        public List<CEvidenceEntry> GetAllEvidenceEntries();
        public CEvidenceEntry GetEvidenceEntryByID(int id);
    }
}
