using SodukoSolver.DataStructures;
using SodukoSolver.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SodukoSolver.Algoritms.HelperFunctions;
namespace SodukoSolver.BoardSolvers
{
    public class BoardSolver : Isolvable
    {
        // Size of the Board
        public int Size { get; set; }

        // blockSize is the Size of the block that the Board is divided into
        public int BlockSize { get; set; }

        public int[,] BoardInts { get; set; }

        // general matrix that will hold the values
        public byte[,] matrix;

        /// <summary>
        /// constructor that gets the board and the size of the board
        /// and initialzes the helper mask and the allowed values for each cell
        /// </summary>
        /// <param name="board">the board</param>
        /// <param name="size">the size</param>
        public BoardSolver(int[,] board, int size)
        {
            BoardInts = board;
            Size = size;
            BlockSize = (int)Math.Sqrt(Size);
        }

        public BoardSolver(byte[,] matrix, int size)
        {
            this.matrix = matrix;
            Size = size;
            BlockSize = (int)Math.Sqrt(Size);
        }

        /// <summary>
        /// solve function implemented from the interface,
        /// this will never be used and every class that inherits from this class
        /// will override this function and imlpement it on its own
        /// </summary>
        /// <returns>if the board is solved or not</returns>
        public virtual bool Solve()
        {
            return true;
        }

        /// <summary>
        /// solve and return board string function implemented from the interface,
        /// this will never be used and every class that inherits from this class
        /// will override this function and imlpement it on its own
        /// </summary>
        /// <returns>the board of the solved board</returns>
        public virtual string GetSolutionString()
        {
            return "";
        }
    }

}