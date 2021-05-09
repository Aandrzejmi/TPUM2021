using System.Collections.Generic;
using Server.DataAPI;
using CommunicationAPI.Models;
using Server.LogicAPI.Interfaces;
using Server.LogicAPI.Exceptions;

namespace Server.LogicAPI.Services
{
    internal class EvidenceEntryService : IEvidenceEntryService
    {
        
        private readonly IProductService _productService;
        private readonly IRepository _repository;
        public EvidenceEntryService(IRepository repository)
        {
            _repository = repository;
            _productService = new ProductService(repository);
        }

        public bool ValidateModel(IModel _model)
        {
            // it actually is
            if (_model is EvidenceEntry evidenceEntry)
            {
                if (evidenceEntry.ID < 0)
                    throw new EvidenceEntryInvalidIDException();

                // it should be at least 0
                if (evidenceEntry.ProductAmount < 0)
                    throw new EvidenceEntryInvalidProductAmountException();

                return true;
            }
            throw new ModelIsNotEvidenceEntryException();
        }

        public bool ValidateModel(CEvidenceEntry evidenceEntry)
        {
            // it actually is
            if (evidenceEntry is CEvidenceEntry)
            {
                if (evidenceEntry.Product.ID < 0)
                    throw new EvidenceEntryInvalidIDException();

                if (!_productService.ValidateModel(evidenceEntry.Product))
                    return false;

                // it should be at least 0
                if (evidenceEntry.Amount < 0)
                    throw new EvidenceEntryInvalidProductAmountException();

                return true;
            }
            throw new ModelIsNotEvidenceEntryException();
        }

        public CEvidenceEntry GetEvidenceEntryByID(int id)
        {
            var cEvidenceEntry = new CEvidenceEntry();

            if (_repository.FindEvidenceEntryByID(id) is EvidenceEntry evidenceEntry)
            {
                cEvidenceEntry.Product = _productService.GetProductByID(evidenceEntry.ID);
                cEvidenceEntry.Amount = evidenceEntry.ProductAmount;

                return cEvidenceEntry;
            }
            throw new EvidenceEntryNotFoundException();
        }

        public List<CEvidenceEntry> GetAllEvidenceEntries()
        {
            List<CEvidenceEntry> cEvidenceEntrys = new List<CEvidenceEntry>();
            foreach(EvidenceEntry evidenceEntry in _repository.GetAllEntries())
            {
                cEvidenceEntrys.Add(GetEvidenceEntryByID(evidenceEntry.ID));
            }
            return cEvidenceEntrys;
        }

        public bool AddEvidenceEntry(CEvidenceEntry evidenceEntry)
        {
            if (ValidateModel(evidenceEntry))
            {
                List<CEvidenceEntry> cEvidenceEntrys = GetAllEvidenceEntries();
                int newID = 0;
                foreach (CEvidenceEntry cEvidenceEntryInList in cEvidenceEntrys)
                {
                    if (newID == cEvidenceEntryInList.Product.ID)
                        newID++;
                    else
                        break;
                }

                var evidenceEntryModel = new EvidenceEntry() { ProductID = newID, ProductAmount = evidenceEntry.Amount };
                if (_productService.AddProduct(evidenceEntry.Product))
                {
                    ValidateModel(evidenceEntryModel);
                    ChangeEvidenceEntry(newID, evidenceEntry);
                    Logic.InvokeEvidenceEntryChanged();
                    return true;
                }
            }
            return false;
        }

        public bool ChangeEvidenceEntry(int evidenceEntryID, CEvidenceEntry cEvidenceEntry)
        {
            if (_repository.FindEvidenceEntryByID(evidenceEntryID) is EvidenceEntry evidenceEntry)
            {
                if (ValidateModel(cEvidenceEntry))
                {
                    if (_repository.ChangeProductAmount(evidenceEntryID, cEvidenceEntry.Amount))
                    {
                        Logic.InvokeEvidenceEntryChanged();
                        return true;
                    }
                    else
                        return false;
                }
                else
                    return false;
            }
            else
                throw new EvidenceEntryNotFoundException();
        }
    }
}
