using System;
using System.Collections.Generic;

namespace BashSoft
{
   public class RepositoryFilter
    {
        public  void FilterAndTake(Dictionary<string, double> studentWithMarks, string filter, int studentsToTake)
        {
            if (filter == "excellent")
            {
                FilterAndTake(studentWithMarks, x => x >= 5, studentsToTake);
            }
            else if (filter == "average")
            {
                FilterAndTake(studentWithMarks, x => x < 5 && x >= 3.5, studentsToTake);
            }
            else if (filter == "poor")
            {
                FilterAndTake(studentWithMarks, x => x < 3.5, studentsToTake);
            }
            else
            {
                OutputWriter.WriteMessageOnNewLine(ExceptionMessages.InvalidStudentFilter);
            }
        }
        private  void FilterAndTake(Dictionary<string, double> studentWithMarks, Predicate<double> filter, int studentsToTake)
        {
            var counter = 0;

            foreach (var studentMark in studentWithMarks)
            {
                if (counter == studentsToTake)
                {
                    break;
                }
                
                if (filter(studentMark.Value))
                {
                    OutputWriter.PrintStudent(new KeyValuePair<string, double>(studentMark.Key, studentMark.Value));
                    counter++;
                }
            }
        }
    }
}
