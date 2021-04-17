using System;
using System.Collections.Generic;
using System.Text;

namespace LogicAPI.DTOs
{
    public class EvidenceEntryDTO
    {
        public int ID { get { return Product.ID; } set { ID = Product.ID; } }
        public ProductDTO Product { get; set; }
        public int ProductAmount { get; set; }

    }
}
