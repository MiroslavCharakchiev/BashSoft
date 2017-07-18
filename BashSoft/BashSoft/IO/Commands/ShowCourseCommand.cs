using BashSoft.Exceptions;

namespace BashSoft.IO.Commands
{
   public class ShowCourseCommand : Command
    {
        public ShowCourseCommand(string input, string[] data, Tester judge, StudentsRepository repository, IOManager inputOutputManager) : base(input, data, judge, repository, inputOutputManager)
        {
        }

        public override void Execute()
        {
            if (Data.Length == 2)
            {
                var courseName = Data[1];
                this.Repositiry.GetAllStudentsFromCourse(courseName);
            }
            else if (Data.Length == 3)
            {
                var courseName = Data[1];
                var studentName = Data[2];
                this.Repositiry.GetStudentScoteFromCourse(courseName, studentName);
            }
            else
            {
                throw new InvalidCommandException(this.Input);
            }
        }
    }
}
