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
        public List<CEvidenceEntry> Products { get; set; } = new List<CEvidenceEntry>();

        [DataMember]
        public int ClientID { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is COrder order && ID == order.ID && ClientID == order.ClientID && Products.Count == order.Products.Count)
            {
                for (int i = 0; i < Products.Count; i++)
                {
                    if (!order.Products[i].Equals(Products[i]))
                        return false;
                }
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ID, Products, ClientID);
        }
    }
}
