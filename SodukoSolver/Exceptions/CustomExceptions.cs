using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodukoSolver.Exceptions
{
    public class CustomExceptions
    {
        // Size exception that gets a Size as its value and dispalys it in the message
        public class SizeException : Exception
        {
            public SizeException(int size)
                : base("The Board string cannot be used to create a Board with the given dimensions, " +
                    $"the current Board string's Size is {size} and that's not a square number.")
            {
            }
            public SizeException()
                : base("The Board string cannot be used to create a Board with the given dimensions, " +
                    $"the current Board string's length is 4, but a 2 by 2 Board is not a thing.")
            {
            }
        }

        // invalid character exception that just prints the message
        public class InvalidCharacterException : Exception
        {
            public InvalidCharacterException()
                : base("The Board string contains invalid characters")
            {
            }
        }


        // exception if the Board's cells are not valid
        public class BoardCellsNotValidException : Exception
        {
            public BoardCellsNotValidException(int row, int col)
                : base($"Cell ({row},{col}) is invalid")
            {
            }
        }

        // exception if the Board is null
        public class NullBoardException : Exception
        {
            public NullBoardException()
                : base("The Board is null")
            {
            }
        }
    }
}
