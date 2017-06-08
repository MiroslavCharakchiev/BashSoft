
using System.ComponentModel;

namespace BashSoft
{
    using System;
    using System.Collections.Generic;
    using System.IO;


    public static class IOManager
    {
        public static void TraverseDirectory(string path)
        {
            OutputWriter.WriteEmptyLine();
            var initialIndentation = SessionData.currentPath.Split('\\').Length;
            var subFolders = new Queue<string>();
            subFolders.Enqueue(path);

            while (subFolders.Count != 0)
            {
                var currentPath = subFolders.Dequeue();
                var identation = currentPath.Split('\\').Length - initialIndentation;

                OutputWriter.WriteMessageOnNewLine(string.Format("{0}{1}", new string('-', identation), currentPath));

                try
                {
                    foreach (var file in Directory.GetFiles(currentPath))
                    {
                        var indexOflastSlash = file.LastIndexOf("\\");
                        var fileName = file.Substring(indexOflastSlash);
                        OutputWriter.WriteMessageOnNewLine(new string('-', indexOflastSlash) + fileName);
                    }
                    foreach (var directoryPath in Directory.GetDirectories(currentPath))
                    {
                        subFolders.Enqueue(directoryPath);
                    }
                }
                catch (UnauthorizedAccessException)
                {
                    OutputWriter.WriteMessageOnNewLine(ExceptionMessages.UnauthorizedAccessException);
                }
                

            }
        }

        public static void CreateDirectoryInCurrentFolder(string name)
        {
            var path = Directory.GetCurrentDirectory() + "\\" + name;
            try
            {
                Directory.CreateDirectory(path);

            }
            catch (ArgumentException)
            {
                OutputWriter.WriteMessageOnNewLine(ExceptionMessages.ForbiddenSymbolsContainedInName);
            }
        }

        public static void ChangeCurrentDirectoryRelative(string relativePaath)
        {
            if (relativePaath == "..")
            {
                try
                {
                    var currentPath = SessionData.currentPath;
                    var indexOflastSlash = currentPath.LastIndexOf("\\");
                    var newPath = currentPath.Substring(0, indexOflastSlash);
                    SessionData.currentPath = newPath;
                }
                catch (ArgumentOutOfRangeException)
                {
                    OutputWriter.WriteMessageOnNewLine(ExceptionMessages.UnableToGoHighterInpartitionHierarchy);
                }
               
            }
            else
            {
                try
                {
                    var currentPath = SessionData.currentPath;
                    currentPath += "\\" + relativePaath;
                    ChangeCurrentDirectoryAbsolute(currentPath);
                }
                catch (Exception)
                {
                   OutputWriter.WriteMessageOnNewLine(ExceptionMessages.InvalidPath);
                }
              
            }
        }

        public static void ChangeCurrentDirectoryAbsolute(string currentPath)
        {
            if (!Directory.Exists(currentPath))
            {
                OutputWriter.DisplayExeption(ExceptionMessages.InvalidPath);
                return;
            }
            SessionData.currentPath = currentPath;
        }
    }
}
