using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SodukoSolver
{
    /// <summary>
    /// this is the class that will run the program
    /// </summary>
    public class Handler
    {
        static void Main()
        {
            // start the program that gets soduko Board from the user 
            // and runs the algorithm
            UserInterface.Run();
        }
    }
}
