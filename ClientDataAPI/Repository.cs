﻿using System;
using System.Collections.Generic;
using CommunicationAPI.Models;

namespace Client.DataAPI
{
    public class Repository : IRepository
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
        public bool AddProduct(CProduct product)
        {
            if (FindProductByID(product.ID) == null)
            {
                dataContext.CProducts.Add(product);
                return AddEvidenceEntry((new CEvidenceEntry() { ProductID = product.ID, ProductAmount = 1 }));
            }
            else
                return false;
        }
        
        public bool AddEvidenceEntry(CEvidenceEntry evidenceEntry)
        {
            if (FindEvidenceEntryByID(evidenceEntry.ProductID) == null)
            {
                if (FindProductByID(evidenceEntry.ProductID) == null)
                    return false;

                dataContext.CEvidenceEntries.Add(evidenceEntry);
                return true;
            }
            return false;
        }

        public bool AddOrder(COrder order)
        {
            if (FindOrderByID(order.ID) == null)
            {
                dataContext.COrders.Add(order);
                return true;
            }
            else
                return false;
                
        }
        public bool AddClient(CClient client)
        {
            if (FindClientByID(client.ID) == null)
            {
                dataContext.CClients.Add(client);
                return true;
            }
            return false;
        }

        // MODIFY
        public bool ModifyProduct(CProduct product)
        {
            foreach(CProduct pr in dataContext.CProducts)
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

        public bool ModifyClient(CClient client)
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

        public bool ModifyOrder(COrder order)
        {
            foreach (COrder or in dataContext.COrders)
            {
                if (or.ID == order.ID)
                {
                    or.Products = new List<CEvidenceEntry>(order.Products);
                    or.ClientID = order.ClientID;
                    return true;
                }
            }
            return false;
        }

        public bool ChangeProductAmount(int productID, int newAmount)
        {
            foreach (CEvidenceEntry ev in dataContext.CEvidenceEntries)
            {
                if (ev.ProductID == productID)
                {
                    ev.ProductAmount = newAmount;
                    return true;
                }
            }
            return false;
        }
        // FIND
        public CProduct FindProductByName(string name)
        {
            return dataContext.CProducts?.Find(x => x.Name == name);
        }

        public CProduct FindProductByID(int id)
        {
            return dataContext.CProducts?.Find(x => x.ID == id);
        }

        public CEvidenceEntry FindEvidenceEntryByID(int id)
        {
            return dataContext.CEvidenceEntries?.Find(x => x.ProductID == id);
        }

        public CClient FindClientByID(int id)
        {
            return dataContext.CClients?.Find(x => x.ID == id);
        }

        public CClient FindClientByName(string name)
        {
            return dataContext.CClients?.Find(x => x.Name == name);
        }

        public COrder FindOrderByID(int id)
        {
            return dataContext.COrders?.Find(x => x.ID == id);
        }

        public List<COrder> FindOrdersByClientID(int clientID)
        {
            List<COrder> copy = new List<COrder>();
            foreach(COrder order in dataContext.COrders.FindAll(x => x.ClientID == clientID))
            {
                copy.Add(order);
            }
            return copy;
        }

        // LIST GETTERS
        public int CountProducts { get { return dataContext.CProducts.Count; } }

        public int CountOrders { get { return dataContext.COrders.Count; } }

        public int CountClients { get { return dataContext.CClients.Count; } }

        public int CountProductEntries { get { return dataContext.CProducts.Count; } }

        public List<CProduct> GetAllProducts()
        {
            return new List<CProduct>(dataContext.CProducts);
        }

        public List<CClient> GetAllClients()
        {
            return new List<CClient>(dataContext.CClients);
        }
        public List<COrder> GetAllOrders()
        {
            return new List<COrder>(dataContext.COrders);
        }

        public List<CEvidenceEntry> GetAllEntries()
        {
            return new List<CEvidenceEntry>(dataContext.CEvidenceEntries);
        }
    }
}
