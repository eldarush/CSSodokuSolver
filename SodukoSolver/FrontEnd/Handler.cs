using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SodukoSolver.Algoritms;
using SodukoSolver.DataStructures;

namespace SodukoSolver
{
    public class Handler
    {
        static void Main()
        {
            // start the program that gets soduko Board from the user 
            // and runs the algorithm
            //UserInterface.Run();

            BoardSolver solver = new BoardSolver("0000000000000000");
            Stack<Node> solution = solver.SolveUsingDancingLinks();
            Console.WriteLine("finito");

        }
    }
}
