using System;
using System.Runtime.Serialization;

namespace CommunicationAPI.Models
{
    [DataContract]
    public class CEvidenceEntry
    {
        [DataMember]
        public CProduct Product { get; set; }
        
        [DataMember]
        public int Amount { get; set; }

        public override bool Equals(object obj)
        {
            return obj is CEvidenceEntry entry &&
                   Product.Equals(entry.Product) &&
                   Amount == entry.Amount;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Product, Amount);
        }
    }
}
