﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

// import both classes whose functions will be used
using static SodukoSolver.Algoritms.ValidatingFunctions;
using static SodukoSolver.Algoritms.HelperFunctions;
using SodukoSolver.Interfaces;
using SodukoSolver.BoardSolvers;

namespace SodukoSolver
{
    // main class that will run the program
    public class UserInterface
    {
        // create a soduko Board object
        static SodokuBoard? board;

        // function that will run the program
        public static void Run()
        {
            // draw the ascii art
            PrintAsciiArt();

            // print welcome message
            PrintWelcomeMessage();

            // ask for the user input and start the program
            AskUserForInput();

        }
        
        // function that will print the welcome message
        private static void AskUserForInput()
        {
            Console.WriteLine("\nPlease choose the form of input for the soduko: \n" +
                "\t s: input a Board string that will represent a Board \n" +
                "\t f: input a file that will conntain the Board \n" +
                "\t press any other key to exit the program");

            // get the user input
            Console.Write("\nPlease enter your choice: ");
            string tempInput = Console.ReadKey().KeyChar.ToString();
            Console.WriteLine("");
            bool validInput = false;
            char input;

            // check if the input is valid
            while (!validInput)
            {
                validInput = (!string.IsNullOrWhiteSpace(tempInput));
                if (!validInput)
                {
                    Console.Write("\nPlease Enter a valid character: ");
                    tempInput = Console.ReadKey().KeyChar.ToString(); ;
                }
            }
            Console.WriteLine("");
            // when a valid char was given get the first char into 'input'
            input = tempInput[0];

            // boolean that will keep track if the Board can be solved 
            bool CanBeSolved;

            // create new board solver object
            BoardSolver solver;

            // watch to keep trac of how long it took the algoritm to solve
            var watch = new Stopwatch();

            // reset the stopwatch
            watch.Reset();

            switch (input)
            {
                // if the user chose to input a manual console string
                case 'S':
                case 's':

                    // ask the user in which way does he want to solve the Board
                    Console.WriteLine("\nPlease choose the way you want to solve the Board: \n" +
                        "\t d: using the Dancing Links algorithm \n" +
                        "\t b: using the Backtracking algorithm \n");

                    // get the user input
                    Console.Write("Please enter your choice: ");
                    tempInput = Console.ReadKey().KeyChar.ToString();
                    Console.WriteLine("");
                    validInput = false;
                    while (tempInput != "d" && tempInput != "b")
                    {
                        Console.Write("\nPlease Enter a valid character (d or b): ");
                        tempInput = Console.ReadKey().KeyChar.ToString(); ;
                    }

                    // create a soduko Board from the string
                    board = new SodokuBoard();

                    if (tempInput == "d")
                    {
                        byte[,] matrix = IntBoardToByteMatrix(board.Board, board.Size);

                        solver = new DancingLinks(matrix, board.Size);
                    }
                    else {
                        solver = new BackTracker(board.Board,board.Size);
                    }

                    // print the input Board
                    //Console.WriteLine("\nInput Board is: \n");
                    //PrintBoard(Board.getBoard(), Board.getSize());
                    Console.WriteLine("\nBoard successfully read from console, Finding solution...");

                    // Solve the Board and start the timer
                    watch.Start();

                    // run the algoritms to solve the Board
                    CanBeSolved = solver.Solve();

                    // stop the timer
                    watch.Stop();

                    // if the Board can be solved then print the solved Board
                    if (CanBeSolved)
                    {

                        // print the solved Board
                        Console.WriteLine("\nSolved Board is: \n");
                        PrintBoard(solver.BoardInts, solver.Size);

                        // print the solved Board string
                        Console.WriteLine("\nSolved Board string is: \n");
                        Console.WriteLine(ConvertToString(solver.BoardInts, solver.Size));

                        PrintOutTime(watch);

                    }
                    else
                    {
                        // if the Board can not be solved then display message 
                        Console.WriteLine("Board Cannot Be Solved :(");

                        PrintOutTime(watch);

                    }
                    // continue to run the program recursivally
                    AskUserForInput();
                    break;

                // in the case that the user wants to input a file path
                case 'F':
                case 'f':

                    // ask the user for the file path
                    Console.WriteLine("Please enter the file path:");
                    string filepath = Console.ReadLine();
                    // while the Board string is null or white space, ask the user to enter Board again
                    while (String.IsNullOrWhiteSpace(filepath))
                    {
                        Console.WriteLine("The entered file path is empty, please enter a valid path:");
                        filepath = Console.ReadLine();
                    }


                    // ask the user in which way does he want to solve the Board
                    Console.WriteLine("\nPlease choose the way you want to solve the Board: \n" +
                        "\t d: using the Dancing Links algorithm \n" +
                        "\t b: using the Backtracking algorithm \n");

                    // get the user input
                    Console.Write("Please enter your choice: ");
                    tempInput = Console.ReadKey().KeyChar.ToString();
                    Console.WriteLine("");
                    validInput = false;
                    while (tempInput != "d" && tempInput != "b")
                    {
                        Console.Write("\nPlease Enter a valid character (d or b): ");
                        tempInput = Console.ReadKey().KeyChar.ToString(); ;
                    }

                    // create a soduko Board from the string
                    board = new SodokuBoard(filepath);

                    if (tempInput == "d")
                    {
                        byte[,] matrix = IntBoardToByteMatrix(board.Board, board.Size);

                        solver = new DancingLinks(matrix, board.Size);
                    }
                    else
                    {
                        solver = new BackTracker(board.Board, board.Size);
                    }

                    //// print the input Board
                    //Console.WriteLine("\nInput Board is: \n");
                    //PrintBoard(Board.getBoard(), Board.getSize());
                    Console.WriteLine("\nBoard successfully read from file, Finding solution...");

                    // Solve the Board and start the timer
                    watch.Start();

                    // run the algoritms to solve the Board
                    CanBeSolved = solver.Solve();

                    // stop the timer
                    watch.Stop();

                    // if the Board can be solved then print the solved Board
                    if (CanBeSolved)
                    {

                        // print the solved Board
                        Console.WriteLine("\nSolved Board is: \n");
                        PrintBoard(solver.BoardInts, solver.Size);

                        // print the solved Board string
                        Console.WriteLine("\nSolved Board string is: \n");
                        string solvedboardstring = ConvertToString(solver.BoardInts, solver.Size);
                        // create new console reader object and write the string to the console
                        IWritable writer = new ConsoleReader();
                        writer.Write(solvedboardstring);

                        // create a new file reader object and write the string to a new file 'nameSOLVED'
                        writer = new FileReader(filepath);
                        
                        // get the solved file directory
                        string filedirectory = Path.GetDirectoryName(filepath);
                        string filename = Path.GetFileName(filepath);
                        string solvedfilename = filename.Replace(".txt", "SOLVED.txt");
                        string solvedfilepath = Path.Combine(filedirectory, solvedfilename);

                        Console.WriteLine($"\nSolved Board string was written to a file with the path:\n{solvedfilepath}");
                        writer.Write(solvedboardstring);

                        // print the elapsed times in seconds, milliseconds
                        Console.WriteLine("\nElapsed time: {0} seconds", watch.Elapsed.TotalSeconds);
                        Console.WriteLine("Elapsed time: {0} milliseconds", watch.Elapsed.TotalMilliseconds);

                    }
                    else
                    {
                        // if the Board can not be solved then display message 
                        Console.WriteLine("Board Cannot Be Solved :(");

                        // print the elapsed times in seconds, milliseconds
                        Console.WriteLine("\nElapsed time: {0} seconds", watch.Elapsed.TotalSeconds);
                        Console.WriteLine("Elapsed time: {0} milliseconds", watch.Elapsed.TotalMilliseconds);
                    }
                    // continue to run the program recursivally
                    AskUserForInput();
                    break;
                    
                default:
                    Console.WriteLine("Exiting the program, Thank you and goodbye!");
                    Environment.Exit(0);
                    break;
            }
        }
    }
}
