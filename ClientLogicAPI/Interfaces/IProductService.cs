using System.Collections.Generic;
using Client.LogicAPI.DTOs;
using CommunicationAPI.Models;

namespace Client.LogicAPI.Interfaces
{
    public interface IProductService
    {
        public bool ValidateModel(ProductDTO product);
        public bool ValidateModel(CProduct _model);
        public bool AddProductDTO(ProductDTO product);
        public bool ChangeProductDTO(int productID, ProductDTO productDTO);
        public List<ProductDTO> GetAllProductDTOs();
        public ProductDTO GetProductDTOByID(int id);
        public ProductDTO GetProductDTOByName(string name);
    }
}
