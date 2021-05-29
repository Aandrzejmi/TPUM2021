using NUnit.Framework;
using Client.LogicAPI.Interfaces;
using Client.DataAPI;
using Server.DataAPI;
using Server.App;
using System;
using CommunicationAPI.Models;
using static CommunicationAPI.Serialization;
using System.Threading.Tasks;
using Client.LogicAPI.DTOs;
using System.Threading;

namespace IntegrationTests
{
    public class IntegrationTest
    {
        Action<string> consoleLogger = Console.WriteLine;

        CommunicationManager node;
        IConnectionService connectionService;
        IProductService productService;
        IClientService clientService;
        IEvidenceEntryService evidenceEntryService;
        IOrderService orderService;
        Client.DataAPI.IRepository clientRepository;
        Server.DataAPI.IRepository serverRepository;
        [SetUp]
        public void Setup()
        {
            Client.DataAPI.Data.ClearContext();
            Server.DataAPI.Data.ClearRepository();
            clientRepository = Client.DataAPI.Data.GetRepository();
            serverRepository = Server.DataAPI.Data.Repository;
            connectionService = Client.LogicAPI.Logic.CreateConnectionService();
            productService = Client.LogicAPI.Logic.CreateProductService();
            clientService = Client.LogicAPI.Logic.CreateClientService();
            evidenceEntryService = Client.LogicAPI.Logic.CreateEvidenceEntryService();
            orderService = Client.LogicAPI.Logic.CreateOrderService();

            var task1 = Task.Run(async () =>
            {
                node = new CommunicationManager(8081, consoleLogger);
                await node.InitServerAsync();

            });
            var task2 = Task.Run(async () =>
            {
                await connectionService.CreateConnection();

            });
            task2.Wait();
        }

        [Test]
        public void BasicCommunicationTest()
        {
            Assert.AreEqual(0, clientRepository.GetAllProducts().Count);
            Assert.AreEqual(4, serverRepository.GetAllProducts().Count);

            var task3 = Task.Run(async () =>
            {
                var taskA = connectionService.SendTask(Serialize(new CSendRequest()
                { Type = typeof(CProduct).ToString(), RequestedID = null }));
                taskA.Wait();

            });
            task3.Wait();
            Thread.Sleep(250);
            Assert.AreNotEqual(0, clientRepository.GetAllProducts().Count);
        }

        [Test]
        public void AddClientIntegrationTest()
        {
            var task3 = Task.Run(async () =>
            {
                var taskA = connectionService.SendTask(Serialize(new CSendRequest()
                { Type = typeof(CClient).ToString(), RequestedID = null }));
                taskA.Wait();

            });
            task3.Wait();
            Thread.Sleep(100);

            Assert.AreEqual(4, clientRepository.CountClients);
            Assert.AreEqual(4, serverRepository.CountClients);

            var clientModel = new ClientDTO();
            clientModel.ID = 0;
            clientModel.Name = "IntegrationName";
            clientModel.Adress = "IntegrationAdress";

            var task = Task.Run(async () =>
            {
                Assert.IsTrue(clientService.AddClientDTO(clientModel));
            });
            Thread.Sleep(100);

            Assert.AreEqual(5, clientRepository.CountClients);
            Assert.AreEqual(5, serverRepository.CountClients);
            Assert.AreEqual(serverRepository.FindClientByID(4).Name, "IntegrationName");

            clientModel.Name = "OtherName";
            var task2 = Task.Run(async () =>
            {
                Assert.IsTrue(clientService.ChangeClientDTO(4, clientModel));
            });
            Thread.Sleep(100);
            Assert.AreEqual(clientRepository.FindClientByID(4).Name, "OtherName");
            Assert.AreEqual(serverRepository.FindClientByID(4).Name, "OtherName");
        }

        [Test]
        public void AddProductIntegrationTest()
        {
            var task3 = Task.Run(async () =>
            {
                var taskA = connectionService.SendTask(Serialize(new CSendRequest()
                { Type = typeof(CProduct).ToString(), RequestedID = null }));
                taskA.Wait();

            });
            task3.Wait();
            Thread.Sleep(100);

            Assert.AreEqual(4, clientRepository.CountProducts);
            Assert.AreEqual(4, serverRepository.CountProducts);

            var productModel = new ProductDTO();
            productModel.ID = 0;
            productModel.Name = "IntegrationName";
            productModel.Price = 30;

            var task = Task.Run(async () =>
            {
                Assert.IsTrue(productService.AddProductDTO(productModel));
            });
            Thread.Sleep(100);

            Assert.AreEqual(5, clientRepository.CountProducts);
            Assert.AreEqual(5, serverRepository.CountProducts);
            Assert.AreEqual(serverRepository.FindProductByID(4).Name, "IntegrationName");

            productModel.Name = "OtherName";
            var task2 = Task.Run(async () =>
            {
                Assert.IsTrue(productService.ChangeProductDTO(4, productModel));
            });
            Thread.Sleep(100);
            Assert.AreEqual(clientRepository.FindProductByID(4).Name, "OtherName");
            Assert.AreEqual(serverRepository.FindProductByID(4).Name, "OtherName");


            Assert.AreEqual(clientRepository.FindProductByID(4).Price, 30);
            Assert.AreEqual(serverRepository.FindProductByID(4).Price, 30);
            productModel.Price = 40;
            var task4 = Task.Run(async () =>
            {
                Assert.IsTrue(productService.ChangeProductDTO(4, productModel));
            });
            Thread.Sleep(100);
            Assert.AreEqual(clientRepository.FindProductByID(4).Price, 40);
            Assert.AreEqual(serverRepository.FindProductByID(4).Price, 40);
        }

