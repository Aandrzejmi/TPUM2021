using System;
using System.Collections.Generic;
using System.Text;

namespace Client.LogicAPI.Exceptions
{
    public class ModelIsNotOrderException : Exception
    {
        public ModelIsNotOrderException() : base() { }
        public ModelIsNotOrderException(string message) : base(message) { }
    }

    public class OrderNotFoundException : Exception
    {
        public OrderNotFoundException() : base() { }
        public OrderNotFoundException(string message) : base(message) { }
    }

    public class OrderInvalidIDException : Exception
    {
        public OrderInvalidIDException() : base() { }
        public OrderInvalidIDException(string message) : base(message) { }
    }

    public class OrderInvalidClientIDException : Exception
    {
        public OrderInvalidClientIDException() : base() { }
        public OrderInvalidClientIDException(string message) : base(message) { }
    }

    public class OrderClientNotFoundException : Exception
    {
        public OrderClientNotFoundException() : base() { }
        public OrderClientNotFoundException(string message) : base(message) { }
    }
}
