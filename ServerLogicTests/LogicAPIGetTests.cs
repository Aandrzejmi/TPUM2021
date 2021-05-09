using NUnit.Framework;
using System.Collections.Generic;
using Server.DataAPI;
using CommunicationAPI.Models;
using Server.LogicAPI.Interfaces;
using Server.LogicAPI.Services;
using Server.LogicAPI.Exceptions;
using Moq;

namespace Server.LogicTests
{
    public class LogicAPIGetTests
    {
        Mock<IRepository> repositoryMock;
        IOrderService _orderService;
        IProductService _productService;
        IEvidenceEntryService _evidenceEntryService;
        IClientService _clientService;

        [SetUp]
        public void Setup()
        {
            repositoryMock = new Mock<IRepository>();

            List<EvidenceEntry> evidenceEntries = new List<EvidenceEntry>();
            Product product = new Product() { ID = 0, Name = "Temp Product", Price = 1.0M };

            EvidenceEntry evidenceEntry = new EvidenceEntry() { ProductID = 0, ProductAmount = 0 };
            evidenceEntries.Add(evidenceEntry);

            repositoryMock.Setup(p => p.FindClientByID(0)).Returns(new Server.DataAPI.Client() { ID = 0, Adress = "Temp Adress", Name = "Temp Name" });
            repositoryMock.Setup(p => p.FindClientByID(-1)).Returns(null as Server.DataAPI.Client);

            repositoryMock.Setup(p => p.FindClientByName("Temp Name")).Returns(new Server.DataAPI.Client() { ID = 0, Adress = "Temp Adress", Name = "Temp Name" });
            repositoryMock.Setup(p => p.FindClientByName("")).Returns(null as Server.DataAPI.Client);

            repositoryMock.Setup(p => p.FindProductByID(0)).Returns(new Product() { ID = 0, Name = "Temp Product", Price = 1.0M });
            repositoryMock.Setup(p => p.FindProductByID(-1)).Returns(null as Product);

            repositoryMock.Setup(p => p.FindProductByName("Temp Product")).Returns(new Product() { ID = 0, Name = "Temp Product", Price = 1.0M });
            repositoryMock.Setup(p => p.FindProductByName("")).Returns(null as Product);

            repositoryMock.Setup(p => p.FindEvidenceEntryByID(0)).Returns(new EvidenceEntry() { ProductID = 0, ProductAmount = 0 });
            repositoryMock.Setup(p => p.FindEvidenceEntryByID(-1)).Returns(null as EvidenceEntry);

            repositoryMock.Setup(p => p.FindOrderByID(0)).Returns(new Order() { Products = evidenceEntries, ClientID = 0 });
            repositoryMock.Setup(p => p.FindOrderByID(-1)).Returns(null as Order);

            repositoryMock.Setup(p => p.FindOrdersByClientID(0)).Returns(new List<Order> { new Order() { Products = evidenceEntries, ClientID = 0 } });
            repositoryMock.Setup(p => p.FindOrdersByClientID(-1)).Returns(new List<Order>());

            repositoryMock.Setup(p => p.GetAllClients()).Returns(new List<Server.DataAPI.Client>() { new Server.DataAPI.Client() { ID = 0, Adress = "Temp Adress", Name = "Temp Name" } });
            repositoryMock.Setup(p => p.GetAllEntries()).Returns(new List<EvidenceEntry>() { new EvidenceEntry() { ProductID = 0, ProductAmount = 0 } });
            repositoryMock.Setup(p => p.GetAllOrders()).Returns(new List<Order>() { new Order() { Products = evidenceEntries, ClientID = 0 } });
            repositoryMock.Setup(p => p.GetAllProducts()).Returns(new List<Product>() { new Product() { ID = 0, Name = "Temp Product", Price = 1.0M } });

            _orderService = new OrderService(repositoryMock.Object);
            _productService = new ProductService(repositoryMock.Object);
            _evidenceEntryService = new EvidenceEntryService(repositoryMock.Object);
            _clientService = new ClientService(repositoryMock.Object);
        }

