using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SodukoSolver.Exceptions.CustomExceptions;
using static SodukoSolver.Algoritms.HelperFunctions;
#pragma warning disable CS8604 // Possible null reference argument.
#pragma warning disable CA2211 // Non-constant fields should not be visible

namespace SodukoSolver.Algoritms
{
    public static class ValidatingFunctions
    {
        // the current Board that is being validated
        public static int[,]? Vboard;

        // the allowed values for the Board
        private static char[]? allowedValues;

        // the bit mask for avalues in row
        public static int[]? VRowValues;

        // the bit mask for avalues in column
        public static int[]? VColValues;

        // the bit mask for avalues in block
        public static int[]? VBlockValues;

        // the helper mask
        public static int[]? VHelperMask;

        // TODO add the validation functions here

        // main validation function that will call all the other validation functions
        public static bool Validate(int size, string boardString)
        {

            // check if the Board string is the correct length
            if (!CheckLength(boardString))
            {
                // throw new Size exception
                throw new SizeException(boardString.Length);
            }

            // get allwoed values for the Board with given length
            allowedValues = GetAllowedChars(size);

            // check if the Board string contains only numbers
            if (!ValidateBoardString(boardString))
            {
                throw new InvalidCharacterException();
            }

            // copy the Board string to the Board
            Vboard = CreateBoard(size, boardString, out VRowValues, out VColValues, out VBlockValues,out VHelperMask);

            // check if the Board is valid, if not throw an exception
            return ValidateBoard(Vboard, size);
        }

        // function that checks that the Board string is the same Size as the
        // Board Size squard
        private static bool CheckLength(string boardString)
        {
            // if the Size is negrive return false
            if (boardString.Length < 1) return false;

            // if the boardstring length is 4, then retues false because a 2 by 2 Board is not valid
            if (boardString.Length == 4) throw new SizeException();

            // check the the square root of the Board string length is an integer
            return Math.Sqrt(boardString.Length) % 1 == 0;
        }

        // function that gets all the allowed numbers for the Board
        // with given Size
        public static char[] GetAllowedChars(int size)
        {
            // create an array of allowed chars
            char[] allowed = new char[size + 1];

            // add the allowed chars to the array
            for (int i = 0; i <= size; i++)
            {
                allowed[i] = (char)(i + '0');
            }
            // return the array of allowed chars
            return allowed;

        }

        // this function will validate the Board string
        // it will return true if the Board string is valid
        // and false if the Board string is invalid
        private static bool ValidateBoardString(string boardString)
        {
            // if the Board string is null or empty
            if (string.IsNullOrEmpty(boardString))
            {
                // return false
                return false;
            }

            // check for every char in the string that it is a number
            for (int i = 0; i < boardString.Length; i++)
            {

                if (!allowedValues.Contains(boardString[i]))
                {
                    return false;
                }
            }
            // if the Board string is valid
            // return true
            return true;

        }

        private static void UpdateCandidateValues(int Row, int Col, int Value, int BlockSize)
        {
            //Use the bitwise OR operator (|) to add the mask at index value - 1 in the masks array to the element at index row in the RowValues array.
            //This has the effect of setting the bit corresponding to value in the binary representation of the element to 1,
            //indicating that the value is valid for the row.
            VRowValues[Row] |= VHelperMask[Value - 1];
            VColValues[Col] |= VHelperMask[Value - 1];
            int SquareLocation = (Row / BlockSize) * BlockSize + Col / BlockSize;
            VBlockValues[SquareLocation] |= VHelperMask[Value - 1];
        }

        private static bool isValidBits(int Row, int Col, int Value, int BlockSize)
        {
            int SquareLocation = (Row / BlockSize) * BlockSize + Col / BlockSize;
            // Use the bitwise AND operator (&) to check if the value is valid for the row, col and block
            // This is done by performing a bitwise AND operation between the element at index row in the RowValues array
            // and the mask at index value in the masks array.
            // If the result is 0, it means that the value is valid for the row
            // (i.e., the bit corresponding to value is not set in the binary representation of the element).
            return (VRowValues[Row] & VHelperMask[Value]) == 0
                && (VColValues[Col] & VHelperMask[Value]) == 0
                && (VBlockValues[SquareLocation] & VHelperMask[Value]) == 0;

        }

        // this function will validate the Board
        // it will return true if the Board is valid
        // and false if the Board is invalid
        private static bool ValidateBoard(int[,] board, int size)
        {
            // if the Board is null
            if (board == null)
            {
                throw new NullBoardException();
            }

            // calculate the block Size
            int BlockSize = (int)Math.Sqrt(size);

            // go over the valid Board and check if it is valid
            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    // if the value is 0, just continue
                    if (board[row, col] == 0) continue;

                    // try and insert the value in the Board, if it is not valid, throw exception
                    if (!isValidBits(row, col, board[row, col]-1, BlockSize))
                    {
                        throw new BoardCellsNotValidException(row, col);
                    }
                    // if the value is valid, update the candidate values
                    UpdateCandidateValues(row, col, board[row, col], BlockSize);
                }
            }
            // if the Board is valid
            // return true
            return true;
        }
    }
}
