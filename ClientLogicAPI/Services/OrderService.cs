using System.Collections.Generic;
using Client.DataAPI;
using Client.LogicAPI.DTOs;
using Client.LogicAPI.Interfaces;
using Client.LogicAPI.Exceptions;
using CommunicationAPI.Models;

namespace Client.LogicAPI.Services
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

        public bool ValidateModel(COrder _model)
        {
            if (_model is COrder order)
            {
                // Maybe find client from repo and find if it actually exists
                if (order.ID < 0)
                    throw new OrderInvalidIDException();

                if (_repository.FindOrderByID(order.ID) is null)
                    throw new OrderNotFoundException();                    

                if (order.Client.ID < 0)
                    throw new OrderInvalidClientIDException();

                foreach (CEvidenceEntry product in order.Entries)
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
            foreach(COrder order in _repository.GetAllOrders())
            {
                orderDTOs.Add(GetOrderDTOByID(order.ID));
            }
            return orderDTOs;
        }

        public OrderDTO GetOrderDTOByID(int id)
        {
            var orderDTO = new OrderDTO();

            if (_repository.FindOrderByID(id) is COrder order)
            {
                orderDTO.ID = order.ID;
                orderDTO.Client = _clientService.GetClientDTOByID(order.Client.ID);
                List<EvidenceEntryDTO> evidenceEntriesDTO = new List<EvidenceEntryDTO>();
                foreach(CEvidenceEntry product in order.Entries )
                {
                    evidenceEntriesDTO.Add(_evidenceEntryService.GetEvidenceEntryDTOByID(product.Product.ID));
                }
                orderDTO.Products = new List<EvidenceEntryDTO>(evidenceEntriesDTO);
                return orderDTO;
            }
            throw new OrderNotFoundException();
        }
        public List<OrderDTO> GetOrderDTOsByClientID(int clientID)
        {
            List<OrderDTO> orderDTOs = new List<OrderDTO>();

            if (_repository.FindOrdersByClientID(clientID) is List<COrder> orders && orders.Count > 0)
            {
                foreach(COrder order in orders)
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
                var orderModel = new COrder();

                List<OrderDTO> orderDTOs = new List<OrderDTO>();
                orderDTOs = GetAllOrderDTOs();
                int newID = 0;
                foreach(OrderDTO orderDTOListObject in orderDTOs)
                {
                    if (newID == orderDTOListObject.ID)
                        newID++;
                    else
                        break;
                }

                orderModel.ID = newID;
                orderModel.Client.ID = order.Client.ID;
                orderModel.Client.Name = order.Client.Name;
                orderModel.Client.Adress = order.Client.Adress;
                orderModel.Entries = new List<CEvidenceEntry>();

                foreach(EvidenceEntryDTO entryDTO in order.Products)
                {
                    var evidenceEntryModel = new CEvidenceEntry();
                    evidenceEntryModel.Product.ID = entryDTO.Product.ID;
                    evidenceEntryModel.Product.Name = entryDTO.Product.Name;
                    evidenceEntryModel.Product.Price = entryDTO.Product.Price;
                    evidenceEntryModel.Amount = entryDTO.ProductAmount;

                    _evidenceEntryService.ValidateModel(evidenceEntryModel);
                    if (_repository.FindProductByID(evidenceEntryModel.Product.ID) is null)
                        throw new ProductNotFoundException();

                    orderModel.Entries.Add(evidenceEntryModel);
                }
                if (_repository.AddOrder(orderModel))
                {
                    Logic.InvokeOrdersChanged();
                    return true;
                }

            }
            return false;
        }

        public bool ChangeOrderDTO(int orderID, OrderDTO orderDTO)
        {
            if (_repository.FindOrderByID(orderID) is COrder order)
            {
                if (ValidateModel(orderDTO))
                {
                    order.Client.ID = orderDTO.Client.ID;
                    order.Client.Name = orderDTO.Client.Name;
                    order.Client.Adress = orderDTO.Client.Adress;
                    order.Entries = new List<CEvidenceEntry>();
                    foreach(EvidenceEntryDTO evidenceEntryDTO in orderDTO.Products)
                    {
                        order.Entries.Add(new CEvidenceEntry()
                        { 
                            Product = new CProduct() 
                            { 
                                ID = evidenceEntryDTO.Product.ID,
                                Name = evidenceEntryDTO.Product.Name,
                                Price = evidenceEntryDTO.Product.Price 
                            },
                            Amount = evidenceEntryDTO.ProductAmount 
                        });
                    }
                    if (_repository.ModifyOrder(order))
                    {
                        Logic.InvokeOrdersChanged();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                throw new OrderNotFoundException();
            }
        }
    }
}
