using System;
using DataAPI;
using LogicAPI.Interfaces;
using LogicAPI.Exceptions;
using LogicAPI.Services;

namespace LogicAPI
{
    public class OrderService : IOrderService
    {
        private readonly IRepository _repository;
        private readonly IEvidenceEntryService _evidenceEntryService;
        private readonly IClientService _clientService;
        
        public OrderService(IRepository repository)
        {
            _repository = repository;
            _evidenceEntryService = new EvidenceEntryService(repository);
            _clientService = new ClientService(repository);
        }

        public bool ValidateModel(IModel _model )
        {
            if (_model is Order order)
            {
                // Maybe find client from repo and find if it actually exists
                if (order.ID < 0)
                    throw new OrderInvalidIDException();

                if (_repository.FindOrderByID(order.ID) is null)
                    throw new OrderNotFoundException();                    

                if (order.ClientID < 0)
                    throw new OrderInvalidClientIDException();

                if (_repository.FindClientByID(order.ClientID) is Client client)
                    _clientService.ValidateModel(client);
                else
                    throw new OrderClientNotFoundException();

                

                foreach (DataAPI.EvidenceEntry entry in order.Products)
                {
                    if (!_evidenceEntryService.ValidateModel(entry))
                        return false;
                }
                return true;
            }
            throw new ModelIsNotOrderException();
        }
        public decimal GetPriceOfOrder(Order order)
        {
            decimal sum = 0.0M;
            foreach (var item in order.Products)
            {
                int itemCount = item.productAmount;
                sum += (itemCount * item.Product.Price);
            }
            return sum;
        }
    }
}
