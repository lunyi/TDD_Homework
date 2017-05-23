using System;
using System.Runtime.Serialization;

namespace Day1Homework.Exceptions
{
    [Serializable]
    internal class InvalidColumnException : Exception
    {
        public InvalidColumnException()
        {
        }

        public InvalidColumnException(string message) : base(message)
        {
        }

        public InvalidColumnException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidColumnException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}