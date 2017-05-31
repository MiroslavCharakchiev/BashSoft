
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
          // IOManager.TraverseDirectory(@"C:\Users\MiroslavCharakchiev\Downloads\C# Advance docx\BashSoft-FirstWeek\BashSoft-FirstWeek\02. CSharp-Advanced-Sets-And-Dictionaries-Lab");
          
            //Second Part
            StudentsRepository.initializeData();
            StudentsRepository.GetStudentScoteFromCourse("Unity","Ivan");
        }
    }
}
