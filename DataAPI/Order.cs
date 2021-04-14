using System;
using System.Collections.Generic;
using System.Text;

namespace DataAPI
{
    public class Order
    {
        public int ID { get; set; }
        public Dictionary<int, int> Products { get; set; } = new Dictionary<int, int>();
        public int ClientID { get; set; }
    }
}
