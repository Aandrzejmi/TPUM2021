using System;

namespace Server.DataAPI
{
    public interface IModel : ICloneable
    {
        public int ID { get; set; }
    }
}
