using System.Collections.Generic;
using Client.DataAPI;
using Client.LogicAPI.DTOs;
using Client.LogicAPI.Interfaces;
using Client.LogicAPI.Exceptions;
using CommunicationAPI.Models;
using static CommunicationAPI.Serialization;

namespace Client.LogicAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepository _repository;
        private readonly IConnectionService connectionService;

        public ProductService(IRepository repository)
        {
            _repository = repository;
            connectionService = Logic.CreateConnectionService();
        }
        public bool ValidateModel(CProduct _model)
        {
            if (_model is CProduct product)
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

            if (_repository.FindProductByID(id) is CProduct product)
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

            if (_repository.FindProductByName(name) is CProduct product)
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
            foreach(CProduct product in _repository.GetAllProducts())
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

                var productModel = new CProduct();
                productModel.ID = newID;
                productModel.Name = product.Name;
                productModel.Price = product.Price;
                ValidateModel(productModel);

                if (_repository.AddProduct(productModel))
                {
                    productModel.ID = -1;
                    connectionService.SendTask(Serialize<CProduct>(productModel));
                    productModel.ID = newID;
                    Logic.InvokeProductsChanged();
                    return true;
                }
            }
            return false;
        }

        public bool ChangeProductDTO(int productID, ProductDTO productDTO)
        {
            if (_repository.FindProductByID(productID) is CProduct product)
            {
                if (ValidateModel(productDTO))
                {
                    product.Name = productDTO.Name;
                    product.Price = productDTO.Price;
                    if (_repository.ModifyProduct(product))
                    {
                        connectionService.SendTask(Serialize<CProduct>(product));
                        Logic.InvokeProductsChanged();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                throw new ProductNotFoundException();
            }
        }
    }
}
