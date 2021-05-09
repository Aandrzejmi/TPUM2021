using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CommunicationAPI.Models
{
    [DataContract]
    public class COrder
    {
        [DataMember(IsRequired = true)]
        public int ID { get; set; }

        [DataMember]
        public List<CEvidenceEntry> Entries { get; set; } = new List<CEvidenceEntry>();

        [DataMember]
        public CClient Client { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is COrder order && ID == order.ID && Client.Equals(order.Client) && Entries.Count == order.Entries.Count)
            {
                for (int i = 0; i < Entries.Count; i++)
                {
                    if (!order.Entries[i].Equals(Entries[i]))
                        return false;
                }
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ID, Entries, Client);
        }
    }
}
