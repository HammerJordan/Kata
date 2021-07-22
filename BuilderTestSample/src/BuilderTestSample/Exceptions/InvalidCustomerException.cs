using System;

namespace BuilderTestSample.Exceptions
{
    public class InvalidCustomerException : Exception
    {
        public InvalidCustomerException()
        {
        }

        public InvalidCustomerException(string message) : base(message)
        {
        }

        public InvalidCustomerException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}