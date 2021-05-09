using System.Collections.Generic;
using CommunicationAPI.Models;

namespace Server.LogicAPI.Interfaces
{
    public interface IProductService : IService
    {
        public bool ValidateModel(CProduct product);
        public bool AddProduct(CProduct product);
        public bool ChangeProduct(int productID, CProduct CProduct);
        public List<CProduct> GetAllProducts();
        public CProduct GetProductByID(int id);
        public CProduct GetProductByName(string name);
    }
}
