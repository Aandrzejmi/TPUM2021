using System;

namespace Server.LogicAPI.Exceptions
{
    public class ModelIsNotClientException : Exception
    {
        public ModelIsNotClientException() : base() { }
        public ModelIsNotClientException(string message) : base(message) { }
    }

    public class ClientNotFoundException : Exception
    {
        public ClientNotFoundException() : base() { }
        public ClientNotFoundException(string message) : base(message) { }
    }

    public class ClientInvalidIDException : Exception
    {
        public ClientInvalidIDException() : base() { }
        public ClientInvalidIDException(string message) : base(message) { }
    }

    public class ClientInvalidNameException : Exception
    {
        public ClientInvalidNameException() : base() { }
        public ClientInvalidNameException(string message) : base(message) { }
    }

    public class ClientInvalidAdressException : Exception
    {
        public ClientInvalidAdressException() : base() { }
        public ClientInvalidAdressException(string message) : base(message) { }
    }

}
