using BashSoft.Exceptions;

namespace BashSoft.IO.Commands
{
    public class PrintFilteredStudentsCommand : Command
    {
        public PrintFilteredStudentsCommand(string input, string[] data, Tester judge, StudentsRepository repository, IOManager inputOutputManager) : base(input, data, judge, repository, inputOutputManager)
        {
        }

        public override void Execute()
        {
            if (Data.Length == 5)
            {
                var courseName = Data[1];
                var filter = Data[2].ToLower();
                var takeComand = Data[3].ToLower();
                var takeQuantity = Data[4].ToLower();

                tryParseParametersForFilterAndTake(takeComand, takeQuantity, filter, courseName);

            }
            else
            {
                throw new InvalidCommandException(this.Input);
            }
        }
        private void tryParseParametersForFilterAndTake(string takeComand, string takeQuantity, string filter, string courseName)
        {
            if (takeComand == "take")
            {
                if (takeQuantity == "all")
                {
                    this.Repositiry.FilterAndTake(courseName, filter);
                }
                else
                {
                    int studentsToTake;
                    var hasParsed = int.TryParse(takeQuantity, out studentsToTake);
                    if (hasParsed)
                    {
                        this.Repositiry.FilterAndTake(courseName, filter, studentsToTake);
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
