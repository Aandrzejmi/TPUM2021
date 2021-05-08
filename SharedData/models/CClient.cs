using System.Runtime.Serialization;

namespace SharedData.models
{
    [DataContract]
    class CClient
    {
        [DataMember(IsRequired = true)]
        public int ID { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Adress { get; set; }
    }
}
