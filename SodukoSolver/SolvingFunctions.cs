using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SodukoSolver.ValidatingFunctions;


namespace SodukoSolver
{
    internal class SolvingFunctions
    {
        // size of the board
        private static int size;

        // blockSize is the size of the block that the board is divided into
        private static int blockSize;

        // the board
        private static Cell[,] board;

        // setter for the size
        public static void setSize(int size)
        {
            SolvingFunctions.size = size;
        }

        // setter for the square size
        public static void setBlockSize(int blockSize)
        {
            SolvingFunctions.blockSize = blockSize;
        }

        // setter for the board
        public static void setBoard(Cell[,] board)
        {
            SolvingFunctions.board = board;
        }

        // getter for the board
        public static Cell[,] getBoard()
        {
            return board;
        }

        public static bool Solve()
        {
            // get the next empty cell
            int[] nextEmptyCell = getNextEmptyCell();

            // if there is no empty cell then the board is solved
            if (nextEmptyCell == null)
            {
                return true;
            }

            // get the row and column of the next empty cell
            int row = nextEmptyCell[0];
            int col = nextEmptyCell[1];

            // update the candidates of the current cell based on the values of the cells in the same row, column, and block
            for (int i = 0; i < size; i++)
            {
                if (i != col && board[row, i].Value != 0)
                {
                    board[row, col].Candidates.Remove(board[row, i].Value);
                }
                if (i != row && board[i, col].Value != 0)
                {
                    board[row, col].Candidates.Remove(board[i, col].Value);
                }
            }
            int blockRowStart = row - row % blockSize;
            int blockColStart = col - col % blockSize;
            for (int i = blockRowStart; i < blockRowStart + blockSize; i++)
            {
                for (int j = blockColStart; j < blockColStart + blockSize; j++)
                {
                    if (i != row && j != col && board[i, j].Value != 0)
                    {
                        board[row, col].Candidates.Remove(board[i, j].Value);
                    }
                }
            }

            // for each possible candidate in the cell
            for (int i = 1; i <= size; i++)
            {
                // if the candidate is valid
                if (isValidCandidate(row, col, i))
                {
                    // set the value of the cell to the candidate
                    board[row, col].Value = i;

                    // update the candidates of the cells that are affected by this cell's value
                    updateAffectedCells(row, col);

                    // if the board is solved then return true
                    if (Solve())
                    {
                        return true;
                    }

                    // if the board is not solved then set the value of the cell to 0
                    // and restore the candidates of the affected cells to their original state
                    board[row, col].Value = 0;
                    restoreAffectedCells(row, col);
                }
            }

            // if the board is not solved then return false
            return false;
        }


        // restores the candidates of the cells that are affected by the cell at the given row and column
        // to their original state before the value of the cell at the given row and column was set
        private static void restoreAffectedCells(int row, int col)
        {
            int value = board[row, col].Value;

            // restore the candidates of the cells in the same row
            for (int i = 0; i < size; i++)
            {
                if (i != col)
                {
                    board[row, i].Candidates.Add(value);
                }
            }

            // restore the candidates of the cells in the same column
            for (int i = 0; i < size; i++)
            {
                if (i != row)
                {
                    board[i, col].Candidates.Add(value);
                }
            }

            // restore the candidates of the cells in the same block
            int blockRowStart = row - row % blockSize;
            int blockColStart = col - col % blockSize;
            for (int i = blockRowStart; i < blockRowStart + blockSize; i++)
            {
                for (int j = blockColStart; j < blockColStart + blockSize; j++)
                {
                    if (i != row && j != col)
                    {
                        board[i, j].Candidates.Add(value);
                    }
                }
            }
        }

        // updates the candidates of the cells that are affected by the cell at the given row and column
        private static void updateAffectedCells(int row, int col)
        {
            int value = board[row, col].Value;

            // update the candidates of the cells in the same row
            for (int i = 0; i < size; i++)
            {
                board[row, i].Candidates.Remove(value);
            }

            // update the candidates of the cells in the same column
            for (int i = 0; i < size; i++)
            {
                board[i, col].Candidates.Remove(value);
            }

            // update the candidates of the cells in the same block
            int blockRowStart = row - row % blockSize;
            int blockColStart = col - col % blockSize;
            for (int i = blockRowStart; i < blockRowStart + blockSize; i++)
            {
                for (int j = blockColStart; j < blockColStart + blockSize; j++)
                {
                    board[i, j].Candidates.Remove(value);
                }
            }
        }

        // returns the next empty cell on the board, or null if there are no more empty cells
        private static int[] getNextEmptyCell()
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (board[i, j].Value == 0)
                    {
                        return new int[] { i, j };
                    }
                }
            }
            return null;
        }


        // checks if the given candidate is valid for the cell at the given row and column
        private static bool isValidCandidate(int row, int col, int candidate)
        {
            // check if the candidate is already used in the same row
            for (int i = 0; i < size; i++)
            {
                if (board[row, i].Value == candidate)
                {
                    return false;
                }
            }

            // check if the candidate is already used in the same column
            for (int i = 0; i < size; i++)
            {
                if (board[i, col].Value == candidate)
                {
                    return false;
                }
            }

            // check if the candidate is already used in the same block
            int blockRowStart = row - row % blockSize;
            int blockColStart = col - col % blockSize;
            for (int i = blockRowStart; i < blockRowStart + blockSize; i++)
            {
                for (int j = blockColStart; j < blockColStart + blockSize; j++)
                {
                    if (board[i, j].Value == candidate)
                    {
                        return false;
                    }
                }
            }

            // if the candidate is not used in the same row, column, or block then it is valid
            return true;
        }


        // function that climinates all obvios cells from the board, this are the cells
        // that have only one possilbe candidate
        // this function is ran before the solving function, to make the solving process
        // faster
        public static void Eliminate()
        {
            // iterate through all cells in the board
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    // run the isSolved function on all the cells
                    board[i, j].isSolved();
                }
            }
        }

    }
}
