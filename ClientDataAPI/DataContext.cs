using System.Collections.Generic;
using CommunicationAPI.Models;

namespace Client.DataAPI
{
    public class DataContext
    {
        public List<CClient> CClients { get; set; }
        public List<CEvidenceEntry> CEvidenceEntries { get; set; }
        public List<COrder> COrders { get; set; }
        public List<CProduct> CProducts { get; set; }
    }
}
