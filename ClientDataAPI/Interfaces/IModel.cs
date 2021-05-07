using System;
using System.Collections.Generic;
using System.Text;

namespace Client.DataAPI
{
    public interface IModel : ICloneable
    {
        public int ID { get; set; }
    }
}
