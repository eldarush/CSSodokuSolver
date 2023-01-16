using SodukoSolver.BoardSolvers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SodukoSolver.Algoritms.BoardConvertors;

namespace SodukoSolver.Algoritms
{
    /// <summary>
    /// this module will contain all the functions to print stuff
    /// on the console
    /// </summary>
    public static class PrintingFunctions
    {
        /// <summary>
        /// function that gets a SudokuBoard solver, ask the user using what algorith, he wants to solve the SudokuBoard,
        /// creates new solver using the input from the user
        /// </summary>
        /// <param name="board">the SudokuBoard</param>
        /// <param name="boardSolver">the returned SudokuBoard solver</param>
        public static void GetTypeOfBoardSolver(SudokuBoard board, out BoardSolver boardSolver)
        {
            // create input string
            string userInput;

            // ask the user in which way does he want to solve the Board
            Console.WriteLine("\nPlease choose the way you want to solve the Board:");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\t D: using the Dancing Links algorithm (Highly Recommended)");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\t B: using the Backtracking algorithm \n");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\t Dancing Links works better for bigger and more complicated Sudoku Board but takes up more memory.");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\t Backtracking works better with simpler and smaller boards and takes up less memory, \n" +
                "\t But may struggle with more complicated boards. \n");
            Console.ForegroundColor = ConsoleColor.White;
            
            // get the user input
            Console.Write("Please enter your choice: ");
            userInput = Console.ReadKey().KeyChar.ToString();

            // while the user doesnt enter a valid charachter, put him in a while loop
            while (userInput != "d" && userInput != "b" && userInput != "D" && userInput != "B")
            {
                Console.Write("\nPlease Enter a valid character (d or b): ");
                userInput = Console.ReadKey().KeyChar.ToString(); ;
            }

            // if the user wanted to solve using dancing links
            if (userInput == "d" || userInput == "D")
            {
                byte[,] matrix = IntBoardToByteMatrix(board.Board, board.Size);

                boardSolver = new DancingLinks(matrix, board.Size);
                return;
            }
            // if the user wanted to solve using backtracking
            else if (userInput == "b" || userInput == "B")
            {
                boardSolver = new BackTracking(board.Board, board.Size);
                return;
            }

