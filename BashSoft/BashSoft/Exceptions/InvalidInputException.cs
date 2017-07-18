using System;

namespace BashSoft.Exceptions
{
    public class InvalidCommandException : Exception
    {
        public const string UnableToGoHighterInpartitionHierarchy = "You reach the higher point of the path.";

        public InvalidCommandException() : base ()
        {
            
        }

        public InvalidCommandException(string message) : base(message)
        {
            
        }

        public void  DisplayInvalidCommandMessage (string input)
        {
            OutputWriter.DisplayExeption($"The command '{input}' is invalid");

        }
    }
}
