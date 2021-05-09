using System;
using System.Collections.Generic;
using System.Text;
using CommunicationAPI.Models;

namespace Server.DataAPI
{
    public sealed class DbContext : DataContext
    {
        private static DbContext instance = null;
        private static readonly object lockobj = new object();

        DbContext()
        {
            Clients = new List<Client>();
            Orders = new List<Order>();
            Products = new List<Product>();
            EvidenceEntries = new List<EvidenceEntry>();
        }

        public static DbContext Instance
        {
            get
            {
                lock (lockobj)
                {
                    if (instance == null)
                    {
                        instance = new DbContext();
                    }
                }
                return instance;
            }
        }

        public static void ClearContext()
        {
            Instance.Clients.Clear();
            Instance.Orders.Clear();
            Instance.Products.Clear();
            Instance.EvidenceEntries.Clear();
        }

    }
}