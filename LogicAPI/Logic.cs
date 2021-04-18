using System;
using System.Collections.Generic;
using System.Text;
using LogicAPI.Interfaces;
using LogicAPI.Services;
using DataAPI;

namespace LogicAPI
{
    public static class Logic
    {
        public static IProductService CreateProductService()
        {
            return new ProductService(Data.GetRepository());
        }

        public static IOrderService CreateOrderService()
        {
            return new OrderService(Data.GetRepository());
        }

        public static IClientService CreateClientService()
        {
            return new ClientService(Data.GetRepository());
        }

        public static IEvidenceEntryService CreateEvidenceEntryService()
        {
            return new EvidenceEntryService(Data.GetRepository());
        }
    }
}
