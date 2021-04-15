using System;
using DataAPI;
using LogicAPI.Interfaces;
using LogicAPI.Exceptions;

namespace LogicAPI
{
    public class OrderService : IOrderService
    {
        private readonly IRepository _repository;
        private readonly IEvidenceEntryService _evidenceEntryService;
        
        public OrderService()
        {

        }

        public bool ValidateModel(IModel _model )
        {
            if (_model is Order order)
            {
                // Maybe find client from repo and find if it actually exists
                if (order.ID > 0)
                {
                    if (_repository.FindOrderByID(order.ID) is null)
                        throw new OrderNotFoundException();
                }
                else
                    throw new OrderInvalidIDException();

                if (order.ClientID > 0)
                {
                    if (_repository.FindClientByID(order.ClientID) is null)
                        throw new OrderClientNotFoundException();
                }
                else
                    throw new OrderInvalidClientIDException();
                    

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