        [Test]
        public void AddEvidenceEntryIntegrationTest()
        {
            var task3 = Task.Run(async () =>
            {
                var taskA = connectionService.SendTask(Serialize(new CSendRequest()
                { Type = typeof(CProduct).ToString(), RequestedID = null }));
                taskA.Wait();

            });
            Thread.Sleep(100);
            var task4 = Task.Run(async () =>
            {
                var taskA = connectionService.SendTask(Serialize(new CSendRequest()
                { Type = typeof(CEvidenceEntry).ToString(), RequestedID = null }));
                taskA.Wait();

            });
            task4.Wait();

            Thread.Sleep(100);
            Assert.AreEqual(4, serverRepository.CountProductEntries);
            Assert.AreEqual(4, clientRepository.CountProductEntries);


            var productModel = new ProductDTO();
            productModel.ID = 0;
            productModel.Name = "IntegrationName";
            productModel.Price = 30;

            var task = Task.Run(async () =>
            {
                Assert.IsTrue(productService.AddProductDTO(productModel));
            });
            Thread.Sleep(100);

            Assert.AreEqual(5, clientRepository.CountProductEntries);
            Assert.AreEqual(5, serverRepository.CountProductEntries);

            var evidenceEntryModel = evidenceEntryService.GetEvidenceEntryDTOByID(4);
            Assert.AreEqual(serverRepository.FindEvidenceEntryByID(4).ProductAmount, 1);

            evidenceEntryModel.ProductAmount = 5;
            var task2 = Task.Run(async () =>
            {
                Assert.IsTrue(evidenceEntryService.ChangeEvidenceEntryDTO(4, evidenceEntryModel));
            });
            Thread.Sleep(100);
            Assert.AreEqual(clientRepository.FindEvidenceEntryByID(4).Amount, 5);
            Assert.AreEqual(serverRepository.FindEvidenceEntryByID(4).ProductAmount, 5);
        }

        [Test]
        public void AddOrderIntegrationTest()
        {
            var task3 = Task.Run(async () =>
            {
                var taskA = connectionService.SendTask(Serialize(new CSendRequest()
                { Type = typeof(CProduct).ToString(), RequestedID = null }));
                taskA.Wait();
                var taskB = connectionService.SendTask(Serialize(new CSendRequest()
                { Type = typeof(CEvidenceEntry).ToString(), RequestedID = null }));
                taskB.Wait();
                var taskC = connectionService.SendTask(Serialize(new CSendRequest()
                { Type = typeof(CClient).ToString(), RequestedID = null }));
                taskC.Wait();
                var taskD = connectionService.SendTask(Serialize(new CSendRequest()
                { Type = typeof(COrder).ToString(), RequestedID = null }));
                taskD.Wait();

            });
            Thread.Sleep(250);

            Assert.AreEqual(1, serverRepository.CountOrders);
            Assert.AreEqual(1, clientRepository.CountOrders);


            OrderDTO orderDTO = new OrderDTO();
            orderDTO.Client = clientService.GetClientDTOByID(0);
            orderDTO.ID = 1;
            EvidenceEntryDTO evidenceEntryDTO = new EvidenceEntryDTO();
            evidenceEntryDTO.Product = productService.GetProductDTOByID(0);
            evidenceEntryDTO.ProductAmount = 1;
            orderDTO.Products.Add(evidenceEntryDTO);

            var task2 = Task.Run(async () =>
            {
                Assert.IsTrue(orderService.AddOrderDTO(orderDTO));
            });
            Thread.Sleep(100);

            Assert.AreEqual(2, serverRepository.CountOrders);
            Assert.AreEqual(2, clientRepository.CountOrders);

            Assert.AreEqual(1, serverRepository.FindOrderByID(1).Products[0].ProductAmount);
            Assert.AreEqual(1, clientRepository.FindOrderByID(1).Entries[0].Amount);

            orderDTO.Products[0].ProductAmount = 2;
            var task1 = Task.Run(async () =>
            {
                Assert.IsTrue(orderService.ChangeOrderDTO(1,orderDTO));
            });
            Thread.Sleep(100);
            Assert.AreEqual(2, serverRepository.FindOrderByID(1).Products[0].ProductAmount);
            Assert.AreEqual(2, clientRepository.FindOrderByID(1).Entries[0].Amount);
        }
    }
}