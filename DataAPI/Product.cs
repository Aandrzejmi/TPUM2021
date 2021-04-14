using System;

namespace DataAPI
{
    public class Product : ICloneable
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Decimal Price { get; set; }
        public int AmountInMagazine { get; set; }

        public object Clone()
        {
            return new Product() { ID = ID, Name = Name, Price = Price, AmountInMagazine = AmountInMagazine };
        }
    }
}
