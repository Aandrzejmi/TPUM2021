using System;
using System.Collections.Generic;
using System.Text;
using LogicAPI.DTOs;

namespace LogicAPI
{
    public interface IOrderService : IService
    {
        public List<OrderDTO> GetAllOrderDTOs();
        public OrderDTO GetOrderDTOByID(int id);
        public List<OrderDTO> GetOrderDTOsByClientID(int clientID);
        public decimal GetPriceOfOrder(OrderDTO order);
    }
}
