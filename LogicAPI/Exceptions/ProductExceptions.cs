using System;
using System.Collections.Generic;
using System.Text;

namespace Server.LogicAPI.Exceptions
{
    public class ModelIsNotProductException : Exception
    {
        public ModelIsNotProductException() : base() { }
        public ModelIsNotProductException(string message) : base(message) { }
    }

    public class ProductNotFoundException : Exception
    {
        public ProductNotFoundException() : base() { }
        public ProductNotFoundException(string message) : base(message) { }
    }
    public class ProductInvalidIDException : Exception
    {
        public ProductInvalidIDException() : base() { }
        public ProductInvalidIDException(string message) : base(message) { }
    }

    public class ProductInvalidNameException : Exception
    {
        public ProductInvalidNameException() : base() { }
        public ProductInvalidNameException(string message) : base(message) { }
    }

    public class ProductInvalidPriceException : Exception
    {
        public ProductInvalidPriceException() : base() { }
        public ProductInvalidPriceException(string message) : base(message) { }
    }

    

}
