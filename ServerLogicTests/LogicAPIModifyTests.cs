using NUnit.Framework;
using System.Collections.Generic;
using Server.DataAPI;
using CommunicationAPI.Models;
using Server.LogicAPI.Interfaces;
using Server.LogicAPI.Services;
using Moq;

namespace Server.LogicTests
{
    public class LogicAPIModifyTests
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
            repositoryMock.Setup(p => p.GetAllClients()).Returns(new List<Server.DataAPI.Client>() { new Server.DataAPI.Client() { ID = 0, Adress = "Temp Adress", Name = "Temp Name" } });

            repositoryMock.Setup(p => p.FindClientByName("Temp Name")).Returns(new Server.DataAPI.Client() { ID = 0, Adress = "Temp Adress", Name = "Temp Name" });
            repositoryMock.Setup(p => p.FindClientByName("")).Returns(null as Server.DataAPI.Client);

            repositoryMock.Setup(p => p.FindProductByID(0)).Returns(new Product() { ID = 0, Name = "Temp Product", Price = 1.0M });
            repositoryMock.Setup(p => p.FindProductByID(-1)).Returns(null as Product);
            repositoryMock.Setup(p => p.GetAllProducts()).Returns(new List<Product>() { new Product() { ID = 0, Name = "Temp Product", Price = 1.0M } });

            repositoryMock.Setup(p => p.FindProductByName("Temp Product")).Returns(new Product() { ID = 0, Name = "Temp Product", Price = 1.0M });
            repositoryMock.Setup(p => p.FindProductByName("")).Returns(null as Product);

            repositoryMock.Setup(p => p.FindEvidenceEntryByID(0)).Returns(new EvidenceEntry() { ProductID = 0, ProductAmount = 0 });
            repositoryMock.Setup(p => p.FindEvidenceEntryByID(-1)).Returns(null as EvidenceEntry);
            repositoryMock.Setup(p => p.GetAllEntries()).Returns(new List<EvidenceEntry>() { new EvidenceEntry() { ProductID = 0, ProductAmount = 0 } });

            repositoryMock.Setup(p => p.FindOrderByID(0)).Returns(new Order() { Products = evidenceEntries, ClientID = 0 });
            repositoryMock.Setup(p => p.FindOrderByID(-1)).Returns(null as Order);

            repositoryMock.Setup(p => p.FindOrdersByClientID(0)).Returns(new List<Order> { new Order() { Products = evidenceEntries, ClientID = 0 } });
            repositoryMock.Setup(p => p.FindOrdersByClientID(-1)).Returns(new List<Order>());
            repositoryMock.Setup(p => p.GetAllOrders()).Returns(new List<Order>() { new Order() { Products = evidenceEntries, ClientID = 0 } });

            repositoryMock.Setup(p => p.GetAllClients()).Returns(new List<Server.DataAPI.Client>() { new Server.DataAPI.Client() { ID = 0, Adress = "Temp Adress", Name = "Temp Name" } });
            repositoryMock.Setup(p => p.GetAllEntries()).Returns(new List<EvidenceEntry>() { new EvidenceEntry() { ProductID = 0, ProductAmount = 0 } });
            repositoryMock.Setup(p => p.GetAllOrders()).Returns(new List<Order>() { new Order() { Products = evidenceEntries, ClientID = 0 } });
            repositoryMock.Setup(p => p.GetAllProducts()).Returns(new List<Product>() { new Product() { ID = 0, Name = "Temp Product", Price = 1.0M } });

            repositoryMock.Setup(p => p.ModifyClient(It.IsAny<Server.DataAPI.Client>())).Returns(true);
            repositoryMock.Setup(p => p.ModifyOrder(It.IsAny<Order>())).Returns(true);
            repositoryMock.Setup(p => p.ModifyProduct(It.IsAny<Product>())).Returns(true);
            repositoryMock.Setup(p => p.ChangeProductAmount(It.IsAny<int>(), It.IsAny<int>())).Returns(true);

            _orderService = new OrderService(repositoryMock.Object);
            _productService = new ProductService(repositoryMock.Object);
            _evidenceEntryService = new EvidenceEntryService(repositoryMock.Object);
            _clientService = new ClientService(repositoryMock.Object);
        }

        [Test]
        public void ModifyProductTests()
        {
            CProduct cProduct = _productService.GetProductByID(0);
            cProduct.Name = "Something else";
            Assert.IsTrue(_productService.ValidateModel(cProduct));
            Assert.IsTrue(_productService.ChangeProduct(cProduct.ID, cProduct));
        }

        [Test]
        public void ModifyOrderTests()
        {
            List<CEvidenceEntry> cEvEntries = new List<CEvidenceEntry>();
            CEvidenceEntry cEvEntry = new CEvidenceEntry() {Product = new CProduct() { ID = 0, Name = "Temp", Price = 1.0M }, Amount = 1};
            cEvEntries.Add(cEvEntry);
            COrder cOrder = new COrder() { Entries = cEvEntries, Client = new CClient() {ID = 0, Name = "Temp name", Adress = "Temp Adress" } };
            cOrder.Entries[0].Amount = 2;
            Assert.IsTrue(_orderService.ValidateModel(cOrder));
            Assert.IsTrue(_orderService.ChangeOrder(cOrder.ID, cOrder));
        }

        [Test]
        public void ModifyClientTests()
        {
            CClient cClient = _clientService.GetClientByID(0);
            cClient.Name = "Something else";
            Assert.IsTrue(_clientService.ValidateModel(cClient));
            Assert.IsTrue(_clientService.ChangeClient(cClient.ID, cClient));
        }

        [Test]
        public void ChangeProductAmountTest()
        {
            CEvidenceEntry cEvEntry = _evidenceEntryService.GetEvidenceEntryByID(0);
            cEvEntry.Amount = 2;
            Assert.IsTrue(_evidenceEntryService.ValidateModel(cEvEntry));
            Assert.IsTrue(_evidenceEntryService.ChangeEvidenceEntry(cEvEntry.Product.ID, cEvEntry));
        }
    }
}
