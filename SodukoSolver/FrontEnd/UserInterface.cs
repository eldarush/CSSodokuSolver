using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

// import both classes whose functions will be used
using static SodukoSolver.Algoritms.ValidatingFunctions;
using static SodukoSolver.Algoritms.SolvingFunctions;
using static SodukoSolver.Algoritms.HelperFunctions;
using SodukoSolver.Interfaces;
using SodukoSolver.Algoritms;
using System.Xml.Linq;
using System.Drawing;
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8604 // Possible null reference argument.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.

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
                    Console.WriteLine("\nPlease Enter a valid character: ");
                    tempInput = Console.ReadKey().KeyChar.ToString(); ;
                }
            }
            Console.WriteLine("");
            // when a valid char was given get the first char into 'input'
            input = tempInput[0];

            // boolean that will keep track if the Board can be solved 
            bool CanBeSolved;

            SolvingFunctions solver;

            // watch to keep trac of how long it took the algoritm to solve
            var watch = new Stopwatch();

            // reset the stopwatch
            watch.Reset();

            switch (input)
            {
                // if the user chose to input a manual console string
                case 'S':
                case 's':
                    // create a soduko Board from the string
                    board = new SodokuBoard();

                    // print the input Board
                    //Console.WriteLine("\nInput Board is: \n");
                    //PrintBoard(Board.getBoard(), Board.getSize());
                    Console.WriteLine("\nBoard successfully read from console, Finding solution...");

                    // create new solving functions object
                    solver = new SolvingFunctions(board.Board, board.Size);

                    // Solve the Board and start the timer
                    watch.Start();

                    // run the algoritms to solve the Board
                    CanBeSolved = Solve(solver);

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


                    // create a soduko Board from the file path
                    board = new SodokuBoard(filepath);

                    //// print the input Board
                    //Console.WriteLine("\nInput Board is: \n");
                    //PrintBoard(Board.getBoard(), Board.getSize());
                    Console.WriteLine("\nBoard successfully read from file, Finding solution...");

                    // create new solving functions object
                    solver = new SolvingFunctions(board.Board, board.Size);

                    // Solve the Board and start the timer
                    watch.Start();

                    // run the algoritms to solve the Board
                    CanBeSolved = Solve(solver);

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


        private static bool Solve(SolvingFunctions solver)
        {
            return solver.BacktrackingWithBitwiseManipulation();
        }

        #region old solving functions

        //function that solves using dlx
        //private static bool Solve2(SolvingFunctions solver, string boardString, int size)
        //{
        //    //Console.WriteLine("Board string is: " + boardString);
        //    Node[][] board = new Node[size][];
        //    List<Node> headers = new List<Node>();
        //    (board, headers) = ConvertStringBoardFixed(boardString, size);

        //    //if the Board can be solved then return true
        //    if (SolveUsingDancingLinks(board, size, headers))
        //    {
        //        Console.WriteLine("sucsessfully solved Board: ");
        //        PrintBoardNode(board, size);
        //        Console.ReadLine();
        //        return true;
        //    }
        //    Console.WriteLine("Board cannot be solved");
        //    Console.ReadLine();
        //    return true;
        //}

        // function that solves the Board using all the implemented algorithms
        //private static bool Solve(SolvingFunctions solver)
        //    {

        //        // we make an assumption that if less then 10 percent of the cells are empty,
        //        // then the Board is solvable faster using the backtracking and dancing links

        //        float emptyCells = CountEmptyCells(board.GetBoard(), board.GetSize());
        //        bool solvable;
        //        // if the number of empty cells is less than 10 percent of the Board then run the algorithms
        //        // skip straight to the brute force method
        //        if (emptyCells <= (board.GetSize() * board.GetSize()) * 0.1)
        //        {
        //            // run the backtracking algorithm
        //            solvable = RunBacktracking(solver, emptyCells);

        //            // return if the algoritms managed to solve the Board or not
        //            return solvable;
        //        }

        //        // fill in cells using the simple elimination technique
        //        while (solver.Eliminate())
        //        {
        //            // do nothing, just keep calling Eliminate until it returns false
        //        }


        //        //Console.WriteLine("\nSimple elimination Board is: \n");
        //        //PrintBoard(Board.getBoard(), Board.getSize());
        //        //Console.WriteLine("");


        //        emptyCells = CountEmptyCells(board.GetBoard(), board.GetSize());
        //        // if the number of empty cells is less than 10 percent of the Board then run the algorithms
        //        // skip straight to the brute force method
        //        if (emptyCells <= (board.GetSize() * board.GetSize()) * 0.1)
        //        {
        //            // run the backtracking algorithm
        //            solvable = RunBacktracking(solver, emptyCells);

        //            // return if the algoritms managed to solve the Board or not
        //            return solvable;
        //        }

        //        // fill in more cells using the hidden singles method
        //        while (solver.HiddenSingles())
        //        {
        //            // do nothing, just keep calling hiddenSingles until it returns false
        //        }

        //        //Console.WriteLine("\nHidden singles Board is: \n");
        //        //PrintBoard(Board.GetBoard(), Board.GetSize());
        //        //Console.WriteLine("");
        //        //Console.ReadLine();


        //        emptyCells = CountEmptyCells(board.GetBoard(), board.GetSize());
        //        // if the number of empty cells is less than 10 percent of the Board then run the algorithms
        //        // skip straight to the brute force method
        //        if (emptyCells <= (board.GetSize() * board.GetSize()) * 0.1)
        //        {
        //            // run the backtracking algorithm
        //            solvable = RunBacktracking(solver, emptyCells);

        //            // return if the algoritms managed to solve the Board or not
        //            return solvable;
        //        }

        //        // fill in more cells using the naked pairs method
        //        while (solver.NakedPairs())
        //        {
        //            // do nothing, just keep calling nakedpairs untill it returns false
        //        }

        //        //Console.WriteLine("\nHidden pairs Board is: \n");
        //        //PrintBoard(Board.getBoard(), Board.getSize());
        //        //Console.WriteLine("");


        //        emptyCells = CountEmptyCells(board.GetBoard(), board.GetSize());
        //        // if the number of empty cells is less than 10 percent of the Board then run the algorithms
        //        // skip straight to the brute force method
        //        if (emptyCells <= (board.GetSize() * board.GetSize()) * 0.1)
        //        {
        //            // run the backtracking algorithm
        //            solvable = RunBacktracking(solver, emptyCells);

        //            // return if the algoritms managed to solve the Board or not
        //            return solvable;
        //        }


        //        // TODO: intersection, naked triples and naked quads are not working properly, evertyhing else above this line is working
        //        // ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------           

        //        // fill in more cells using the naked triples method
        //        //while (solver.nakedTriples())
        //        //{
        //        //    // do nothing, just keep calling nakedtriples untill it returns false
        //        //}

        //        ////Console.WriteLine("\nHidden triples Board is: \n");
        //        ////PrintBoard(Board.getBoard(), Board.getSize());
        //        ////Console.WriteLine("");

        //        //emptyCells = CountEmptyCells(Board.getBoard(), Board.getSize());
        //        //// if the number of empty cells is less than 66 percent of the Board then run the algorithms
        //        //// skip straight to the brute force method
        //        //if (emptyCells <= (Board.getSize() * Board.getSize()) * 0.66)
        //        //{
        //        //    // run the backtracking algorithm
        //        //    solvable = RunBacktracking(solver);

        //        //    // return if the algoritms managed to solve the Board or not
        //        //    return solvable;
        //        //}

        //        // fill in more cells using the naked quads method
        //        //while (solver.nakedQuads())
        //        //{
        //        //    // do nothing, just keep calling nakedquads untill it returns false
        //        //}

        //        //Console.WriteLine("\nHidden quads Board is: \n");
        //        //PrintBoard(Board.getBoard(), Board.getSize());
        //        //Console.WriteLine("");


        //        ////emptyCells = CountEmptyCells(Board.getBoard(), Board.getSize());
        //        ////// if the number of empty cells is less than 66 percent of the Board then run the algorithms
        //        ////// skip straight to the brute force method
        //        ////if (emptyCells <= (Board.getSize() * Board.getSize()) * 0.66)
        //        ////{
        //        ////    // run the backtracking algorithm
        //        ////    solvable = RunBacktracking(solver);

        //        ////    // return if the algoritms managed to solve the Board or not
        //        ////    return solvable;
        //        ////}

        //        //// fill in more cells using the pointing doubles method
        //        //while (solver.PointingDoubles())
        //        //{
        //        //    // do nothing, just keep calling pointingdoubles untill it returns false
        //        //}

        //        ////Console.WriteLine("\nPointing doubles Board is: \n");
        //        ////PrintBoard(Board.getBoard(), Board.getSize());
        //        ////Console.WriteLine("");

        //        ////emptyCells = CountEmptyCells(Board.getBoard(), Board.getSize());
        //        ////// if the number of empty cells is less than 66 percent of the Board then run the algorithms
        //        ////// skip straight to the brute force method
        //        ////if (emptyCells <= (Board.getSize() * Board.getSize()) * 0.66)
        //        ////{
        //        ////    // run the backtracking algorithm
        //        ////    solvable = RunBacktracking(solver);

        //        ////    // return if the algoritms managed to solve the Board or not
        //        ////    return solvable;
        //        ////}

        //        //// fill in more cells using the pointing triples method
        //        //while (solver.PointingTriples())
        //        //{
        //        //    // do nothing, just keep calling pointingtriples untill it returns false
        //        //}

        //        ////Console.WriteLine("\nPointing triples Board is: \n");
        //        ////PrintBoard(Board.getBoard(), Board.getSize());
        //        ////Console.WriteLine("");

        //        ////emptyCells = CountEmptyCells(Board.getBoard(), Board.getSize());
        //        ////// if the number of empty cells is less than 66 percent of the Board then run the algorithms
        //        ////// skip straight to the brute force method
        //        ////if (emptyCells <= (Board.getSize() * Board.getSize()) * 0.66)
        //        ////{
        //        ////    // run the backtracking algorithm
        //        ////    solvable = RunBacktracking(solver);

        //        ////    // return if the algoritms managed to solve the Board or not
        //        ////    return solvable;
        //        ////}

        //        //// fill in more cells using the box line reduction method
        //        //while (solver.boxLineReduction())
        //        //{
        //        //    // do nothing, just keep calling boxlinereduction untill it returns false
        //        //}

        //        ////Console.WriteLine("\nBox line reduction Board is: \n");
        //        ////PrintBoard(Board.getBoard(), Board.getSize());
        //        ////Console.WriteLine("");

        //        // run the backtracking algorithm
        //        emptyCells = CountEmptyCells(board.GetBoard(), board.GetSize());
        //        solvable = RunBacktracking(solver, emptyCells);

        //        // return if the algoritms managed to solve the Board or not
        //        return solvable;
        //    }

        //    // the function that will run the diffrent version of the backtracking algorithms using diffrent
        //    // tasks and once one of the thread returns a result, it will stop the other threads and return the result
        //    public static bool RunBacktracking(SolvingFunctions solver, float emptyCellsCount)
        //    {

        //        // update the emptyCellsArray
        //        EmptyCellsArray = UpdateEmptyCells(EmptyCellsArray, solver.Board, solver.Size, emptyCellsCount);
        //        // update the backwards location
        //        BackwardsLocation = EmptyCellsArray.Length - 1;
        //        //BackwardsLocationBits = EmptyCellsArray.Length - 1;
        //        // print backwards locatiom
        //        //Console.WriteLine("backwards location: " + BackwardsLocation);
        //        //// print amount of empty cells
        //        //Console.WriteLine($"there are {emptyCellsCount} empty cells");

        //        //PrintBoard(solver.Board, solver.Size);

        //        //// print the empty cells array
        //        //Console.WriteLine("\nEmpty cells array is: \n");
        //        //for (int i = 0; i < EmptyCellsArray.Length; i++)
        //        //{
        //        //    Console.Write(EmptyCellsArray[i] + " ");
        //        //}
        //        //Console.WriteLine(" ");

        //        //convert the Board to string and from it to 2d array of ints for bitwisw solving
        //        string boardstring = GetBoardString(board.GetBoard(), board.GetSize());
        //        bitWiseBoard = new int[board.GetSize(), board.GetSize()];
        //        bitWiseBoard = ConvertTo2DArray(boardstring, bitWiseBoard, board.GetSize());
        //        //bitWiseBoard2 = new int[Board.GetSize(), Board.GetSize()];
        //        //bitWiseBoard2 = ConvertTo2DArray(boardstring, bitWiseBoard2, Board.GetSize());

        //        // create a new solving functions class
        //        SolvingFunctions solver2 = new(solver.Size, null);
        //        SolvingFunctions solver3 = new(solver.Size, null);
        //        //SolvingFunctions solver4 = new(solver.Size, null);

        //        // get a copy of the Board to solver2
        //        solver2.Board = CopyBoard(solver.Board, solver.Size);           

        //        // Create a CancellationTokenSource
        //        CancellationTokenSource cts = new();

        //        // Create three tasks, one for each algorithm
        //        Task<bool> t1 = Task.Run(() => solver.Backtracking(cts.Token));
        //        Task<bool> t2 = Task.Run(() => solver2.BacktrackingR(cts.Token));
        //        //Task<bool> t3 = Task.Run(() => solver3.SolveSudokuUsingBitwiseBacktracking(bitWiseBoard,cts.Token));
        //        //Task<bool> t4 = Task.Run(() => solver3.SolveSudokuUsingBitwiseBacktrackingReversed(bitWiseBoard2,cts.Token));

        //        // Wait for the first task to complete
        //        //int completedTaskIndex = Task.WaitAny(t1, t2, t3);
        //        //int completedTaskIndex = Task.WaitAny(t1, t2, t3, t4);
        //        int completedTaskIndex = Task.WaitAny(t1, t2);
        //        //int completedTaskIndex = Task.WaitAny(t2);

        //        // Cancel the other tasks

        //        cts.Cancel();

        //        // Return the result of the completed task
        //        //bool finished = Task.WhenAny(t1, t2, t3).Result.Result;
        //        //bool finished = Task.WhenAny(t1, t2, t3, t4).Result.Result;
        //        bool finished = Task.WhenAny(t1, t2).Result.Result;
        //        //bool finished = Task.WhenAny(t2).Result.Result;
        //        bool solved = false;

        //        //Console.WriteLine("\nBacktracking Board:\n");
        //        //PrintBoard(solver.Board, solver.Size);
        //        //Console.WriteLine("\nBacktracking reversed Board:\n");
        //        //PrintBoard(solver2.Board, solver2.Size);

        //        //if the first task finished first, copy its Board into our Board
        //        solved = IsSolved(solver.Board, solver.Size);
        //        if (solved)
        //        {
        //            // set the Board to the solved Board and return
        //            board.SetBoard(solver.Board);
        //            return solved;
        //        }

        //        // if solved with backtracking reversed
        //        solved = IsSolved(solver2.Board, solver2.Size);
        //        if (solved)
        //        {
        //            // set the Board to the solved Board and return
        //            board.SetBoard(solver2.Board);
        //            return solved;
        //        }

        //        //if solved with backtracking bitwise
        //        solved = IsSolvedInts(bitWiseBoard, solver3.Size);
        //        if (solved)
        //        {

        //            // set the Board to the solved Board and return
        //            board.SetBoard(IntsToCells(bitWiseBoard));
        //            return solved;
        //        }

        //        //solved = IsSolvedInts(bitWiseBoard2, solver4.Size);
        //        //if (solved)
        //        //{

        //        //    // set the Board to the solved Board and return
        //        //    Board.SetBoard(IntsToCells(bitWiseBoard));
        //        //    return solved;
        //        //}
        //        // if we reached here then none of the algorithms managed to solve the Board
        //        return false;
        //    }

        //    private static bool Solve3(SolvingFunctions solver)
        //    {
        //        //string boardstring = GetBoardString(Board.GetBoard(), Board.GetSize());
        //        //bitWiseBoard = new int[Board.GetSize(), Board.GetSize()];
        //        //bitWiseBoard = ConvertTo2DArray(boardstring, bitWiseBoard, Board.GetSize());
        //        //if (solver.SolveUsingBacktracking(bitWiseBoard))
        //        //{
        //        //    Console.WriteLine("Solved!");
        //        //}
        //        //else
        //        //{
        //        //    Console.WriteLine("Not solved!");
        //        //}
        //        //// print out the solved Board
        //        //for (int i = 0; i < Board.GetSize(); i++)
        //        //{
        //        //    for (int j = 0; j < Board.GetSize(); j++)
        //        //    {
        //        //        Console.Write(solver.BoardInts[i, j] + " ");
        //        //    }
        //        //    Console.WriteLine("");
        //        //}
        //        //Console.ReadLine();

        //        //Console.WriteLine("\nBitWise backtracking Board is:\n");
        //        //PrintBoard(Board.GetBoard(), Board.GetSize());
        //        //Console.ReadLine();
        //        //return IsSolvedInts(bitWiseBoard, solver.Size);
        //        return false;
        //    }
        # endregion old solving functions

    }
}
