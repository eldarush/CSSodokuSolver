using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SodukoSolver.DataStructures;
using static SodukoSolver.Algoritms.HelperFunctions;
using SodukoSolver.BoardSolvers;

namespace SodukoSolver
{
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
