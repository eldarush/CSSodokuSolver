using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// import both classes whose functions will be used
using static SodukoSolver.ValidatingFunctions;
using static SodukoSolver.SolvingFunctions;
using static SodukoSolver.HelperFunctions;
using static SodukoSolver.CustomExceptions;

namespace SodukoSolver
{
    // this is a class for the soduko board
    // that will contain the board, its properties, and methods
    // to solve the soduko board
    class SodokuBoard
    {
        // board dimentions
        private int size;

        // the string that will be used to create the board
        private string boardString;

        // the board itself
        private Cell[,] board;

        // getters for the dimensions
        public int getSize()
        {
            return size;
        }

        // getter for the board
        public Cell[,] getBoard()
        {
            return board;
        }

        // setter for the board
        public void setBoard(Cell[,] board)
        {

            this.board = board;
        }


        // Constructor that will the board string from the console
        // and then creates the board
        public SodokuBoard()
        {
            // create new console reader
            IReadable reader = new ConsoleReader();

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

            if (isTheBoardValid(size, boardString))
            {
                // copy the board that the testing was done on
                // because it is valid
                board = ValidatingFunctions.Vboard;
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
            if (isTheBoardValid(size,boardString))
            {
                // copy the board that the testing was done on
                // because it is valid
                board = ValidatingFunctions.Vboard;
            }
            // if the board is not valid, an exception will be thrown 
        }
    }
}
