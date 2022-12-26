using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

// import both classes whose functions will be used
using static SodukoSolver.ValidatingFunctions;
using static SodukoSolver.SolvingFunctions;
using static SodukoSolver.HelperFunctions;

namespace SodukoSolver
{
    // main class that will run the program
    class UserInterface
    {
        // create a soduko board object
        static SodokuBoard board;
        
        // function that will run the program
        public static void Run()
        {
            // print welcome message
            PrintWelcomeMessage();
        }

        // function that will print the welcome message
        private static void PrintWelcomeMessage()
        {
            Console.WriteLine("Welcome to the Soduko Solver! \n" +
                "This is a program that will solve any soduko board \n" +
                "Please choose the form of input for the soduko: \n" +
                "\t s: input a board string that will represent a board \n" +
                "\t f: input a file that will conntain the board \n" +
                "\t press any other key to exit the program");

            // get the user input
            String tempInput = Console.ReadLine();
            bool validInput = false;
            char input;

            // check if the input is valid
            while (!validInput)
            {
                validInput = (!string.IsNullOrWhiteSpace(tempInput));
                if (!validInput)
                {
                    Console.WriteLine("Please Enter a valid character");
                    tempInput = Console.ReadLine();
                }        
            }

            // when a valid char was given get the first char into 'input'
            input = tempInput[0];

            // boolean that will keep track if the board can be solved 
            bool CanBeSolved;

            // watch to keep trac of how long it took the algoritm to solve
            var watch = new System.Diagnostics.Stopwatch();

            switch (input)
            {
                case 's':
                    // create a soduko board from the string
                    board = new SodokuBoard();
                    // print the input board
                    Console.WriteLine("\nInput board is: \n");
                    PrintBoard(board.getBoard(), board.getSize());

                    // pass the size of the board and the board to the solver 
                    // class
                    SolvingFunctions.setSize(board.getSize());
                    SolvingFunctions.setBlockSize((int)Math.Sqrt(board.getSize()));
                    SolvingFunctions.setBoard(board.getBoard());


                    // start timer to see how long the solving took

                    // Solve the board and start the timer
                    watch.Start();

                    // run the algoritms to solve the board
                    CanBeSolved =  Solve();

                    // stop the timer
                    watch.Stop();

                    // if the board can be solved then print the solved board
                    if (CanBeSolved)
                    {
                        // copy the solved board 
                        board.setBoard(SolvingFunctions.getBoard());

                        // print the solved board
                        Console.WriteLine("\nSolved board is: \n");
                        PrintBoard(board.getBoard(), board.getSize());
                        Console.WriteLine($"\nIt took the algoritm {watch.ElapsedMilliseconds} ms to solve the board.\n" +
                            $"This is the equivilent to {(float)watch.ElapsedMilliseconds / 1000} seconds");

                    }
                    else
                    {
                        // if the board can not be solved then display message 
                        Console.WriteLine("Board Cannot Be Solved :(");
                    }

                    break;
                case 'f':
                    // implement getting the board from a file
                    //board = new SodokuBoardF();
                    break;
                default:
                    Console.WriteLine("Exiting the program");
                    break;
            }
        }

        // function that solves the board using all the implemented algorithms
        private static bool Solve()
        {
            bool solvable = false;

            // fill in cells using the simple elimination technique
            while (Eliminate())
            {
                // do nothing, just keep calling Eliminate until it returns false
            }

            Console.WriteLine("\nSimple elimination board is: \n");
            PrintBoard(board.getBoard(), board.getSize());
            Console.WriteLine("");

            // fill in more cells using the hidden singles method
            while (hiddenSingles())
            {
                // do nothing, just keep calling hiddenSingles until it returns false
            }

            Console.WriteLine("\nHidden singles board is: \n");
            PrintBoard(board.getBoard(), board.getSize());
            Console.WriteLine("");

            // fill in more cells using the naked pairs method
            //while (nakedPairs())
            //{
            //    // do nothing, just keep calling nakedpairs untill it returns false
            //}

            //Console.WriteLine("\nHidden pairs board is: \n");
            //PrintBoard(board.getBoard(), board.getSize());
            //Console.WriteLine("");

            //// fill in more cells using the naked triples method
            //while (nakedTriples())
            //{
            //    // do nothing, just keep calling nakedtriples untill it returns false
            //}

            //Console.WriteLine("\nHidden triples board is: \n");
            //PrintBoard(board.getBoard(), board.getSize());
            //Console.WriteLine("");

            //// fill in more cells using the naked quads method
            //while (nakedQuads())
            //{
            //    // do nothing, just keep calling nakedquads untill it returns false
            //}

            //Console.WriteLine("\nHidden quads board is: \n");
            //PrintBoard(board.getBoard(), board.getSize());
            //Console.WriteLine("");

            // run the backtracking algorithm
            solvable =Backtracking();

            // return if the algoritms managed to solve the board or not
            return solvable;
        }
    }
}
