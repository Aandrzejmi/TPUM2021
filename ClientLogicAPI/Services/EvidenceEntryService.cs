using System.Collections.Generic;
using Client.DataAPI;
using Client.LogicAPI.DTOs;
using Client.LogicAPI.Interfaces;
using Client.LogicAPI.Exceptions;
using CommunicationAPI.Models;
using static CommunicationAPI.Serialization;

namespace Client.LogicAPI.Services
{
    public class EvidenceEntryService : IEvidenceEntryService
    {
        
        private readonly IProductService _productService;
        private readonly IConnectionService connectionService;
        private readonly IRepository _repository;
        public EvidenceEntryService(IRepository repository)
        {
            _repository = repository;
            _productService = new ProductService(repository);
            connectionService = Logic.CreateConnectionService();
        }

        public bool ValidateModel(CEvidenceEntry _model)
        {
            // it actually is
            if (_model is CEvidenceEntry evidenceEntry)
            {
                if (evidenceEntry.Product.ID < 0)
                    throw new EvidenceEntryInvalidIDException();

                // it should be at least 0
                if (evidenceEntry.Amount < 0)
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

            if (_repository.FindEvidenceEntryByID(id) is CEvidenceEntry evidenceEntry)
            {
                evidenceEntryDTO.Product = _productService.GetProductDTOByID(evidenceEntry.Product.ID);
                evidenceEntryDTO.ProductAmount = evidenceEntry.Amount;

                return evidenceEntryDTO;
            }
            throw new EvidenceEntryNotFoundException();
        }

        public List<EvidenceEntryDTO> GetAllEvidenceEntryDTOs()
        {
            List<EvidenceEntryDTO> evidenceEntryDTOs = new List<EvidenceEntryDTO>();
            foreach(CEvidenceEntry evidenceEntry in _repository.GetAllEntries())
            {
                evidenceEntryDTOs.Add(GetEvidenceEntryDTOByID(evidenceEntry.Product.ID));
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

                var evidenceEntryModel = new CEvidenceEntry() 
                { 
                    Product = new CProduct 
                    { 
                        ID = newID,
                        Name = evidenceEntry.Product.Name,
                        Price = evidenceEntry.Product.Price,
                    }, 
                    Amount = evidenceEntry.ProductAmount 
                };
                if (_productService.AddProductDTO(evidenceEntry.Product))
                {
                    ValidateModel(evidenceEntryModel);
                    ChangeEvidenceEntryDTO(newID, evidenceEntry);
                    connectionService.SendTask($"add#entry#{Serialize<CEvidenceEntry>(evidenceEntryModel)}");
                    Logic.InvokeEvidenceEntryChanged();
                    return true;
                }
            }
            return false;
        }

        public bool ChangeEvidenceEntryDTO(int evidenceEntryID, EvidenceEntryDTO evidenceEntryDTO)
        {
            if (_repository.FindEvidenceEntryByID(evidenceEntryID) is CEvidenceEntry evidenceEntry)
            {
                if (ValidateModel(evidenceEntryDTO))
                {
                    if (_repository.ChangeProductAmount(evidenceEntryID, evidenceEntryDTO.ProductAmount))
                    {
                        connectionService.SendTask($"update#entry#{evidenceEntryID}#{Serialize<CEvidenceEntry>(evidenceEntry)}");
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
