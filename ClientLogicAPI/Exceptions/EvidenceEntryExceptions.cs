using System;
using System.Collections.Generic;
using System.Text;

namespace Client.LogicAPI.Exceptions
{
    public class ModelIsNotEvidenceEntryException : Exception
    {
        public ModelIsNotEvidenceEntryException() : base() { }
        public ModelIsNotEvidenceEntryException(string message) : base(message) { }
    }

    public class EvidenceEntryNotFoundException : Exception
    {
        public EvidenceEntryNotFoundException() : base() { }
        public EvidenceEntryNotFoundException(string message) : base(message) { }
    }

    public class EvidenceEntryInvalidIDException : Exception
    {
        public EvidenceEntryInvalidIDException() : base() { }
        public EvidenceEntryInvalidIDException(string message) : base(message) { }
    }

    public class EvidenceEntryInvalidProductAmountException : Exception
    {
        public EvidenceEntryInvalidProductAmountException() : base() { }
        public EvidenceEntryInvalidProductAmountException(string message) : base(message) { }
    }
}