        [Test]
        public void GetOrderTests()
        {
            // Find order by ID
            COrder cOrder = _orderService.GetOrderByID(0);
            Order order = repositoryMock.Object.FindOrderByID(0);
            Assert.AreEqual(cOrder.ID, order.ID);
            Assert.AreEqual(cOrder.Client.ID, order.ClientID);
            Assert.AreEqual(cOrder.Entries.Count, order.Products.Count);
            foreach (var cEvEntry in cOrder.Entries)
            {
                foreach(EvidenceEntry evidenceEntry in order.Products)
                {
                    if (cEvEntry.Product.ID == evidenceEntry.ID)
                    {
                        Assert.AreEqual(cEvEntry.Product.ID, evidenceEntry.ProductID);
                        Assert.AreEqual(cEvEntry.Amount, evidenceEntry.ProductAmount);
                    }
                }               
            }

            Assert.That(() => _orderService.GetOrderByID(-1), Throws.TypeOf<OrderNotFoundException>());

            // Find order by ClientID
            List<COrder> cOrders = _orderService.GetOrdersByClientID(0);
            List<Order> orders = repositoryMock.Object.FindOrdersByClientID(0);

            Assert.AreEqual(orders.Count, orders.Count);
            Assert.AreEqual(orders.Count, 1);
        }

        [Test]
        public void GetAllModelsTest()
        {
            // COrder
            Assert.AreEqual(_orderService.GetAllOrders().Count, 1);
            Assert.AreEqual(_orderService.GetAllOrders()[0].ID, 0);

            // CClient
            Assert.AreEqual(_clientService.GetAllClients().Count, 1);
            Assert.AreEqual(_clientService.GetAllClients()[0].ID, 0);

            // CProduct
            Assert.AreEqual(_productService.GetAllProducts().Count, 1);
            Assert.AreEqual(_productService.GetAllProducts()[0].ID, 0);

            // CEvidenceEntry
            Assert.AreEqual(_evidenceEntryService.GetAllEvidenceEntries().Count, 1);
            Assert.AreEqual(_evidenceEntryService.GetAllEvidenceEntries()[0].Product.ID, 0);
        }

        [Test]
        public void GetProductTests()
        {
            // Find Product by ID
            CProduct cProduct1 = _productService.GetProductByID(0);
            Product product1 = repositoryMock.Object.FindProductByID(0);
            Assert.AreEqual(cProduct1.ID, product1.ID);
            Assert.AreEqual(cProduct1.Name, product1.Name);
            Assert.AreEqual(cProduct1.Price, product1.Price);

            Assert.That(() => _productService.GetProductByID(-1), Throws.TypeOf<ProductNotFoundException>());

            // Find Product by name
            CProduct cProduct2 = _productService.GetProductByName("Temp Product");
            Product product2 = repositoryMock.Object.FindProductByName("Temp Product");
            Assert.AreEqual(cProduct2.ID, product2.ID);
            Assert.AreEqual(cProduct2.Name, product2.Name);
            Assert.AreEqual(cProduct2.Price, product2.Price);

            Assert.That(() => _productService.GetProductByName(""), Throws.TypeOf<ProductNotFoundException>());
        }

        [Test]
        public void GetClientTests()
        {
            CClient cClient1 = _clientService.GetClientByID(0);
            Server.DataAPI.Client client1 = repositoryMock.Object.FindClientByID(0);
            Assert.AreEqual(cClient1.ID, client1.ID);
            Assert.AreEqual(cClient1.Name, client1.Name);
            Assert.AreEqual(cClient1.Adress, client1.Adress);

            Assert.That(() => _clientService.GetClientByID(-1), Throws.TypeOf<ClientNotFoundException>());

            CClient cClient2 = _clientService.GetClientByName("Temp Name");
            Server.DataAPI.Client client2 = repositoryMock.Object.FindClientByName("Temp Name");
            Assert.AreEqual(cClient2.ID, client2.ID);
            Assert.AreEqual(cClient2.Name, client2.Name);
            Assert.AreEqual(cClient2.Adress, client2.Adress);

            Assert.That(() => _clientService.GetClientByName(""), Throws.TypeOf<ClientNotFoundException>());
        }

        [Test]
        public void GetEvidenceEntryTests()
        {
            CEvidenceEntry cEvEntry1 = _evidenceEntryService.GetEvidenceEntryByID(0);
            EvidenceEntry evidenceEntry1 = repositoryMock.Object.FindEvidenceEntryByID(0);

            Assert.AreEqual(cEvEntry1.Product.ID, evidenceEntry1.ID);
            Assert.AreEqual(cEvEntry1.Amount, evidenceEntry1.ProductAmount);
            Assert.AreEqual(cEvEntry1.Product.ID, evidenceEntry1.ProductID);

            Assert.That(() => _evidenceEntryService.GetEvidenceEntryByID(-1), Throws.TypeOf<EvidenceEntryNotFoundException>());
        }
    }    
}
