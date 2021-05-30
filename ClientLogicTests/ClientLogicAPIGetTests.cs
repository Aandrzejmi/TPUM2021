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
    public class ClientLogicAPIGetTests
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
        public void GetOrderTests()
        {
            // Find order by ID
            OrderDTO orderDTO = _orderService.GetOrderDTOByID(0);
            COrder cOrder = repositoryMock.Object.FindOrderByID(0);
            Assert.AreEqual(cOrder.ID, orderDTO.ID);
            Assert.AreEqual(cOrder.Client.ID, orderDTO.Client.ID);
            Assert.AreEqual(cOrder.Entries.Count, orderDTO.Products.Count);
            foreach (var cEvEntry in cOrder.Entries)
            {
                foreach (EvidenceEntryDTO evidenceEntry in orderDTO.Products)
                {
                    if (cEvEntry.Product.ID == evidenceEntry.ID)
                    {
                        Assert.AreEqual(cEvEntry.Product.ID, evidenceEntry.Product.ID);
                        Assert.AreEqual(cEvEntry.Amount, evidenceEntry.ProductAmount);
                    }
                }
            }

            Assert.That(() => _orderService.GetOrderDTOByID(-1), Throws.TypeOf<OrderNotFoundException>());

            // Find order by ClientID
            List<OrderDTO> orderDTOs = _orderService.GetOrderDTOsByClientID(0);
            List<COrder> cOrders = repositoryMock.Object.FindOrdersByClientID(0);

            Assert.AreEqual(cOrders.Count, orderDTOs.Count);
            Assert.AreEqual(orderDTOs.Count, 1);
        }

        [Test]
        public void GetAllModelsTest()
        {
            // COrder
            Assert.AreEqual(_orderService.GetAllOrderDTOs().Count, 1);
            Assert.AreEqual(_orderService.GetAllOrderDTOs()[0].ID, 0);

            // CClient
            Assert.AreEqual(_clientService.GetAllClientDTOs().Count, 1);
            Assert.AreEqual(_clientService.GetAllClientDTOs()[0].ID, 0);

            // CProduct
            Assert.AreEqual(_productService.GetAllProductDTOs().Count, 1);
            Assert.AreEqual(_productService.GetAllProductDTOs()[0].ID, 0);

            // CEvidenceEntry
            Assert.AreEqual(_evidenceEntryService.GetAllEvidenceEntryDTOs().Count, 1);
            Assert.AreEqual(_evidenceEntryService.GetAllEvidenceEntryDTOs()[0].Product.ID, 0);
        }

        [Test]
        public void GetProductTests()
        {
            // Find Product by ID
            ProductDTO productDTO1 = _productService.GetProductDTOByID(0);
            CProduct cProduct1 = repositoryMock.Object.FindProductByID(0);
            Assert.AreEqual(cProduct1.ID, productDTO1.ID);
            Assert.AreEqual(cProduct1.Name, productDTO1.Name);
            Assert.AreEqual(cProduct1.Price, productDTO1.Price);

            Assert.That(() => _productService.GetProductDTOByID(-1), Throws.TypeOf<ProductNotFoundException>());

            // Find Product by name
            ProductDTO productDTO2 = _productService.GetProductDTOByName("Temp Product");
            CProduct cProduct2 = repositoryMock.Object.FindProductByName("Temp Product");
            Assert.AreEqual(cProduct2.ID, productDTO2.ID);
            Assert.AreEqual(cProduct2.Name, productDTO2.Name);
            Assert.AreEqual(cProduct2.Price, productDTO2.Price);

            Assert.That(() => _productService.GetProductDTOByName(""), Throws.TypeOf<ProductNotFoundException>());
        }

        [Test]
        public void GetClientTests()
        {
            ClientDTO clientDTO1 = _clientService.GetClientDTOByID(0);
            CClient cClient1 = repositoryMock.Object.FindClientByID(0);
            Assert.AreEqual(cClient1.ID, clientDTO1.ID);
            Assert.AreEqual(cClient1.Name, clientDTO1.Name);
            Assert.AreEqual(cClient1.Adress, clientDTO1.Adress);

            Assert.That(() => _clientService.GetClientDTOByID(-1), Throws.TypeOf<ClientNotFoundException>());

            ClientDTO clientDTO12 = _clientService.GetClientDTOByName("Temp Name");
            CClient cClient2 = repositoryMock.Object.FindClientByName("Temp Name");
            Assert.AreEqual(cClient2.ID, clientDTO12.ID);
            Assert.AreEqual(cClient2.Name, clientDTO12.Name);
            Assert.AreEqual(cClient2.Adress, clientDTO12.Adress);

            Assert.That(() => _clientService.GetClientDTOByName(""), Throws.TypeOf<ClientNotFoundException>());
        }

        [Test]
        public void GetEvidenceEntryTests()
        {
            EvidenceEntryDTO evidenceEntryDTO1 = _evidenceEntryService.GetEvidenceEntryDTOByID(0);
            CEvidenceEntry cEvEntry1 = repositoryMock.Object.FindEvidenceEntryByID(0);

            Assert.AreEqual(cEvEntry1.Product.ID, evidenceEntryDTO1.ID);
            Assert.AreEqual(cEvEntry1.Amount, evidenceEntryDTO1.ProductAmount);
            Assert.AreEqual(cEvEntry1.Product.ID, evidenceEntryDTO1.Product.ID);

            Assert.That(() => _evidenceEntryService.GetEvidenceEntryDTOByID(-1), Throws.TypeOf<EvidenceEntryNotFoundException>());
        }
    }
}
