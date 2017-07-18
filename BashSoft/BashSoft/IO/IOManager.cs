
using BashSoft.Exceptions;

namespace BashSoft
{
    using System;
    using System.Collections.Generic;
    using System.IO;


    public  class IOManager
    {
        public  void TraverseDirectory(int depth)
        {
            OutputWriter.WriteEmptyLine();
            var initialIndentation = SessionData.currentPath.Split('\\').Length;
            var subFolders = new Queue<string>();
            subFolders.Enqueue(SessionData.currentPath);

            while (subFolders.Count != 0)
            {
                var currentPath = subFolders.Dequeue();
                var identation = currentPath.Split('\\').Length - initialIndentation;

                if (depth - identation < 0)
                {
                    break;
                }

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
                    throw new UnauthorizedAccessException(ExceptionMessages.UnauthorizedAccessException);
                }
                

            }
        }

        public  void CreateDirectoryInCurrentFolder(string name)
        {
            var path = Directory.GetCurrentDirectory() + "\\" + name;
            try
            {
                Directory.CreateDirectory(path);

            }
            catch (ArgumentException)
            {
               throw new InvalidFileNameException();
            }
        }

        public  void ChangeCurrentDirectoryRelative(string relativePaath)
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
                    throw new InvalidPathException();
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
                  throw new InvalidPathException();
                }
              
            }
        }

        public  void ChangeCurrentDirectoryAbsolute(string currentPath)
        {
            if (!Directory.Exists(currentPath))
            {
               throw new InvalidPathException();
            }
            SessionData.currentPath = currentPath;
        }
    }
}
