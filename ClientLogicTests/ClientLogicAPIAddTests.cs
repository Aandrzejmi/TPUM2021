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
    public class ClientLogicAPIAddTests
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
        public void AddProductTests()
        {
            // Try to add new product
            ProductDTO productDTO1 = new ProductDTO() { ID = 1, Name = "Temporary Product", Price = 1.0M };
            Assert.IsTrue(_productService.AddProductDTO(productDTO1));

            // Try to add product with invalid ID
            ProductDTO productDTO2 = new ProductDTO() { ID = -1, Name = "Temporary Product", Price = 1.0M };
            Assert.Throws<ProductInvalidIDException>(() => _productService.AddProductDTO(productDTO2));

            // Try to add product with invalid Name
            ProductDTO productDTO3 = new ProductDTO() { ID = 2, Name = "", Price = 1.0M };
            Assert.Throws<ProductInvalidNameException>(() => _productService.AddProductDTO(productDTO3));

            // Try to add product with invalid Price
            ProductDTO productDTO4 = new ProductDTO() { ID = 2, Name = "Temporary Product", Price = -1.0M };
            Assert.Throws<ProductInvalidPriceException>(() => _productService.AddProductDTO(productDTO4));
        }

        [Test]
        public void AddClientTests()
        {
            // Try to add new client
            ClientDTO clientDTO1 = new ClientDTO() { ID = 1, Name = "Temporary Name", Adress = "Temporary Adress" };
            Assert.IsTrue(_clientService.AddClientDTO(clientDTO1));

            // Try to add new client with invalid ID
            ClientDTO clientDTO2 = new ClientDTO() { ID = -1, Name = "Temporary Name", Adress = "Temporary Adress" };
            Assert.Throws<ClientInvalidIDException>(() => _clientService.AddClientDTO(clientDTO2));

            // Try to add new client with invalid Name
            ClientDTO clientDTO3 = new ClientDTO() { ID = 1, Name = "", Adress = "Temporary Adress" };
            Assert.Throws<ClientInvalidNameException>(() => _clientService.AddClientDTO(clientDTO3));

            // Try to add new client with invalid Adress
            ClientDTO clientDTO4 = new ClientDTO() { ID = 1, Name = "Temporary Name", Adress = "" };
            Assert.Throws<ClientInvalidAdressException>(() => _clientService.AddClientDTO(clientDTO4));
        }

        [Test]
        public void AddEvidenceEntryTests()
        {
            EvidenceEntryDTO evidenceEntryDTO1 = new EvidenceEntryDTO() { Product = new ProductDTO() { ID = 0, Name = "Temporary Product", Price = 1.0M }, ProductAmount = 1 };
            Assert.IsTrue(_evidenceEntryService.AddEvidenceEntryDTO(evidenceEntryDTO1));

            EvidenceEntryDTO evidenceEntryDTO2 = new EvidenceEntryDTO() { Product = new ProductDTO() { ID = -1, Name = "Temporary Product", Price = 1.0M }, ProductAmount = 1 };
            Assert.Throws<EvidenceEntryInvalidIDException>(() => _evidenceEntryService.AddEvidenceEntryDTO(evidenceEntryDTO2));

            EvidenceEntryDTO evidenceEntryDTO3 = new EvidenceEntryDTO() { Product = new ProductDTO() { ID = 1, Name = "", Price = 1.0M }, ProductAmount = 1 };
            Assert.Throws<ProductInvalidNameException>(() => _evidenceEntryService.AddEvidenceEntryDTO(evidenceEntryDTO3));

            EvidenceEntryDTO evidenceEntryDTO4 = new EvidenceEntryDTO() { Product = new ProductDTO() { ID = 1, Name = "Temporary Product", Price = 0.0M }, ProductAmount = 1 };
            Assert.Throws<ProductInvalidPriceException>(() => _evidenceEntryService.AddEvidenceEntryDTO(evidenceEntryDTO4));

            EvidenceEntryDTO evidenceEntryDTO5 = new EvidenceEntryDTO() { Product = new ProductDTO() { ID = 1, Name = "Temporary Product", Price = 1.0M }, ProductAmount = -1 };
            Assert.Throws<EvidenceEntryInvalidProductAmountException>(() => _evidenceEntryService.AddEvidenceEntryDTO(evidenceEntryDTO5));
        }

        [Test]
        public void AddOrderTests()
        {
            ClientDTO clientDTO = new ClientDTO() { ID = 0, Name = "Temporary Name", Adress = "Temporary Adress" };
            EvidenceEntryDTO evidenceEntryDTO = new EvidenceEntryDTO() { Product = new ProductDTO() { ID = 0, Name = "Temporary Product", Price = 1.0M }, ProductAmount = 1 };
            List<EvidenceEntryDTO> evidenceEntryDTOs = new List<EvidenceEntryDTO>();
            evidenceEntryDTOs.Add(evidenceEntryDTO);
            OrderDTO orderDTO1 = new OrderDTO() { ID = 0, Products = evidenceEntryDTOs, Client = clientDTO };
            Assert.IsTrue(_orderService.AddOrderDTO(orderDTO1));

            OrderDTO orderDTO2 = new OrderDTO() { ID = -1, Products = evidenceEntryDTOs, Client = clientDTO };
            Assert.Throws<OrderInvalidIDException>(() => _orderService.AddOrderDTO(orderDTO2));

            ClientDTO clientDTO2 = new ClientDTO() { ID = -1, Name = "Temporary Name", Adress = "Temporary Adress" };
            List<EvidenceEntryDTO> evidenceEntryDTOs2 = new List<EvidenceEntryDTO>();
            EvidenceEntryDTO evidenceEntryDTO2 = new EvidenceEntryDTO() { Product = new ProductDTO() { ID = 0, Name = "Temporary Product", Price = 1.0M }, ProductAmount = 1 };
            evidenceEntryDTOs2.Add(evidenceEntryDTO2);
            OrderDTO orderDTO3 = new OrderDTO() { ID = 0, Products = evidenceEntryDTOs2, Client = clientDTO2 };
            Assert.Throws<OrderInvalidClientIDException>(() => _orderService.AddOrderDTO(orderDTO3));
        }
    }
}
