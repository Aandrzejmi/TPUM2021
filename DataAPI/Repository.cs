using System;
using System.Collections.Generic;
using System.Text;

namespace DataAPI
{
    public class Repository : IRepository
    {
        private List<Client> Clients { get; set; } = new List<Client>();
        private List<Order> Orders { get; set; } = new List<Order>();
        private List<Product> Products { get; set; } = new List<Product>();
        private List<EvidenceEntry> ProductEvidency { get; set; } = new List<EvidenceEntry>();

        public Repository()
        {
            
        }

        // ADD
        public bool AddProduct(Product product)
        {
            if (FindProductByID(product.ID) == null)
            {
                Products.Add(product.Clone() as Product);
                return AddEvidenceEntry((new EvidenceEntry() { ProductID = product.ID, ProductAmount = 1 }));
            }
            else
                return false;
        }
        
        public bool AddEvidenceEntry(EvidenceEntry evidenceEntry)
        {
            if (FindEvidenceEntryByID(evidenceEntry.ID) == null)
            {
                ProductEvidency.Add(evidenceEntry.Clone() as EvidenceEntry);
                return true;
            }
            return false;
        }

        public bool AddOrder(Order order)
        {
            if (FindOrderByID(order.ID) == null)
            {
                Orders.Add(order.Clone() as Order);
                return true;
            }
            else
                return false;
                
        }
        public bool AddClient(Client client)
        {
            if (FindClientByID(client.ID) == null)
            {
                Clients.Add(client.Clone() as Client);
                return true;
            }
            return false;
        }

        // MODIFY
        public bool ModifyProduct(Product product)
        {
            foreach(Product pr in Products)
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
            foreach (Client cl in Clients)
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
            foreach (Order or in Orders)
            {
                if (or.ID == order.ID)
                {
                    or.Products = new List<int>(order.Products);
                    or.ClientID = order.ClientID;
                    return true;
                }
            }
            return false;
        }

        public bool ChangeProductAmount(int productID, int newAmount)
        {
            foreach (EvidenceEntry ev in ProductEvidency)
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
            return Products?.Find(x => x.Name == name)?.Clone() as Product;
        }

        public Product FindProductByID(int id)
        {
            return Products?.Find(x => x.ID == id)?.Clone() as Product;
        }

        public EvidenceEntry FindEvidenceEntryByID(int id)
        {
            return ProductEvidency?.Find(x => x.ID == id)?.Clone() as EvidenceEntry;
        }

        public Client FindClientByID(int id)
        {
            return Clients?.Find(x => x.ID == id)?.Clone() as Client;
        }

        public Client FindClientByName(string name)
        {
            return Clients?.Find(x => x.Name == name)?.Clone() as Client;
        }

        public Order FindOrderByID(int id)
        {
            return Orders?.Find(x => x.ID == id)?.Clone() as Order;
        }

        public List<Order> FindOrdersByClientID(int clientID)
        {
            List<Order> copy = new List<Order>();
            foreach(Order order in Orders.FindAll(x => x.ClientID == clientID))
            {
                copy.Add(order.Clone() as Order);
            }
            return copy;
        }

        // LIST GETTERS
        public int CountProducts()
        {
            return Products.Count;
        }

        public int CountOrders()
        {
            return Orders.Count;
        }

        public int CountClients()
        {
            return Clients.Count;
        }

        public int CountProductEntries()
        {
            return ProductEvidency.Count;
        }
    }
}
