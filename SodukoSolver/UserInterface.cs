using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            // time stamps that will keep track of how long it took the algorithm to solve
            // the board
            int start_time, elapsed_time;

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
                    SolvingFunctions.setBoard(board.getBoard());


                    // start timer to see how long the solving took

                    // Solve the board
                    start_time = DateTime.Now.Millisecond;
                    CanBeSolved =  Solve();
                    elapsed_time = DateTime.Now.Millisecond - start_time;

                    // if the board can be solved then print the solved board
                    if (CanBeSolved)
                    {
                        // copy the solved board 
                        board.setBoard(SolvingFunctions.getBoard());

                        // print the solved board
                        Console.WriteLine("\nSolved board is: \n");
                        PrintBoard(board.getBoard(), board.getSize());
                        Console.WriteLine($"\nIt took the algoritm {elapsed_time}ms to solve the board.\n" +
                            $"This is the equivilent to {(float)elapsed_time / 1000} seconds");
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
    }
}
