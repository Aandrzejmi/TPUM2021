﻿using System;
using System.Collections.Generic;
using System.Text;
using LogicAPI.DTOs;

namespace LogicAPI.Interfaces
{
    public interface IProductService : IService
    {
        public List<ProductDTO> GetAllProductDTOs();
        public ProductDTO GetProductDTOByID(int id);
        public ProductDTO GetProductDTOByName(string name);
    }
}