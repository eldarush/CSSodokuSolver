using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SodukoSolver.CustomExceptions;
using static SodukoSolver.HelperFunctions;

namespace SodukoSolver
{
    public static class ValidatingFunctions
    {
        // the current board that is being validated
        public static Cell[,] Vboard;

        // the allowed values for the board
        private static char[] allowedValues;

        // TODO add the validation functions here

        // main validation function that will call all the other validation functions
        public static bool Validate(int size, string boardString)
        {

            // check if the board string is the correct length
            if (!checkLength(boardString))
            {
                // throw new size exception
                throw new SizeException(boardString.Length);
            }

            // get allwoed values for the board with given length
            allowedValues = GetAllowedChars(size);

            // check if the board string contains only numbers
            if (!ValidateBoardString(boardString))
            {
                throw new InvalidCharacterException();
            }

            // copy the board string to the board
            Vboard = CreateBoard(size, boardString);

            // check if the board is valid, if not throw an exception
            return ValidateBoard(Vboard,size);
        }

        // function that checks that the board string is the same size as the
        // board size squard
        private static bool checkLength(string boardString)
        {
            // if the size is negrive return false
            if (boardString.Length <1) return false;

            // check the the square root of the board string length is an integer
            return Math.Sqrt(boardString.Length) % 1 == 0;
        }

        // function that gets all the allowed numbers for the board
        // with given size
        public static char[] GetAllowedChars(int size)
        {
            // create an array of allowed chars
            char[] allowed = new char[size+1];

            // add the allowed chars to the array
            for (int i = 0; i <= size; i++)
            {
                allowed[i] = (char)(i + '0');
            }
            // return the array of allowed chars
            return allowed;
            
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

            // check for every char in the string that it is a number
            for (int i = 0; i < boardString.Length; i++)
            {
                if (!allowedValues.Contains(boardString[i])) {
                    return false;
                }
            }
            // if the board string is valid
            // return true
            return true;

        }


        // this function will validate the board
        // it will return true if the board is valid
        // and false if the board is invalid
        private static bool ValidateBoard(Cell[,] board, int size)
        {
            // if the board is null
            if (board == null)
            {
                throw new NullBoardException();
            }

            // go over the valid board and check if it is valid
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    // if the current cell is not valid
                    if (!ValidateCell(board, i, j, size, board[i,j].Value))
                    {
                        throw new BoardCellsNotValidException(i, j);
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
        public static bool ValidateCell(Cell[,] board, int Crow,
            int Ccol, int size, int value)
        {
            // if the value is 0 update the possible candidates in the cell and then return true
            if (value == 0)
            {
                board[Crow, Ccol].UpdatePossibleCandidates(board, size, Ccol, Crow);
                return true;
            }

            // go over the current row an col and check and check if the
            // value appears in the current row or col
            for (int i = 0; i < size; i++)
            {
                // if the value appears in the current row or col return false
                if ((i != Ccol && board[Crow, i].Value == value)
                    || (i!= Crow && board[i,Ccol].Value == value))
                    return false;
            }

            // go over the sub square and check if the cell value is not repeated

            // save the values of the rows and cols of the sub squares
            int srow = (int)Math.Sqrt(size);
            int scol = (int)Math.Sqrt(size);

            // find the starting col and row of the sub square in which the cell
            // is located
            int srowStart = (Crow / srow) * srow;
            int scolStart = (Ccol / scol) * scol;

            // go over the sub square and check if the cell value is not repeated
            for (int i = srowStart; i < srowStart + srow; i++)
            {
                for (int j = scolStart; j < scolStart + scol; j++)
                {
                    // if the current cell is not the current cell
                    // and the value is the same as the value of the cell
                    if (i != Crow && j != Ccol && board[i,j].Value ==  value)
                    {
                        // return false because the cell cant be in the same cube
                        return false;
                    }
                }
            }

            // if the cell passed all its validations then return true
            return true;
        }

    }
}
