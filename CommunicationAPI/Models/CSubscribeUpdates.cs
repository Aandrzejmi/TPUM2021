using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace CommunicationAPI.Models
{
    [DataContract]
    public class CSubscribeUpdates
    {
        [DataMember]
        public bool Subscribe { get; set; }
        [DataMember]
        public int CycleInSeconds { get; set; }

        public override bool Equals(object obj)
        {
            return obj is CSubscribeUpdates updates &&
                   Subscribe == updates.Subscribe &&
                   CycleInSeconds == updates.CycleInSeconds;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Subscribe, CycleInSeconds);
        }
    }
}
