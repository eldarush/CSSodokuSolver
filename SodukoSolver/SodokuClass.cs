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


        // Constructor that asks the user for board dimensions and board string
        // and then creates the board
        public SodokuBoard()
        {
            // ask the user for the board string
            Console.WriteLine("Please enter the board string:");
            boardString = Console.ReadLine();

            // size is the square root of the length of the board string
            size = (int)Math.Sqrt(boardString.Length);


            // if the given board string passed the validation,
            // create the board
            if (Validate(size, boardString))
            {
                // copy the board that the testing was done on
                // because it is valid
                board = ValidatingFunctions.Vboard;
            }
            
            // else exit the program
            // TODO: add exceptions
            else
            {
                Console.WriteLine("The board string is not valid");
                Environment.Exit(0);
            }

        }

    }
}
