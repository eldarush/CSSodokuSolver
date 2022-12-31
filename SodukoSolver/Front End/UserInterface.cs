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
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8604 // Possible null reference argument.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.

namespace SodukoSolver
{
    // main class that will run the program
    class UserInterface
    {
        // create a soduko board object
        static SodokuBoard? board;

        // a 2d board of integers that will be used to solve the soduko
        // using bit wise backtracking
        static int[,]? bitWiseBoard;

        // function that will run the program
        public static void Run()
        {
            // draw the ascii art
            PrintAsciiArt();

            // print welcome message
            PrintWelcomeMessage();
        }

        // function that will print the welcome message
        private static void PrintWelcomeMessage()
        {
            Console.WriteLine("Welcome to the Soduko Solver! \n" +
                "This is a program written in c# by @Eldar Aslanbeily \n" +
                "For more information about the program, please visit: \n" +
                "https://github.com/eldarush/CSSodokuSolver.git \n" +
                "This is a program that will solve any soduko board \n" +
                "\nPlease choose the form of input for the soduko: \n" +
                "\t s: input a board string that will represent a board \n" +
                "\t f: input a file that will conntain the board \n" +
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
                    Console.WriteLine("Please Enter a valid character");
                    tempInput = Console.ReadKey().KeyChar.ToString(); ;
                }
            }

            // when a valid char was given get the first char into 'input'
            input = tempInput[0];

            // boolean that will keep track if the board can be solved 
            bool CanBeSolved;

            SolvingFunctions solver;

            // watch to keep trac of how long it took the algoritm to solve
            var watch = new Stopwatch();


            switch (input)
            {
                // if the user chose to input a manual console string
                case 's':
                    // create a soduko board from the string
                    board = new SodokuBoard();

                    // print the input board
                    //Console.WriteLine("\nInput board is: \n");
                    //PrintBoard(board.getBoard(), board.getSize());
                    Console.WriteLine("\nBoard successfully read from console, Finding solution...");

                    // create new solving functions object
                    solver = new SolvingFunctions(board.GetSize(), board.GetBoard());

                    // Solve the board and start the timer
                    watch.Start();

                    // run the algoritms to solve the board
                    CanBeSolved = Solve(solver);

                    // stop the timer
                    watch.Stop();

                    // if the board can be solved then print the solved board
                    if (CanBeSolved)
                    {

                        // print the solved board
                        Console.WriteLine("\nSolved board is: \n");
                        PrintBoard(board.GetBoard(), board.GetSize());

                        // print the solved board string
                        Console.WriteLine("\nSolved board string is: \n");
                        Console.WriteLine(GetBoardString(board.GetBoard(), board.GetSize()));

                        // print the elapsed times in seconds, milliseconds
                        Console.WriteLine("\nElapsed time: {0} seconds", watch.Elapsed.TotalSeconds);
                        Console.WriteLine("Elapsed time: {0} milliseconds", watch.Elapsed.TotalMilliseconds);

                    }
                    else
                    {
                        // if the board can not be solved then display message 
                        Console.WriteLine("Board Cannot Be Solved :(");

                        // print the elapsed times in seconds, milliseconds
                        Console.WriteLine("\nElapsed time: {0} seconds", watch.Elapsed.TotalSeconds);
                        Console.WriteLine("Elapsed time: {0} milliseconds", watch.Elapsed.TotalMilliseconds);
                    }
                    break;

                // in the case that the user wants to input a file path
                case 'f':
                    // ask the user for the file path
                    Console.WriteLine("Please enter the file path:");
                    string filepath = Console.ReadLine();
                    // while the board string is null or white space, ask the user to enter board again
                    while (String.IsNullOrWhiteSpace(filepath))
                    {
                        Console.WriteLine("The entered file path is empty, please enter a valid path:");
                        filepath = Console.ReadLine();
                    }


                    // create a soduko board from the file path
                    board = new SodokuBoard(filepath);

                    //// print the input board
                    //Console.WriteLine("\nInput board is: \n");
                    //PrintBoard(board.getBoard(), board.getSize());
                    Console.WriteLine("\nBoard successfully read from file, Finding solution...");

                    // create new solving functions object
                    solver = new SolvingFunctions(board.GetSize(), board.GetBoard());

                    // Solve the board and start the timer
                    watch.Start();

                    // run the algoritms to solve the board
                    CanBeSolved = Solve(solver);

                    // stop the timer
                    watch.Stop();

                    // if the board can be solved then print the solved board
                    if (CanBeSolved)
                    {

                        // print the solved board
                        Console.WriteLine("\nSolved board is: \n");
                        PrintBoard(board.GetBoard(), board.GetSize());

                        // print the solved board string
                        Console.WriteLine("\nSolved board string is: \n");
                        string solvedboardstring = GetBoardString(board.GetBoard(), board.GetSize());
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

                        Console.WriteLine($"\nSolved board string was written to a file with the path:\n{solvedfilepath}");
                        writer.Write(solvedboardstring);

                        // print the elapsed times in seconds, milliseconds
                        Console.WriteLine("\nElapsed time: {0} seconds", watch.Elapsed.TotalSeconds);
                        Console.WriteLine("Elapsed time: {0} milliseconds", watch.Elapsed.TotalMilliseconds);

                    }
                    else
                    {
                        // if the board can not be solved then display message 
                        Console.WriteLine("Board Cannot Be Solved :(");

                        // print the elapsed times in seconds, milliseconds
                        Console.WriteLine("\nElapsed time: {0} seconds", watch.Elapsed.TotalSeconds);
                        Console.WriteLine("Elapsed time: {0} milliseconds", watch.Elapsed.TotalMilliseconds);
                    }

                    break;
                    
                default:
                    Console.WriteLine("Exiting the program");
                    Environment.Exit(0);
                    break;
            }
        }

