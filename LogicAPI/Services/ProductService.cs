using System;
using System.Collections.Generic;
using System.Text;
using DataAPI;
using LogicAPI.Interfaces;
using LogicAPI.Exceptions;

namespace LogicAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepository _repository;

        public ProductService()
        {

        }
        public bool ValidateModel(IModel _model)
        {
            if (_model is Product product)
            {
                if (product.ID > 0)
                {
                    if (_repository.FindProductByID(product.ID) is null)
                        throw new ProductNotFoundException();
                }
                else
                    throw new ProductInvalidIDException();


                if (product.Name.Length == 0)
                    throw new ProductInvalidNameException();

                if (product.Price == 0.0M)
                    throw new ProductInvalidPriceException();

                return true;
            }
            throw new ModelIsNotProductException();
        }
    }
}
