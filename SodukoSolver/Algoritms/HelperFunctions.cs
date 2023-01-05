using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static SodukoSolver.Exceptions.CustomExceptions;
using static SodukoSolver.Algoritms.ValidatingFunctions;
using System.Diagnostics.Metrics;

namespace SodukoSolver.Algoritms
{
    public static class HelperFunctions
    {
        /// <summary>
        /// Function that gets a Board string and returns a Board of cells
        /// </summary>
        /// <param name="size"> the Size of the Board</param>
        /// <param name="boardString">the string that represents the Board</param>
        /// <returns>the Board of cells</returns>
        public static int[,] CreateBoard(int size, string boardString, out int[] rowValues, out int[] colValues, out int[] blockValues, out int[] HelperMask)
        {
            // create a new Board
            int[,] board = new int[size, size];

            // copy the Board string to the Board
            CopyBoardStringToBoard(size, boardString, board);

            // create the row, col, and block values
            rowValues = new int[size];
            colValues = new int[size];
            blockValues = new int[size];
            // create the mask
            HelperMask = new int[size];

            // initialize the mask 
            for (int index = 0; index < size; index++)
            {
                // Board values at each index will be 2 to the power of the index
                // HelperMask[0] = 1 << 0 = 1, HelperMask[1] = 1 << 1 = 2, HelperMask[2] = 1 << 2 = 4 and so on...
                HelperMask[index] = 1 << index;
            }

            // initialize the row, col, and block values to be all 0s
            for (int index = 0; index < size; index++)
            {
                rowValues[index] = 0;
                colValues[index] = 0;
                blockValues[index] = 0;
            }

            // return the Board
            return board;
        }

        /// <summary>
        /// Function that copies the Board string to the Board
        /// </summary>
        /// <param name="size"> the Size of the Board</param>
        /// <param name="boardString">the string that represents the Board</param>
        /// <param name="board">the Board to copy the string to</param>
        private static void CopyBoardStringToBoard(int size, string boardString, int[,] board)
        {
            // create a counter for the Board string
            int counter = 0;

            // loop through the rows
            for (int i = 0; i < size; i++)
            {
                // loop through the columns
                for (int j = 0; j < size; j++)
                {
                    // copy the Board string to the Board
                    board[i, j] = Convert.ToInt32(boardString[counter]) - '0';
                    // increment the counter
                    counter++;
                }
            }
        }

        

        /// <summary>
        /// Function that chekcs if the Board is valid
        /// </summary>
        /// <param name="size">the Size of the Board</param>
        /// <param name="boardString">the string that represents the Board</param>
        /// <returns>if the Board is valid or not</returns>
        public static bool IsTheBoardValid(int size, string boardString)
        {
            bool valid = false;
            try
            {
                valid = Validate(size, boardString);
            }
            // catch the custom exceptions, print their error messages and exit
            catch (SizeException se)
            {
                Console.WriteLine(se.Message);
                Environment.Exit(0);
            }
            catch (InvalidCharacterException ice)
            {
                Console.WriteLine(ice.Message);
                Environment.Exit(0);
            }
            catch (BoardCellsNotValidException bcne)
            {
                Console.WriteLine(bcne.Message);
                Environment.Exit(0);
            }
            catch (NullBoardException nbe)
            {
                Console.WriteLine(nbe.Message);
                Environment.Exit(0);
            }
            return valid;
        }

