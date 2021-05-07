using System;
using System.Collections.Generic;
using System.Text;
using Server.LogicAPI.Interfaces;
using Server.LogicAPI.Services;
using Server.DataAPI;

namespace Server.LogicAPI
{
    public static class Logic
    {
        public static event Action ClientsChanged;
        public static event Action EvidenceEntryChanged;
        public static event Action OrdersChanged;
        public static event Action ProductsChanged;

        public static IClientService CreateClientService() => new ClientService(Data.Repository);
        public static IEvidenceEntryService CreateEvidenceEntryService() => new EvidenceEntryService(Data.Repository);
        public static IOrderService CreateOrderService() => new OrderService(Data.Repository);
        public static IProductService CreateProductService() => new ProductService(Data.Repository);

        internal static void InvokeClientsChanged() => ClientsChanged?.Invoke();
        internal static void InvokeEvidenceEntryChanged() => EvidenceEntryChanged?.Invoke();
        internal static void InvokeOrdersChanged() => OrdersChanged?.Invoke();
        internal static void InvokeProductsChanged() => ProductsChanged?.Invoke();
    }
}
