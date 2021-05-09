using System;
using System.Collections.Generic;
using System.Text;
using CommunicationAPI.Models;

namespace Client.DataAPI
{
    public sealed class DbContext : DataContext
    {
        private static DbContext instance = null;
        private static readonly object lockobj = new object();

        DbContext()
        {
            CClients = new List<CClient>();
            COrders = new List<COrder>();
            CProducts = new List<CProduct>();
            CEvidenceEntries = new List<CEvidenceEntry>();
        }

        public static DbContext Instance
        {
            get
            {
                lock(lockobj)
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
            Instance.CClients.Clear();
            Instance.COrders.Clear();
            Instance.CProducts.Clear();
            Instance.CEvidenceEntries.Clear();
        }

    }
}
