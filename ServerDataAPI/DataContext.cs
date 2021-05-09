using System.Collections.Generic;
using CommunicationAPI.Models;

namespace Server.DataAPI
{
    public class DataContext
    {
        public List<Client> Clients { get; set; }
        public List<EvidenceEntry> EvidenceEntries { get; set; }
        public List<Order> Orders { get; set; }
        public List<Product> Products { get; set; }
    }
}