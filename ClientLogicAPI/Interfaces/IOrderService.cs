using System.Collections.Generic;
using Client.LogicAPI.DTOs;
using CommunicationAPI.Models;

namespace Client.LogicAPI.Interfaces
{
    public interface IOrderService
    {
        public bool ValidateModel(OrderDTO order);
        public bool ValidateModel(COrder _model);
        public List<OrderDTO> GetAllOrderDTOs();
        public bool AddOrderDTO(OrderDTO order);
        public bool ChangeOrderDTO(int orderID, OrderDTO orderDTO);
        public OrderDTO GetOrderDTOByID(int id);
        public List<OrderDTO> GetOrderDTOsByClientID(int clientID);
        public decimal GetPriceOfOrder(OrderDTO order);
    }
}
