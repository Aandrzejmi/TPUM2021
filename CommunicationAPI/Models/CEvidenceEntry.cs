using System;
using System.Runtime.Serialization;

namespace CommunicationAPI.Models
{
    [DataContract]
    public class CEvidenceEntry
    {
        [DataMember]
        public int ProductID { get; set; }
        
        [DataMember]
        public int ProductAmount { get; set; }

        public override bool Equals(object obj)
        {
            return obj is CEvidenceEntry entry &&
                   ProductID == entry.ProductID &&
                   ProductAmount == entry.ProductAmount;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ProductID, ProductAmount);
        }
    }
}
