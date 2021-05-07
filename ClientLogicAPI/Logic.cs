using System;
using System.Collections.Generic;
using System.Text;
using Client.LogicAPI.Interfaces;
using Client.LogicAPI.Services;
using Client.DataAPI;

namespace Client.LogicAPI
{
    public static class Logic
    {
        public static event Action ClientsChanged;
        public static event Action EvidenceEntryChanged;
        public static event Action OrdersChanged;
        public static event Action ProductsChanged;

        public static IClientService CreateClientService() => new ClientService(Data.GetRepository());
        public static IEvidenceEntryService CreateEvidenceEntryService() => new EvidenceEntryService(Data.GetRepository());
        public static IOrderService CreateOrderService() => new OrderService(Data.GetRepository());
        public static IProductService CreateProductService() => new ProductService(Data.GetRepository());

        internal static void InvokeClientsChanged() => ClientsChanged?.Invoke();
        internal static void InvokeEvidenceEntryChanged() => EvidenceEntryChanged?.Invoke();
        internal static void InvokeOrdersChanged() => OrdersChanged?.Invoke();
        internal static void InvokeProductsChanged() => ProductsChanged?.Invoke();
    }
}
