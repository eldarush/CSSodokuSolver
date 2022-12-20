using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SodukoSolver.HelperFunctions;

namespace SodukoSolver
{
    public static class ValidatingFunctions
    {
        // the current board that is being validated
        private static int[,] Vboard;

        // TODO add the validation functions here

        // main validation function that will call all the other validation functions
        public static bool Validate(int size, string boardString)
        {
            // check if the board string is the correct length
            if (boardString.Length != size * size)
            {
                Console.WriteLine("The board string is not the correct length");
                return false;
            }

            // check if the board string contains only numbers
            if (!ValidateBoardString(boardString))
            {
                Console.WriteLine("The board string contains invalid characters");
                return false;
            }

            // check if the board dimensions are valid
            if (!ValidateBoardDimensions(size))
            {
                return false;
            }
            
            // copy the board string to the board
            Vboard = CreateBoard(size, boardString);

            // check if the board is valid
            if (!ValidateBoard(Vboard, size))
            {
                return false;
            }

            // if all the validations passed, return true
            return true;
        }

        // this function will validate the board string
        // it will return true if the board string is valid
        // and false if the board string is invalid
        private static bool ValidateBoardString(string boardString)
        {
            // if the board string is null or empty
            if (string.IsNullOrEmpty(boardString))
            {
                // return false
                return false;
            }

            // if the board string is not null or empty
            else
            {
                // check for every char in the string that it is a number
                foreach (char c in boardString)
                {
                    // if the char is not a number
                    if (!char.IsNumber(c))
                    {
                        // return false
                        return false;
                    }
                }
                // if the board string is valid
                // return true
                return true;
            }
        }

        // this function will validate the board dimensions
        // it will return true if the board dimensions are valid
        // and false if the board dimensions are invalid
        private static bool ValidateBoardDimensions(int size)
        {
            // if the board size is less than 1
            if (size < 1 )
            {
                // return false
                return false;
            }

            // if the board dimensions are not less than 1
            else
            {
                // return true
                return true;
            }
        }

        // this function will validate the board
        // it will return true if the board is valid
        // and false if the board is invalid
        private static bool ValidateBoard(int[,] board, int size)
        {
            // if the board is null
            if (board == null)
            {
                // return false
                return false;
            }

            // go over the valid board and check if it is valid
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    // if the current cell is not valid
                    if (!ValidateCell(board, i, j, size))
                    {
                        // return false
                        return false;
                    }
                }
            }
        // if the board is valid
        // return true
        return true;
        }

        // this function will validate a cell
        // it will return true if the cell is valid
        // and false if the cell is invalid
        private static bool ValidateCell(int[,] board, int Crow, int Ccol, int size)
        {
            // if the cell is not valid
            // TODO: change 0 and 9 to min and max possible given value
            if (board[Crow, Ccol] < 0 || board[Crow, Ccol] > 9)
            {
                // return false
                return false;
            }

            // save the current cell value
            int cellValue = board[Crow, Ccol];

            // go over the row and check if the cell value is not repeated
            for (int i = 0; i < size; i++)
            {
                // if the current cell is not the current cell
                if (i != Crow)
                {
                    // if the current cell value is equal to the cell value
                    if (board[i, Ccol] == cellValue)
                    {
                        // return false
                        return false;
                    }
                }
            }
            
            // go over the column and check if the cell value is not repeated
            for (int i = 0; i < size; i++)
            {
                // if the current cell is not the current cell
                if (i != Ccol)
                {
                    // if the current cell value is equal to the cell value
                    if (board[Crow, i] == cellValue)
                    {
                        // return false
                        return false;
                    }
                }
            }

            // go over the sub square and check if the cell value is not repeated

            // check if size is a sqaure number
            if (!(Math.Sqrt(size) % 1 != 0 ))
            {
                // if one of the rows or cols are not square numbers,
                // that means that the board is not a square board
                // and we cant check the sub square
                return true;
            }
            // else if rows and cols are square numbers
            // that means that the board is a square board and we can check the sub square
            else
            {
                // save the values of the rows and cols of the sub squares
                int srow = (int)Math.Sqrt(size);
                int scol = (int)Math.Sqrt(size);

                // go over the sub squares and check if the cell value appears in the sub square

                // we find out in which sub square the cell is located in by dividing
                // the row and col by the sub square rows and cols
                // and then we multiply the result by the sub square rows and cols
                // this will give us the starting row and col of the sub square
                int srowStart = (Crow / srow) * srow;
                int scolStart = (Ccol / scol) * scol;

                // go over the sub square and check if the cell value is not repeated
                for (int i = srowStart; i < srowStart + srow; i++)
                {
                    for (int j = scolStart; j < scolStart + scol; j++)
                    {
                        // if the current cell is not the current cell
                        if (i != Crow && j != Ccol)
                        {
                            // if the current cell value is equal to the cell value
                            if (board[i, j] == cellValue)
                            {
                                // return false
                                return false;
                            }
                        }
                    }
                }
            }

            // if the cell passed all its validations then return true
            return true;
        }

    }
}