        /// <summary>
        /// function that prints the ascci art in the console
        /// </summary>
        public static void PrintAsciiArt()
        {
            // the most beautiful art known to mankind
            var arr = new[] {
            @"┌────────────────────────────────────────────────────────────────────────────────────────────────────┐",
            @"│ ╔═══╗────╔╗──╔╗───────╔═══╗──╔╗──────────╔══╗───────╔═══╦╗──╔╗───────╔═══╗──╔╗──────╔╗─────╔╗───── │",
            @"│ ║╔═╗║────║║──║║───────║╔═╗║──║║──────────║╔╗║───────║╔══╣║──║║───────║╔═╗║──║║──────║║─────║║───── │",
            @"│ ║╚══╦══╦═╝╠╗╔╣║╔╦╗╔╗──║╚══╦══╣╠╗╔╦══╦═╗──║╚╝╚╦╗─╔╗──║╚══╣║╔═╝╠══╦═╗──║║─║╠══╣║╔══╦═╗║╚═╦══╦╣║╔╗─╔╗ │",
            @"│ ╚══╗║╔╗║╔╗║║║║╚╝╣║║║──╚══╗║╔╗║║╚╝║║═╣╔╝──║╔═╗║║─║║──║╔══╣║║╔╗║╔╗║╔╝──║╚═╝║══╣║║╔╗║╔╗╣╔╗║║═╬╣║║║─║║ │",
            @"│ ║╚═╝║╚╝║╚╝║╚╝║╔╗╣╚╝║──║╚═╝║╚╝║╚╗╔╣║═╣║───║╚═╝║╚═╝║──║╚══╣╚╣╚╝║╔╗║║───║╔═╗╠══║╚╣╔╗║║║║╚╝║║═╣║╚╣╚═╝║ │",
            @"│ ╚═══╩══╩══╩══╩╝╚╩══╝──╚═══╩══╩═╩╝╚══╩╝───╚═══╩═╗╔╝──╚═══╩═╩══╩╝╚╩╝───╚╝─╚╩══╩═╩╝╚╩╝╚╩══╩══╩╩═╩═╗╔╝ │",
            @"│ ─────────────────────────────────────────────╔═╝║────────────────────────────────────────────╔═╝║─ │",
            @"│ ─────────────────────────────────────────────╚══╝────────────────────────────────────────────╚══╝─ │",
            @"└────────────────────────────────────────────────────────────────────────────────────────────────────┘"
            };
            Console.WriteLine("\n");
            foreach (string line in arr)
                Console.WriteLine(line);
            Console.WriteLine("\n");
            // set the console title
            Console.Title = "Sudoku Solver by @Eldar Aslanbeily";
        }

        /// <summary>
        /// function that print the welcome message that greets the user
        /// </summary>
        public static void PrintWelcomeMessage()
        {
            // function that will ask the user for input and will perform the actions
            Console.WriteLine("Welcome to the Soduko Solver! \n" +
                "This is a program written in c# by @Eldar Aslanbeily \n" +
                "For more information about the program, please visit: \n" +
                "https://github.com/eldarush/CSSodokuSolver.git \n" +
                "This is a program that will solve any soduko Board \n");
        }

        /// <summary>
        /// function that converts a boardstring to a Board of ints
        /// </summary>
        /// <param name="boardstring">the string that represents the Board</param>
        /// <param name="array">the 2d array where the Board will be stored</param>
        /// <param name="size">the Size of the Board</param>
        /// <returns>the 2d array of ints</returns>
        public static int[,] ConvertTo2DArray(string boardstring, int[,] array, int size)
        {
            if (array is null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            int[,] newBoard = new int[size, size];
            int counter = 0;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    newBoard[i, j] = boardstring[counter] - '0';
                    counter++;
                }
            }
            return newBoard;
        }

        /// <summary>
        /// function that converts Board of ints to string
        /// </summary>
        /// <param name="array">the 2d array where the Board will be stored</param>
        /// <param name="size">the Size of the Board</param>
        /// <returns>the string that represents the Board</returns>
        public static string ConvertToString(int[,] array, int size)
        {
            string boardstring = "";
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    boardstring += (char)(array[i, j] + '0');
                }
            }
            return boardstring;
        }
    
        /// <summary>
        /// function that prints the board 
        /// </summary>
        /// <param name="board">the board</param>
        /// <param name="size">the size of the board</param>
        public static void PrintBoard(int[,] board, int size)
        {
            {
                // Find the maximum length of the string representation of any element in the Board
                int maxLength = 0;
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
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

                // Calculate the Size of the sub-squares (if any)
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
                        // if the Size more then 9, print the value with a 0 in front
                        string element;
                        if (size >= 10)
                        {
                            element = board[i, j].ToString().PadLeft(2, '0');  // add leading zero for single digit values
                        }
                        else
                        {
                            element = board[i, j].ToString();
                        }
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
        }
    }
}