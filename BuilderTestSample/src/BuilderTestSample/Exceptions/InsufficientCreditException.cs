using System;

namespace BuilderTestSample.Exceptions
{
    public class InsufficientCreditException : Exception
    {
        public InsufficientCreditException()
        {
        }

        public InsufficientCreditException(string message) : base(message)
        {
        }

        public InsufficientCreditException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}