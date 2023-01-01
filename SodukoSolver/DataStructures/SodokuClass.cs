using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// import both classes whose functions will be used
using static SodukoSolver.Algoritms.ValidatingFunctions;
using static SodukoSolver.Algoritms.SolvingFunctions;
using static SodukoSolver.Algoritms.HelperFunctions;
using static SodukoSolver.Exceptions.CustomExceptions;
using SodukoSolver.Interfaces;
using SodukoSolver.Algoritms;
#pragma warning disable CS8601 // Possible null reference assignment.
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace SodukoSolver
{
    // this is a class for the soduko board
    // that will contain the board, its properties, and methods
    // to solve the soduko board
    class SodokuBoard
    {
        // board dimentions
        private readonly int size;

        // the string that will be used to create the board
        public readonly string boardString;

        // the board itself
        private Cell[,] board;

        // getters for the dimensions
        public int GetSize()
        {
            return size;
        }

        // getter for the board
        public Cell[,] GetBoard()
        {
            return board;
        }

        // setter for the board
        public void SetBoard(Cell[,] board)
        {

            this.board = board;
        }


        // Constructor that will the board string from the console
        // and then creates the board
        public SodokuBoard()
        {
            // create new console reader
            _ = new ConsoleReader();

            // ask the user for the board string
            Console.WriteLine("Please enter the board string:");
            boardString = Console.ReadLine();
            // while the board string is null or white space, ask the user to enter board again
            while (String.IsNullOrWhiteSpace(boardString))
            {
                Console.WriteLine("The entered board string is empty, please enter a valid string:");
                boardString = Console.ReadLine();
            }

            // size is the square root of the length of the board string
            size = (int)Math.Sqrt(boardString.Length);

            if (IsTheBoardValid(size, boardString))
            {
                // copy the board that the testing was done on
                // because it is valid
                board = Vboard;
            }
            // if the board is not valid, an exception will be thrown 
        }

        // Constructor that will take the board string from a given file path
        // and will create the board
        public SodokuBoard(string path)
        {
            // create new file reader
            IReadable reader = new FileReader(path);

            // read the board string from the file
            boardString = reader.Read();

            // size is the square root of the length of the board string
            size = (int)Math.Sqrt(boardString.Length);

            // if the given board string passed the validation,
            // create the board
            if (IsTheBoardValid(size,boardString))
            {
                // copy the board that the testing was done on
                // because it is valid
                board = ValidatingFunctions.Vboard;
            }
            // if the board is not valid, an exception will be thrown 
        }
    }
}
