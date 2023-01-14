using SodukoSolver.DataStructures;
using SodukoSolver.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SodukoSolver.BoardSolvers
{
    /// <summary>
    /// this is an abstract class for every board solver that will implement a dif
    /// </summary>
    public abstract class BoardSolver : Isolvable
    {
        // Size of the Board
        public int Size { get; set; }

        // blockSize is the Size of the block that the Board is divided into
        public int BlockSize { get; set; }

        // the SudokuBoard of ints that will contain the result
        public int[,] BoardInts { get; set; }

        // general Matrix that will hold the values, used for the dancing links algorithm
        public byte[,] Matrix { get; set; }

        /// <summary>
        /// constructor that gets the SudokuBoard and the size of the SudokuBoard
        /// and creates new general SudokuBoard solver
        /// </summary>
        /// <param name="board">the SudokuBoard</param>
        /// <param name="size">the size</param>
        public BoardSolver(int[,] board, int size)
        {
            // initialize the SudokuBoard to be the passed SudokuBoard
            BoardInts = board;
            // set the size and calcualte the block size
            Size = size;
            BlockSize = (int)Math.Sqrt(Size);
        }

        /// <summary>
        /// constructor that gets a matrix of bytes and a size of the SudokuBoard
        /// and creates a new general SudokuBoard solver
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="size"></param>
        public BoardSolver(byte[,] matrix, int size)
        {
            // initialize the matrix to be the passed matrix
            Matrix = matrix;
            // set the size and calcualte the block size
            Size = size;
            BlockSize = (int)Math.Sqrt(Size);
        }

        /// <summary>
        /// solve function implemented from the interface,
        /// this will never be used and every class that inherits from this class
        /// will override this function and imlpement it on its own
        /// </summary>
        /// <returns>if the SudokuBoard is solved or not</returns>
        public virtual bool Solve()
        {
            return true;
        }

        /// <summary>
        /// solve and return SudokuBoard string function implemented from the interface,
        /// this will never be used and every class that inherits from this class
        /// will override this function and imlpement it on its own
        /// </summary>
        /// <returns>the SudokuBoard of the solved SudokuBoard</returns>
        public virtual string GetSolutionString()
        {
            return "";
        }
    }

}