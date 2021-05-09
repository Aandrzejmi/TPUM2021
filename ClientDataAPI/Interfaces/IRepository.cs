using System.Collections.Generic;
using CommunicationAPI.Models;

namespace Client.DataAPI
{
    public interface IRepository
    {
        public bool AddProduct(CProduct product);
        public bool AddEvidenceEntry(CEvidenceEntry evidenceEntry);
        public bool AddOrder(COrder order);
        public bool AddClient(CClient client);
        public bool ModifyProduct(CProduct product);
        public bool ModifyClient(CClient client);
        public bool ModifyOrder(COrder order);
        public bool ChangeProductAmount(int productID, int newAmount);
        public CProduct FindProductByName(string name);
        public CProduct FindProductByID(int id);
        public CEvidenceEntry FindEvidenceEntryByID(int id);
        public CClient FindClientByID(int id);
        public CClient FindClientByName(string name);
        public COrder FindOrderByID(int id);
        public List<COrder> FindOrdersByClientID(int clientID);
        public List<COrder> GetAllOrders();
        public List<CProduct> GetAllProducts();
        public List<CClient> GetAllClients();
        public List<CEvidenceEntry> GetAllEntries();
        public int CountProducts { get; }
        public int CountOrders { get; }
        public int CountClients { get; }
        public int CountProductEntries { get; }
    }
}
