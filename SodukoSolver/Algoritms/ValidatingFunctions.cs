using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SodukoSolver.Exceptions.CustomExceptions;
using static SodukoSolver.Algoritms.BoardConvertors;

namespace SodukoSolver.Algoritms
{
    /// <summary>
    /// class that holds all the validating functions for the SudokuBoard
    /// </summary>
    public static class ValidatingFunctions
    {
        // the current Board that is being validated
        public static int[,] Vboard;

        // the allowed values for the Board
        private static char[] _allowedValues;

        // the bit mask for values in row
        public static int[] VRowValues;

        // the bit mask for values in column
        public static int[] VColValues;

        // the bit mask for values in block
        public static int[] VBlockValues;

        // the helper mask
        public static int[] VHelperMask;

        /// <summary>
        /// main validation function that will call all the other validation functions
        /// </summary>
        /// <param name="size">size of the SudokuBoard</param>
        /// <param name="boardString">string that represents SudokuBoard</param>
        /// <returns>if SudokuBoard valid or not</returns>
        public static bool Validate(int size, string boardString)
        {

            // check if the Board string is the correct length
            if (!CheckLength(boardString))
            {
                // throw new Size exception
                throw new SizeException(boardString.Length);
            }

            // get allwoed values for the Board with given length
            _allowedValues = GetAllowedChars(size);

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

        /// <summary>
        /// function that checks that the Board string is the same Size as the
        /// Board Size squard
        /// </summary>
        /// <param name="boardString">string that represents SudokuBoard</param>
        /// <returns>if the size is valid or not</returns>
        private static bool CheckLength(string boardString)
        {
            // if the Size is negrive return false
            if (boardString.Length < 1) return false;

            // check the the square root of the Board string length is an integer
            return Math.Sqrt(Math.Sqrt(boardString.Length)) % 1 == 0;
        }

        /// <summary>
        /// function that gets all the allowed numbers for the Board
        /// with given Size
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        private static char[] GetAllowedChars(int size)
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

        /// <summary>
        /// this function will validate the Board string
        /// it will return true if the Board string is valid
        /// and false if the Board string is invalid
        /// </summary>
        /// <param name="boardString">the SudokuBoard string</param>
        /// <returns>if SudokuBoard string is valid or not</returns>
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

                if (!_allowedValues.Contains(boardString[i]))
                {
                    return false;
                }
            }
            // if the Board string is valid
            // return true
            return true;

        }

        /// <summary>
        /// update the valid candidates for row, col and block in the validating candidates's bit masks
        /// </summary>
        /// <param name="row">row of value</param>
        /// <param name="col">col of value</param>
        /// <param name="value">the value itself</param>
        /// <param name="blockSize">the size of a block</param>
        private static void UpdateCandidateValues(int row, int col, int value, int blockSize)
        {
            //Use the bitwise OR operator (|) to add the mask at index value - 1 in the masks array to the element at index row in the RowValues array.
            //This has the effect of setting the bit corresponding to value in the binary representation of the element to 1,
            //indicating that the value is valid for the row.
            VRowValues[row] |= VHelperMask[value - 1];
            VColValues[col] |= VHelperMask[value - 1];
            int squareLocation = (row / blockSize) * blockSize + col / blockSize;
            VBlockValues[squareLocation] |= VHelperMask[value - 1];
        }

        /// <summary>
        /// function that gets a location in the Board and a value and returns if it valid to put 
        /// value inside the Board in this location, this is done using the possible values held in the bitmasks 
        /// for row, col and block in the validating candidates's bit masks
        /// </summary>
        /// <param name="row">row of value</param>
        /// <param name="col">col of value</param>
        /// <param name="value">the value</param>
        /// <param name="blockSize">the size of a block</param>
        /// <returns>if the candidate is valid or not</returns>
        private static bool IsValidBits(int row, int col, int value, int blockSize)
        {
            int squareLocation = (row / blockSize) * blockSize + col / blockSize;
            // Use the bitwise AND operator (&) to check if the value is valid for the row, col and block
            // This is done by performing a bitwise AND operation between the element at index row in the RowValues array
            // and the mask at index value in the masks array.
            // If the result is 0, it means that the value is valid for the row
            // (i.e., the bit corresponding to value is not set in the binary representation of the element).
            return (VRowValues[row] & VHelperMask[value]) == 0
                && (VColValues[col] & VHelperMask[value]) == 0
                && (VBlockValues[squareLocation] & VHelperMask[value]) == 0;

        }

        /// <summary>
        /// function that checks if the Board is valid
        /// </summary>
        /// <param name="board">the SudokuBoard</param>
        /// <param name="size">size of SudokuBoard</param>
        /// <returns>if SudokuBoard valid or not</returns>
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
                    if (!IsValidBits(row, col, board[row, col]-1, BlockSize))
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

        /// <summary>
        /// Function that chekcs if the Board is valid
        /// </summary>
        /// <param name="size">the Size of the Board</param>
        /// <param name="boardString">the string that represents the Board</param>
        /// <returns>if the Board is valid or not</returns>
        public static bool IsTheBoardValid(int size, string boardString)
        {
            // to keep track if the SudokuBoard is valid or not
            bool valid = false;
            // try and validate, if any exceptions appear, catch them
            try
            {
                valid = Validate(size, boardString);
            }
            // catch the custom exceptions, print their error messages and return false;
            catch (SizeException se)
            {
                Console.WriteLine("\nERROR: " + se.Message);
                return false;
            }
            catch (InvalidCharacterException ice)
            {
                Console.WriteLine("\nERROR: " + ice.Message);
                return false;
            }
            catch (BoardCellsNotValidException bcne)
            {
                Console.WriteLine("\nERROR: " + bcne.Message);
                return false;
            }
            catch (NullBoardException nbe)
            {
                Console.WriteLine("\nERROR: " + nbe.Message);
                return false;
            }
            return valid;
        }
    }
}
