using System.Runtime.Serialization;

namespace SharedData.models
{
    [DataContract]
    class CProduct
    {
        [DataMember(IsRequired = true)]
        public int ID { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public decimal Price { get; set; }
    }
}
