using System.Collections.Generic;
using Server.DataAPI;
using CommunicationAPI.Models;
using Server.LogicAPI.Interfaces;
using Server.LogicAPI.Exceptions;

namespace Server.LogicAPI.Services
{
    internal class OrderService : IOrderService
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

        public bool ValidateModel(COrder order)
        {
            if (order is COrder)
            {
                // Maybe find client from repo and find if it actually exists
                if (order.ID < 0)
                    throw new OrderInvalidIDException();

                if (_repository.FindOrderByID(order.ID) is null)
                    throw new OrderNotFoundException();

                if (order.Client.ID < 0)
                    throw new OrderInvalidClientIDException();

                if (order.Client is CClient client)
                    _clientService.ValidateModel(client);
                else
                    throw new OrderClientNotFoundException();

                foreach (CEvidenceEntry product in order.Entries)
                {
                    if (!_evidenceEntryService.ValidateModel(product))
                        return false;
                }
                return true;
            }
            throw new ModelIsNotOrderException();
        }

        public List<COrder> GetAllOrders()
        {
            List<COrder> cOrders = new List<COrder>();
            foreach(Order order in _repository.GetAllOrders())
            {
                cOrders.Add(GetOrderByID(order.ID));
            }
            return cOrders;
        }

        public COrder GetOrderByID(int id)
        {
            var cOrder = new COrder();

            if (_repository.FindOrderByID(id) is Order order)
            {
                cOrder.ID = order.ID;
                cOrder.Client = _clientService.GetClientByID(order.ClientID);
                List<CEvidenceEntry> cEvEntries = new List<CEvidenceEntry>();
                foreach(EvidenceEntry product in order.Products )
                {
                    cEvEntries.Add(_evidenceEntryService.GetEvidenceEntryByID(product.ID));
                }
                cOrder.Entries = new List<CEvidenceEntry>(cEvEntries);
                return cOrder;
            }
            throw new OrderNotFoundException();
        }
        public List<COrder> GetOrdersByClientID(int clientID)
        {
            List<COrder> cOrders = new List<COrder>();

            if (_repository.FindOrdersByClientID(clientID) is List<Order> orders && orders.Count > 0)
            {
                foreach(Order order in orders)
                {
                    cOrders.Add(GetOrderByID(order.ID));
                }
                return cOrders;
            }
            throw new OrderNotFoundException();
        }

        public decimal GetPriceOfOrder(COrder order)
        {
            decimal sum = 0.0M;
            foreach (var item in order.Entries)
            {
                int itemCount = item.Amount;
                sum += (itemCount * item.Product.Price);
            }
            return sum;
        }

        public bool AddOrder(COrder order)
        {
            if (ValidateModel(order))
            {
                var orderModel = new Order();

                List<COrder> cOrders = new List<COrder>();
                cOrders = GetAllOrders();
                int newID = 0;
                foreach(COrder cOrderListObject in cOrders)
                {
                    if (newID == cOrderListObject.ID)
                        newID++;
                    else
                        break;
                }

                orderModel.ID = newID;
                orderModel.ClientID = order.Client.ID;
                orderModel.Products = new List<EvidenceEntry>();

                foreach(CEvidenceEntry cEvEntries in order.Entries)
                {
                    var evidenceEntryModel = new EvidenceEntry();
                    evidenceEntryModel.ProductID = cEvEntries.Product.ID;
                    evidenceEntryModel.ProductAmount = cEvEntries.Amount;

                    _evidenceEntryService.ValidateModel(evidenceEntryModel);
                    if (_repository.FindProductByID(evidenceEntryModel.ProductID) is null)
                        throw new ProductNotFoundException();

                    orderModel.Products.Add(evidenceEntryModel);
                }
                if (_repository.AddOrder(orderModel))
                {
                    Logic.InvokeOrdersChanged();
                    return true;
                }

            }
            return false;
        }

        public bool ChangeOrder(int orderID, COrder cOrder)
        {
            if (_repository.FindOrderByID(orderID) is Order order)
            {
                if (ValidateModel(cOrder))
                {
                    order.ClientID = cOrder.Client.ID;
                    order.Products = new List<EvidenceEntry>();
                    foreach(CEvidenceEntry cEvEntry in cOrder.Entries)
                    {
                        order.Products.Add(new EvidenceEntry() { ProductID = cEvEntry.Product.ID, ProductAmount = cEvEntry.Amount });
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
