using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using static SodukoSolver.Algoritms.HelperFunctions;
using static SodukoSolver.Algoritms.SolvingFunctions;
using static SodukoSolver.Algoritms.ValidatingFunctions;

namespace SodukoSolver
{
    public class Handler
    {
        static void Main()
        {
            // start the program that gets soduko board from the user 
            // and runs the algorithm
            UserInterface.Run();

        }
    }
}
