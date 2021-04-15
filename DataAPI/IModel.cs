using System;
using System.Collections.Generic;
using System.Text;

namespace DataAPI
{
    public interface IModel : ICloneable
    {
        public int ID { get; set; }
    }
}
