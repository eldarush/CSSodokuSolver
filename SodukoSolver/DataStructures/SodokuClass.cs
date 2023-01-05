using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// import classes whose functions will be used
using static SodukoSolver.Algoritms.ValidatingFunctions;
using static SodukoSolver.Algoritms.HelperFunctions;
using SodukoSolver.Interfaces;

namespace SodukoSolver
{
    // general class that will take care of creating new boards
    class SodokuBoard
    {
        // Board dimentions
        public int Size { get; set; }

        // the string that will be used to create the Board
        public string BoardString { get; set; }

        // the Board itself
        public int[,] Board { get; set; }

        #region console cunstructor

        /// <summary>
        /// Constructor that will the Board string from the console
        /// and then creates the Board
        /// </summary>
        public SodokuBoard()
        {
            // create new console reader
            IReadable Reader= new ConsoleReader();

            // get the boardstring using the reader
            string boardString = Reader.Read();

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
            }
            // if the Board is not valid, an exception will be thrown 
        }

        #endregion console cunstructor

        #region file cunstructor

        /// <summary>
        /// Constructor that will take the Board string from a given file path
        /// and will create the Board
        /// </summary>
        /// <param name="path"></param>
        public SodokuBoard(string path)
        {
            // create new file reader
            IReadable reader = new FileReader(path);

            // read the Board string from the file
            BoardString = reader.Read();

            // Size is the square root of the length of the Board string
            Size = (int)Math.Sqrt(BoardString.Length);

            // if the given Board string passed the validation,
            // create the Board
            if (IsTheBoardValid(Size,BoardString))
            {
                // copy the Board that the testing was done on
                // because it is valid
                Board = Vboard;
            }
            // if the Board is not valid, an exception will be thrown 
        }

        #endregion file cunstructor
    }
}
