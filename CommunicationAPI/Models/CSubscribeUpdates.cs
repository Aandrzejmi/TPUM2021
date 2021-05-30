using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace CommunicationAPI.Models
{
    [DataContract]
    class CSubscribeUpdates
    {
        [DataMember]
        public bool Subscribe { get; set; }
        [DataMember]
        public int CycleInMs { get; set; }

        public override bool Equals(object obj)
        {
            return obj is CSubscribeUpdates updates &&
                   Subscribe == updates.Subscribe &&
                   CycleInMs == updates.CycleInMs;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Subscribe, CycleInMs);
        }
    }
}
