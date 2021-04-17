using System;
using System.Collections.Generic;
using System.Text;
using DataAPI.DTOs;

namespace LogicAPI
{
    public interface IOrderService : IService
    {
        public OrderDTO GetOrderDTOByID(int id);
        public List<OrderDTO> GetOrdersDTOByClientID(int clientID);
        public decimal GetPriceOfOrder(OrderDTO order);
    }
}
