using System;
using System.Collections.Generic;
using System.Text;
using Server.LogicAPI.DTOs;

namespace Server.LogicAPI.Interfaces
{
    public interface IProductService : IService
    {
        public bool ValidateModel(ProductDTO product);
        public bool AddProductDTO(ProductDTO product);
        public bool ChangeProductDTO(int productID, ProductDTO productDTO);
        public List<ProductDTO> GetAllProductDTOs();
        public ProductDTO GetProductDTOByID(int id);
        public ProductDTO GetProductDTOByName(string name);
    }
}
