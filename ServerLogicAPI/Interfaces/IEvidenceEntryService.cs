using System.Collections.Generic;
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