        // function that solves using dlx
        //private static bool Solve2(SolvingFunctions solver)
        //{
        //    bool solvable = false;

        //    solvable = solver.DancingLinks(solver.Board);

        //    PrintBoard(solver.Board, solver.Size);

        //    Console.ReadLine();

        //    board.SetBoard(solver.Board);

        //    return solvable;
        //}

        // function that solves the board using all the implemented algorithms
        private static bool Solve(SolvingFunctions solver)
        {

            // count the number of empty cells
            _ = CountEmptyCells(board.GetBoard(), board.GetSize());

            // we make an assumption that if less then 10 percent of the cells are empty,
            // then the board is solvable faster using the backtracking and dancing links

            float emptyCells = CountEmptyCells(board.GetBoard(), board.GetSize());
            bool solvable;
            // if the number of empty cells is less than 10 percent of the board then run the algorithms
            // skip straight to the brute force method
            if (emptyCells <= (board.GetSize() * board.GetSize()) * 0.1)
            {
                // run the backtracking algorithm
                solvable = RunBacktracking(solver);

                // return if the algoritms managed to solve the board or not
                return solvable;
            }

            // fill in cells using the simple elimination technique
            while (solver.Eliminate())
            {
                // do nothing, just keep calling Eliminate until it returns false
            }


            //Console.WriteLine("\nSimple elimination board is: \n");
            //PrintBoard(board.getBoard(), board.getSize());
            //Console.WriteLine("");


            emptyCells = CountEmptyCells(board.GetBoard(), board.GetSize());
            // if the number of empty cells is less than 10 percent of the board then run the algorithms
            // skip straight to the brute force method
            if (emptyCells <= (board.GetSize() * board.GetSize()) * 0.1)
            {
                // run the backtracking algorithm
                solvable = RunBacktracking(solver);

                // return if the algoritms managed to solve the board or not
                return solvable;
            }

            // fill in more cells using the hidden singles method
            while (solver.HiddenSingles())
            {
                // do nothing, just keep calling hiddenSingles until it returns false
            }

            //Console.WriteLine("\nHidden singles board is: \n");
            //PrintBoard(board.getBoard(), board.getSize());
            //Console.WriteLine("");


            emptyCells = CountEmptyCells(board.GetBoard(), board.GetSize());
            // if the number of empty cells is less than 10 percent of the board then run the algorithms
            // skip straight to the brute force method
            if (emptyCells <= (board.GetSize() * board.GetSize()) * 0.1)
            {
                // run the backtracking algorithm
                solvable = RunBacktracking(solver);

                // return if the algoritms managed to solve the board or not
                return solvable;
            }

            // fill in more cells using the naked pairs method
            while (solver.NakedPairs())
            {
                // do nothing, just keep calling nakedpairs untill it returns false
            }

            //Console.WriteLine("\nHidden pairs board is: \n");
            //PrintBoard(board.getBoard(), board.getSize());
            //Console.WriteLine("");


            emptyCells = CountEmptyCells(board.GetBoard(), board.GetSize());
            // if the number of empty cells is less than 10 percent of the board then run the algorithms
            // skip straight to the brute force method
            if (emptyCells <= (board.GetSize() * board.GetSize()) * 0.1)
            {
                // run the backtracking algorithm
                solvable = RunBacktracking(solver);

                // return if the algoritms managed to solve the board or not
                return solvable;
            }


            // TODO: intersection, naked triples and naked quads are not working properly, evertyhing else above this line is working
            // ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------           

            // fill in more cells using the naked triples method
            //while (solver.nakedTriples())
            //{
            //    // do nothing, just keep calling nakedtriples untill it returns false
            //}

            ////Console.WriteLine("\nHidden triples board is: \n");
            ////PrintBoard(board.getBoard(), board.getSize());
            ////Console.WriteLine("");

            //emptyCells = CountEmptyCells(board.getBoard(), board.getSize());
            //// if the number of empty cells is less than 66 percent of the board then run the algorithms
            //// skip straight to the brute force method
            //if (emptyCells <= (board.getSize() * board.getSize()) * 0.66)
            //{
            //    // run the backtracking algorithm
            //    solvable = RunBacktracking(solver);

            //    // return if the algoritms managed to solve the board or not
            //    return solvable;
            //}

            // fill in more cells using the naked quads method
            //while (solver.nakedQuads())
            //{
            //    // do nothing, just keep calling nakedquads untill it returns false
            //}

            //Console.WriteLine("\nHidden quads board is: \n");
            //PrintBoard(board.getBoard(), board.getSize());
            //Console.WriteLine("");


            ////emptyCells = CountEmptyCells(board.getBoard(), board.getSize());
            ////// if the number of empty cells is less than 66 percent of the board then run the algorithms
            ////// skip straight to the brute force method
            ////if (emptyCells <= (board.getSize() * board.getSize()) * 0.66)
            ////{
            ////    // run the backtracking algorithm
            ////    solvable = RunBacktracking(solver);

            ////    // return if the algoritms managed to solve the board or not
            ////    return solvable;
            ////}

            //// fill in more cells using the pointing doubles method
            //while (solver.PointingDoubles())
            //{
            //    // do nothing, just keep calling pointingdoubles untill it returns false
            //}

            ////Console.WriteLine("\nPointing doubles board is: \n");
            ////PrintBoard(board.getBoard(), board.getSize());
            ////Console.WriteLine("");

            ////emptyCells = CountEmptyCells(board.getBoard(), board.getSize());
            ////// if the number of empty cells is less than 66 percent of the board then run the algorithms
            ////// skip straight to the brute force method
            ////if (emptyCells <= (board.getSize() * board.getSize()) * 0.66)
            ////{
            ////    // run the backtracking algorithm
            ////    solvable = RunBacktracking(solver);

            ////    // return if the algoritms managed to solve the board or not
            ////    return solvable;
            ////}

            //// fill in more cells using the pointing triples method
            //while (solver.PointingTriples())
            //{
            //    // do nothing, just keep calling pointingtriples untill it returns false
            //}

            ////Console.WriteLine("\nPointing triples board is: \n");
            ////PrintBoard(board.getBoard(), board.getSize());
            ////Console.WriteLine("");

            ////emptyCells = CountEmptyCells(board.getBoard(), board.getSize());
            ////// if the number of empty cells is less than 66 percent of the board then run the algorithms
            ////// skip straight to the brute force method
            ////if (emptyCells <= (board.getSize() * board.getSize()) * 0.66)
            ////{
            ////    // run the backtracking algorithm
            ////    solvable = RunBacktracking(solver);

            ////    // return if the algoritms managed to solve the board or not
            ////    return solvable;
            ////}

            //// fill in more cells using the box line reduction method
            //while (solver.boxLineReduction())
            //{
            //    // do nothing, just keep calling boxlinereduction untill it returns false
            //}

            ////Console.WriteLine("\nBox line reduction board is: \n");
            ////PrintBoard(board.getBoard(), board.getSize());
            ////Console.WriteLine("");

            // run the backtracking algorithm
            solvable = RunBacktracking(solver);

            // return if the algoritms managed to solve the board or not
            return solvable;
        }

