
namespace BashSoft
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

   public class BashSoft
    {
       public static void Main()
        {
            // Frist Part
            IOManager.ChangeCurrentDirectoryRelative(@"..");
            IOManager.ChangeCurrentDirectoryRelative(@"..");
            IOManager.ChangeCurrentDirectoryRelative(@"..");
            IOManager.ChangeCurrentDirectoryRelative(@"..");
            IOManager.ChangeCurrentDirectoryRelative(@"..");
            IOManager.ChangeCurrentDirectoryRelative(@"..");
            IOManager.ChangeCurrentDirectoryRelative(@"..");
            IOManager.ChangeCurrentDirectoryRelative(@"..");
            IOManager.ChangeCurrentDirectoryRelative(@"..");
            IOManager.ChangeCurrentDirectoryRelative(@"..");
            IOManager.ChangeCurrentDirectoryRelative(@"..");
            IOManager.ChangeCurrentDirectoryRelative(@"..");
            // IOManager.TraverseDirectory(SessionData.currentPath);

            //Second Part
            //StudentsRepository.initializeData();
            //StudentsRepository.GetStudentScoteFromCourse("Unity","Ivan");

            // Part Three
               /*Tester.CompareContent(@"C:\Users\MiroslavCharakchiev\Downloads\C# Advance docx\BashSoft-FirstWeek\BashSoft-FirstWeek\04. CSharp-Advanced-Exception-Handling-Lab\actdual.txt",
                   @"C:\Users\MiroslavCharakchiev\Downloads\C# Advance docx\BashSoft-FirstWeek\BashSoft-FirstWeek\04. CSharp-Advanced-Exception-Handling-Lab\expedcted.txt");*/

           // IOManager.CreateDirectoryInCurrentFolder("*2");

        }
    }
}
