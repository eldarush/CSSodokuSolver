using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using static SodukoSolver.HelperFunctions;
using static SodukoSolver.SolvingFunctions;
using static SodukoSolver.ValidatingFunctions;

namespace SodukoSolver
{
    public class Handler
    {
        static void Main(string[] args)
        {
            // start the program that gets soduko board from the user 
            // and runs the algorithm
            UserInterface.Run();

        }
    }
}
