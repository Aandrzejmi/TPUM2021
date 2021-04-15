using System;
using System.Collections.Generic;
using System.Text;

namespace DataAPI
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
        public int CountProducts();
        public int CountOrders();
        public int CountClients();
        public int CountProductEntries();
    }
}
