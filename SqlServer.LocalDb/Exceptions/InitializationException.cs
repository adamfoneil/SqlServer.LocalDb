using System;

namespace SqlServer.LocalDb.Exceptions
{
    public class InitializationException : Exception
    {
        public InitializationException(string message) : base(message)
        {
        }
    }
}
