using System.Collections.Generic;
using CommunicationAPI.Models;

namespace Server.LogicAPI.Interfaces
{
    public interface IOrderService : IService
    {
        public bool ValidateModel(COrder order);
        public List<COrder> GetAllOrders();
        public bool AddOrder(COrder order);
        public bool ChangeOrder(int orderID, COrder orderDTO);
        public COrder GetOrderByID(int id);
        public List<COrder> GetOrdersByClientID(int clientID);
        public decimal GetPriceOfOrder(COrder order);
    }
}
