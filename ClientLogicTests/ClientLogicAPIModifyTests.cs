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
    class ClientLogicAPIModifyTests
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

            repositoryMock.Setup(p => p.ModifyClient(It.IsAny<CClient>())).Returns(true);
            repositoryMock.Setup(p => p.ModifyOrder(It.IsAny<COrder>())).Returns(true);
            repositoryMock.Setup(p => p.ModifyProduct(It.IsAny<CProduct>())).Returns(true);
            repositoryMock.Setup(p => p.ChangeProductAmount(It.IsAny<int>(), It.IsAny<int>())).Returns(true);

            _orderService = new OrderService(repositoryMock.Object);
            _productService = new ProductService(repositoryMock.Object);
            _evidenceEntryService = new EvidenceEntryService(repositoryMock.Object);
            _clientService = new ClientService(repositoryMock.Object);
        }

        [Test]
        public void ModifyProductTests()
        {
            ProductDTO productDTO = _productService.GetProductDTOByID(0);
            productDTO.Name = "Something else";
            Assert.IsTrue(_productService.ValidateModel(productDTO));
            Assert.IsTrue(_productService.ChangeProductDTO(productDTO.ID, productDTO));
        }

        [Test]
        public void ModifyOrderTests()
        {
            List<EvidenceEntryDTO> evidenceEntryDTOs = new List<EvidenceEntryDTO>();
            EvidenceEntryDTO evidenceEntryDTO = new EvidenceEntryDTO() { Product = new ProductDTO() { ID = 0, Name = "Temp", Price = 1.0M }, ProductAmount = 1 };
            evidenceEntryDTOs.Add(evidenceEntryDTO);
            OrderDTO orderDTO = new OrderDTO() { Products = evidenceEntryDTOs, Client = new ClientDTO() { ID = 0, Name = "Temp name", Adress = "Temp Adress" } };
            orderDTO.Products[0].ProductAmount = 2;
            Assert.IsTrue(_orderService.ValidateModel(orderDTO));
            Assert.IsTrue(_orderService.ChangeOrderDTO(orderDTO.ID, orderDTO));
        }

        [Test]
        public void ModifyClientTests()
        {
            ClientDTO clientDTO = _clientService.GetClientDTOByID(0);
            clientDTO.Name = "Something else";
            Assert.IsTrue(_clientService.ValidateModel(clientDTO));
            Assert.IsTrue(_clientService.ChangeClientDTO(clientDTO.ID, clientDTO));
        }

        [Test]
        public void ChangeProductAmountTest()
        {
            EvidenceEntryDTO evidenceEntryDTO = _evidenceEntryService.GetEvidenceEntryDTOByID(0);
            evidenceEntryDTO.ProductAmount = 2;
            Assert.IsTrue(_evidenceEntryService.ValidateModel(evidenceEntryDTO));
            Assert.IsTrue(_evidenceEntryService.ChangeEvidenceEntryDTO(evidenceEntryDTO.Product.ID, evidenceEntryDTO));
        }
    }
}
