using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodukoSolver
{
    public static class HelperFunctions
    {
        // function that gets rows, columns, and board string and creates the board
        public static Cell[,] CreateBoard(int size, string boardString)
        {
            // create a new board
            Cell[,] board = new Cell[size, size];

            // copy the board string to the board
            CopyBoardStringToBoard(size, boardString, board);

            // return the board
            return board;
        }

        // CopyBoardStringToBoard function that copies the board string to the board
        private static void CopyBoardStringToBoard(int size, string boardString, Cell[,] board)
        {
            // create a counter for the board string
            int counter = 0;

            // loop through the rows
            for (int i = 0; i < size; i++)
            {
                // loop through the columns
                for (int j = 0; j < size; j++)
                {
                    // copy the board string to the board
                    board[i, j] = new Cell(size,Convert.ToInt32(boardString[counter].ToString()));
                    // increment the counter
                    counter++;
                }
            }
        }

        //TODO: fix this function so that it prints the board correctly (currently missing the right border)
        // PrintBoard method that prints the board to the console
        public static void PrintBoard(Cell[,] board, int size)
        {
            // Find the maximum length of the string representation of any element in the board
            int maxLength = 0;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    int length = board[i, j].Value.ToString().Length;
                    if (length > maxLength)
                    {
                        maxLength = length;
                    }
                }
            }

            // Calculate the width of each square
            int squareWidth = maxLength + 2;

            // Calculate the size of the sub-squares (if any)
            int subSquareSize = (int)Math.Sqrt(size);

            // Print the top border
            Console.Write(" ");
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < squareWidth; j++)
                {
                    Console.Write("-");
                }
                if ((i + 1) % subSquareSize == 0 && i != size - 1)
                {
                    Console.Write("+");
                }
            }
            Console.WriteLine();

            // Print the rows
            for (int i = 0; i < size; i++)
            {
                // Print the left border
                Console.Write("|");
                
                // Print the elements in the row
                for (int j = 0; j < size; j++)
                {
                    string element = board[i, j].Value.ToString();
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
                    if ((j + 1) % subSquareSize == 0 && j != size - 1)
                    {
                        Console.Write("|");
                    }
                }

                // Print the right border
                Console.WriteLine("|");

                // Print the bottom border for the row
                if ((i + 1) % subSquareSize == 0 && i != size - 1)
                {
                    Console.Write(" ");
                    for (int j = 0; j < size; j++)
                    {
                        for (int k = 0; k < squareWidth; k++)
                        {
                            Console.Write("-");
                        }
                        if ((j + 1) % subSquareSize == 0 && j != size - 1)
                        {
                            Console.Write("+");
                        }
                    }
                    Console.WriteLine();
                }
            }

            // Print the bottom border
            Console.Write(" ");
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < squareWidth; j++)
                {
                    Console.Write("-");
                }
                if ((i + 1) % subSquareSize == 0 && i != size - 1)
                {
                    Console.Write("+");
                }
            }
            Console.WriteLine();
        }

        //TODO: add validation here 
        // GetBoardString method that gets the board string from the user
        public static string GetBoardString()
        {
            // create a new string builder
            StringBuilder boardString = new StringBuilder();

            // get the board string from the user
            Console.WriteLine("Please enter the board string (81 digits):");
            boardString.Append(Console.ReadLine());

            // return the board string
            return boardString.ToString();
        }
    }
}
