using System;
using System.Collections.Generic;
using System.Linq;

namespace BashSoft
{
    class RepositorySorters
    {
        public static void OrderAndTake(Dictionary<string, List<int>> data, string sorter, int StudentsToTake)
        {
            sorter = sorter.ToLower();
            if (sorter == "ascending")
            {
                PrintStudents(data.OrderBy(x => x.Value.Sum())
                    .Take(StudentsToTake)
                    .ToDictionary(pair => pair.Key, pair => pair.Value));
            }
            else if (sorter == "desending")
            {
                PrintStudents(data.OrderByDescending(x => x.Value.Sum())
                    .Take(StudentsToTake)
                    .ToDictionary(pair => pair.Key, pair => pair.Value));
            }
            else
            {
                OutputWriter.WriteMessageOnNewLine(ExceptionMessages.InvalidComparisonQuery);
            }
        }

        private static void PrintStudents(Dictionary<string, List<int>> studentsSorted)
        {
            foreach (var kvp in studentsSorted)
            {
                OutputWriter.PrintStudent(kvp);
            }
        }
    }
}
