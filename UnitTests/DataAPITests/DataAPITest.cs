using NUnit.Framework;
using DataAPI;
using System.Collections;

namespace UnitTests
{
    public class DataAPITests
    {
        IRepository repo;
        [SetUp]
        public void Setup()
        {
            repo = Data.CreateRepository();
        }

        [Test]
        public void RepositoryProductEvidency()
        {
            // Size of the list of Entries
            Assert.AreEqual(repo.CountProductEntries, 4);

            // Find entry by ID
            Assert.IsNotNull(repo.FindEvidenceEntryByID(0));

            // Check if it has 1 product
            Assert.AreEqual(repo.FindEvidenceEntryByID(1).ProductAmount, 1);

            // Check if you can change product amount to 2
            Assert.IsTrue(repo.ChangeProductAmount(1, 2));

            // Check if it changed
            Assert.AreEqual(repo.FindEvidenceEntryByID(1).ProductAmount, 2);

            // Add new product with new ID, check if it created new evidence entry with 1 as amount
            Product product2 = new Product() { ID = 4, Name = "Boring Product", Price = 25.0M };
            repo.AddProduct(product2);
            Assert.IsNotNull(repo.FindEvidenceEntryByID(4));
            Assert.AreEqual(repo.FindEvidenceEntryByID(4).ProductAmount, 1);
            // Size of the list of Entries
            Assert.AreEqual(repo.CountProductEntries, 5);

            // Try to add new evidence entry with product that already exists
            Assert.IsFalse(repo.AddEvidenceEntry(new EvidenceEntry() { ProductID = product2.ID, ProductAmount = 1 }));
            // Size of the list of Entries
            Assert.AreEqual(repo.CountProductEntries, 5);
        }

        [Test]
        public void RepositoryProducts()
        {
            // Size of the list of Products
            Assert.AreEqual(repo.CountProducts, 4);

            // Find product by name
            Assert.IsNotNull(repo.FindProductByName("Product2"));

            // Find product by ID
            Assert.IsNotNull(repo.FindProductByID(3));

            // Modify existing product and check its name
            Product product1 = new Product() { ID = 0, Name = "Extra New Cool Product", Price = 10.0M};
            repo.ModifyProduct(product1);
            Assert.IsTrue(repo.FindProductByID(0).Name == "Extra New Cool Product");

            // Try to find non existing product
            Assert.IsNull(repo.FindProductByID(4));

            // Create new product and try to add it
            Product product2 = new Product() { ID = 4, Name = "Boring Product", Price = 25.0M};
            repo.AddProduct(product2);
            Assert.IsNotNull(repo.FindProductByID(4));

            // Check if you can add product with existing ID, you shouldn't be able to do that
            Assert.AreEqual(repo.CountProducts, 5);
            repo.AddProduct(product2);
            Assert.AreEqual(repo.CountProducts, 5);
        }

        [Test]
        public void RepositoryClients()
        {
            // Size of the list of Clients
            Assert.AreEqual(repo.CountClients, 4);

            // Find Client by name
            Assert.IsNotNull(repo.FindClientByName("Jane Doe"));

            // Find Client by ID
            Assert.IsNotNull(repo.FindProductByID(2));

            // Modify existing client and check its adress
            Client client1 = new Client() { ID = 0, Name = "Janusz Patena", Adress = "Zgierz, £ódzka 5" };
            repo.ModifyClient(client1);
            Assert.IsTrue(repo.FindClientByID(0).Adress == "Zgierz, £ódzka 5");

            // Try to find non existing client
            Assert.IsNull(repo.FindClientByID(4));

            // Create new product and try to add it
            Client client2 = new Client() { ID = 4, Name = "Mateusz Zawiadowski", Adress = "Chrz¹szczy¿ewoszyce 5" };
            repo.AddClient(client2);
            Assert.IsNotNull(repo.FindClientByID(4));

            // Check if you can add client with existing ID, you shouldn't be able to do that
            Assert.AreEqual(repo.CountClients, 5);
            repo.AddClient(client2);
            Assert.AreEqual(repo.CountClients, 5);
        }


        [Test]
        public void RepositoryOrders()
        {
            // Size of the list of Clients
            Assert.AreEqual(repo.CountOrders, 4);

            // Find order by ID
            Assert.IsNotNull(repo.FindOrderByID(0));

            // Find all orders by clients, 0 products, 1 product, more then 1 product
            Assert.AreEqual(repo.FindOrdersByClientID(1).Count, 2);

            repo.FindOrdersByClientID(1)[0].ID = 555;
            Assert.IsNull(repo.FindOrderByID(555));

            Assert.AreEqual(repo.FindOrdersByClientID(2).Count, 1);
            Assert.AreEqual(repo.FindOrdersByClientID(0).Count, 0);

            // Modify existing order and check its clientID
            Order order1 = repo.FindOrderByID(3);
            order1.ClientID = 0;            
            repo.ModifyOrder(order1);
            Assert.IsTrue(repo.FindOrderByID(3).ClientID == 0);

            // Try to find non existing order by ID
            Assert.IsNull(repo.FindOrderByID(4));

            // Create new order and try to add it
            Order order2 = new Order() { ID = 4, ClientID = 0 };
            repo.AddOrder(order2);
            Assert.IsNotNull(repo.FindOrderByID(4));

            // Check if you can add order with existing ID, you shouldn't be able to do that
            Assert.AreEqual(repo.CountOrders, 5);
            repo.AddOrder(order2);
            Assert.AreEqual(repo.CountOrders, 5);
        }
    }
}