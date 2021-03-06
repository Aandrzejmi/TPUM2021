using Client.DataAPI;
using CommunicationAPI.Models;
using NUnit.Framework;

namespace Client.DataTests
{
    public class ClientDataAPITests
    {
        
        IRepository repo;
        [SetUp]
        public void Setup()
        {
            Data.ClearContext();
            repo = Data.GetRepository();
            FillDbContext();
        }

        [Test]
        public void RepositoryProductEvidency()
        {
            // Size of the list of Entries
            Assert.AreEqual(repo.CountProductEntries, 4);

            // Find entry by ID
            Assert.IsNotNull(repo.FindEvidenceEntryByID(0));

            // Check if it has 1 product
            Assert.AreEqual(repo.FindEvidenceEntryByID(1).Amount, 1);

            // Check if you can change product amount to 2
            Assert.IsTrue(repo.ChangeProductAmount(1, 2));

            // Check if it changed
            Assert.AreEqual(repo.FindEvidenceEntryByID(1).Amount, 2);

            // Add new product with new ID, check if it created new evidence entry with 1 as amount
            CProduct product2 = new CProduct() { ID = 4, Name = "Boring Product", Price = 25.0M };
            repo.AddProduct(product2);
            Assert.IsNotNull(repo.FindEvidenceEntryByID(4));
            Assert.AreEqual(repo.FindEvidenceEntryByID(4).Amount, 1);
            // Size of the list of Entries
            Assert.AreEqual(repo.CountProductEntries, 5);

            // Try to add new evidence entry with product that already exists
            Assert.IsFalse(repo.AddEvidenceEntry(new CEvidenceEntry() { Product = product2, Amount = 1 }));
            // Size of the list of Entries
            Assert.AreEqual(repo.CountProductEntries, 5);
        }

        [Test]
        public void RepositoryProducts()
        {
            // Size of the list of Products
            Assert.AreEqual(4, repo.CountProducts);

            // Find product by name
            Assert.IsNotNull(repo.FindProductByName("Product2"));

            // Find product by ID
            Assert.IsNotNull(repo.FindProductByID(3));

            // Modify existing product and check its name
            CProduct product1 = new CProduct() { ID = 0, Name = "Extra New Cool Product", Price = 10.0M };
            repo.ModifyProduct(product1);
            Assert.IsTrue(repo.FindProductByID(0).Name == "Extra New Cool Product");

            // Try to find non existing product
            Assert.IsNull(repo.FindProductByID(4));

            // Create new product and try to add it
            CProduct product2 = new CProduct() { ID = 4, Name = "Boring Product", Price = 25.0M };
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
            CClient client1 = new CClient() { ID = 0, Name = "Janusz Patena", Adress = "Zgierz, ??dzka 5" };
            repo.ModifyClient(client1);
            Assert.IsTrue(repo.FindClientByID(0).Adress == "Zgierz, ??dzka 5");

            // Try to find non existing client
            Assert.IsNull(repo.FindClientByID(4));

            // Create new product and try to add it
            CClient client2 = new CClient() { ID = 4, Name = "Mateusz Zawiadowski", Adress = "Chrz?szczy?ewoszyce 5" };
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
            Assert.AreEqual(4, repo.CountOrders);

            // Find order by ID
            Assert.IsNotNull(repo.FindOrderByID(0));

            // Find all orders by clients, 0 products, 1 product, more then 1 product
            Assert.AreEqual(3, repo.FindOrdersByClientID(1).Count);

            repo.FindOrdersByClientID(1)[0].ID = 555;
            Assert.IsNotNull(repo.FindOrderByID(555));

            Assert.AreEqual(1, repo.FindOrdersByClientID(2).Count);
            Assert.AreEqual(0, repo.FindOrdersByClientID(0).Count);

            // Modify existing order and check its clientID
            COrder order1 = repo.FindOrderByID(3);
            order1.Client = repo.FindClientByID(0);
            repo.ModifyOrder(order1);
            Assert.IsTrue(repo.FindOrderByID(3).Client.ID == 0);

            // Try to find non existing order by ID
            Assert.IsNull(repo.FindOrderByID(4));

            // Create new order and try to add it
            COrder order2 = new COrder() { ID = 4, Client = repo.FindClientByID(0) };
            repo.AddOrder(order2);
            Assert.IsNotNull(repo.FindOrderByID(4));

            // Check if you can add order with existing ID, you shouldn't be able to do that
            Assert.AreEqual(repo.CountOrders, 5);
            repo.AddOrder(order2);
            Assert.AreEqual(repo.CountOrders, 5);
        }

        private void FillDbContext()
        {
            CProduct product1 = new CProduct() { ID = 0, Name = "Product1", Price = 10 };
            CProduct product2 = new CProduct() { ID = 1, Name = "Product2", Price = 20 };
            CProduct product3 = new CProduct() { ID = 2, Name = "Product3", Price = 30 };
            CProduct product4 = new CProduct() { ID = 3, Name = "Product4", Price = 40 };
            repo.AddProduct(product1);
            repo.AddProduct(product2);
            repo.AddProduct(product3);
            repo.AddProduct(product4);

            repo.AddClient(new CClient() { ID = 0, Name = "Anny Mouse", Adress = "Warsaw, Aleje Jerozolimskie 2" });
            repo.AddClient(new CClient() { ID = 1, Name = "Jane Doe", Adress = "??d?, Dolna 10" });
            repo.AddClient(new CClient() { ID = 2, Name = "Hrabia Tyczy?ski", Adress = "??d?, W?lcza?ska 160" });
            repo.AddClient(new CClient() { ID = 3, Name = "Jan Jer", Adress = "??d?, Pomorska 45" });

            var order0 = new COrder() { ID = 0, Client = repo.FindClientByID(1) };
            order0.Entries.Add(new CEvidenceEntry() { Product = product1, Amount = 1 });
            order0.Entries.Add(new CEvidenceEntry() { Product = product2, Amount = 1 });
            order0.Entries.Add(new CEvidenceEntry() { Product = product3, Amount = 1 });
            order0.Entries.Add(new CEvidenceEntry() { Product = product4, Amount = 1 });
            repo.AddOrder(order0);

            var order1 = new COrder() { ID = 1, Client = repo.FindClientByID(2) };
            order1.Entries.Add(new CEvidenceEntry() { Product = product1, Amount = 1 });
            order1.Entries.Add(new CEvidenceEntry() { Product = product3, Amount = 1 });
            repo.AddOrder(order1);

            var order2 = new COrder() { ID = 2, Client = repo.FindClientByID(1) };
            order2.Entries.Add(new CEvidenceEntry() { Product = product2, Amount = 1 });
            order2.Entries.Add(new CEvidenceEntry() { Product = product4, Amount = 1 });
            repo.AddOrder(order2);

            var order3 = new COrder() { ID = 3, Client = repo.FindClientByID(1) };
            order3.Entries.Add(new CEvidenceEntry() { Product = product1, Amount = 1 });
            order3.Entries.Add(new CEvidenceEntry() { Product = product2, Amount = 1 });
            order3.Entries.Add(new CEvidenceEntry() { Product = product4, Amount = 1 });
            repo.AddOrder(order3);
        }
    }
}