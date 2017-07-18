using BashSoft.Exceptions;

namespace BashSoft.IO.Commands
{
    public class CompareFilesCommand : Command
    {
        public CompareFilesCommand(string input, string[] data, Tester judge, StudentsRepository repository, IOManager inputOutputManager) : base(input, data, judge, repository, inputOutputManager)
        {
        }

        public override void Execute()
        {
            if (Data.Length == 3)
            {
                var firstFile = Data[1];
                var secondFile = Data[2];

                this.Judge.CompareContent(firstFile, secondFile);
            }
            else
            {
                throw new InvalidCommandException(this.Input);
            }
        }
    }
}
