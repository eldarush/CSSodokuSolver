using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodukoSolver.Exceptions
{
    public class CustomExceptions
    {
        // size exception that gets a size as its value and dispalys it in the message
        public class SizeException : Exception
        {
            public SizeException(int size)
                : base("The board string cannot be used to create a board with the given dimensions, " +
                    $"the current board string's size is {size} and that's not a square number.")
            {
            }
            public SizeException()
                : base("The board string cannot be used to create a board with the given dimensions, " +
                    $"the current board string's length is 4, but a 2 by 2 board is not a thing.")
            {
            }
        }

        // invalid character exception that just prints the message
        public class InvalidCharacterException : Exception
        {
            public InvalidCharacterException()
                : base("The board string contains invalid characters")
            {
            }
        }


        // exception if the board's cells are not valid
        public class BoardCellsNotValidException : Exception
        {
            public BoardCellsNotValidException(int row, int col)
                : base($"Cell ({row},{col}) is invalid")
            {
            }
        }

        // exception if the board is null
        public class NullBoardException : Exception
        {
            public NullBoardException()
                : base("The board is null")
            {
            }
        }
    }
}
