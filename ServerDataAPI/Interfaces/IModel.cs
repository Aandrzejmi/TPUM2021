using System;
using System.Collections.Generic;
using System.Text;

namespace Server.DataAPI
{
    public interface IModel : ICloneable
    {
        public int ID { get; set; }
    }
}
