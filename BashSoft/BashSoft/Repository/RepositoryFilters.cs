using System;
using System.Collections.Generic;
using System.Linq;

namespace BashSoft
{
    class RepositoryFilters
    {
        public static void FilterAndTake(Dictionary<string, List<int>> data, string filter, int studentsToTake)
        {
            if (filter == "excellent")
            {
                FilterAndTake(data, x => x >= 5, studentsToTake);
            }
            else if (filter == "average")
            {
                FilterAndTake(data, x => x < 5 && x >= 3.5, studentsToTake);
            }
            else if (filter == "poor")
            {
                FilterAndTake(data, x => x < 3.5, studentsToTake);
            }
            else
            {
                OutputWriter.WriteMessageOnNewLine(ExceptionMessages.InvalidStudentFilter);
            }
        }
        private static void FilterAndTake(Dictionary<string, List<int>> data, Predicate<double> filter, int studentsToTake)
        {
            var counter = 0;

            foreach (var userName_Points in data)
            {
                if (counter == studentsToTake)
                {
                    break;
                }
                var averageScore = userName_Points.Value.Average();
                var persantage = averageScore / 100;
                var mark = persantage * 4 + 2;

                if (filter(mark))
                {
                    OutputWriter.PrintStudent(userName_Points);
                    counter++;
                }
            }
        }
    }
}
