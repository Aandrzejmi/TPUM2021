using System;
using System.Collections.Generic;
using System.Text;

namespace Server.LogicAPI.DTOs
{
    public class EvidenceEntryDTO
    {
        public EvidenceEntryDTO()
        {
            Product = new ProductDTO();
        }

        public ProductDTO Product { get; set; }

        public int ID => Product.ID;
        public string Name { get => Product.Name; set => Product.Name = value; }
        public decimal Price { get => Product.Price; set => Product.Price = value; }

        public int ProductAmount { get; set; }

    }
}
