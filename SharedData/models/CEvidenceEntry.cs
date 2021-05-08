using System.Runtime.Serialization;

namespace SharedData.models
{
    [DataContract]
    class CEvidenceEntry
    {
        [DataMember]
        public int ProductID { get; set; }
        
        [DataMember]
        public int ProductAmount { get; set; }
    }
}
