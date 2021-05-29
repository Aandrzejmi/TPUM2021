using System;
using System.Runtime.Serialization;

namespace CommunicationAPI.Models
{
    [DataContract]
    public class CSendRequest
    {
        [DataMember]
        public string Type { get; set; }

        // null means send all
        [DataMember]
        public int? RequestedID { get; set; }

        public override bool Equals(object obj)
        {
            return obj is CSendRequest request &&
                   Type == request.Type &&
                   RequestedID == request.RequestedID;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Type, RequestedID);
        }
    }
}
