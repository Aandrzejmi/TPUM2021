using NUnit.Framework;
using System.Collections.Generic;
using Client.DataAPI;
using Client.LogicAPI.Interfaces;
using Client.LogicAPI.DTOs;
using Client.LogicAPI.Services;
using Client.LogicAPI.Exceptions;
using CommunicationAPI.Models;
using Moq;

namespace Client.LogicTests
{
    class ClientLogicAPIValidationTests
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

            List<CEvidenceEntry> evidenceEntries = new List<CEvidenceEntry>();
            CProduct product = new CProduct() { ID = 0, Name = "Temp Product", Price = 1.0M };

            CEvidenceEntry evidenceEntry = new CEvidenceEntry() { Product = product, Amount = 0 };
            evidenceEntries.Add(evidenceEntry);

            CProduct product2 = new CProduct() { ID = 1, Name = "Temporary Product", Price = 1.0M };

            repositoryMock.Setup(p => p.FindClientByID(0)).Returns(new CClient() { ID = 0, Adress = "Temp Adress", Name = "Temp Name" });
            repositoryMock.Setup(p => p.FindClientByID(-1)).Returns(null as CClient);
            repositoryMock.Setup(p => p.GetAllClients()).Returns(new List<CClient>() { new CClient() { ID = 0, Adress = "Temp Adress", Name = "Temp Name" } });

            repositoryMock.Setup(p => p.FindClientByName("Temp Name")).Returns(new CClient() { ID = 0, Adress = "Temp Adress", Name = "Temp Name" });
            repositoryMock.Setup(p => p.FindClientByName("")).Returns(null as CClient);
            repositoryMock.Setup(p => p.AddClient(It.IsAny<CClient>())).Returns(true);

            repositoryMock.Setup(p => p.FindProductByID(0)).Returns(new CProduct() { ID = 0, Name = "Temp Product", Price = 1.0M });
            repositoryMock.Setup(p => p.FindProductByID(1)).Returns(new CProduct() { ID = 1, Name = "Temp Product", Price = 1.0M });
            repositoryMock.Setup(p => p.FindProductByID(-1)).Returns(null as CProduct);
            repositoryMock.Setup(p => p.AddProduct(It.IsAny<CProduct>())).Returns(true);
            repositoryMock.Setup(p => p.GetAllProducts()).Returns(new List<CProduct>() { new CProduct() { ID = 0, Name = "Temp Product", Price = 1.0M } });

            repositoryMock.Setup(p => p.FindProductByName("Temp Product")).Returns(new CProduct() { ID = 0, Name = "Temp Product", Price = 1.0M });
            repositoryMock.Setup(p => p.FindProductByName("")).Returns(null as CProduct);

            repositoryMock.Setup(p => p.FindEvidenceEntryByID(-1)).Returns(null as CEvidenceEntry);
            repositoryMock.Setup(p => p.FindEvidenceEntryByID(It.IsInRange<int>(0, 100, Range.Inclusive))).Returns(new CEvidenceEntry() { Product = new CProduct() { ID = 0, Name = "Temp Product", Price = 1.0M }, Amount = 0 });
            repositoryMock.Setup(p => p.AddEvidenceEntry(It.IsAny<CEvidenceEntry>())).Returns(true);
            repositoryMock.Setup(p => p.GetAllEntries()).Returns(new List<CEvidenceEntry>() { new CEvidenceEntry() { Product = new CProduct() { ID = 0, Name = "Temp Product", Price = 1.0M }, Amount = 0 } });

            repositoryMock.Setup(p => p.FindOrderByID(0)).Returns(new COrder() { Entries = evidenceEntries, Client = new CClient() { ID = 0, Adress = "Temp Adress", Name = "Temp Name" } });
            repositoryMock.Setup(p => p.FindOrderByID(-1)).Returns(null as COrder);
            repositoryMock.Setup(p => p.AddOrder(It.IsAny<COrder>())).Returns(true);
            repositoryMock.Setup(p => p.GetAllOrders()).Returns(new List<COrder>() { new COrder() { Entries = evidenceEntries, Client = new CClient() { ID = 0, Adress = "Temp Adress", Name = "Temp Name" } } });

            repositoryMock.Setup(p => p.FindOrdersByClientID(0)).Returns(new List<COrder> { new COrder() { Entries = evidenceEntries, Client = new CClient() { ID = 0, Adress = "Temp Adress", Name = "Temp Name" } } });
            repositoryMock.Setup(p => p.FindOrdersByClientID(-1)).Returns(new List<COrder>());

            repositoryMock.Setup(p => p.GetAllClients()).Returns(new List<CClient>() { new CClient() { ID = 0, Adress = "Temp Adress", Name = "Temp Name" } });
            repositoryMock.Setup(p => p.GetAllEntries()).Returns(new List<CEvidenceEntry>() { new CEvidenceEntry() { Product = new CProduct() { ID = 0, Name = "Temp Product", Price = 1.0M }, Amount = 0 } });
            repositoryMock.Setup(p => p.GetAllOrders()).Returns(new List<COrder>() { new COrder() { Entries = evidenceEntries, Client = new CClient() { ID = 0, Adress = "Temp Adress", Name = "Temp Name" } } });
            repositoryMock.Setup(p => p.GetAllProducts()).Returns(new List<CProduct>() { new CProduct() { ID = 0, Name = "Temp Product", Price = 1.0M } });

