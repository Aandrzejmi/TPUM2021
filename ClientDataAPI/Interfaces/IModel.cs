using System;

namespace Client.DataAPI
{
    public interface IModel : ICloneable
    {
        public int ID { get; set; }
    }
}
