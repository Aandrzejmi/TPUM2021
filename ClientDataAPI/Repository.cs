using System.Collections.Generic;
using CommunicationAPI.Models;

namespace Client.DataAPI
{
    public class Repository : IRepository
    {
        private readonly DataContext dataContext;
        private readonly object lockobj = new object();

        public Repository()
        {
            dataContext = DbContext.Instance;
        }

        public Repository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        // ADD
        public bool AddProduct(CProduct product)
        {
            lock (lockobj)
            {
                if (FindProductByID(product.ID) == null)
                {
                    dataContext.CProducts.Add(product);
                    return AddEvidenceEntry((new CEvidenceEntry() { Product = product, Amount = 1 }));
                }
                else
                    return false;
            }
        }
        
        public bool AddEvidenceEntry(CEvidenceEntry evidenceEntry)
        {
            lock (lockobj)
            {
                if (FindEvidenceEntryByID(evidenceEntry.Product.ID) == null)
                {
                    if (FindProductByID(evidenceEntry.Product.ID) == null)
                        return false;

                    dataContext.CEvidenceEntries.Add(evidenceEntry);
                    return true;
                }
                return false;
            }
        }

        public bool AddOrder(COrder order)
        {
            lock (lockobj)
            {
                if (FindOrderByID(order.ID) == null)
                {
                    dataContext.COrders.Add(order);
                    return true;
                }
                else
                    return false;
            }
                
        }
        public bool AddClient(CClient client)
        {
            lock (lockobj)
            {
                if (FindClientByID(client.ID) == null)
                {
                    dataContext.CClients.Add(client);
                    return true;
                }
                return false;
            }
        }

        // MODIFY
        public bool ModifyProduct(CProduct product)
        {
            lock (lockobj)
            {
                foreach (CProduct pr in dataContext.CProducts)
                {
                    if (pr.ID == product.ID)
                    {
                        pr.Name = product.Name;
                        pr.Price = product.Price;
                        return true;
                    }
                }
                return false;
            }
        }

        public bool ModifyClient(CClient client)
        {
            lock (lockobj)
            {
                foreach (CClient cl in dataContext.CClients)
                {
                    if (cl.ID == client.ID)
                    {
                        cl.Name = client.Name;
                        cl.Adress = client.Adress;
                        return true;
                    }
                }
                return false;
            }
        }

        public bool ModifyOrder(COrder order)
        {
            lock (lockobj)
            {
                foreach (COrder or in dataContext.COrders)
                {
                    if (or.ID == order.ID)
                    {
                        or.Entries = new List<CEvidenceEntry>(order.Entries);
                        or.Client = order.Client;
                        return true;
                    }
                }
                return false;
            }
        }

        public bool ChangeProductAmount(int productID, int newAmount)
        {
            lock (lockobj)
            {
                foreach (CEvidenceEntry ev in dataContext.CEvidenceEntries)
                {
                    if (ev.Product.ID == productID)
                    {
                        ev.Amount = newAmount;
                        return true;
                    }
                }
                return false;
            }
        }
        // FIND
        public CProduct FindProductByName(string name)
        {
            lock (lockobj)
            {
                return dataContext.CProducts?.Find(x => x.Name == name);
            }
        }

        public CProduct FindProductByID(int id)
        {
            lock (lockobj)
            {
                return dataContext.CProducts?.Find(x => x.ID == id);
            }
        }

        public CEvidenceEntry FindEvidenceEntryByID(int id)
        {
            lock (lockobj)
            {
                return dataContext.CEvidenceEntries?.Find(x => x.Product.ID == id);
            }
        }

        public CClient FindClientByID(int id)
        {
            lock (lockobj)
            {
                return dataContext.CClients?.Find(x => x.ID == id);
            }
        }

        public CClient FindClientByName(string name)
        {
            lock (lockobj)
            {
                return dataContext.CClients?.Find(x => x.Name == name);
            }
        }

        public COrder FindOrderByID(int id)
        {
            lock (lockobj)
            {
                return dataContext.COrders?.Find(x => x.ID == id);
            }
        }

        public List<COrder> FindOrdersByClientID(int clientID)
        {
            lock (lockobj)
            {
                List<COrder> copy = new List<COrder>();
                foreach (COrder order in dataContext.COrders.FindAll(x => x.Client.ID == clientID))
                {
                    copy.Add(order);
                }
                return copy;
            }
        }

        // LIST GETTERS
        public int CountProducts { get { lock (lockobj) return dataContext.CProducts.Count; } }

        public int CountOrders { get { lock (lockobj) return dataContext.COrders.Count; } }

        public int CountClients { get { lock (lockobj) return dataContext.CClients.Count; } }

        public int CountProductEntries { get { lock (lockobj) return dataContext.CProducts.Count; } }

        public List<CProduct> GetAllProducts()
        {
            lock (lockobj)
            {
                return new List<CProduct>(dataContext.CProducts);
            }
        }

        public List<CClient> GetAllClients()
        {
            lock (lockobj)
            {
                return new List<CClient>(dataContext.CClients);
            }
        }
        public List<COrder> GetAllOrders()
        {
            lock (lockobj)
            {
                return new List<COrder>(dataContext.COrders);
            }
        }

        public List<CEvidenceEntry> GetAllEntries()
        {
            lock (lockobj)
            {
                return new List<CEvidenceEntry>(dataContext.CEvidenceEntries);
            }
        }
    }
}
