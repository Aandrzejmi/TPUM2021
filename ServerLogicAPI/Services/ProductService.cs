using System.Collections.Generic;
using Server.DataAPI;
using CommunicationAPI.Models;
using Server.LogicAPI.Interfaces;
using Server.LogicAPI.Exceptions;

namespace Server.LogicAPI.Services
{
    internal class ProductService : IProductService
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

        public bool ValidateModel(CProduct product)
        {
            if (product is CProduct)
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

        public CProduct GetProductByID(int id)
        {
            var cProduct = new CProduct();

            if (_repository.FindProductByID(id) is Product product)
            {
                cProduct.ID = product.ID;
                cProduct.Name = product.Name;
                cProduct.Price = product.Price;

                return cProduct;
            }
            throw new ProductNotFoundException();
        }

        public CProduct GetProductByName(string name)
        {
            var cProduct = new CProduct();

            if (_repository.FindProductByName(name) is Product product)
            {
                cProduct.ID = product.ID;
                cProduct.Name = product.Name;
                cProduct.Price = product.Price;

                return cProduct;
            }
            throw new ProductNotFoundException();
        }

        public List<CProduct> GetAllProducts()
        {
            List<CProduct> cProducts = new List<CProduct>();
            foreach(Product product in _repository.GetAllProducts())
            {
                cProducts.Add(GetProductByID(product.ID));
            }
            return cProducts;
        }

        public bool AddProduct(CProduct product)
        {
            if (ValidateModel(product))
            {
                List<CProduct> cProducts = GetAllProducts();
                int newID = 0;
                foreach (CProduct cProductListObject in cProducts)
                {
                    if (newID == cProductListObject.ID)
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
                {
                    Logic.InvokeProductsChanged();
                    return true;
                }
            }
            return false;
        }

        public bool ChangeProduct(int productID, CProduct cProduct)
        {
            if (_repository.FindProductByID(productID) is Product product)
            {
                if (ValidateModel(cProduct))
                {
                    product.Name = product.Name;
                    product.Price = product.Price;
                    if (_repository.ModifyProduct(product))
                    {
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
