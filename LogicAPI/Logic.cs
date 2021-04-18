using System;
using System.Collections.Generic;
using System.Threading;
using LogicAPI.Interfaces;
using LogicAPI.Services;
using LogicAPI.DTOs;
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

        public static void NewThreadAddClient()
        {
            IClientService clientService = CreateClientService();
            System.Console.WriteLine("Waiting 5 seconds before adding new client");
            Thread.Sleep(5000);
            ClientDTO clientDTO1 = new ClientDTO() { ID = 0, Adress = "Temporary Adress", Name = "Temporary Name" };
            clientService.AddClientDTO(clientDTO1);
            while (true)
            {
                System.Console.WriteLine("Waiting 30 seconds before adding new client");
                ClientDTO clientDTO2 = new ClientDTO() { ID = 0, Adress = "Latter adress", Name = "Latter Name" };
                Thread.Sleep(30000);
                clientService.AddClientDTO(clientDTO2);
            }
        }
    }
}
