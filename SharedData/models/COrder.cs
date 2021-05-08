using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SharedData.models
{
    [DataContract]
    class COrder
    {
        [DataMember(IsRequired = true)]
        public int ID { get; set; }

        [DataMember]
        public List<CEvidenceEntry> Products { get; set; } = new List<CEvidenceEntry>();

        [DataMember]
        public int ClientID { get; set; }
    }
}
