using BashSoft.Exceptions;

namespace BashSoft.IO.Commands
{
   public class PrintOrderedStudentsCommand : Command
    {
        public PrintOrderedStudentsCommand(string input, string[] data, Tester judge, StudentsRepository repository, IOManager inputOutputManager) : base(input, data, judge, repository, inputOutputManager)
        {
        }

        public override void Execute()
        {
            if (Data.Length == 5)
            {
                var courseName = Data[1];
                var comparer = Data[2].ToLower();
                var takeComand = Data[3].ToLower();
                var takeQuantity = Data[4].ToLower();

                tryParseParametersForOrderAndTake(takeComand, takeQuantity, comparer, courseName);

            }
            else
            {
                throw new InvalidCommandException(this.Input);
            }
        }
        private void tryParseParametersForOrderAndTake(string takeComand, string takeQuantity, string comparer, string courseName)
        {
            if (takeComand == "take")
            {
                if (takeQuantity == "all")
                {
                    this.Repositiry.OrderAndTake(courseName, comparer);
                }
                else
                {
                    int studentsToTake;
                    var hasParsed = int.TryParse(takeQuantity, out studentsToTake);
                    if (hasParsed)
                    {
                        this.Repositiry.OrderAndTake(courseName, comparer, studentsToTake);
                    }
                    else
                    {
                        OutputWriter.WriteMessageOnNewLine(ExceptionMessages.InvalidTakeQuantityParameter);
                    }
                }
            }
            else
            {
                OutputWriter.WriteMessageOnNewLine(ExceptionMessages.InvalidTakeQuantityParameter);
            }
        }
    }
}
