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
        /// <summary>
        /// defined entry point for the project
        /// </summary>
        static void Main()
        {
            // start the program that gets soduko Board from the user 
            // and runs the algorithm
            UserInterface.Run();
        }
    }
}
