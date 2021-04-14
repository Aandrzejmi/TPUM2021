using System;
using System.Collections.Generic;
using System.Text;

namespace DataAPI
{
    public class Order : ICloneable
    {
        public int ID { get; set; }
        public Dictionary<int, int> Products { get; set; } = new Dictionary<int, int>();
        public int ClientID { get; set; }

        public object Clone()
        {
            return new Order() { ID = ID, ClientID = ClientID, Products = new Dictionary<int, int>(Products) };
        }
    }
}
