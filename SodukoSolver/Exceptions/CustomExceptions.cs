using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodukoSolver.Exceptions
{
    /// <summary>
    /// thois the class the will hold all the custom exceptions
    /// </summary>
    public class CustomExceptions
    {
        /// <summary>
        /// Size exception that gets a Size as its value and dispalys it in the message
        /// </summary>
        public class SizeException : Exception
        {
            /// <summary>
            ///  constructor that takes the size as a parameter
            /// </summary>
            /// <param name="size">the size</param>
            public SizeException(int size)
                : base("The Board string cannot be used to create a Board with the given dimensions, \n" +
                    $"The current Board string's Size is {size} and the aquare root of {size} is {Math.Sqrt(size)}, \n" +
                      $"and for a borad to be valid, the square root of the total number of chars has to be a square number.")
            {
            }
        }

        /// <summary>
        /// invalid character exception that just prints the message
        /// </summary>
        public class InvalidCharacterException : Exception
        {
            /// <summary>
            /// constructor that prints out the message
            /// </summary>
            public InvalidCharacterException()
                : base("The Board string contains invalid characters")
            {
            }
        }


        /// <summary>
        /// exception if the Board's cells are not valid
        /// </summary>
        public class BoardCellsNotValidException : Exception
        {
            /// <summary>
            ///  constructor that taked the row and the col and prints out the message
            /// </summary>
            /// <param name="row">the row</param>
            /// <param name="col">the col</param>
            public BoardCellsNotValidException(int row, int col)
                : base($"Cell ({row},{col}) is invalid")
            {
            }
        }

        /// <summary>
        ///exception if the Board is null
        /// </summary>
        public class NullBoardException : Exception
        {
            /// <summary>
            /// constructor that just prints out of the SudokuBoard is null
            /// </summary>
            public NullBoardException()
                : base("The Board is null")
            {
            }
        }
    }
}
