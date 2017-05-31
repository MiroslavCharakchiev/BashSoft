using System;
using System.Collections.Generic;
using System.IO;


namespace BashSoft
{
    public static class IOManager
    {
        public static void TraverseDirectory(string path)
        {
            OutputWriter.WriteEmptyLine();
            var initialIndentation = path.Split('\\').Length;
            var subFolders = new Queue<string>();
            subFolders.Enqueue(path);

            while (subFolders.Count != 0)
            {
                var currentPath = subFolders.Dequeue();
                var identation = currentPath.Split('\\').Length - initialIndentation;

                OutputWriter.WriteMessageOnNewLine(string.Format("{0}{1}", new string('-', identation), currentPath));

                foreach (var directoryPath in Directory.GetDirectories(currentPath))
                {
                    subFolders.Enqueue(directoryPath);
                }
            }
        }
    }
}
