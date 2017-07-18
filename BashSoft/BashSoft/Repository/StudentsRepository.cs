
using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using BashSoft.Exceptions;
using BashSoft.Models;

namespace BashSoft
{
    using System.Collections.Generic;

    public  class StudentsRepository
    {
        private  bool IsDataInitialized = false;

        private Dictionary<string, Course> courses;
        private Dictionary<string, Student> students;
        private RepositoryFilter filter;
        private RepositorySorter sorter;

        public StudentsRepository(RepositoryFilter filter, RepositorySorter sorter)
        {
            this.filter = filter;
            this.sorter = sorter;
           
        }

        public  void LoadData(string fileName)
        {
            if (this.IsDataInitialized)
            {
                OutputWriter.DisplayExeption(ExceptionMessages.ExeptiDataAlreadyInitialised);
                return;
            }


            OutputWriter.WriteMessageOnNewLine("Reading data...");
            this.courses = new Dictionary<string, Course>();
            this.students = new Dictionary<string, Student>();
            ReadData(fileName);
        }

        public void UnloadData()
        {
            if (!this.IsDataInitialized)
            {
                throw new ArgumentException(ExceptionMessages.DataNotInitializedExceptionMessage);
            }

            this.courses = null;
            this.students = null;
            this.IsDataInitialized = false;
        }

        private  void ReadData(string fileName)
        {

            var path = SessionData.currentPath + "\\" + fileName;
            if (File.Exists(path))
            {
                var pattern = @"([A-Z][a-zA-Z#\++]*_[A-Z][a-z]{2}_\d{4})\s+([A-Za-z]+\d{2}_\d{2,4})\s([\s0-9]+)";
                var regex = new Regex(pattern);
                var allInputLines = File.ReadAllLines(path);
                for (var line = 0; line < allInputLines.Length; line++)
                {
                    if (!string.IsNullOrEmpty(allInputLines[line]) && regex.IsMatch(allInputLines[line]))
                    {

                        var tokens = regex.Match(allInputLines[line]);
                        var courseName = tokens.Groups[1].Value;
                        var userName = tokens.Groups[2].Value;
                        var scoreStr = tokens.Groups[3].Value;
                        try
                        {
                            var scores = scoreStr.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                                .Select(int.Parse).ToArray();

                            if (scores.Any(x => x > 100 || x < 0))
                            {
                                OutputWriter.DisplayExeption(ExceptionMessages.InvalidScore);
                                continue;
                            }
                            if (scores.Length > Course.NumberOfTaskOnexam)
                            {
                                OutputWriter.DisplayExeption(ExceptionMessages.InvalidNumberOfScores);
                                continue;
                            }
                            if (!this.students.ContainsKey(userName))
                            {
                                this.students.Add(userName, new Student(userName));
                            }
                            if (!this.courses.ContainsKey(courseName))
                            {
                                this.courses.Add(courseName, new Course(courseName));
                            }

                            Course course = this.courses[courseName];
                            Student student = this.students[userName];

                            student.EnrollInCourse(course);
                            student.SetMarksInCourse(courseName, scores);
                        }
                        catch (FormatException e)
                        {
                           throw new FormatException(e.Message + $"at line : {line}");
                        }
                        
                        
                    }

                }
                IsDataInitialized = true;
                OutputWriter.WriteMessageOnNewLine("Data read!");
            }
            else
            {
                throw new InvalidPathException();
            }
        }

        public  bool IsQueryForCoursePossible(string courseName)
        {
            if (IsDataInitialized)
            {
                if (this.courses.ContainsKey(courseName))
                {
                    return true;
                }
                else
                {
                    OutputWriter.DisplayExeption(ExceptionMessages.InexistingCourseInDataBase);
                }
            }
            else
            {
                OutputWriter.DisplayExeption(ExceptionMessages.DataNotInitializedExceptionMessage);
            }

            return false;
        }

        public  bool IsQueryForStudentPossiblе(string courseName, string studentsUserName)
        {
            if (IsQueryForCoursePossible(courseName) && this.courses[courseName].StudentByName.ContainsKey(studentsUserName))
            {
                return true;
            }
            else
            {
                OutputWriter.DisplayExeption(ExceptionMessages.InexistingStudentInDataBase);
            }
            return false;
        }

        public  void GetStudentScoteFromCourse(string courseName, string userName)
        {
            if (IsQueryForStudentPossiblе(courseName, userName))
            {
                OutputWriter.PrintStudent(new KeyValuePair<string, double>(userName, this.courses[courseName].StudentByName[userName].MarksByCoursesName[courseName]));
            }
        }

        public  void GetAllStudentsFromCourse(string courseName)
        {
            if (IsQueryForCoursePossible(courseName))
            {
                OutputWriter.WriteMessageOnNewLine($"{courseName}.");
                foreach (var studentsMarks in this.courses[courseName].StudentByName)
                {
                    this.GetStudentScoteFromCourse(courseName, studentsMarks.Key);
                }
            }
        }

        public  void FilterAndTake(string courseName, string givenFilter, int? studentsToTake = null)
        {
            if (IsQueryForCoursePossible(courseName))
            {
                if (studentsToTake == null)
                {
                    studentsToTake = this.courses[courseName].StudentByName.Count;
                }
                var marks = this.courses[courseName].StudentByName
                    .ToDictionary(x => x.Key, x => x.Value.MarksByCoursesName[courseName]);
                filter.FilterAndTake(marks, givenFilter, studentsToTake.Value);
            }
        }
        public  void OrderAndTake(string courseName, string comparison, int? studentsToTake = null)
        {
            if (IsQueryForCoursePossible(courseName))
            {
                if (studentsToTake == null)
                {
                    studentsToTake = this.courses[courseName].StudentByName.Count;
                }
                var marks = this.courses[courseName].StudentByName
                    .ToDictionary(x => x.Key, x => x.Value.MarksByCoursesName[courseName]);
                sorter.OrderAndTake(marks, comparison, studentsToTake.Value);
            }

        }

    }
}
