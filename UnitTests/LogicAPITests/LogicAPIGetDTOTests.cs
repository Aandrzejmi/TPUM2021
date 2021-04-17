using NUnit.Framework;
using System.Collections.Generic;
using LogicAPI;
using DataAPI;
using DataAPI.DTOs;
using LogicAPI.Interfaces;
using LogicAPI.Services;
using LogicAPI.Exceptions;
using Moq;

namespace UnitTests.LogicAPITests
{
    public class LogicAPIGetDTOTests
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

            List<int> evidenceEntries = new List<int>();
            Product product = new Product() { ID = 0, Name = "Temp Product", Price = 1.0M };

            EvidenceEntry evidenceEntry = new EvidenceEntry() { ProductID = 0, ProductAmount = 0 };
            evidenceEntries.Add(evidenceEntry.ID);

            repositoryMock.Setup(p => p.FindClientByID(0)).Returns(new Client() { ID = 0, Adress = "Temp Adress", Name = "Temp Name" });
            repositoryMock.Setup(p => p.FindClientByID(-1)).Returns(null as Client);
            repositoryMock.Setup(p => p.FindClientByID(5)).Returns(null as Client);

            repositoryMock.Setup(p => p.FindClientByName("Temp Name")).Returns(new Client() { ID = 0, Adress = "Temp Adress", Name = "Temp Name" });
            repositoryMock.Setup(p => p.FindClientByName("")).Returns(null as Client);

            repositoryMock.Setup(p => p.FindProductByID(0)).Returns(new Product() { ID = 0, Name = "Temp Product", Price = 1.0M });
            repositoryMock.Setup(p => p.FindProductByID(-1)).Returns(null as Product);
            repositoryMock.Setup(p => p.FindProductByID(5)).Returns(null as Product);

            repositoryMock.Setup(p => p.FindProductByName("Temp Product")).Returns(new Product() { ID = 0, Name = "Temp Product", Price = 1.0M });
            repositoryMock.Setup(p => p.FindProductByName("")).Returns(null as Product);

            repositoryMock.Setup(p => p.FindEvidenceEntryByID(0)).Returns(new EvidenceEntry() { ProductID = 0, ProductAmount = 0 });
            repositoryMock.Setup(p => p.FindEvidenceEntryByID(-1)).Returns(null as EvidenceEntry);
            repositoryMock.Setup(p => p.FindEvidenceEntryByID(5)).Returns(null as EvidenceEntry);

            repositoryMock.Setup(p => p.FindOrderByID(0)).Returns(new Order() { Products = evidenceEntries, ClientID = 0 });
            repositoryMock.Setup(p => p.FindOrderByID(-1)).Returns(null as Order);
            repositoryMock.Setup(p => p.FindOrderByID(5)).Returns(null as Order);

            repositoryMock.Setup(p => p.FindOrdersByClientID(0)).Returns(new List<Order> { new Order() { Products = evidenceEntries, ClientID = 0 } });
            repositoryMock.Setup(p => p.FindOrdersByClientID(-1)).Returns(new List<Order>());
            repositoryMock.Setup(p => p.FindOrdersByClientID(5)).Returns(new List<Order>());

            _orderService = new OrderService(repositoryMock.Object);
            _productService = new ProductService(repositoryMock.Object);
            _evidenceEntryService = new EvidenceEntryService(repositoryMock.Object);
            _clientService = new ClientService(repositoryMock.Object);
        }

        [Test]
        public void GetOrderDTOTests()
        {
            // Find order by ID
            OrderDTO orderDTO = _orderService.GetOrderDTOByID(0);
            Order order = repositoryMock.Object.FindOrderByID(0);
            Assert.AreEqual(orderDTO.ID, order.ID);
            Assert.AreEqual(orderDTO.ClientID, order.ClientID);
            Assert.AreEqual(orderDTO.Products.Count, order.Products.Count);
            foreach (EvidenceEntryDTO evidenceEntryDTO in orderDTO.Products)
            {
                Assert.IsTrue(order.Products.Contains(evidenceEntryDTO.ID));
            }

            Assert.That(() => _orderService.GetOrderDTOByID(-1), Throws.TypeOf<OrderNotFoundException>());

            // Find order by ClientID
            List<OrderDTO> orderDTOs = _orderService.GetOrdersDTOByClientID(0);
            List<Order> orders = repositoryMock.Object.FindOrdersByClientID(0);

            Assert.AreEqual(orders.Count, orders.Count);
            Assert.AreEqual(orders.Count, 1);
        }

        [Test]
        public void GetProductDTOTests()
        {
            // Find Product by ID
            ProductDTO productDTO1 = _productService.GetProductDTOByID(0);
            Product product1 = repositoryMock.Object.FindProductByID(0);
            Assert.AreEqual(productDTO1.ID, product1.ID);
            Assert.AreEqual(productDTO1.Name, product1.Name);
            Assert.AreEqual(productDTO1.Price, product1.Price);

            Assert.That(() => _productService.GetProductDTOByID(-1), Throws.TypeOf<ProductNotFoundException>());

            // Find Product by name
            ProductDTO productDTO2 = _productService.GetProductDTOByName("Temp Product");
            Product product2 = repositoryMock.Object.FindProductByName("Temp Product");
            Assert.AreEqual(productDTO2.ID, product2.ID);
            Assert.AreEqual(productDTO2.Name, product2.Name);
            Assert.AreEqual(productDTO2.Price, product2.Price);

            Assert.That(() => _productService.GetProductDTOByName(""), Throws.TypeOf<ProductNotFoundException>());
        }

        [Test]
        public void GetClientDTOTests()
        {
            ClientDTO clientDTO1 = _clientService.GetClientDTOByID(0);
            Client client1 = repositoryMock.Object.FindClientByID(0);
            Assert.AreEqual(clientDTO1.ID, client1.ID);
            Assert.AreEqual(clientDTO1.Name, client1.Name);
            Assert.AreEqual(clientDTO1.Adress, client1.Adress);

            Assert.That(() => _clientService.GetClientDTOByID(-1), Throws.TypeOf<ClientNotFoundException>());

            ClientDTO clientDTO2 = _clientService.GetClientDTOByName("Temp Name");
            Client client2 = repositoryMock.Object.FindClientByName("Temp Name");
            Assert.AreEqual(clientDTO2.ID, client2.ID);
            Assert.AreEqual(clientDTO2.Name, client2.Name);
            Assert.AreEqual(clientDTO2.Adress, client2.Adress);

            Assert.That(() => _clientService.GetClientDTOByName(""), Throws.TypeOf<ClientNotFoundException>());

        }

        [Test]
        public void GetEvidenceEntryDTOTests()
        {
            EvidenceEntryDTO evidenceEntryDTO1 = _evidenceEntryService.GetEvidenceEntryDTOByID(0);
            EvidenceEntry evidenceEntry1 = repositoryMock.Object.FindEvidenceEntryByID(0);

            Assert.AreEqual(evidenceEntryDTO1.ID, evidenceEntry1.ID);
            Assert.AreEqual(evidenceEntryDTO1.ProductAmount, evidenceEntry1.ProductAmount);
            Assert.AreEqual(evidenceEntryDTO1.Product.ID, evidenceEntry1.ProductID);

            Assert.That(() => _evidenceEntryService.GetEvidenceEntryDTOByID(-1), Throws.TypeOf<EvidenceEntryNotFoundException>());
        }
    }    
}
