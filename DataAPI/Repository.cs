using System;
using System.Collections.Generic;
using System.Text;

namespace DataAPI
{
    public class Repository : IRepository
    {
        private List<Client> clients = new List<Client>();
        private List<Order> orders = new List<Order>();
        private List<Product> products = new List<Product>();
        private List<EvidenceEntry> productEvidency = new List<EvidenceEntry>();

        public Repository()
        {
            
        }

        // ADD
        public bool AddProduct(Product product)
        {
            if (FindProductByID(product.ID) == null)
            {
                products.Add(product.Clone() as Product);
                return AddEvidenceEntry((new EvidenceEntry() { ProductID = product.ID, ProductAmount = 1 }));
            }
            else
                return false;
        }
        
        public bool AddEvidenceEntry(EvidenceEntry evidenceEntry)
        {
            if (FindEvidenceEntryByID(evidenceEntry.ID) == null)
            {
                //if (FindProductByID(evidenceEntry.ProductID) == null)
                    //return false;

                productEvidency.Add(evidenceEntry.Clone() as EvidenceEntry);
                return true;
            }
            return false;
        }

        public bool AddOrder(Order order)
        {
            if (FindOrderByID(order.ID) == null)
            {
                orders.Add(order.Clone() as Order);
                return true;
            }
            else
                return false;
                
        }
        public bool AddClient(Client client)
        {
            if (FindClientByID(client.ID) == null)
            {
                clients.Add(client.Clone() as Client);
                return true;
            }
            return false;
        }

        // MODIFY
        public bool ModifyProduct(Product product)
        {
            foreach(Product pr in products)
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
            foreach (Client cl in clients)
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
            foreach (Order or in orders)
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
            foreach (EvidenceEntry ev in productEvidency)
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
            return products?.Find(x => x.Name == name)?.Clone() as Product;
        }

        public Product FindProductByID(int id)
        {
            return products?.Find(x => x.ID == id)?.Clone() as Product;
        }

        public EvidenceEntry FindEvidenceEntryByID(int id)
        {
            return productEvidency?.Find(x => x.ID == id)?.Clone() as EvidenceEntry;
        }

        public Client FindClientByID(int id)
        {
            return clients?.Find(x => x.ID == id)?.Clone() as Client;
        }

        public Client FindClientByName(string name)
        {
            return clients?.Find(x => x.Name == name)?.Clone() as Client;
        }

        public Order FindOrderByID(int id)
        {
            return orders?.Find(x => x.ID == id)?.Clone() as Order;
        }

        public List<Order> FindOrdersByClientID(int clientID)
        {
            List<Order> copy = new List<Order>();
            foreach(Order order in orders.FindAll(x => x.ClientID == clientID))
            {
                copy.Add(order.Clone() as Order);
            }
            return copy;
        }

        // LIST GETTERS
        public int CountProducts { get { return products.Count; } }

        public int CountOrders { get { return orders.Count; } }

        public int CountClients { get { return clients.Count; } }

        public int CountProductEntries { get { return productEvidency.Count; } }

        public List<Product> GetAllProducts()
        {
            return new List<Product>(products);
        }

        public List<Client> GetAllClients()
        {
            return new List<Client>(clients);
        }
        public List<Order> GetAllOrders()
        {
            return new List<Order>(orders);
        }

        public List<EvidenceEntry> GetAllEntries()
        {
            return new List<EvidenceEntry>(productEvidency);
        }
    }
}
