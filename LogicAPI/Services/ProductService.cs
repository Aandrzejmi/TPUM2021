using System;
using System.Collections.Generic;
using System.Text;
using DataAPI;
using LogicAPI.DTOs;
using LogicAPI.Interfaces;
using LogicAPI.Exceptions;

namespace LogicAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepository _repository;

        public ProductService(IRepository repository)
        {
            _repository = repository;
        }
        public bool ValidateModel(IModel _model)
        {
            if (_model is Product product)
            {
                if (product.ID < 0)
                    throw new ProductInvalidIDException();

                if (_repository.FindProductByID(product.ID) is null)
                    throw new ProductNotFoundException();

                if (product.Name.Length == 0)
                    throw new ProductInvalidNameException();

                if (product.Price <= 0.0M)
                    throw new ProductInvalidPriceException();

                return true;
            }
            throw new ModelIsNotProductException();
        }

        public ProductDTO GetProductDTOByID(int id)
        {
            var productDTO = new ProductDTO();

            if (_repository.FindProductByID(id) is Product product)
            {
                productDTO.ID = product.ID;
                productDTO.Name = product.Name;
                productDTO.Price = product.Price;

                return productDTO;
            }
            throw new ProductNotFoundException();
        }

        public ProductDTO GetProductDTOByName(string name)
        {
            var productDTO = new ProductDTO();

            if (_repository.FindProductByName(name) is Product product)
            {
                productDTO.ID = product.ID;
                productDTO.Name = product.Name;
                productDTO.Price = product.Price;

                return productDTO;
            }
            throw new ProductNotFoundException();
        }

        public List<ProductDTO> GetAllProductDTOs()
        {
            List<ProductDTO> productDTOs = new List<ProductDTO>();
            foreach(Product product in _repository.GetAllProducts())
            {
                productDTOs.Add(GetProductDTOByID(product.ID));
            }
            return productDTOs;
        }
    }
}
