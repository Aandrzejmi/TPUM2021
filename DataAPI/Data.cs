using System;
using System.Collections.Generic;
using System.Text;

namespace DataAPI
{
    public static class Data
    {
        public static Repository CreateRepository()
        {
            Repository repository = new Repository();
            Product product1 = new Product() { ID = 0, Name = "Product1", Price = 10 };
            Product product2 = new Product() { ID = 1, Name = "Product2", Price = 20 };
            Product product3 = new Product() { ID = 2, Name = "Product3", Price = 30 };
            Product product4 = new Product() { ID = 3, Name = "Product4", Price = 40 };
            repository.AddProduct(product1);
            repository.AddProduct(product2);
            repository.AddProduct(product3);
            repository.AddProduct(product4);

            repository.AddClient(new Client() { ID = 0, Name = "Anny Mouse", Adress = "Warsaw, Aleje Jerozolimskie 2" });
            repository.AddClient(new Client() { ID = 1, Name = "Jane Doe", Adress = "Łódź, Dolna 10" });
            repository.AddClient(new Client() { ID = 2, Name = "Hrabia Tyczyński", Adress = "Łódź, Wólczańska 160" });
            repository.AddClient(new Client() { ID = 3, Name = "Jan Jer", Adress = "Łódź, Pomorska 45" });

            var order0 = new Order() { ID = 0, ClientID = 3 };
            order0.Products.Add(product1.ID);
            order0.Products.Add(product2.ID);
            order0.Products.Add(product3.ID);
            order0.Products.Add(product4.ID);
            repository.AddOrder(order0);

            var order1 = new Order() { ID = 1, ClientID = 2 };
            order1.Products.Add(product1.ID);
            order1.Products.Add(product3.ID);
            repository.AddOrder(order1);

            var order2 = new Order() { ID = 2, ClientID = 1 };
            order2.Products.Add(product2.ID);
            order2.Products.Add(product4.ID);
            repository.AddOrder(order2);

            var order3 = new Order() { ID = 3, ClientID = 1 };
            order3.Products.Add(product1.ID);
            order3.Products.Add(product2.ID);
            order3.Products.Add(product4.ID);
            repository.AddOrder(order3);

            return repository;
        }
    }
}
