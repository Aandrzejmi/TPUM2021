using System;
using System.Collections.Generic;
using System.Text;
using DataAPI;
using LogicAPI.Interfaces;
using LogicAPI.Exceptions;

namespace LogicAPI
{
    public class EvidenceEntryService : IEvidenceEntryService
    {
        public EvidenceEntryService()
        {

        }

        private readonly IProductService _productService;
        private readonly IRepository _repository;
        public bool ValidateModel(IModel _model)
        {
            // it actually is
            if (_model is EvidenceEntry evidenceEntry)
            {
                if (evidenceEntry.ID < 0)
                    throw new EvidenceEntryInvalidIDException();

                if (_repository.FindEvidenceEntryByID(evidenceEntry.ID) is null)
                    throw new EvidenceEntryNotFoundException();

                if (!_productService.ValidateModel(evidenceEntry.Product))
                    return false;

                // it should be at least 0
                if (evidenceEntry.productAmount < 0)
                    throw new EvidenceEntryInvalidProductAmountException();

                return true;
            }
            throw new ModelIsNotEvidenceEntryException();
        }
    }
}
