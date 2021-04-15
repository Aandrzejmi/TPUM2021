using System;
using System.Collections.Generic;
using System.Text;

namespace DataAPI
{
    public class EvidenceEntry : IModel
    {
        public int ID { get { return Product.ID; } set { ID = Product.ID; } }

        public Product Product;
        public int productAmount;
        public object Clone()
        {
            return new EvidenceEntry() { Product = Product, productAmount = productAmount };
        }
    }
}
