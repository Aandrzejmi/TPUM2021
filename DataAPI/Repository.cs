using System;
using System.Collections.Generic;
using System.Text;

namespace DataAPI
{
    public class Repository
    {
        public List<Client> Clients { get; set; } = new List<Client>();
        public List<Order> Orders { get; set; } = new List<Order>();
        public List<Product> Products { get; set; } = new List<Product>();

        public Repository()
        {
            Products.Add(new Product() { ID = 0, Name = "Product1", Price = 10.0, AmountInMagazine = 1 });
            Products.Add(new Product() { ID = 1, Name = "Product2", Price = 20.0, AmountInMagazine = 1 });
            Products.Add(new Product() { ID = 2, Name = "Product3", Price = 30.0, AmountInMagazine = 1 });
            Products.Add(new Product() { ID = 3, Name = "Product4", Price = 40.0, AmountInMagazine = 1 });

            Clients.Add(new Client() { ID = 0, Name = "Anny Mouse", Adress = "Warsaw, Aleje Jerozolimskie 2"});
            Clients.Add(new Client() { ID = 1, Name = "Jane Doe", Adress = "Łódź, Dolna 10"});
            Clients.Add(new Client() { ID = 2, Name = "Hrabia Tyczyński", Adress = "Łódź, Wólczańska 160"});
            Clients.Add(new Client() { ID = 3, Name = "Jan Jer", Adress = "Łódź, Pomorska 45" });

            Orders.Add(new Order() { ID = 0, ClientID = 3});
            Orders[0].Products.Add(0, 1);
            Orders[0].Products.Add(1, 2);
            Orders[0].Products.Add(2, 3);
            Orders[0].Products.Add(3, 5);

            Orders.Add(new Order() { ID = 1 , ClientID = 2});
            Orders[1].Products.Add(0, 2);
            Orders[1].Products.Add(2, 6);

            Orders.Add(new Order() { ID = 2 , ClientID = 1});
            Orders[2].Products.Add(1, 2);
            Orders[2].Products.Add(3, 1);

            Orders.Add(new Order() { ID = 3 , ClientID = 1});
            Orders[3].Products.Add(0, 2);
            Orders[3].Products.Add(1, 2);
            Orders[3].Products.Add(3, 2);
        }

        // ADD
        public void AddProduct(Product product)
        {
            if (FindProductByID(product.ID) == null)
                Products.Add(new Product() { ID = product.ID, Name = product.Name, Price = product.Price, AmountInMagazine = product.AmountInMagazine});
        }

        public void AddOrder(Order order)
        {
            if (FindOrderByID(order.ID) == null)
                Orders.Add(new Order() { ID = order.ID, ClientID = order.ClientID, Products = new Dictionary<int, int>(order.Products)});
        }
        public void AddClient(Client client)
        {
            if (FindClientByID(client.ID) == null)
            {
                Clients.Add(new Client() { ID = client.ID, Name = client.Name, Adress = client.Adress});
            }
                
        }

        // MODIFY
        public void ModifyProduct(Product product)
        {
            foreach(Product pr in Products)
            {
                if (pr.ID == product.ID)
                {
                    pr.Name = product.Name;
                    pr.Price = product.Price;
                    pr.AmountInMagazine = product.AmountInMagazine;
                }
            }
        }

        public void ModifyClient(Client client)
        {
            foreach (Client cl in Clients)
            {
                if (cl.ID == client.ID)
                {
                    cl.Name = client.Name;
                    cl.Adress = client.Adress;
                }
            }
        }

        public void ModifyOrder(Order order)
        {
            foreach (Order or in Orders)
            {
                if (or.ID == order.ID)
                {
                    or.Products = new Dictionary<int, int>(order.Products);
                    or.ClientID = order.ClientID;
                }
            }
        }
        // FIND
        public Product FindProductByName(string name)
        {
            return Products?.Find(x => x.Name == name);
        }

        public Product FindProductByID(int id)
        {
            return Products?.Find(x => x.ID == id);
        }

        public Client FindClientByID(int id)
        {
            return Clients?.Find(x => x.ID == id);
        }

        public Client FindClientByName(string name)
        {
            return Clients?.Find(x => x.Name == name);
        }

        public Order FindOrderByID(int id)
        {
            return Orders?.Find(x => x.ID == id);
        }

        public List<Order> FindOrdersByClientID(int clientID)
        {
            return Orders?.FindAll(x => x.ClientID == clientID);
        }
    }
}
