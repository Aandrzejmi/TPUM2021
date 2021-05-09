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
    public class LogicAPIAddTests
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

            CProduct CProduct1 = new CProduct() { ID = 1, Name = "Temporary Product", Price = 1.0M };

            repositoryMock.Setup(p => p.FindClientByID(0)).Returns(new Server.DataAPI.Client() { ID = 0, Adress = "Temp Adress", Name = "Temp Name" });
            repositoryMock.Setup(p => p.FindClientByID(-1)).Returns(null as Server.DataAPI.Client);
            repositoryMock.Setup(p => p.GetAllClients()).Returns(new List<Server.DataAPI.Client>() { new Server.DataAPI.Client() { ID = 0, Adress = "Temp Adress", Name = "Temp Name" }});

            repositoryMock.Setup(p => p.FindClientByName("Temp Name")).Returns(new Server.DataAPI.Client() { ID = 0, Adress = "Temp Adress", Name = "Temp Name" });
            repositoryMock.Setup(p => p.FindClientByName("")).Returns(null as Server.DataAPI.Client);
            repositoryMock.Setup(p => p.AddClient(It.IsAny<Server.DataAPI.Client>())).Returns(true);

            repositoryMock.Setup(p => p.FindProductByID(0)).Returns(new Product() { ID = 0, Name = "Temp Product", Price = 1.0M });
            repositoryMock.Setup(p => p.FindProductByID(1)).Returns(new Product() { ID = 1, Name = "Temp Product", Price = 1.0M });
            repositoryMock.Setup(p => p.FindProductByID(-1)).Returns(null as Product);
            repositoryMock.Setup(p => p.AddProduct(It.IsAny<Product>())).Returns(true);
            repositoryMock.Setup(p => p.GetAllProducts()).Returns(new List<Product>() { new Product() { ID = 0, Name = "Temp Product", Price = 1.0M } });

            repositoryMock.Setup(p => p.FindProductByName("Temp Product")).Returns(new Product() { ID = 0, Name = "Temp Product", Price = 1.0M });
            repositoryMock.Setup(p => p.FindProductByName("")).Returns(null as Product);

            repositoryMock.Setup(p => p.FindEvidenceEntryByID(-1)).Returns(null as EvidenceEntry);
            repositoryMock.Setup(p => p.FindEvidenceEntryByID(It.IsInRange<int>(0, 100, Range.Inclusive))).Returns(new EvidenceEntry() { ProductID = 0, ProductAmount = 0 });
            repositoryMock.Setup(p => p.AddEvidenceEntry(It.IsAny<EvidenceEntry>())).Returns(true);
            repositoryMock.Setup(p => p.GetAllEntries()).Returns(new List<EvidenceEntry>() { new EvidenceEntry() { ProductID = 0, ProductAmount = 0 } });

            repositoryMock.Setup(p => p.FindOrderByID(0)).Returns(new Order() { Products = evidenceEntries, ClientID = 0 });
            repositoryMock.Setup(p => p.FindOrderByID(-1)).Returns(null as Order);
            repositoryMock.Setup(p => p.AddOrder(It.IsAny<Order>())).Returns(true);
            repositoryMock.Setup(p => p.GetAllOrders()).Returns(new List<Order>() { new Order() { Products = evidenceEntries, ClientID = 0 } });

            repositoryMock.Setup(p => p.FindOrdersByClientID(0)).Returns(new List<Order> { new Order() { Products = evidenceEntries, ClientID = 0 } });
            repositoryMock.Setup(p => p.FindOrdersByClientID(-1)).Returns(new List<Order>());

            repositoryMock.Setup(p => p.GetAllClients()).Returns(new List<Server.DataAPI.Client>() { new Server.DataAPI.Client() { ID = 0, Adress = "Temp Adress", Name = "Temp Name" } });
            repositoryMock.Setup(p => p.GetAllEntries()).Returns(new List<EvidenceEntry>() { new EvidenceEntry() { ProductID = 0, ProductAmount = 0 } });
            repositoryMock.Setup(p => p.GetAllOrders()).Returns(new List<Order>() { new Order() { Products = evidenceEntries, ClientID = 0 } });
            repositoryMock.Setup(p => p.GetAllProducts()).Returns(new List<Product>() { new Product() { ID = 0, Name = "Temp Product", Price = 1.0M } });

            repositoryMock.Setup(p => p.ChangeProductAmount(It.IsAny<int>(), It.IsAny<int>())).Returns(true);

            _orderService = new OrderService(repositoryMock.Object);
            _productService = new ProductService(repositoryMock.Object);
            _evidenceEntryService = new EvidenceEntryService(repositoryMock.Object);
            _clientService = new ClientService(repositoryMock.Object);
        }

        [Test]
        public void AddProductTests()
        {
            // Try to add new product
            CProduct CProduct1 = new CProduct() { ID = 1, Name = "Temporary Product", Price = 1.0M };
            Assert.IsTrue(_productService.AddProduct(CProduct1));

            // Try to add product with invalid ID
            CProduct CProduct3 = new CProduct() { ID = -1, Name = "Temporary Product", Price = 1.0M };
            Assert.Throws<ProductInvalidIDException>(() => _productService.AddProduct(CProduct3));

            // Try to add product with invalid Name
            CProduct CProduct4 = new CProduct() { ID = 2, Name = "", Price = 1.0M };
            Assert.Throws<ProductInvalidNameException>(() => _productService.AddProduct(CProduct4));

            // Try to add product with invalid Price
            CProduct CProduct5 = new CProduct() { ID = 2, Name = "Temporary Product", Price = -1.0M };
            Assert.Throws<ProductInvalidPriceException>(() => _productService.AddProduct(CProduct5));
        }

        [Test]
        public void AddClientTests()
        {
            // Try to add new client
            CClient cClient1 = new CClient() { ID = 1, Name = "Temporary Name", Adress = "Temporary Adress"};
            Assert.IsTrue(_clientService.AddClient(cClient1));

            // Try to add new client with invalid ID
            CClient cClient2 = new CClient() { ID = -1, Name = "Temporary Name", Adress = "Temporary Adress" };
            Assert.Throws<ClientInvalidIDException>(() => _clientService.AddClient(cClient2));

            // Try to add new client with invalid Name
            CClient cClient3 = new CClient() { ID = 1, Name = "", Adress = "Temporary Adress" };
            Assert.Throws<ClientInvalidNameException>(() => _clientService.AddClient(cClient3));

            // Try to add new client with invalid Adress
            CClient cClient4 = new CClient() { ID = 1, Name = "Temporary Name", Adress = "" };
            Assert.Throws<ClientInvalidAdressException>(() => _clientService.AddClient(cClient4));
        }

        [Test]
        public void AddEvidenceEntryTests()
        {
            CEvidenceEntry cEvEntry1 = new CEvidenceEntry() { Product = new CProduct() { ID = 0, Name = "Temporary Product", Price = 1.0M }, Amount = 1 };
            Assert.IsTrue(_evidenceEntryService.AddEvidenceEntry(cEvEntry1));

            CEvidenceEntry cEvEntry2 = new CEvidenceEntry() { Product = new CProduct() { ID = -1, Name = "Temporary Product", Price = 1.0M }, Amount = 1 };
            Assert.Throws<EvidenceEntryInvalidIDException>(() => _evidenceEntryService.AddEvidenceEntry(cEvEntry2));

            CEvidenceEntry cEvEntry3 = new CEvidenceEntry() { Product = new CProduct() { ID = 1, Name = "", Price = 1.0M }, Amount = 1 };
            Assert.Throws<ProductInvalidNameException>(() => _evidenceEntryService.AddEvidenceEntry(cEvEntry3));

            CEvidenceEntry cEvEntry4 = new CEvidenceEntry() { Product = new CProduct() { ID = 1, Name = "Temporary Product", Price = 0.0M }, Amount = 1 };
            Assert.Throws<ProductInvalidPriceException>(() => _evidenceEntryService.AddEvidenceEntry(cEvEntry4));

            CEvidenceEntry cEvEntry5 = new CEvidenceEntry() { Product = new CProduct() { ID = 1, Name = "Temporary Product", Price = 1.0M }, Amount = -1 };
            Assert.Throws<EvidenceEntryInvalidProductAmountException>(() => _evidenceEntryService.AddEvidenceEntry(cEvEntry5));
        }

        [Test]
        public void AddOrderTests()
        {
            CClient cClient = new CClient() { ID = 0, Name = "Temporary Name", Adress = "Temporary Adress" };
            CEvidenceEntry cEvEntry = new CEvidenceEntry() { Product = new CProduct() { ID = 0, Name = "Temporary Product", Price = 1.0M }, Amount = 1 };
            List<CEvidenceEntry> cEvEntrys = new List<CEvidenceEntry>();
            cEvEntrys.Add(cEvEntry);
            COrder cOrder1 = new COrder() { ID = 0, Entries = cEvEntrys, Client = cClient };
            Assert.IsTrue(_orderService.AddOrder(cOrder1));

            COrder cOrder2 = new COrder() { ID = -1, Entries = cEvEntrys, Client = cClient };
            Assert.Throws<OrderInvalidIDException>(() => _orderService.AddOrder(cOrder2));

            CClient cClient2 = new CClient() { ID = -1, Name = "Temporary Name", Adress = "Temporary Adress" };
            List<CEvidenceEntry> cEvEntrys2 = new List<CEvidenceEntry>();
            CEvidenceEntry cEvEntry2 = new CEvidenceEntry() { Product = new CProduct() { ID = 0, Name = "Temporary Product", Price = 1.0M }, Amount = 1 };
            cEvEntrys2.Add(cEvEntry2);
            COrder cOrder3 = new COrder() { ID = 0, Entries = cEvEntrys2, Client = cClient2 };
            Assert.Throws<OrderInvalidClientIDException>(() => _orderService.AddOrder(cOrder3));
        }
    }
}
