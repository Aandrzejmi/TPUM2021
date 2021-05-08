using System;
using System.Runtime.Serialization;

namespace CommunicationAPI.Models
{
    [DataContract]
    public class CProduct
    {
        [DataMember(IsRequired = true)]
        public int ID { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public decimal Price { get; set; }

        public override bool Equals(object obj)
        {
            return obj is CProduct product &&
                   ID == product.ID &&
                   Name == product.Name &&
                   Price == product.Price;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ID, Name, Price);
        }
    }
}