            // this part will never be reached
            boardSolver = new BackTracking(board.Board, board.Size);
        }


        /// <summary>
        /// function that gets a stop watch and prints out the seconds and ms in the watch
        /// </summary>
        /// <param name="watch">the stopwatch</param>
        public static void PrintOutTime(Stopwatch watch)
        {
            // print the elapsed times in seconds and milliseconds (1000 ms =  1 sec)
            Console.WriteLine("\nElapsed time: {0} seconds", watch.Elapsed.TotalSeconds);
            Console.WriteLine("Elapsed time: {0} milliseconds", watch.Elapsed.TotalMilliseconds);
        }

        /// <summary>
        /// function that takes in the SudokuBoard solver that contains the result and
        /// prints out the solved SudokuBoard and the SudokuBoard string
        /// </summary>
        /// <param name="solver">the solved SudokuBoard</param>
        public static void PrintSolvedBoard(BoardSolver solver)
        {
            // print the solved Board
            Console.WriteLine("\nSolved Board is: \n");
            PrintBoard(solver.BoardInts, solver.Size);

            // print the solved Board string
            Console.WriteLine("\nSolved Board string is: \n");
            Console.WriteLine(ConvertToString(solver.BoardInts, solver.Size));
        }

        /// <summary>
        /// function that prints the ascci art in the console
        /// </summary>
        public static void PrintAsciiArt()
        {
            // the most beautiful art known to mankind
            var art = new[] {
            @"┌────────────────────────────────────────────────────────────────────────────────────────────────────┐",
            @"│ ╔═══╗────╔╗──╔╗───────╔═══╗──╔╗──────────╔══╗───────╔═══╦╗──╔╗───────╔═══╗──╔╗──────╔╗─────╔╗───── │",
            @"│ ║╔═╗║────║║──║║───────║╔═╗║──║║──────────║╔╗║───────║╔══╣║──║║───────║╔═╗║──║║──────║║─────║║───── │",
            @"│ ║╚══╦══╦═╝╠╗╔╣║╔╦╗╔╗──║╚══╦══╣╠╗╔╦══╦═╗──║╚╝╚╦╗─╔╗──║╚══╣║╔═╝╠══╦═╗──║║─║╠══╣║╔══╦═╗║╚═╦══╦╣║╔╗─╔╗ │",
            @"│ ╚══╗║╔╗║╔╗║║║║╚╝╣║║║──╚══╗║╔╗║║╚╝║║═╣╔╝──║╔═╗║║─║║──║╔══╣║║╔╗║╔╗║╔╝──║╚═╝║══╣║║╔╗║╔╗╣╔╗║║═╬╣║║║─║║ │",
            @"│ ║╚═╝║╚╝║╚╝║╚╝║╔╗╣╚╝║──║╚═╝║╚╝║╚╗╔╣║═╣║───║╚═╝║╚═╝║──║╚══╣╚╣╚╝║╔╗║║───║╔═╗╠══║╚╣╔╗║║║║╚╝║║═╣║╚╣╚═╝║ │",
            @"│ ╚═══╩══╩══╩══╩╝╚╩══╝──╚═══╩══╩═╩╝╚══╩╝───╚═══╩═╗╔╝──╚═══╩═╩══╩╝╚╩╝───╚╝─╚╩══╩═╩╝╚╩╝╚╩══╩══╩╩═╩═╗╔╝ │",
            @"│ ─────────────────────────────────────────────╔═╝║────────────────────────────────────────────╔═╝║─ │",
            @"│ ─────────────────────────────────────────────╚══╝────────────────────────────────────────────╚══╝─ │",
            @"└────────────────────────────────────────────────────────────────────────────────────────────────────┘"
            };
            Console.WriteLine("\n");
            // print in blue and then change it back to white
            Console.ForegroundColor = ConsoleColor.Blue;
            foreach (string line in art)
                Console.WriteLine(line);
            Console.WriteLine("\n");
            Console.ForegroundColor = ConsoleColor.White;
            // set the console title
            Console.Title = "Sudoku Solver by @Eldar Aslanbeily";

        }
        /// <summary>
        /// function that print the welcome message that greets the user
        /// </summary>
        public static void PrintWelcomeMessage()
        {
            // print out the welcome message
            Console.WriteLine("Welcome to the Sudoku Solver! \n" +
                "This is a program written in c# by @Eldar Aslanbeily \n" +
                "For more information about the program, please visit: \n" +
                "https://github.com/eldarush/CSSodokuSolver.git \n" +
                "This is a program that will solve any sudoku Board \n" +
                "The compatible sizes are (1x1 4x4 9x9 16x16 25x25) \n");
        }

        /// <summary>
        /// function that prints the SudokuBoard 
        /// </summary>
        /// <param name="board">the SudokuBoard</param>
        /// <param name="size">the size of the SudokuBoard</param>
        public static void PrintBoard(int[,] board, int size)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            {
                // Find the maximum length of the string representation of any element in the Board
                int maxLength = 0;
                for (int row = 0; row < size; row++)
                {
                    for (int col = 0; col < size; col++)
                    {
                        int length = board[row, col].ToString().Length;
                        // if the current length is bigger then the max length
                        if (length > maxLength)
                        {
                            // make the amx size the current size
                            maxLength = length;
                        }
                    }
                }

                // Calculate the width of each square
                int squareWidth = maxLength + 2;

                // Calculate the Size of the sub-squares (if any)
                int subSquareSize = (int)Math.Sqrt(size);

                // Print the top border
                Console.Write(" ");
                for (int row = 0; row < size; row++)
                {
                    for (int col = 0; col < squareWidth; col++)
                    {
                        Console.Write(@"─");
                    }
                    if ((row + 1) % subSquareSize == 0 && row != size - 1)
                    {
                        Console.Write(@" ");
                    }
                }

                Console.WriteLine();

                // Print the rows
                for (int row = 0; row < size; row++)
                {
                    // Print the left border
                    Console.Write("│");

                    // Print the elements in the row
                    for (int col = 0; col < size; col++)
                    {
                        // if the Size more then 9, print the value with a 0 in front
                        string element;
                        if (size >= 10)
                        {
                            // add leading zero for single digit values
                            element = board[row, col].ToString().PadLeft(2, '0');
                        }
                        else
                        {
                            element = board[row, col].ToString();
                        }
                        // calculate the padding and print it
                        int padding = (squareWidth - element.Length) / 2;
                        for (int k = 0; k < padding; k++)
                        {
                            Console.Write(" ");
                        }

                        // print the next element
                        Console.Write(element);

                        for (int k = 0; k < padding; k++)
                        {
                            Console.Write(@" ");
                        }

                        if ((col + 1) % subSquareSize == 0 && col != size - 1)
                        {
                            Console.Write(@"│");
                        }
                    }

                    // Print the right border
                    Console.WriteLine(@"│");

                    // Print the bottom border for the row
                    if ((row + 1) % subSquareSize == 0 && row != size - 1)
                    {
                        Console.Write(" ");
                        for (int row1 = 0; row1 < size; row1++)
                        {
                            for (int col1 = 0; col1 < squareWidth; col1++)
                            {
                                Console.Write(@"─");
                            }
                            if ((row1 + 1) % subSquareSize == 0 && row1 != size - 1)
                            {
                                Console.Write(@" ");
                            }
                        }
                        Console.WriteLine();
                    }
                }

                // Print the bottom border
                Console.Write(" ");
                for (int row = 0; row < size; row++)
                {
                    for (int col = 0; col < squareWidth; col++)
                    {
                        Console.Write(@"─");
                    }

                    if ((row + 1) % subSquareSize == 0 && row != size - 1)
                    {
                        Console.Write(@" ");
                    }
                }
                Console.WriteLine();
            }
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
