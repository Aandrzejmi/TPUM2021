using System.Collections.Generic;

namespace Client.DataAPI
{
    public class Order : IModel
    {
        public int ID { get; set; }

        public List<EvidenceEntry> Products { get; set; } = new List<EvidenceEntry>();
        public int ClientID { get; set; }

        public object Clone()
        {
            return new Order() { ID = ID, ClientID = ClientID, Products = new List<EvidenceEntry>(Products) };
        }
    }
}
