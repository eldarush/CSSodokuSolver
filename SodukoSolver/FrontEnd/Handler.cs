﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using static SodukoSolver.Algoritms.HelperFunctions;
using static SodukoSolver.Algoritms.SolvingFunctions;
using static SodukoSolver.Algoritms.ValidatingFunctions;
using SodukoSolver.Algoritms;

namespace SodukoSolver
{
    public class Handler
    {
        //static void Main()
        //{
        //    // start the program that gets soduko board from the user 
        //    // and runs the algorithm
        //    UserInterface.Run();

        //}
        static void Main()
        {
            SolvingFunctions solver = new SolvingFunctions();
            string solvedString = solver.SolveUsingBacktracking();
            Console.WriteLine("\nSolved Board String:\n" + solvedString + "\n");
            Console.WriteLine("Count is " + SolvingFunctions.countT); 
        }
    }
}
