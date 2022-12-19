using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodukoSolver
{
    // main class that will run the program
    class Program
    {
        // main method that will run the program
        static void Main(string[] args)
        {
            // create a new soduko board object
            // inside this object the board will be created from the user input
            SodokuBoard board;

            // TODO: add welcome message and instructions on the first run
            // TODO: add a menu to the program
            // TODO: add a solving and validating method to the program
            // TODO: add exception handling to the program
            
            while (Console.ReadLine() != "q")
            {
                // create a new soduko board object
                // inside this object the board will be created from the user input
                board = new SodokuBoard();

                // print the board
                //board.PrintBoard();
                
                // solve the board
                //board.Solve();

                // print the board
                //board.PrintBoard();
            }
        }
    }
}
