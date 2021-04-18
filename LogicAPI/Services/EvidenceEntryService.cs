using System;
using System.Collections.Generic;
using System.Text;
using DataAPI;
using LogicAPI.DTOs;
using LogicAPI.Interfaces;
using LogicAPI.Exceptions;
using LogicAPI.Services;

namespace LogicAPI.Services
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

                _productService.ValidateModel(_repository.FindProductByID(evidenceEntry.ProductID));

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
                var evidenceEntryModel = new EvidenceEntry() { ProductID = evidenceEntry.Product.ID , ProductAmount = evidenceEntry.ProductAmount };
                ValidateModel(evidenceEntryModel);
                if (_repository.AddEvidenceEntry(evidenceEntryModel))
                    return true;
            }
            return false;
        }
    }
}
