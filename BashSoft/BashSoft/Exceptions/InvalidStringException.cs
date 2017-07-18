using System;

namespace BashSoft.Exceptions
{
   public class InvalidStringException : Exception
    {
        public const string ArgumentNullException = "The value of the variable CANNOT be null or empty!";

        public InvalidStringException() : base ()
        {
            
        }

        public InvalidStringException(string message) : base()
        {
            
        }

    }
}
