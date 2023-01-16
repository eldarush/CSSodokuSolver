using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodukoSolver.Algoritms
{
    /// <summary>
    /// this is a module that will be responsible to have functions
    /// to convert board from diffrent formats
    /// </summary>
    public static class BoardConvertors
    {
        /// <summary>
        /// function that returns a copy of the given Board
        /// </summary>
        /// <param name="board">the Board</param>
        /// <returns>the copied Board</returns>
        public static int[,] GetBoardIntsCopy(int[,] board, int size)
        {
            // craete copy of the Board
            int[,] boardCopy = new int[size, size];
            // go over the current Board and copy each value to the corresponding value in the new Board
            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    boardCopy[row, col] = board[row, col];
                }
            }
            // return the copied Board
            return boardCopy;
        }

        /// <summary>
        /// Function that copies the Board string to the Board
        /// </summary>
        /// <param name="size"> the Size of the Board</param>
        /// <param name="boardString">the string that represents the Board</param>
        /// <param name="board">the Board to copy the string to</param>
        private static void CopyBoardStringToBoard(int size, string boardString, int[,] board)
        {
            // create a location for the Board string
            int location = 0;

            // loop through the rows
            for (int row = 0; row < size; row++)
            {
                // loop through the columns
                for (int col = 0; col < size; col++)
                {
                    // copy the Board string to the Board
                    board[row, col] = Convert.ToInt32(boardString[location]) - '0';
                    // increment the location
                    location++;
                }
            }
        }

        /// <summary>
        /// Function that gets a Board string and returns a Board of cells
        /// </summary>
        /// <param name="size"> the Size of the Board</param>
        /// <param name="boardString">the string that represents the Board</param>
        /// <returns>the Board of cells</returns>
        public static int[,] CreateBoard(int size, string boardString, out int[] rowValues, out int[] colValues, out int[] blockValues, out int[] helperMask)
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
            helperMask = new int[size];

            // initialize the mask 
            for (int index = 0; index < size; index++)
            {
                // Board values at each index will be 2 to the power of the index
                // helperMask[0] = 1 << 0 = 1, helperMask[1] = 1 << 1 = 2, helperMask[2] = 1 << 2 = 4 and so on...
                helperMask[index] = 1 << index;
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
        /// function that converts Board of ints to string
        /// </summary>
        /// <param name="array">the 2d array where the Board will be stored</param>
        /// <param name="size">the Size of the Board</param>
        /// <returns>the string that represents the Board</returns>
        public static string ConvertToString(int[,] array, int size)
        {
            // the string that will hold the SudokuBoard representation
            string boardstring = "";
            // go over the SudokuBoard
            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    // add the current value in string format
                    boardstring += (char)(array[row, col] + '0');
                }
            }
            // return the string
            return boardstring;
        }

        /// <summary>
        /// function that converts a int SudokuBoard to a byte Matrix
        /// </summary>
        /// <param name="board">the SudokuBoard of ints</param>
        /// <param name="size">size of each dimention</param>
        /// <returns>tje converted Matrix</returns>
        public static byte[,] IntBoardToByteMatrix(int[,] board, int size)
        {
            // create new Matrix of bytes of the same size as the int Matrix
            byte[,] matrix = new byte[size, size];
            // go over the SudokuBoard
            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    // convert to byte and insert into Matrix
                    matrix[row, col] = (byte)board[row, col];
                }
            }
            // return the resulted Matrix
            return matrix;
        }
    }
}
