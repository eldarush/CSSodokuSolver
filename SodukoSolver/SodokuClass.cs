using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodukoSolver
{
    // this is a class for the soduko board
    // that will contain the board, its properties, and methods
    // to solve the soduko board
    class SodokuBoard
    {
        // board dimentions
        private int rows;
        private int cols;

        // the string that will be used to create the board
        private string boardString;

        // the board itself
        private int[,] board;

        // getters for the dimensions
        public int getRows()
        {
            return rows;
        }

        public int getCols()
        {
            return cols;
        }

        // Constructor that asks the user for board dimensions and board string
        // and then creates the board
        public SodokuBoard()
        {
            // ask the user for the board dimensions
            Console.WriteLine("Please enter the number of rows and columns for the board");
            Console.Write("Rows: ");
            rows = Convert.ToInt32(Console.ReadLine());
            Console.Write("Columns: ");
            cols = Convert.ToInt32(Console.ReadLine());

            // ask the user for the board string
            Console.WriteLine("Please enter the board string");
            boardString = Console.ReadLine();

            // if the given board string passed the validation,
            // create the board

            // create the board
            CreateBoard();
        }


        // CreateBoard method that creates the board from the board string
        private void CreateBoard()
        {
            // create the board
            board = new int[rows, cols];

            // create a counter to keep track of the index of the board string
            int counter = 0;

            // loop through the board and add the values from the board string
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    // add the value from the board string to the board
                    board[i, j] = Convert.ToInt32(boardString[counter].ToString());

                    // increment the counter
                    counter++;
                }
            }
        }


        // PrintBoard method that prints the board to the console
        public void PrintBoard()
        {
            // Find the maximum length of the string representation of any element in the board
            int maxLength = 0;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    int length = board[i, j].ToString().Length;
                    if (length > maxLength)
                    {
                        maxLength = length;
                    }
                }
            }

            // Calculate the width of each square
            int squareWidth = maxLength + 2;

            // Calculate the size of the sub-squares (if any)
            int subSquareSize = (int)Math.Sqrt(rows);

            // Print the top border
            Console.Write(" ");
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < squareWidth; j++)
                {
                    Console.Write("-");
                }
                if ((i + 1) % subSquareSize == 0 && i != rows - 1)
                {
                    Console.Write("+");
                }
            }
            Console.WriteLine();

            // Print the rows
            for (int i = 0; i < rows; i++)
            {
                // Print the left border
                Console.Write("|");

                // Print the elements in the row
                for (int j = 0; j < cols; j++)
                {
                    string element = board[i, j].ToString();
                    int padding = (squareWidth - element.Length) / 2;
                    for (int k = 0; k < padding; k++)
                    {
                        Console.Write(" ");
                    }
                    Console.Write(element);
                    for (int k = 0; k < padding; k++)
                    {
                        Console.Write(" ");
                    }
                    if ((j + 1) % subSquareSize == 0 && j != cols - 1)
                    {
                        Console.Write("|");
                    }
                }

                // Print the right border
                Console.WriteLine();

                // Print the bottom border for the row
                if ((i + 1) % subSquareSize == 0 && i != rows - 1)
                {
                    Console.Write(" ");
                    for (int j = 0; j < cols; j++)
                    {
                        for (int k = 0; k < squareWidth; k++)
                        {
                            Console.Write("-");
                        }
                        if ((j + 1) % subSquareSize == 0 && j != cols - 1)
                        {
                            Console.Write("+");
                        }
                    }
                    Console.WriteLine();
                }
            }

            // Print the bottom border
            Console.Write(" ");
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < squareWidth; j++)
                {
                    Console.Write("-");
                }
                if ((i + 1) % subSquareSize == 0 && i != rows - 1)
                {
                    Console.Write("+");
                }
            }
            Console.WriteLine();
        }

    }
}
