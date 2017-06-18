using System.Diagnostics;

namespace BashSoft
{
   public static class CommandInterpreter
    {
        public static void InterpredComand(string input)
        {
            var data = input.Split();
            var command = data[0];

            switch (command)
            {
                case "open":
                    TryOpenFile(input, data);
                    break;
                case "mkdir":
                    TryCreateDirectory(input, data);
                    break;
                case "ls":
                    TryTraverseFolders(input, data);
                    break;
                case "cmp":
                    TryCompareFiles(input, data);
                    break;
                case "cdRel":
                    TryCahngePathRelatively(input, data);
                    break;
                case "cdAbs":
                    TryChangePathAbsolute(input, data);
                    break;
                case "readDb":
                    TryReadDatabaseFromFile(input, data);
                    break;
                case "help":
                    TryGetHelp();
                    break;
                case  "filter":
                    TryFilterAndTake(input, data);
                    break;
                case "order":
                    TryOrderAndTake(input, data);
                    break;
                case "show":
                    TryShowWantedData(input, data);
                    break;
                default:
                    DisplayInvalidCommandMessage(input);
                    break;
            }
        }

        private static void TryOrderAndTake(string input, string[] data)
        {
            if (data.Length == 5)
            {
                var courseName = data[1];
                var comparer = data[2].ToLower();
                var takeComand = data[3].ToLower();
                var takeQuantity = data[4].ToLower();

                tryParseParametersForOrderAndTake(takeComand, takeQuantity, comparer, courseName);

            }
            else
            {
                DisplayInvalidCommandMessage(input);
            }
        }

        private static void tryParseParametersForOrderAndTake(string takeComand, string takeQuantity, string comparer, string courseName)
        {
            if (takeComand == "take")
            {
                if (takeQuantity == "all")
                {
                    StudentsRepository.OrderAndTake(courseName, comparer);
                }
                else
                {
                    int studentsToTake;
                    var hasParsed = int.TryParse(takeQuantity, out studentsToTake);
                    if (hasParsed)
                    {
                        StudentsRepository.OrderAndTake(courseName, comparer, studentsToTake);
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

        private static void TryFilterAndTake(string input, string[] data)
        {
            if (data.Length == 5)
            {
                var courseName = data[1];
                var filter = data[2].ToLower();
                var takeComand = data[3].ToLower();
                var takeQuantity = data[4].ToLower();

                tryParseParametersForFilterAndTake(takeComand, takeQuantity, filter, courseName);

            }
            else
            {
                DisplayInvalidCommandMessage(input);
            }
        }

        private static void tryParseParametersForFilterAndTake(string takeComand, string takeQuantity, string filter, string courseName)
        {
            if (takeComand == "take")
            {
                if (takeQuantity == "all")
                {
                    StudentsRepository.FilterAndTake(courseName, filter);
                }
                else
                {
                    int studentsToTake;
                    var hasParsed = int.TryParse(takeQuantity, out studentsToTake);
                    if (hasParsed)
                    {
                        StudentsRepository.FilterAndTake(courseName, filter, studentsToTake);
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

        private static void TryShowWantedData(string input, string[] data)
        {
            if (data.Length == 2)
            {
                var courseName = data[1];
                StudentsRepository.GetAllStudentsFromCourse(courseName);
            }
            else if (data.Length == 3)
            {
                var courseName = data[1];
                var studentName = data[2];
                StudentsRepository.GetStudentScoteFromCourse(courseName, studentName);
            }
            else
            {
                DisplayInvalidCommandMessage(input);
            }
        }

        private static void TryGetHelp()
        {
            OutputWriter.WriteMessageOnNewLine($"{new string('_', 123)}");
            OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -121}|", "make directory - mkdir: path "));
            OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -121}|", "traverse directory - ls: depth "));
            OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -121}|", "comparing files - cmp: path1 path2"));
            OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -121}|", "change directory - cdRel: relative path"));
            OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -121}|", "change directory - cdAbs: absolute path"));
            OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -121}|", "read students data base - readDb: path"));
            OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -121}|", "filter {courseName} excelent/average/poor  take 2/5/all students - filterExcelent (the output is written on the console)"));
            OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -121}|", "order increasing students - order {courseName} ascending/descending take 20/10/all (the output is written on the console)"));
            OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -121}|", "download file - download: path of file (saved in current directory)"));
            OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -121}|", "download file asinchronously - downloadAsynch: path of file (save in the current directory)"));
            OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -121}|", "get help – help"));
            OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -121}|", "Exit – quit"));
            OutputWriter.WriteMessageOnNewLine($"{new string('_', 123)}");
        }

        private static void TryReadDatabaseFromFile(string input, string[] data)
        {
            if (data.Length == 2)
            {
                var filename = data[1];
                StudentsRepository.initializeData(filename);
            }
            else
            {
                DisplayInvalidCommandMessage(input);
            }
            
        }

        private static void TryChangePathAbsolute(string input, string[] data)
        {
            if (data.Length == 2)
            {
                var absolutePath = data[1];
                IOManager.ChangeCurrentDirectoryAbsolute(absolutePath);
            }
            else
            {
                DisplayInvalidCommandMessage(input);
            }

        }

        private static void TryCahngePathRelatively(string input, string[] data)
        {
            if (data.Length == 2)
            {
                var relPath = data[1];
                IOManager.ChangeCurrentDirectoryRelative(relPath);
            }
            else
            {
                DisplayInvalidCommandMessage(input);
            }
        }

        private static void TryTraverseFolders(string input, string[] data)
        {
            if (data.Length == 1)
            {
                IOManager.TraverseDirectory(0);
            }
            else if (data.Length == 2)
            {
                int depth;
                var hasParsed = int.TryParse(data[1], out depth);
                if (hasParsed)
                {
                    IOManager.TraverseDirectory(depth);
                }
                else
                {
                    OutputWriter.WriteMessageOnNewLine(ExceptionMessages.UnableToParseNumber);
                }
            }
            else
            {
                DisplayInvalidCommandMessage(input);
            }
        }

        private static void TryCompareFiles(string input, string[] data)
        {
            if (data.Length == 3)
            {
                var firstFile = data[1];
                var secondFile = data[2];

                Tester.CompareContent(firstFile, secondFile);
            }
            else
            {
                DisplayInvalidCommandMessage(input);
            }
        }

        private static void TryCreateDirectory(string input, string[] data)
        {
            if (data.Length == 2)
            {
                var folderName = data[1];
                IOManager.CreateDirectoryInCurrentFolder(folderName);
            }
            else
            {
                DisplayInvalidCommandMessage(input);
            }
        }

        private static void TryOpenFile(string input, string[] data)
        {
            if (data.Length == 2)
            {
                var fileName = data[1];
                Process.Start(SessionData.currentPath + "\\" + fileName);
            }
            else
            {
                DisplayInvalidCommandMessage(input);
            }
        }

        private static void DisplayInvalidCommandMessage(string input)
        {
            OutputWriter.WriteMessageOnNewLine($"The command '{input}' is invalid");
        }

    }
}
