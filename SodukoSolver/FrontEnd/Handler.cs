﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SodukoSolver.Algoritms;
using SodukoSolver.DataStructures;
using static  SodukoSolver.Algoritms.HelperFunctions;

namespace SodukoSolver
{
    public class Handler
    {
        static void Main()
        {
            // start the program that gets soduko Board from the user 
            // and runs the algorithm
            //UserInterface.Run();

            BoardSolver solver = new BoardSolver("800000070006010053040600000000080400003000700020005038000000800004050061900002000");
            Stack<Node> solution = solver.SolveUsingDancingLinks();
            //Console.WriteLine("finito");
            int[,] board;
            ConvertSolutionStackToBoard(solution, solver.Size, out board);
            PrintBoard(board, solver.Size);
        }
    }
}
