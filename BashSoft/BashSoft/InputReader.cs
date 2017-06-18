
namespace BashSoft
{
    using System;

    public static class InputReader
   {
       private const string endCommand = "quit";
        public static void StartReadingCommands()
        {
            OutputWriter.WriteMessage($"{SessionData.currentPath}> ");
            var input = Console.ReadLine();
            while (input != endCommand)
            {
                CommandInterpreter.InterpredComand(input);

                OutputWriter.WriteMessage($"{SessionData.currentPath}> ");
                input = Console.ReadLine().Trim();
            }
            
        }

       
    }
}
