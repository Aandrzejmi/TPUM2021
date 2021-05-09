using System.Collections.Generic;

namespace Server.DataAPI
{
    public interface IRepository
    {
        public bool AddProduct(Product product);
        public bool AddEvidenceEntry(EvidenceEntry evidenceEntry);
        public bool AddOrder(Order order);
        public bool AddClient(Client client);
        public bool ModifyProduct(Product product);
        public bool ModifyClient(Client client);
        public bool ModifyOrder(Order order);
        public bool ChangeProductAmount(int productID, int newAmount);
        public Product FindProductByName(string name);
        public Product FindProductByID(int id);
        public EvidenceEntry FindEvidenceEntryByID(int id);
        public Client FindClientByID(int id);
        public Client FindClientByName(string name);
        public Order FindOrderByID(int id);
        public List<Order> FindOrdersByClientID(int clientID);
        public List<Order> GetAllOrders();
        public List<Product> GetAllProducts();
        public List<Client> GetAllClients();
        public List<EvidenceEntry> GetAllEntries();
        public int CountProducts { get; }
        public int CountOrders { get; }
        public int CountClients { get; }
        public int CountProductEntries { get; }
    }
}
