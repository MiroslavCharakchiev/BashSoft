using System;

namespace BashSoft.Exceptions
{
    class InvalidPathException : Exception
    {
        public const string InvalidPath = "The folder/file you are trying to access at the current address, does not exist.";

        public InvalidPathException() : base()
        {
            
        }

        public InvalidPathException(string message) : base(message)
        {
            
        }
    }
}
