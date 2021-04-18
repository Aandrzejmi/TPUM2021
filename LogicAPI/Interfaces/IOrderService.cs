using System;
using System.Collections.Generic;
using System.Text;
using LogicAPI.DTOs;

namespace LogicAPI.Interfaces
{
    public interface IOrderService : IService
    {
        public bool ValidateModel(OrderDTO order);
        public List<OrderDTO> GetAllOrderDTOs();
        public bool AddOrderDTO(OrderDTO order);
        public OrderDTO GetOrderDTOByID(int id);
        public List<OrderDTO> GetOrderDTOsByClientID(int clientID);
        public decimal GetPriceOfOrder(OrderDTO order);
    }
}
