using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using static SodukoSolver.Algoritms.PrintingFunctions;
using static SodukoSolver.Algoritms.BoardConvertors;
using SodukoSolver.Interfaces;
using SodukoSolver.BoardSolvers;
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

namespace SodukoSolver
{
    /// <summary>
    /// main class that will run the program
    /// and be responsibale for user - code integration
    /// </summary>
    public class UserInterface
    {
        // create a sudoku Board object
        private static SudokuBoard _sudokuBoard;

        /// <summary>
        /// function that will run the program
        /// </summary>
        public static void Run()
        {
            // draw the ascii art
            PrintAsciiArt();

            // print welcome message
            PrintWelcomeMessage();

            // ask for the user input and start the program
            AskUserForInput();
        }
        
        /// <summary>
        /// function that will get the input from the user and run the algorithms
        /// </summary>
        public static void AskUserForInput()
        {
            Console.WriteLine("\nPlease choose the form of input for the sudoku: \n" +
                "\t C: Use the console to input the board string by hand \n" +
                "\t F: Input a file that will contain the board string\n" +
                "\t Press any other key to exit the program");

            // get the user input
            Console.Write("\nPlease enter your choice: ");
            string tempInput = Console.ReadKey().KeyChar.ToString();
            Console.WriteLine("");

            // check the user input if its valid
            bool validInput = false;

            // check if the input is valid
            while (!validInput)
            {
                validInput = (!string.IsNullOrWhiteSpace(tempInput));
                if (!validInput)
                {
                    Console.Write("\nPlease Enter a valid character (C or F): ");
                    tempInput = Console.ReadKey().KeyChar.ToString(); ;
                }
            }
            Console.WriteLine("");

            // boolean that will keep track if the Board can be solved 
            bool CanBeSolved;

            // create new SudokuBoard solver object
            BoardSolver solver;

            // watch to keep trac of how long it took the algoritm to solve
            var watch = new Stopwatch();

            // reset the stopwatch
            watch.Reset();

            // get input from user
            switch (tempInput[0])
            {
                #region console input

                // if the user chose to input a manual console string
                case 'C':
                case 'c':

                    // create a soduko Board from the string
                    _sudokuBoard = new SudokuBoard();

                    // create new type of solver based on user input
                    GetTypeOfBoardSolver(_sudokuBoard, out solver);

                    // Solve the Board and start the timer
                    // this only the time it took the algorithm to solve the board,
                    // and does not include the time it took it to create or convert the board
                    watch.Start();

                    // run the algoritms to solve the Board
                    CanBeSolved = solver.Solve();

                    // stop the timer
                    watch.Stop();

                    // if the Board can be solved then print the solved Board
                    if (CanBeSolved)
                    {
                        // output the solved SudokuBoard and the SudokuBoard string
                        PrintSolvedBoard(solver);

                        // print the time it took to solve
                        PrintOutTime(watch);

                    }
                    else
                    {
                        // if the Board can not be solved then display message 
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nBoard Cannot Be Solved :( please try again with a different Board");
                        Console.ForegroundColor = ConsoleColor.White;

                        PrintOutTime(watch);

                    }
                    // continue to run the program recursivally
                    AskUserForInput();
                    break;
                #endregion console input

                #region file input
                // in the case that the user wants to input a file path
                case 'F':
                case 'f':

                    // ask the user for the file path
                    Console.WriteLine("Please enter the file path:");
                    string filepath = Console.ReadLine();
                    
                    // while the Board string is null or white space, ask the user to enter Board again
                    while (string.IsNullOrWhiteSpace(filepath))
                    {
                        Console.WriteLine("The entered file path is empty, please enter a valid path:");
                        filepath = Console.ReadLine();
                    }

                    // create a soduko Board from the string
                    _sudokuBoard = new SudokuBoard(filepath);

                    // create new type of solver based on user input
                    GetTypeOfBoardSolver(_sudokuBoard, out solver);

                    // Solve the Board and start the timer
                    // this only the time it took the algorithm to solve the board,
                    // and does not include the time it took it to create or convert the board
                    watch.Start();

                    // run the algoritms to solve the Board
                    CanBeSolved = solver.Solve();

                    // stop the timer
                    watch.Stop();

                    // if the Board can be solved then print the solved Board
                    if (CanBeSolved)
                    {

                        // output the solved SudokuBoard and the SudokuBoard string
                        PrintSolvedBoard(solver);
                        
                        string solvedboardstring = ConvertToString(solver.BoardInts, solver.Size);

                        // create a new file _reader object and write the string to a new file 'nameSOLVED'                        
                        IWritable writer = new FileReader(filepath);

                        // get the solved file path
                        string filedirectory = Path.GetDirectoryName(filepath);
                        string filename = Path.GetFileName(filepath);
                        string solvedfilename = filename.Replace(".txt", "SOLVED.txt");
                        string solvedfilepath = Path.Combine(filedirectory, solvedfilename);

                        Console.WriteLine($"\nSolved Board string was written to a file with the path:\n{solvedfilepath}");
                        writer.Write(solvedboardstring);

                        // print the time it took to solve
                        PrintOutTime(watch);

                    }
                    else
                    {
                        // if the Board can not be solved then display message 
                        Console.WriteLine("\nBoard Cannot Be Solved :(");

                        // print the time it took to solve
                        PrintOutTime(watch);
                    }
                    // continue to run the program recursivally
                    AskUserForInput();
                    break;

                #endregion file input

                // if the user pressed any other key then exit the program
                default:
                    Console.WriteLine("Exiting the program, Thank you and goodbye!");
                    Environment.Exit(0);
                    break;
            }
        }
    }
}