            repositoryMock.Setup(p => p.ChangeProductAmount(It.IsAny<int>(), It.IsAny<int>())).Returns(true);

            _orderService = new OrderService(repositoryMock.Object);
            _productService = new ProductService(repositoryMock.Object);
            _evidenceEntryService = new EvidenceEntryService(repositoryMock.Object);
            _clientService = new ClientService(repositoryMock.Object);
        }

        [Test]
        public void OrderServiceValidationTests()
        {
            // proper order, validate it
            COrder order1 = repositoryMock.Object.FindOrderByID(0);
            Assert.IsTrue(_orderService.ValidateModel(order1));

            // try to validate order that has wrong ID
            COrder order2 = new COrder() { ID = -1, Client = new CClient {ID = 0, Adress = "Temp Adress", Name = "Temp Name" } };
            Assert.That(() => _orderService.ValidateModel(order2), Throws.TypeOf<OrderInvalidIDException>());

            // try to find order that is in repository, but has changed clientID to invalid
            COrder order3 = repositoryMock.Object.FindOrderByID(0);
            order3.Client.ID = -1;
            Assert.That(() => _orderService.ValidateModel(order3), Throws.TypeOf<OrderInvalidClientIDException>());
        }

        [Test]
        public void ProductServiceValidationTests()
        {
            // proper product, validate it
            CProduct product1 = repositoryMock.Object.FindProductByID(0);
            Assert.IsTrue(_productService.ValidateModel(product1));

            // try to validate product with invalid ID
            CProduct product2 = new CProduct() { ID = -1, Name = "Temporary", Price = 1.0M };
            Assert.That(() => _productService.ValidateModel(product2), Throws.TypeOf<ProductInvalidIDException>());

            // try to validate product that has invalid name
            CProduct product3 = repositoryMock.Object.FindProductByID(0);
            product3.Name = "";
            Assert.That(() => _productService.ValidateModel(product3), Throws.TypeOf<ProductInvalidNameException>());
            product3.Name = "Proper Name";
            Assert.IsTrue(_productService.ValidateModel(product3));

            // try to validate product that has invalid price, part 1
            CProduct product4 = repositoryMock.Object.FindProductByID(0);
            product4.Price = 0.0M;
            Assert.That(() => _productService.ValidateModel(product4), Throws.TypeOf<ProductInvalidPriceException>());
            // part 2
            product4.Price = -1.0M;
            Assert.That(() => _productService.ValidateModel(product4), Throws.TypeOf<ProductInvalidPriceException>());
            // part 4, check if its now correct
            product4.Price = 1.0M;
            Assert.IsTrue(_productService.ValidateModel(product4));
        }

        [Test]
        public void ClientServiceValidationTests()
        {
            // proper client, validate it
            CClient client1 = repositoryMock.Object.FindClientByID(0);
            Assert.IsTrue(_clientService.ValidateModel(client1));

            // try to validate client with invalid id
            CClient client2 = new CClient() { ID = -1, Adress = "Temporary Adress", Name = "Temporary Name" };
            Assert.That(() => _clientService.ValidateModel(client2), Throws.TypeOf<ClientInvalidIDException>());

            // try to validate client with invalid name
            CClient client3 = repositoryMock.Object.FindClientByID(0);
            client3.Name = "";
            Assert.That(() => _clientService.ValidateModel(client3), Throws.TypeOf<ClientInvalidNameException>());
            client3.Name = "Proper Name";
            Assert.IsTrue(_clientService.ValidateModel(client3));

            // try to validate client with invalid adress
            CClient client4 = repositoryMock.Object.FindClientByID(0);
            client4.Adress = "";
            Assert.That(() => _clientService.ValidateModel(client4), Throws.TypeOf<ClientInvalidAdressException>());
            client4.Adress = "Proper Adress";
            Assert.IsTrue(_clientService.ValidateModel(client4));
        }

        [Test]
        public void EvidenceEntryValidationTests()
        {
            // proper evidence entry, validate it
            CEvidenceEntry evidenceEntry1 = repositoryMock.Object.FindEvidenceEntryByID(0);
            Assert.IsTrue(_evidenceEntryService.ValidateModel(evidenceEntry1));

            // try to validate entry with invalid id
            CEvidenceEntry evidenceEntry2 = new CEvidenceEntry() { Product = new CProduct {ID = -1 }, Amount = 0 };
            Assert.That(() => _evidenceEntryService.ValidateModel(evidenceEntry2), Throws.TypeOf<EvidenceEntryInvalidIDException>());

            // try to validate entry with invalid product amount
            CEvidenceEntry evidenceEntry3 = repositoryMock.Object.FindEvidenceEntryByID(0);
            evidenceEntry3.Amount = -1;
            Assert.That(() => _evidenceEntryService.ValidateModel(evidenceEntry3), Throws.TypeOf<EvidenceEntryInvalidProductAmountException>());
        }
    }
}
