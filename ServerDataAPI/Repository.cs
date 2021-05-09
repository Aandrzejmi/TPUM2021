using System.Collections.Generic;

namespace Server.DataAPI
{
    internal class Repository : IRepository
    {
        private readonly DataContext dataContext;

        public Repository() 
        {
            dataContext = DbContext.Instance;
        }

        public Repository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        // ADD
        public bool AddProduct(Product product)
        {
            if (FindProductByID(product.ID) == null)
            {
                dataContext.Products.Add(product.Clone() as Product);
                return AddEvidenceEntry((new EvidenceEntry() { ProductID = product.ID, ProductAmount = 1 }));
            }
            else
                return false;
        }
        
        public bool AddEvidenceEntry(EvidenceEntry evidenceEntry)
        {
            if (FindEvidenceEntryByID(evidenceEntry.ID) == null)
            {
                if (FindProductByID(evidenceEntry.ProductID) == null)
                    return false;

                dataContext.EvidenceEntries.Add(evidenceEntry.Clone() as EvidenceEntry);
                return true;
            }
            return false;
        }

        public bool AddOrder(Order order)
        {
            if (FindOrderByID(order.ID) == null)
            {
                dataContext.Orders.Add(order.Clone() as Order);
                return true;
            }
            else
                return false;
                
        }
        public bool AddClient(Client client)
        {
            if (FindClientByID(client.ID) == null)
            {
                dataContext.Clients.Add(client.Clone() as Client);
                return true;
            }
            return false;
        }

        // MODIFY
        public bool ModifyProduct(Product product)
        {
            foreach(Product pr in dataContext.Products)
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

        public bool ModifyClient(Client client)
        {
            foreach (Client cl in dataContext.Clients)
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

        public bool ModifyOrder(Order order)
        {
            foreach (Order or in dataContext.Orders)
            {
                if (or.ID == order.ID)
                {
                    or.Products = new List<EvidenceEntry>(order.Products);
                    or.ClientID = order.ClientID;
                    return true;
                }
            }
            return false;
        }

        public bool ChangeProductAmount(int productID, int newAmount)
        {
            foreach (EvidenceEntry ev in dataContext.EvidenceEntries)
            {
                if (ev.ID == productID)
                {
                    ev.ProductAmount = newAmount;
                    return true;
                }
            }
            return false;
        }
        // FIND
        public Product FindProductByName(string name)
        {
            return dataContext.Products?.Find(x => x.Name == name)?.Clone() as Product;
        }

        public Product FindProductByID(int id)
        {
            return dataContext.Products?.Find(x => x.ID == id)?.Clone() as Product;
        }

        public EvidenceEntry FindEvidenceEntryByID(int id)
        {
            return dataContext.EvidenceEntries?.Find(x => x.ID == id)?.Clone() as EvidenceEntry;
        }

        public Client FindClientByID(int id)
        {
            return dataContext.Clients?.Find(x => x.ID == id)?.Clone() as Client;
        }

        public Client FindClientByName(string name)
        {
            return dataContext.Clients?.Find(x => x.Name == name)?.Clone() as Client;
        }

        public Order FindOrderByID(int id)
        {
            return dataContext.Orders?.Find(x => x.ID == id)?.Clone() as Order;
        }

        public List<Order> FindOrdersByClientID(int clientID)
        {
            List<Order> copy = new List<Order>();
            foreach(Order order in dataContext.Orders.FindAll(x => x.ClientID == clientID))
            {
                copy.Add(order.Clone() as Order);
            }
            return copy;
        }

        // LIST GETTERS
        public int CountProducts { get { return dataContext.Products.Count; } }

        public int CountOrders { get { return dataContext.Orders.Count; } }

        public int CountClients { get { return dataContext.Clients.Count; } }

        public int CountProductEntries { get { return dataContext.EvidenceEntries.Count; } }

        public List<Product> GetAllProducts()
        {
            return new List<Product>(dataContext.Products);
        }

        public List<Client> GetAllClients()
        {
            return new List<Client>(dataContext.Clients);
        }
        public List<Order> GetAllOrders()
        {
            return new List<Order>(dataContext.Orders);
        }

        public List<EvidenceEntry> GetAllEntries()
        {
            return new List<EvidenceEntry>(dataContext.EvidenceEntries);
        }
    }
}