        // the function that will run the diffrent version of the backtracking algorithms using diffrent
        // tasks and once one of the thread returns a result, it will stop the other threads and return the result
        public static bool RunBacktracking(SolvingFunctions solver)
        {
            // fill in the obvoius cells left using hidden singles
            while (solver.HiddenSingles())
            {
                // do nothing, just keep calling hiddensingles untill it returns false
            }

            // convert the board to string and from it to 2d array of ints for bitwisw solving
            string boardstring = GetBoardString(board.GetBoard(), board.GetSize());
            bitWiseBoard = new int[board.GetSize(), board.GetSize()];
            bitWiseBoard = ConvertTo2DArray(boardstring, bitWiseBoard, board.GetSize());

            // create a new solving functions class
            SolvingFunctions solver2 = new(solver.Size, null);
            SolvingFunctions solver3 = new(solver.Size, null);

            // get a copy of the board to solver2
            solver2.Board = CopyBoard(solver.Board, solver.Size);           

            // Create a CancellationTokenSource
            CancellationTokenSource cts = new();

            // Create three tasks, one for each algorithm
            Task<bool> t1 = Task.Run(() => solver.Backtracking(cts.Token));
            Task<bool> t2 = Task.Run(() => solver2.BacktrackingR(cts.Token));
            Task<bool> t3 = Task.Run(() => solver3.SolveSudokuUsingBitwiseBacktracking(bitWiseBoard,cts.Token));

            // Wait for the first task to complete
            int completedTaskIndex = Task.WaitAny(t1, t2, t3);

            // Cancel the other tasks

            cts.Cancel();

            // Return the result of the completed task
            bool finished = Task.WhenAny(t1, t2, t3).Result.Result;
            bool solved = false;

            
            //if the first task finished first, copy its board into our board
            solved = IsSolved(solver.Board, solver.Size);
            if (solved)
            {
                // set the board to the solved board and return
                board.SetBoard(solver.Board);
                return solved;
            }

            // if solved with backtracking reversed
            solved = IsSolved(solver2.Board, solver2.Size);
            if (solved)
            {
                // set the board to the solved board and return
                board.SetBoard(solver2.Board);
                return solved;
            }

            // if solved with backtracking bitwise
            solved = IsSolvedInts(bitWiseBoard, solver3.Size);
            if (solved)
            {

                // set the board to the solved board and return
                board.SetBoard(IntsToCells(bitWiseBoard));
                return solved;
            }
            // if we reached here then none of the algorithms managed to solve the board
            return false;
        }

    }
}
