using System;
using System.Collections.Generic;
using System.Text;
using Server.DataAPI;
using Server.LogicAPI.DTOs;
using Server.LogicAPI.Interfaces;
using Server.LogicAPI.Exceptions;
using Server.LogicAPI.Services;

namespace Server.LogicAPI.Services
{
    public class EvidenceEntryService : IEvidenceEntryService
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

        public bool ValidateModel(EvidenceEntryDTO evidenceEntry)
        {
            // it actually is
            if (evidenceEntry is EvidenceEntryDTO)
            {
                if (evidenceEntry.ID < 0)
                    throw new EvidenceEntryInvalidIDException();

                if (!_productService.ValidateModel(evidenceEntry.Product))
                    return false;

                // it should be at least 0
                if (evidenceEntry.ProductAmount < 0)
                    throw new EvidenceEntryInvalidProductAmountException();

                return true;
            }
            throw new ModelIsNotEvidenceEntryException();
        }

        public EvidenceEntryDTO GetEvidenceEntryDTOByID(int id)
        {
            var evidenceEntryDTO = new EvidenceEntryDTO();

            if (_repository.FindEvidenceEntryByID(id) is EvidenceEntry evidenceEntry)
            {
                evidenceEntryDTO.Product = _productService.GetProductDTOByID(evidenceEntry.ID);
                evidenceEntryDTO.ProductAmount = evidenceEntry.ProductAmount;

                return evidenceEntryDTO;
            }
            throw new EvidenceEntryNotFoundException();
        }

        public List<EvidenceEntryDTO> GetAllEvidenceEntryDTOs()
        {
            List<EvidenceEntryDTO> evidenceEntryDTOs = new List<EvidenceEntryDTO>();
            foreach(EvidenceEntry evidenceEntry in _repository.GetAllEntries())
            {
                evidenceEntryDTOs.Add(GetEvidenceEntryDTOByID(evidenceEntry.ID));
            }
            return evidenceEntryDTOs;
        }

        public bool AddEvidenceEntryDTO(EvidenceEntryDTO evidenceEntry)
        {
            if (ValidateModel(evidenceEntry))
            {
                List<EvidenceEntryDTO> evidenceEntryDTOs = GetAllEvidenceEntryDTOs();
                int newID = 0;
                foreach (EvidenceEntryDTO evidenceEntryDTOInList in evidenceEntryDTOs)
                {
                    if (newID == evidenceEntryDTOInList.ID)
                        newID++;
                    else
                        break;
                }

                var evidenceEntryModel = new EvidenceEntry() { ProductID = newID, ProductAmount = evidenceEntry.ProductAmount };
                if (_productService.AddProductDTO(evidenceEntry.Product))
                {
                    ValidateModel(evidenceEntryModel);
                    ChangeEvidenceEntryDTO(newID, evidenceEntry);
                    Logic.InvokeEvidenceEntryChanged();
                    return true;
                }
            }
            return false;
        }

        public bool ChangeEvidenceEntryDTO(int evidenceEntryID, EvidenceEntryDTO evidenceEntryDTO)
        {
            if (_repository.FindEvidenceEntryByID(evidenceEntryID) is EvidenceEntry evidenceEntry)
            {
                if (ValidateModel(evidenceEntryDTO))
                {
                    if (_repository.ChangeProductAmount(evidenceEntryID, evidenceEntryDTO.ProductAmount))
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
