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

                if (product.Name.Length == 0)
                    throw new ProductInvalidNameException();

                if (product.Price <= 0.0M)
                    throw new ProductInvalidPriceException();

                return true;
            }
            throw new ModelIsNotProductException();
        }

        public bool ValidateModel(ProductDTO product)
        {
            if (product is ProductDTO)
            {
                if (product.ID < 0)
                    throw new ProductInvalidIDException();

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

        public bool AddProductDTO(ProductDTO product)
        {
            if (ValidateModel(product))
            {
                List<ProductDTO> productDTOs = GetAllProductDTOs();
                int newID = 0;
                foreach (ProductDTO productDTOListObject in productDTOs)
                {
                    if (newID == productDTOListObject.ID)
                        newID++;
                    else
                        break;
                }

                var productModel = new Product();
                productModel.ID = newID;
                productModel.Name = product.Name;
                productModel.Price = product.Price;
                ValidateModel(productModel);
                if (_repository.AddProduct(productModel))
                    return true;
            }
            return false;
        }

        public bool ChangeProductDTO(int productID, ProductDTO productDTO)
        {
            if (_repository.FindProductByID(productID) is Product product)
            {
                if (ValidateModel(productDTO))
                {
                    product.Name = product.Name;
                    product.Price = product.Price;
                    if (_repository.ModifyProduct(product))
                        return true;
                    else
                        return false;
                }
                else
                    return false;
            }
            else
                throw new ProductNotFoundException();
        }
    }
}
