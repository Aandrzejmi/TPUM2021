using NUnit.Framework;
using System.Collections.Generic;
using Server.LogicAPI;
using Server.DataAPI;
using Server.LogicAPI.Interfaces;
using Server.LogicAPI.Services;
using Server.LogicAPI.Exceptions;
using Moq;

namespace Server.LogicTests
{
    public class LogicAPIValidationTests
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

            repositoryMock.Setup(p => p.FindProductByID(0)).Returns(new Product() { ID = 0, Name = "Temp Product", Price = 1.0M });
            repositoryMock.Setup(p => p.FindProductByID(-1)).Returns(null as Product);

            repositoryMock.Setup(p => p.FindEvidenceEntryByID(0)).Returns(new EvidenceEntry() { ProductID = 0, ProductAmount = 0 });
            repositoryMock.Setup(p => p.FindEvidenceEntryByID(-1)).Returns(null as EvidenceEntry);

            repositoryMock.Setup(p => p.FindOrderByID(0)).Returns(new Order() { Products = evidenceEntries, ClientID = 0 });
            repositoryMock.Setup(p => p.FindOrderByID(-1)).Returns(null as Order);

            _orderService = new OrderService(repositoryMock.Object);
            _productService = new ProductService(repositoryMock.Object);
            _evidenceEntryService = new EvidenceEntryService(repositoryMock.Object);
            _clientService = new ClientService(repositoryMock.Object);
        }

        [Test]
        public void OrderServiceValidationTests()
        {
            // proper order, validate it
            Order order1 = repositoryMock.Object.FindOrderByID(0);
            Assert.IsTrue(_orderService.ValidateModel(order1));

            // try to validate order that has wrong ID
            Order order2 = new Order() { ID = -1, ClientID = 0 };
            Assert.That(() => _orderService.ValidateModel(order2), Throws.TypeOf<OrderInvalidIDException>());

            // try to find order that is in repository, but has changed clientID to invalid
            Order order3 = repositoryMock.Object.FindOrderByID(0);
            order3.ClientID = -1;
            Assert.That(() => _orderService.ValidateModel(order3), Throws.TypeOf<OrderInvalidClientIDException>());


            // try to validate model other then Order
            Product product = new Product() { ID = 0, Name = "Test product", Price = 1.0M };
            Assert.That(() => _orderService.ValidateModel(product), Throws.TypeOf<ModelIsNotOrderException>());
        }

        [Test]
        public void ProductServiceValidationTests()
        {
            // proper product, validate it
            Product product1 = repositoryMock.Object.FindProductByID(0);
            Assert.IsTrue(_productService.ValidateModel(product1));

            // try to validate product with invalid ID
            Product product2 = new Product() { ID = -1, Name = "Temporary", Price = 1.0M };
            Assert.That(() => _productService.ValidateModel(product2), Throws.TypeOf<ProductInvalidIDException>());

            // try to validate product that has invalid name
            Product product3 = repositoryMock.Object.FindProductByID(0);
            product3.Name = "";
            Assert.That(() => _productService.ValidateModel(product3), Throws.TypeOf<ProductInvalidNameException>());
            product3.Name = "Proper Name";
            Assert.IsTrue(_productService.ValidateModel(product3));

            // try to validate product that has invalid price, part 1
            Product product4 = repositoryMock.Object.FindProductByID(0);
            product4.Price = 0.0M;
            Assert.That(() => _productService.ValidateModel(product4), Throws.TypeOf<ProductInvalidPriceException>());
            // part 2
            product4.Price = -1.0M;
            Assert.That(() => _productService.ValidateModel(product4), Throws.TypeOf<ProductInvalidPriceException>());
            // part 4, check if its now correct
            product4.Price = 1.0M;
            Assert.IsTrue(_productService.ValidateModel(product4));

            // try to validate model that is not product
            Server.DataAPI.Client client = repositoryMock.Object.FindClientByID(0);
            Assert.That(() => _productService.ValidateModel(client), Throws.TypeOf<ModelIsNotProductException>());
        }

        [Test]
        public void ClientServiceValidationTests()
        {
            // proper client, validate it
            Server.DataAPI.Client client1 = repositoryMock.Object.FindClientByID(0);
            Assert.IsTrue(_clientService.ValidateModel(client1));

            // try to validate client with invalid id
            Server.DataAPI.Client client2 = new Server.DataAPI.Client() { ID = -1, Adress = "Temporary Adress", Name = "Temporary Name" };
            Assert.That(() => _clientService.ValidateModel(client2), Throws.TypeOf<ClientInvalidIDException>());

            // try to validate client with invalid name
            Server.DataAPI.Client client3 = repositoryMock.Object.FindClientByID(0);
            client3.Name = "";
            Assert.That(() => _clientService.ValidateModel(client3), Throws.TypeOf<ClientInvalidNameException>());
            client3.Name = "Proper Name";
            Assert.IsTrue(_clientService.ValidateModel(client3));

            // try to validate client with invalid adress
            Server.DataAPI.Client client4 = repositoryMock.Object.FindClientByID(0);
            client4.Adress = "";
            Assert.That(() => _clientService.ValidateModel(client4), Throws.TypeOf<ClientInvalidAdressException>());
            client4.Adress = "Proper Adress";
            Assert.IsTrue(_clientService.ValidateModel(client4));

            // try to validate model that isnt client
            Product product = new Product() { ID = 0, Name = "Test product", Price = 1.0M };
            Assert.That(() => _clientService.ValidateModel(product), Throws.TypeOf<ModelIsNotClientException>());
        }

        [Test]
        public void EvidenceEntryValidationTests()
        {
            // proper evidence entry, validate it
            EvidenceEntry evidenceEntry1 = repositoryMock.Object.FindEvidenceEntryByID(0);
            Assert.IsTrue(_evidenceEntryService.ValidateModel(evidenceEntry1));

            // try to validate entry with invalid id
            EvidenceEntry evidenceEntry2 = new EvidenceEntry() { ProductID = -1, ProductAmount = 0 };
            Assert.That(() => _evidenceEntryService.ValidateModel(evidenceEntry2), Throws.TypeOf<EvidenceEntryInvalidIDException>());

            // try to validate entry with invalid product amount
            EvidenceEntry evidenceEntry3 = repositoryMock.Object.FindEvidenceEntryByID(0);
            evidenceEntry3.ProductAmount = -1;
            Assert.That(() => _evidenceEntryService.ValidateModel(evidenceEntry3), Throws.TypeOf<EvidenceEntryInvalidProductAmountException>());

            //// try to validate model that isnt evidence entry
            Product product = new Product() { ID = 0, Name = "Test product", Price = 1.0M };
            Assert.That(() => _evidenceEntryService.ValidateModel(product), Throws.TypeOf<ModelIsNotEvidenceEntryException>());
        }
    }
}