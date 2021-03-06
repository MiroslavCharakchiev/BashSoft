﻿
namespace BashSoft
{
    using System;
    using System.IO;

    public  class Tester
    {
        public void CompareContent(string UserOutputPath, string ExpectedOutputPath)
        {
            OutputWriter.WriteMessageOnNewLine("Reading files...");
            try
            {
                var mismatchPath = GetMismatchPath(ExpectedOutputPath);
                var actualOutputLines = File.ReadAllLines(UserOutputPath);
                var expectedOutputLines = File.ReadAllLines(ExpectedOutputPath);

                bool hasMismatch;
                var mismatches = GetLineWithPossibleMissmatch(actualOutputLines, expectedOutputLines, out hasMismatch);

                PrintOutput(mismatches, hasMismatch, mismatchPath);
                OutputWriter.WriteMessageOnNewLine("Files read!");
            }
            catch (DirectoryNotFoundException dnfe)
            {
                OutputWriter.DisplayExeption(dnfe.Message);
            }
            catch (FileNotFoundException fnfe)
            {
                OutputWriter.DisplayExeption(fnfe.Message);
            }
            catch(Exception e)
            {
                OutputWriter.WriteMessageOnNewLine(e.Message);
            }
            

        }

        private void PrintOutput(string[] mismatches, bool hasMismatch, string mismatchPath)
        {
            if (hasMismatch)
            {
                foreach (var line in mismatches)
                {
                    OutputWriter.WriteMessageOnNewLine(line);
                }
                    File.WriteAllLines(mismatchPath, mismatches);
            }
            else
            {
                OutputWriter.WriteMessageOnNewLine("Files are identical. There are no mismatches.");
            }

        }

        private  string[] GetLineWithPossibleMissmatch(string[] actualOutputLines, string[] expectedOutputLines, out bool hasMismatch)
        {
            hasMismatch = false;
            var output = string.Empty;
            OutputWriter.WriteMessageOnNewLine("Comparing files...");
            var minOutputLine = actualOutputLines.Length;
            if (actualOutputLines.Length != expectedOutputLines.Length)
            {
                hasMismatch = true;
                minOutputLine = Math.Min(actualOutputLines.Length, expectedOutputLines.Length);
                OutputWriter.WriteMessageOnNewLine(ExceptionMessages.ComparisonOfFilesWithDifferentSizes);
            }
            var mismatches = new string[minOutputLine];

            for (int i = 0; i < minOutputLine; i++)
            {
                var actualLine = actualOutputLines[i];
                var expectedLine = expectedOutputLines[i];
               
                if (!actualLine.Equals(expectedLine))
                {
                    output = string.Format("Mismatch at line{0} -- expected: \"{1}\", actual: \"{2}\"", i, expectedLine,
                        actualLine);
                    output += Environment.NewLine;
                    hasMismatch = true;
                }
                else
                {
                    output = actualLine;
                    output += Environment.NewLine;
                }
                mismatches[i] = output;
            }
            return mismatches;
        }

        private  string GetMismatchPath(string ExpectedOutputPath)
        {
            var indexOf = ExpectedOutputPath.LastIndexOf('\\');
            var directorypath = ExpectedOutputPath.Substring(0, indexOf);
            var finalPath = directorypath + @"\Mismatches.txt";
            return finalPath;
        }
    }
}
