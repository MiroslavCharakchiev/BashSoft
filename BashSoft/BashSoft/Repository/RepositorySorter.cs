using System.Collections.Generic;
using System.Linq;

namespace BashSoft
{
   public class RepositorySorter
    {
        public  void OrderAndTake(Dictionary<string, double> studentMark, string sorter, int StudentsToTake)
        {
            sorter = sorter.ToLower();
            if (sorter == "ascending")
            {
                this.PrintStudents(studentMark.OrderBy(x => x.Value)
                    .Take(StudentsToTake)
                    .ToDictionary(pair => pair.Key, pair => pair.Value));
            }
            else if (sorter == "desending")
            {
                PrintStudents(studentMark.OrderByDescending(x => x.Value)
                    .Take(StudentsToTake)
                    .ToDictionary(pair => pair.Key, pair => pair.Value));
            }
            else
            {
                OutputWriter.WriteMessageOnNewLine(ExceptionMessages.InvalidComparisonQuery);
            }
        }

        private  void PrintStudents(Dictionary<string, double> studentsSorted)
        {
            foreach (var kvp in studentsSorted)
            {
                OutputWriter.PrintStudent(kvp);
            }
        }
    }
}
