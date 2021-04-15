using System;
using System.Collections.Generic;
using System.Text;
using DataAPI;

namespace LogicAPI
{
    public interface IOrderService : IService
    {
        public decimal GetPriceOfOrder(Order order);
    }
}
