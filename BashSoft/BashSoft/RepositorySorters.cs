using System;
using System.Collections.Generic;

namespace BashSoft
{
    class RepositorySorters
    {
        public static void OrderAndTake(Dictionary<string, List<int>> data, string sorter, int StudentsToTake)
        {
            sorter = sorter.ToLower();
            if (sorter == "ascending")
            {
                OrderAndTake(data, StudentsToTake, CompareInOrder);
            }
            else if (sorter == "desending")
            {
                OrderAndTake(data, StudentsToTake, CompareDecendingOrder);
            }
            else
            {
                OutputWriter.WriteMessageOnNewLine(ExceptionMessages.InvalidComparisonQuery);
            }
        }

        private static void OrderAndTake(Dictionary<string, List<int>> data, int StudentsToTake,
            Func<KeyValuePair<string, List<int>>,KeyValuePair<string, List<int>>,int>sorterFunc)
        {

            foreach (var sortedStudent in GetSortedStudents(data, StudentsToTake, sorterFunc))
            {
                OutputWriter.PrintStudent(sortedStudent);
            }
        }

        private static Dictionary<string, List<int>> GetSortedStudents(Dictionary<string, List<int>> data,
            int TakeCount,
            Func<KeyValuePair<string, List<int>>, KeyValuePair<string, List<int>>, int> comparison)
        {
            var ValuesTaken = 0;
            var studentsSorted = new Dictionary<string, List<int>>();
            var nextInOrder = new KeyValuePair<string, List<int>>();
            var isSorted = false;

            while (ValuesTaken < TakeCount)
            {
                isSorted = true;
                foreach (var student in data)
                {
                    if (!string.IsNullOrEmpty(nextInOrder.Key))
                    {
                        var comparisonResult = comparison(student, nextInOrder);
                        if (comparisonResult >= 0 && !studentsSorted.ContainsKey(student.Key))
                        {
                            nextInOrder = student;
                            isSorted = false;
                        }
                    }
                    else
                    {
                        if (!studentsSorted.ContainsKey(student.Key))
                        {
                            nextInOrder = student;
                            isSorted = false;
                        }
                    }
                }
                if (!isSorted)
                {
                    studentsSorted.Add(nextInOrder.Key, nextInOrder.Value);
                    ValuesTaken++;
                    nextInOrder = new KeyValuePair<string, List<int>>();
                }
            }
            return studentsSorted;
        }

        private static int CompareInOrder(KeyValuePair<string, List<int>> firstvalue,
            KeyValuePair<string, List<int>> secondValue)
        {
            var totalofFirstMark = 0;
            foreach (var score in firstvalue.Value)
            {
                totalofFirstMark += score;
            }
            var totalOfSecondMark = 0;
            foreach (var score in secondValue.Value)
            {
                totalOfSecondMark += score;
            }
            return totalOfSecondMark.CompareTo(totalofFirstMark);
        }
        private static int CompareDecendingOrder(KeyValuePair<string, List<int>> firstvalue,
            KeyValuePair<string, List<int>> secondValue)
        {
            var totalofFirstMark = 0;
            foreach (var score in firstvalue.Value)
            {
                totalofFirstMark += score;
            }
            var totalOfSecondMark = 0;
            foreach (var score in secondValue.Value)
            {
                totalOfSecondMark += score;
            }
            return totalofFirstMark.CompareTo(totalOfSecondMark);
        }
    }
}
