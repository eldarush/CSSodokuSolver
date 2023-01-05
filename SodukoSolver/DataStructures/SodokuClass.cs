using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// import both classes whose functions will be used
using static SodukoSolver.Algoritms.ValidatingFunctions;
using static SodukoSolver.Algoritms.SolvingFunctions;
using static SodukoSolver.Algoritms.HelperFunctions;
using static SodukoSolver.Exceptions.CustomExceptions;
using SodukoSolver.Interfaces;
using SodukoSolver.Algoritms;
#pragma warning disable CS8601 // Possible null reference assignment.
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace SodukoSolver
{
    // this is a class for the soduko Board
    // that will contain the Board, its properties, and methods
    // to solve the soduko Board
    class SodokuBoard
    {
        // Board dimentions
        public int Size { get; set; }

        // the string that will be used to create the Board
        public string boardString { get; set; }

        // the Board itself
        public int[,] Board { get; set; }

        // the bit mask for values in row
        public int[] RowValues { get; set; }

        // the bit mask for values in column
        public int[] ColValues { get; set; }

        // the bit mask for values in block
        public int[] BlockValues { get; set; }

        // the helper mask
        public int[] HelperMask { get; set; }


        // Constructor that will the Board string from the console
        // and then creates the Board
        public SodokuBoard()
        {
            // create new console reader
            _ = new ConsoleReader();

            // ask the user for the Board string
            Console.WriteLine("Please enter the Board string:");
            boardString = Console.ReadLine();
            // while the Board string is null or white space, ask the user to enter Board again
            while (String.IsNullOrWhiteSpace(boardString))
            {
                Console.WriteLine("The entered Board string is empty, please enter a valid string:");
                boardString = Console.ReadLine();
            }

            // Size is the square root of the length of the Board string
            Size = (int)Math.Sqrt(boardString.Length);

            if (IsTheBoardValid(Size, boardString))
            {
                // copy the Board that the testing was done on
                // because it is valid
                Board = Vboard;

                // copy the bit masks and the helper mask
                RowValues = VRowValues;
                ColValues = VColValues;
                BlockValues = VBlockValues;
                HelperMask = VHelperMask;

            }
            // if the Board is not valid, an exception will be thrown 
        }

        // Constructor that will take the Board string from a given file path
        // and will create the Board
        public SodokuBoard(string path)
        {
            // create new file reader
            IReadable reader = new FileReader(path);

            // read the Board string from the file
            boardString = reader.Read();

            // Size is the square root of the length of the Board string
            Size = (int)Math.Sqrt(boardString.Length);

            // if the given Board string passed the validation,
            // create the Board
            if (IsTheBoardValid(Size,boardString))
            {
                // copy the Board that the testing was done on
                // because it is valid
                Board = Vboard;

                // copy the bit masks and the helper mask
                RowValues = VRowValues;
                ColValues = VColValues;
                BlockValues = VBlockValues;
                HelperMask = VHelperMask;
            }
            // if the Board is not valid, an exception will be thrown 
        }
    }
}
