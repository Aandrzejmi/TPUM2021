using System;
using System.Collections.Generic;
using DataAPI;
using LogicAPI.DTOs;
using LogicAPI.Interfaces;
using LogicAPI.Exceptions;
using LogicAPI.Services;

namespace LogicAPI.Services
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

                foreach (EvidenceEntry product in order.Products)
                {
                    if (!_evidenceEntryService.ValidateModel(product))
                        return false;
                }
                return true;
            }
            throw new ModelIsNotOrderException();
        }

        public bool ValidateModel(OrderDTO order)
        {
            if (order is OrderDTO)
            {
                // Maybe find client from repo and find if it actually exists
                if (order.ID < 0)
                    throw new OrderInvalidIDException();

                if (_repository.FindOrderByID(order.ID) is null)
                    throw new OrderNotFoundException();

                if (order.Client.ID < 0)
                    throw new OrderInvalidClientIDException();

                if (order.Client is ClientDTO client)
                    _clientService.ValidateModel(client);
                else
                    throw new OrderClientNotFoundException();

                foreach (EvidenceEntryDTO product in order.Products)
                {
                    if (!_evidenceEntryService.ValidateModel(product))
                        return false;
                }
                return true;
            }
            throw new ModelIsNotOrderException();
        }

        public List<OrderDTO> GetAllOrderDTOs()
        {
            List<OrderDTO> orderDTOs = new List<OrderDTO>();
            foreach(Order order in _repository.GetAllOrders())
            {
                orderDTOs.Add(GetOrderDTOByID(order.ID));
            }
            return orderDTOs;
        }

        public OrderDTO GetOrderDTOByID(int id)
        {
            var orderDTO = new OrderDTO();

            if (_repository.FindOrderByID(id) is Order order)
            {
                orderDTO.ID = order.ID;
                orderDTO.Client = _clientService.GetClientDTOByID(order.ClientID);
                List<EvidenceEntryDTO> evidenceEntriesDTO = new List<EvidenceEntryDTO>();
                foreach(EvidenceEntry product in order.Products )
                {
                    evidenceEntriesDTO.Add(_evidenceEntryService.GetEvidenceEntryDTOByID(product.ID));
                }
                orderDTO.Products = new List<EvidenceEntryDTO>(evidenceEntriesDTO);
                return orderDTO;
            }
            throw new OrderNotFoundException();
        }
        public List<OrderDTO> GetOrderDTOsByClientID(int clientID)
        {
            List<OrderDTO> orderDTOs = new List<OrderDTO>();

            if (_repository.FindOrdersByClientID(clientID) is List<Order> orders && orders.Count > 0)
            {
                foreach(Order order in orders)
                {
                    orderDTOs.Add(GetOrderDTOByID(order.ID));
                }
                return orderDTOs;
            }
            throw new OrderNotFoundException();
        }

        public decimal GetPriceOfOrder(OrderDTO order)
        {
            decimal sum = 0.0M;
            foreach (var item in order.Products)
            {
                int itemCount = item.ProductAmount;
                sum += (itemCount * item.Product.Price);
            }
            return sum;
        }

        public bool AddOrderDTO(OrderDTO order)
        {
            if (ValidateModel(order))
            {
                var orderModel = new Order();

                orderModel.ID = order.ID;
                orderModel.ClientID = order.Client.ID;

                foreach(EvidenceEntryDTO entryDTO in order.Products)
                {
                    var evidenceEntryModel = new EvidenceEntry();
                    evidenceEntryModel.ID = entryDTO.ID;
                    evidenceEntryModel.ProductID = entryDTO.Product.ID;
                    evidenceEntryModel.ProductAmount = entryDTO.ProductAmount;

                    _evidenceEntryService.ValidateModel(evidenceEntryModel);

                    if (_repository.FindProductByID(evidenceEntryModel.ProductID) is null)
                        throw new ProductNotFoundException();

                    orderModel.Products.Add(evidenceEntryModel);
                }
                if (_repository.AddOrder(orderModel))
                    return true;

            }
            return false;
        }
    }
}
