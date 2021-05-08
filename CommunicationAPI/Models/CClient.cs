using System;
using System.Runtime.Serialization;

namespace CommunicationAPI.Models
{
    [DataContract]
    public class CClient
    {
        [DataMember(IsRequired = true)]
        public int ID { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Adress { get; set; }

        public override bool Equals(object obj)
        {
            return obj is CClient client &&
                   ID == client.ID &&
                   Name == client.Name &&
                   Adress == client.Adress;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ID, Name, Adress);
        }
    }
}
