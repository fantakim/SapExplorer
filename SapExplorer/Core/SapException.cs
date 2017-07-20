using System;

namespace SapExplorer.Core
{
    public class SapException : ApplicationException
    {
        public SapException(string message) : base(message)
        {
        }

        public SapException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
