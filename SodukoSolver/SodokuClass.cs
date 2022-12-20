using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// import both classes whose functions will be used
using static SodukoSolver.ValidatingFunctions;
using static SodukoSolver.SolvingFunctions;

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
        private int[,] board;

        // getters for the dimensions
        public int getSize()
        {
            return size;
        }

        // Constructor that asks the user for board dimensions and board string
        // and then creates the board
        public SodokuBoard()
        {
            // ask the user for the board dimensions
            Console.WriteLine("Please enter the number of rows and columns for the board");
            Console.Write("Size: ");
            size = Convert.ToInt32(Console.ReadLine());

            // ask the user for the board string
            Console.WriteLine("Please enter the board string");
            boardString = Console.ReadLine();

            
            // if the given board string passed the validation,
            // create the board
            if (Validate(size, boardString) == true)
            {
                // create the board
                //CreateBoard();
            }

        }


        // CreateBoard method that creates the board from the board string
        private void CreateBoard()
        {
            // create the board
            board = new int[size, size];

            // create a counter to keep track of the index of the board string
            int counter = 0;

            // loop through the board and add the values from the board string
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    // add the value from the board string to the board
                    board[i, j] = Convert.ToInt32(boardString[counter].ToString());

                    // increment the counter
                    counter++;
                }
            }
        }
    }
}
