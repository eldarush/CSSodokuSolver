using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SodukoSolver.Algoritms.ValidatingFunctions;
using static SodukoSolver.Algoritms.HelperFunctions;
# pragma warning disable CS8602 // Dereference of a possibly null reference.

namespace SodukoSolver.Algoritms
{
    public class SolvingFunctions
    {
        // size of the board
        public int Size { get; set; }

        // blockSize is the size of the block that the board is divided into
        public int BlockSize { get; set; }

        // the board
        public Cell[,] Board { get; set; }

        // the location in the empty cell array
        public static int Location = 0;
        public static int LocationBits = 0;

        // the backward location in the empty cell array
        public static int BackwardsLocation = 0;
        public static int BackwardsLocationBits = 0;

        // empty cells array
        public static int[]? EmptyCellsArray;

        /// <summary>
        /// Constructor that gets the size and the board
        /// </summary>
        /// <param name="size">the size</param>
        /// <param name="board">the board</param>
        public SolvingFunctions(int size, Cell[,] board)
        {
            this.Size = size;
            this.Board = board;
            BlockSize = (int)Math.Sqrt(size);
        }

        /// <summary>
        ///this is the implementation of the backtracking algorithm
        /// that goes over the board and tries to solve it
        /// this function goes from left to right and top to bottom
        /// </summary>
        /// <param name="token">cancelletion token for multitasking</param>
        /// <returns>if the board is solved</returns>
        public bool Backtracking(CancellationToken token)
        {
            // if the location is out of bounds then the board is solved
            if (Location < 0 || Location >= EmptyCellsArray.Length)
            {
                return true;
            }


            // get the row and column of the current empty cell
            int row = EmptyCellsArray[Location] / Size;
            int col = EmptyCellsArray[Location] % Size;

            // Current Cells is:
            //Console.WriteLine("Current Cell is: " + row + " " + col);

            // for each possible candidate in the cell
            foreach (int i in Board[row, col].Candidates)
            {
                // if the candidate is valid
                if (IsValidCandidate(row, col, i))
                {
                    // set the value of the cell to the candidate
                    Board[row, col].Value = i;

                    // increment the location to move to the next empty cell
                    Location++;

                    // if the board is solved then return true
                    if (Backtracking(token))
                    {
                        return true;
                    }

                    // if the board is not solved then reset the value of the cell and backtrack
                    Board[row, col].Value = 0;
                    Location--;
                }
            }

            // if the board is not solved then return false
            return false;
        }


        /// <summary>
        /// this is an implementation of backtracking to solve the board
        /// that goes in reversed order - from the last empty cell to the first
        /// </summary>
        /// <param name="token">cancelletion token for multitasking</param>
        /// <returns>if the board is solved</returns>
        public bool BacktrackingR(CancellationToken token)
        {
            // if the backwards location is out of bounds then the board is solved
            if (BackwardsLocation < 0 || BackwardsLocation >= EmptyCellsArray.Length)
            {
                return true;
            }

            // get the row and column of the current empty cell
            int row = EmptyCellsArray[BackwardsLocation] / Size;
            int col = EmptyCellsArray[BackwardsLocation] % Size;

            // Current Cells is:
            //Console.WriteLine("Current Cell is: " + row + " " + col);

            // for each possible candidate in the cell
            foreach (int i in Board[row, col].Candidates)
            {
                // if the candidate is valid
                if (IsValidCandidate(row, col, i))
                {
                    // set the value of the cell to the candidate
                    Board[row, col].Value = i;

                    // decrement the backwards location to move to the previous empty cell
                    BackwardsLocation--;

                    // if the board is solved then return true
                    if (BacktrackingR(token))
                    {
                        return true;
                    }

                    // if the board is not solved then reset the value of the cell and backtrack
                    Board[row, col].Value = 0;
                    BackwardsLocation++;
                }
            }

            // if the board is not solved then return false
            return false;
        }


        // Define the Direction enumeration
        public enum Direction
        {
            Right,
            Down,
            Left,
            Up
        }

        // TODO: fix this function - is is returning true even for ussolvable boards

        /// <summary>
        /// this is an implementation of backtracking to solve the board
        /// that goes in a spiral order from the center of the board to the edges
        /// </summary>
        /// <param name="row">current row</param>
        /// <param name="col">current col</param>
        /// <param name="dir">current direction</param>
        /// <param name="visited">how many visited cells</param>
        /// <param name="token">cancelletion token for multitasking</param>
        /// <returns>if the board is solved</returns>
        public bool BacktrackingSpiral(int row, int col, Direction dir, int visited, CancellationToken token)
        {
            // Continue the spiral traversal until all cells have been visited
            while (visited < Size * Size)
            {
                // Check if the current cell is empty
                if (Board[row, col].Value == 0)
                {
                    // Check if the cell has any valid candidates
                    bool hasCandidates = false;
                    for (int k = 1; k <= Size; k++)
                    {
                        if (IsValidCandidate(row, col, k))
                        {
                            hasCandidates = true;
                            break;
                        }
                    }

                    // If the cell has no valid candidates, return false
                    if (!hasCandidates)
                    {
                        return false;
                    }

                    // Try each possible value for the cell
                    for (int k = 1; k <= Size; k++)
                    {
                        // Check if the value is valid for the cell
                        if (IsValidCandidate(row, col, k))
                        {
                            // Set the value for the cell and recursively solve the rest of the board
                            Board[row, col].Value = k;
                            if (BacktrackingSpiral(row, col, dir, visited, token))
                            {
                                return true;
                            }
                            else
                            {
                                // Backtrack and try the next value
                                Board[row, col].Value = 0;
                            }
                        }
                    }
                    // No valid value was found for the cell, so return false
                    return false;
                }
                // Update the row and column based on the current direction
                switch (dir)
                {
                    case Direction.Right:
                        col++;
                        if (col == Size || Board[row, col].Value != 0)
                        {
                            dir = Direction.Down;
                            col--;
                            row++;
                        }
                        break;
                    case Direction.Down:
                        row++;
                        if (row == Size || Board[row, col].Value != 0)
                        {
                            dir = Direction.Left;
                            row--;
                            col--;
                        }
                        break;
                    case Direction.Left:
                        col--;
                        if (col < 0 || Board[row, col].Value != 0)
                        {
                            dir = Direction.Up;
                            col++;
                            row--;
                        }
                        break;
                    case Direction.Up:
                        row--;
                        if (row < 0 || Board[row, col].Value != 0)
                        {
                            dir = Direction.Right;
                            row++;
                            col++;
                        }
                        break;
                }
                visited++;
            }
            // If the board is solved, return true
            return true;
        }

        /// <summary>
        /// Takes a partially filled-in grid and attempts
        /// to assign values to all unassigned locations in
        /// such a way to meet the requirements for
        /// Sudoku solution(non-duplication across rows,
        ///  columns, and boxes) */
        /// </summary>
        /// <param name="row">current row</param>
        /// <param name="col">current col</param>
        /// <param name="board">the board</param>
        /// <param name="submatrixDigits">submatrix of digits</param>
        /// <param name="rowDigits">digits in row</param>
        /// <param name="columnDigits">figits in colum</param>
        /// <param name="cts"></param>
        /// <returns></returns>
        public bool BackTrackingBits(int[,] board,
            int[,] submatrixDigits,
            int[] rowDigits,
            int[] columnDigits,
            CancellationToken cts)
        {
            if (LocationBits >= EmptyCellsArray.Length || LocationBits < 0)
            {
                return true;
            }
            int row = EmptyCellsArray[LocationBits] / Size;
            int col = EmptyCellsArray[LocationBits] % Size;

            for (int i = 1; i <= Size; i++)
            {
                int digit = 1 << i - 1;

                if (!IsDigitUsed(submatrixDigits, rowDigits, columnDigits, row, col, digit))
                {
                    // set digit
                    SetDigit(submatrixDigits, rowDigits, columnDigits, row, col, digit);
                    board[row, col] = i;
                    LocationBits++;

                    if (BackTrackingBits(board, submatrixDigits,
                                rowDigits, columnDigits, cts))
                    {
                        return true;
                    }
                    else
                    {
                        // unset digit
                        UnsetDigit(submatrixDigits, rowDigits, columnDigits, row, col, digit);
                        LocationBits--;
                        board[row, col] = 0;
                    }
                }
            }
            return false;
        }


        // Function checks if the given digit is used in
        // the given submatrix, row, and column
        private bool IsDigitUsed(int[,] submatrixDigits, int[] rowDigits, int[] columnDigits, int row, int col, int digit)
        {
            return (submatrixDigits[row / BlockSize, col / BlockSize] & digit) != 0
                || (rowDigits[row] & digit) != 0
                || (columnDigits[col] & digit) != 0;

        }
        // Function sets the given digit in the given
        // submatrix, row, and column
        private void SetDigit(int[,] submatrixDigits, int[] rowDigits, int[] columnDigits, int row, int col, int digit)
        {
            submatrixDigits[row / BlockSize, col / BlockSize] |= digit;
            rowDigits[row] |= digit;
            columnDigits[col] |= digit;
        }

        // Function unsets the given digit in the given
        // submatrix, row, and column
        private void UnsetDigit(int[,] submatrixDigits, int[] rowDigits, int[] columnDigits, int row, int col, int digit)
        {
            submatrixDigits[row / BlockSize, col / BlockSize] &= ~digit;
            rowDigits[row] &= ~digit;
            columnDigits[col] &= ~digit;
        }

        // Function checks if Sudoku can be
        // solved or not
        public bool SolveSudokuUsingBitwiseBacktracking(int[,] board, CancellationToken cts)
        {
            int[,] submatrixDigits = new int[BlockSize, BlockSize];
            int[] columnDigits = new int[Size];
            int[] rowDigits = new int[Size];

            for (int i = 0; i < BlockSize; i++)
                for (int j = 0; j < BlockSize; j++)
                    submatrixDigits[i, j] = 0;

            for (int i = 0; i < Size; i++)
            {
                rowDigits[i] = 0;
                columnDigits[i] = 0;
            }

            // get submatrix, row and column digits
            for (int i = 0; i < Size; i++)
                for (int j = 0; j < Size; j++)
                    if (board[i, j] > 0)
                    {
                        int value = 1 << board[i, j] - 1;
                        submatrixDigits[i / BlockSize, j / BlockSize] |= value;
                        rowDigits[i] |= value;
                        columnDigits[j] |= value;
                    }
            // backtrack
            return BackTrackingBits(board, submatrixDigits, rowDigits, columnDigits,cts);
        }

        public bool BackTrackingBitsR(int[,] board,
        int[,] submatrixDigits,
        int[] rowDigits,
        int[] columnDigits,
        CancellationToken cts)
        {
            // if the backwards location is out of bounds then the board is solved
            if (BackwardsLocation < 0 || BackwardsLocation >= EmptyCellsArray.Length)
            {
                return true;
            }
            
            int row = EmptyCellsArray[BackwardsLocationBits] / Size;
            int col = EmptyCellsArray[BackwardsLocationBits] % Size;

            for (int i = 1; i <= Size; i++)
            {
                int digit = 1 << i - 1;

                if (!IsDigitUsed(submatrixDigits, rowDigits, columnDigits, row, col, digit))
                {
                    // set digit
                    SetDigit(submatrixDigits, rowDigits, columnDigits, row, col, digit);
                    board[row, col] = i;
                    BackwardsLocationBits--;

                    if (BackTrackingBits(board, submatrixDigits,
                                rowDigits, columnDigits, cts))
                    {
                        return true;
                    }
                    else
                    {
                        // unset digit
                        UnsetDigit(submatrixDigits, rowDigits, columnDigits, row, col, digit);
                        BackwardsLocationBits++;
                        board[row, col] = 0;
                    }
                }
            }
            return false;
        }

        // Function checks if Sudoku can be
        // solved or not
        public bool SolveSudokuUsingBitwiseBacktrackingReversed(int[,] board, CancellationToken cts)
        {
            int[,] submatrixDigits = new int[BlockSize, BlockSize];
            int[] columnDigits = new int[Size];
            int[] rowDigits = new int[Size];

            for (int i = 0; i < BlockSize; i++)
                for (int j = 0; j < BlockSize; j++)
                    submatrixDigits[i, j] = 0;

            for (int i = 0; i < Size; i++)
            {
                rowDigits[i] = 0;
                columnDigits[i] = 0;
            }

            // get submatrix, row and column digits
            for (int i = 0; i < Size; i++)
                for (int j = 0; j < Size; j++)
                    if (board[i, j] > 0)
                    {
                        int value = 1 << board[i, j] - 1;
                        submatrixDigits[i / BlockSize, j / BlockSize] |= value;
                        rowDigits[i] |= value;
                        columnDigits[j] |= value;
                    }
            // backtrack
            return BackTrackingBitsR(board, submatrixDigits, rowDigits, columnDigits, cts);
        }


        // TODO: unneeded function
        // restores the candidates of the cells that are affected by the cell at the given row and column
        // to their original state before the value of the cell at the given row and column was set
        //private static void restoreAffectedCells(int row, int col)
        //{
        //    int value = board[row, col].Value;

        //    // restore the candidates of the cells in the same row
        //    for (int i = 0; i < size; i++)
        //    {
        //        if (i != col)
        //        {
        //            board[row, i].Candidates.Add(value);
        //        }
        //    }

        //    // restore the candidates of the cells in the same column
        //    for (int i = 0; i < size; i++)
        //    {
        //        if (i != row)
        //        {
        //            board[i, col].Candidates.Add(value);
        //        }
        //    }

        //    // restore the candidates of the cells in the same block
        //    int blockRowStart = row - row % blockSize;
        //    int blockColStart = col - col % blockSize;
        //    for (int i = blockRowStart; i < blockRowStart + blockSize; i++)
        //    {
        //        for (int j = blockColStart; j < blockColStart + blockSize; j++)
        //        {
        //            if (i != row && j != col)
        //            {
        //                board[i, j].Candidates.Add(value);
        //            }
        //        }
        //    }
        //}

        // updates the candidates of the cells that are affected by the cell at the given row and column

        private void UpdateAffectedCells(int row, int col)
        {
            int value = Board[row, col].Value;

            // update the candidates of the cells in the same row
            for (int i = 0; i < Size; i++)
            {
                if (i != col)
                {
                    Board[row, i].Candidates.Remove(value);
                }
            }

            // update the candidates of the cells in the same column
            for (int i = 0; i < Size; i++)
            {
                if (i != row)
                {
                    Board[i, col].Candidates.Remove(value);
                }
            }

            // update the candidates of the cells in the same block
            int blockRowStart = row - row % BlockSize;
            int blockColStart = col - col % BlockSize;
            for (int i = blockRowStart; i < blockRowStart + BlockSize; i++)
            {
                for (int j = blockColStart; j < blockColStart + BlockSize; j++)
                {
                    if (i != row && j != col)
                    {
                        Board[i, j].Candidates.Remove(value);
                    }
                }
            }
        }

        // returns the next empty cell on the board, or null if there are no more empty cells
        private int[]? GetNextEmptyCell()
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    if (Board[i, j].Value == 0)
                    {
                        return new int[] { i, j };
                    }
                }
            }
            return null;
        }

        // returns the last empty cell on the board, or null if there are no more empty cells
        private int[]? GetLastEmptyCell()
        {
            for (int i = Size - 1; i >= 0; i--)
            {
                for (int j = Size - 1; j >= 0; j--)
                {
                    if (Board[i, j].Value == 0)
                    {
                        return new int[] { i, j };
                    }
                }
            }
            return null;
        }

        // returnes the cell on the board that has the fewest candidates, starting from top left and going right
        // and down, only changing the cell if it has less candidates than the current cell
        //private int[]? GetLeastCandidatesEmptyCell()
        //{
        //    // initialize the current cell to the top left cell
        //    int[] currentCell = new int[] { 0, 0 };

        //    // initialize the minimum number of candidates to the number of candidates in the current cell
        //    int minCandidates = Size;

        //    // iterate through the cells in the board
        //    for (int row = 0; row < Size; row++)
        //    {
        //        for (int col = 0; col < Size; col++)
        //        {
        //            // if the cell is empty and has fewer candidates than the current cell
        //            if (Board[row, col].Value == 0 && Board[row, col].Candidates.Count < minCandidates)
        //            {
        //                // update the current cell and the minimum number of candidates
        //                currentCell[0] = row;
        //                currentCell[1] = col;
        //                minCandidates = Board[row, col].Candidates.Count;
        //            }
        //        }
        //    }
        //    // if the value at the current cell is 0, return the current cell, otherwise return null
        //    return Board[currentCell[0], currentCell[1]].Value == 0 ? currentCell : null;
        //}

        // returns the cell on the board that has the fewest candidates, starting from bottom right and going left
        // and up, only changing the cell if it has less candidates than the current cell
        //private int[]? getLeastCandidatesEmptyCellReverse()
        //{
        //    // initialize the current cell to the bottom right cell
        //    int[] currentCell = new int[] { Size - 1, Size - 1 };

        //    // initialize the minimum number of candidates to the number of candidates in the current cell
        //    int minCandidates = Size;

        //    // iterate through the cells in the board
        //    for (int row = Size - 1; row >= 0; row--)
        //    {
        //        for (int col = Size - 1; col >= 0; col--)
        //        {
        //            // if the cell is empty and has fewer candidates than the current cell
        //            if (Board[row, col].Value == 0 && Board[row, col].Candidates.Count < minCandidates)
        //            {
        //                // update the current cell and the minimum number of candidates
        //                currentCell[0] = row;
        //                currentCell[1] = col;
        //                minCandidates = Board[row, col].Candidates.Count;
        //            }
        //        }
        //    }
        //    // if the value at the current cell is 0, return the current cell, otherwise return null
        //    return Board[currentCell[0], currentCell[1]].Value == 0 ? currentCell : null;
        //}

        // checks if the given candidate is valid for the cell at the given row and column

        private bool IsValidCandidate(int row, int col, int candidate)
        {
            // check if the candidate is already used in the same row
            for (int i = 0; i < Size; i++)
            {
                if (Board[row, i].Value == candidate)
                {
                    return false;
                }
            }

            // check if the candidate is already used in the same column
            for (int i = 0; i < Size; i++)
            {
                if (Board[i, col].Value == candidate)
                {
                    return false;
                }
            }

            // check if the candidate is already used in the same block
            int blockRowStart = row - row % BlockSize;
            int blockColStart = col - col % BlockSize;
            for (int i = blockRowStart; i < blockRowStart + BlockSize; i++)
            {
                for (int j = blockColStart; j < blockColStart + BlockSize; j++)
                {
                    if (Board[i, j].Value == candidate)
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
        public bool Eliminate()
        {
            bool changesMade = false;

            // create a HashSet to store the candidates of each cell
            HashSet<int> candidates;

            // loop through all the cells of the board
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    // if the cell is empty
                    if (Board[i, j].Value == 0)
                    {
                        // initialize the HashSet with the candidates of the cell
                        candidates = new HashSet<int>(Board[i, j].Candidates);

                        // remove the values of the cells in the same row, column, and block
                        for (int k = 0; k < Size; k++)
                        {
                            if (k != j && Board[i, k].Value != 0)
                            {
                                candidates.Remove(Board[i, k].Value);
                            }
                            if (k != i && Board[k, j].Value != 0)
                            {
                                candidates.Remove(Board[k, j].Value);
                            }
                        }
                        int blockRowStart = i - i % BlockSize;
                        int blockColStart = j - j % BlockSize;
                        for (int k = blockRowStart; k < blockRowStart + BlockSize; k++)
                        {
                            for (int l = blockColStart; l < blockColStart + BlockSize; l++)
                            {
                                if (k != i && l != j && Board[k, l].Value != 0)
                                {
                                    candidates.Remove(Board[k, l].Value);
                                }
                            }
                        }

                        // if the cell has only one candidate left
                        if (candidates.Count == 1)
                        {
                            // set the value of the cell to the candidate
                            Board[i, j].Value = candidates.First();
                            Board[i, j].Candidates.Clear();

                            // update the candidates of the affected cells
                            UpdateAffectedCells(i, j);

                            // set the changesMade flag to true
                            changesMade = true;
                        }
                    }
                }
            }

            // return true if any changes were made, false otherwise
            return changesMade;
        }


        // implement the hidden single algorithm
        // this function is ran before the solving function, to make the solving process
        // faster
        // what it does is that it goes over each row, column, and block, and if there is
        // only one cell that has a number as a candidate, then it sets the value of that
        // cell to the candidate
        // and then it removes the candidate from the candidates of all the other cells in
        // the same row, column, and block
        // this function gets the changed cell's col and row and checks if its 
        // col, row or block has a hidden single
        public bool HiddenSingles()
        {
            // flag to check if any changes have been made to the board
            bool changesMade = false;

            // for each row
            for (int row = 0; row < Size; row++)
            {
                // for each value
                for (int value = 1; value <= Size; value++)
                {
                    // count the number of cells in the row that have the value as a candidate
                    int count = 0;
                    int col = 0;
                    for (int i = 0; i < Size; i++)
                    {
                        if (Board[row, i].Candidates.Contains(value))
                        {
                            count++;
                            col = i;
                        }
                    }

                    // if there is only one cell in the row that has the value as a candidate
                    // then set the value of that cell to the value
                    if (count == 1)
                    {
                        Board[row, col].Value = value;
                        Board[row, col].Candidates.Clear();
                        UpdateAffectedCells(row, col);
                        changesMade = true;
                    }
                }
            }

            // for each column
            for (int col = 0; col < Size; col++)
            {
                // for each value
                for (int value = 1; value <= Size; value++)
                {
                    // count the number of cells in the column that have the value as a candidate
                    int count = 0;
                    int row = 0;
                    for (int i = 0; i < Size; i++)
                    {
                        if (Board[i, col].Candidates.Contains(value))
                        {
                            count++;
                            row = i;
                        }
                    }

                    // if there is only one cell in the column that has the value as a candidate
                    // then set the value of that cell to the value
                    if (count == 1)
                    {
                        Board[row, col].Value = value;
                        UpdateAffectedCells(row, col);
                        changesMade = true;
                    }
                }
            }

            // for each block
            for (int blockRow = 0; blockRow < BlockSize; blockRow++)
            {
                for (int blockCol = 0; blockCol < BlockSize; blockCol++)
                {
                    // for each value
                    for (int value = 1; value <= Size; value++)
                    {
                        // count the number of cells in the block that have the value as a candidate
                        int count = 0;
                        int row = 0;
                        int col = 0;
                        for (int i = blockRow * BlockSize; i < blockRow * BlockSize + BlockSize; i++)
                        {
                            for (int j = blockCol * BlockSize; j < blockCol * BlockSize + BlockSize; j++)
                            {
                                if (Board[i, j].Candidates.Contains(value))
                                {
                                    count++;
                                    row = i;
                                    col = j;
                                }
                            }
                        }

                        // if there is only one cell in the block that has the value as a candidate
                        // then set the value of that cell to the value
                        if (count == 1)
                        {
                            Board[row, col].Value = value;
                            UpdateAffectedCells(row, col);
                            changesMade = true;
                        }
                    }
                }
            }
            // return true if any changes were made, false otherwise
            return changesMade;
        }

        // checks for and eliminates naked pairs in the given row
        public bool CheckRowForNakedPairs(int row)
        {
            bool changesMade = false;

            for (int j = 0; j < Size; j++)
            {
                // check for naked pairs in the row
                if (Board[row, j].Value == 0 && Board[row, j].Candidates.Count == 2)
                {
                    for (int k = j + 1; k < Size; k++)
                    {
                        if (Board[row, k].Value == 0 && Board[row, k].Candidates.Count == 2 && Board[row, j].Candidates.SetEquals(Board[row, k].Candidates))
                        {
                            // eliminate the candidates of the naked pair from the other cells in the row
                            for (int l = 0; l < Size; l++)
                            {
                                if (l != j && l != k)
                                {
                                    if (Board[row, l].Candidates.Remove(Board[row, j].Candidates.ElementAt(0)))
                                    {
                                        changesMade = true;
                                    }
                                    if (Board[row, l].Candidates.Remove(Board[row, j].Candidates.ElementAt(1)))
                                    {
                                        changesMade = true;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return changesMade;
        }

        // checks for and eliminates naked pairs in the given column
        public bool CheckColumnForNakedPairs(int col)
        {
            bool changesMade = false;

            for (int j = 0; j < Size; j++)
            {
                // check for naked pairs in the column
                if (Board[j, col].Value == 0 && Board[j, col].Candidates.Count == 2)
                {
                    for (int k = j + 1; k < Size; k++)
                    {
                        if (Board[k, col].Value == 0 && Board[k, col].Candidates.Count == 2 && Board[j, col].Candidates.SetEquals(Board[k, col].Candidates))
                        {
                            // eliminate the candidates of the naked pair from the other cells in the column
                            for (int l = 0; l < Size; l++)
                            {
                                if (l != j && l != k)
                                {
                                    if (Board[l, col].Candidates.Remove(Board[j, col].Candidates.ElementAt(0)))
                                    {
                                        changesMade = true;
                                    }
                                    if (Board[l, col].Candidates.Remove(Board[j, col].Candidates.ElementAt(1)))
                                    {
                                        changesMade = true;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return changesMade;
        }

        // checks for and eliminates naked pairs in the given block
        public bool CheckBlockForNakedPairs(int blockRowStart, int blockColStart)
        {
            bool changesMade = false;

            for (int j = blockRowStart; j < blockRowStart + BlockSize; j++)
            {
                for (int k = blockColStart; k < blockColStart + BlockSize; k++)
                {
                    if (Board[j, k].Value == 0 && Board[j, k].Candidates.Count == 2)
                    {
                        for (int l = blockRowStart; l < blockRowStart + BlockSize; l++)
                        {
                            for (int m = blockColStart; m < blockColStart + BlockSize; m++)
                            {
                                if (l != j && m != k && Board[l, m].Value == 0 && Board[l, m].Candidates.Count == 2
                                    && Board[j, k].Candidates.SetEquals(Board[l, m].Candidates))
                                {
                                    // eliminate the candidates of the naked pair from the other cells in the block
                                    for (int n = blockRowStart; n < blockRowStart + BlockSize; n++)
                                    {
                                        for (int p = blockColStart; p < blockColStart + BlockSize; p++)
                                        {
                                            if (n != j && n != l && p != k && p != m)
                                            {
                                                if (Board[n, p].Candidates.Remove(Board[j, k].Candidates.ElementAt(0)))
                                                {
                                                    changesMade = true;
                                                }
                                                if (Board[n, p].Candidates.Remove(Board[j, k].Candidates.ElementAt(1)))
                                                {
                                                    changesMade = true;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return changesMade;
        }

        // checks for and eliminates naked pairs in the board
        public bool NakedPairs()
        {
            bool changesMade = false;

            // check for and eliminate naked pairs in each row
            for (int i = 0; i < Size; i++)
            {
                changesMade |= CheckRowForNakedPairs(i);
            }

            // check for and eliminate naked pairs in each column
            for (int i = 0; i < Size; i++)
            {
                changesMade |= CheckColumnForNakedPairs(i);
            }

            // check for and eliminate naked pairs in each block
            for (int i = 0; i < Size; i += BlockSize)
            {
                for (int j = 0; j < Size; j += BlockSize)
                {
                    changesMade |= CheckBlockForNakedPairs(i, j);
                }
            }

            return changesMade;
        }

        // check for and elimiantes naked triples in th given block
        private bool CheckBlockForNakedTriples(int rowStart, int colStart)
        {
            bool changed = false;

            List<int[]> triples = new();

            // find all triples in the block
            for (int i = rowStart; i < rowStart + BlockSize; i++)
            {
                for (int j = colStart; j < colStart + BlockSize; j++)
                {
                    if (Board[i, j].Candidates.Count == 3)
                    {
                        triples.Add(new int[] { i, j });
                    }
                }
            }

            // check if any of the triples have the same candidates
            for (int i = 0; i < triples.Count; i++)
            {
                for (int j = i + 1; j < triples.Count; j++)
                {
                    if (HaveSameCandidates(triples[i], triples[j]))
                    {
                        // if the triples have the same candidates, eliminate those candidates from the other cells in the block
                        changed |= EliminateCandidatesFromOtherCellsInBlock(Board[triples[i][0], triples[i][1]].Candidates, triples[i], triples[j], rowStart, colStart);
                    }
                }
            }

            return changed;
        }

        // Check if the given row has any naked triples
        // A naked triple is 3 cells in a row that have 3 candidates each and those candidates are the same for all 3 cells
        private bool CheckRowForNakedTriples(int row)
        {
            bool changed = false;

            // Find all cells in the row that have 3 candidates
            List<int[]> triples = new();
            for (int i = 0; i < Size; i++)
            {
                if (Board[row, i].Candidates.Count == 3)
                {
                    triples.Add(new int[] { row, i });
                }
            }

            // Check if any of the triples have the same candidates
            for (int i = 0; i < triples.Count; i++)
            {
                for (int j = i + 1; j < triples.Count; j++)
                {
                    if (HaveSameCandidates(triples[i], triples[j]))
                    {
                        // If the triples have the same candidates, eliminate those candidates from the other cells in the row
                        changed |= EliminateCandidatesFromOtherCellsInRow(Board[triples[i][0], triples[i][1]].Candidates, triples[i], triples[j]);
                    }
                }
            }

            return changed;
        }

        private bool CheckColumnForNakedTriples(int col)
        {
            // flag to track if any changes have been made
            bool changed = false;

            // list to store all cells with 3 candidates in the given column
            List<int[]> triples = new();

            // find all triples in the column
            for (int i = 0; i < Size; i++)
            {
                if (Board[i, col].Candidates.Count == 3)
                {
                    triples.Add(new int[] { i, col });
                }
            }

            // check if any of the triples have the same candidates
            for (int i = 0; i < triples.Count; i++)
            {
                for (int j = i + 1; j < triples.Count; j++)
                {
                    if (HaveSameCandidates(triples[i], triples[j]))
                    {
                        // if the triples have the same candidates, eliminate those candidates from the other cells in the column
                        changed |= EliminateCandidatesFromOtherCellsInCol(Board[triples[i][0], triples[i][1]].Candidates, triples[i], triples[j]);
                    }
                }
            }

            return changed;
        }

        // helper function to check if two cells have the same candidates
        private bool HaveSameCandidates(int[] cell1, int[] cell2)
        {
            HashSet<int> candidates1 = Board[cell1[0], cell1[1]].Candidates;
            HashSet<int> candidates2 = Board[cell2[0], cell2[1]].Candidates;

            return candidates1.SetEquals(candidates2);
        }

        // helper function to eliminate candidates from other cells in a block
        private bool EliminateCandidatesFromOtherCellsInBlock(HashSet<int> candidates, int[] cell1,
            int[] cell2, int rowStart, int colStart)
        {
            bool changed = false;
            for (int i = rowStart; i < rowStart + BlockSize; i++)
            {
                for (int j = colStart; j < colStart + BlockSize; j++)
                {
                    if (Board[i, j].Equals(cell1) || Board[i, j].Equals(cell2))
                    {
                        continue;
                    }
                    changed |= Board[i, j].Candidates.RemoveWhere(x => candidates.Contains(x)) > 0;
                }
            }
            return changed;
        }


        // helper function to eliminate candidates from other cells in a row
        private bool EliminateCandidatesFromOtherCellsInRow(HashSet<int> candidates, int[] cell1, int[] cell2)
        {
            bool changed = false;
            for (int j = 0; j < Size; j++)
            {
                if (Board[cell1[0], j].Equals(cell1) || Board[cell1[0], j].Equals(cell2))
                {
                    continue;
                }
                changed |= Board[cell1[0], j].Candidates.RemoveWhere(x => candidates.Contains(x)) > 0;
            }
            return changed;
        }

        // helper function to eliminate candidates from other cells in a column
        private bool EliminateCandidatesFromOtherCellsInCol(HashSet<int> candidates, int[] cell1, int[] cell2)
        {
            bool changed = false;
            for (int i = 0; i < Size; i++)
            {
                if (Board[i, cell1[1]].Equals(cell1) || Board[i, cell1[1]].Equals(cell2))
                {
                    continue;
                }
                changed |= Board[i, cell1[1]].Candidates.RemoveWhere(x => candidates.Contains(x)) > 0;
            }
            return changed;
        }

        // this is an implementation of naked triples algoritm 
        public bool NakedTriples()
        {
            bool changed = false;

            // check each block for naked triples
            for (int row = 0; row < Size; row += BlockSize)
            {
                for (int col = 0; col < Size; col += BlockSize)
                {
                    changed |= CheckBlockForNakedTriples(row, col);
                }
            }

            // check each row for naked triples
            for (int row = 0; row < Size; row++)
            {
                changed |= CheckRowForNakedTriples(row);
            }

            // check each column for naked triples
            for (int col = 0; col < Size; col++)
            {
                changed |= CheckColumnForNakedTriples(col);
            }

            return changed;
        }

        // checks for and eliminates naked quads in the board
        public bool NakedQuads()
        {
            bool changesMade = false;

            // check for and eliminate hidden quads in each row
            for (int i = 0; i < Size; i++)
            {
                changesMade |= CheckRowForNakedQuads(i);
            }

            // check for and eliminate hidden quads in each column
            for (int i = 0; i < Size; i++)
            {
                changesMade |= CheckColumnForNakedQuads(i);
            }

            // check for and eliminate hidden quads in each block
            for (int i = 0; i < Size; i += BlockSize)
            {
                for (int j = 0; j < Size; j += BlockSize)
                {
                    changesMade |= CheckBlockForNakedQuads(i, j);
                }
            }

            return changesMade;
        }

        // Check if the given row has any naked quads
        // A naked quad is 4 cells in a row that have 4 candidates each and those candidates are the same for all 4 cells
        private bool CheckRowForNakedQuads(int row)
        {
            bool changed = false;

            // Find all cells in the row that have 4 candidates
            List<int[]> quads = new();
            for (int i = 0; i < Size; i++)
            {
                if (Board[row, i].Candidates.Count == 4)
                {
                    quads.Add(new int[] { row, i });
                }
            }

            // Check if any of the quads have the same candidates
            for (int i = 0; i < quads.Count; i++)
            {
                for (int j = i + 1; j < quads.Count; j++)
                {
                    for (int k = j + 1; k < quads.Count; k++)
                    {
                        for (int l = k + 1; l < quads.Count; l++)
                        {
                            if (HaveSameCandidates(quads[i], quads[j], quads[k], quads[l]))
                            {
                                // If the quads have the same candidates, eliminate those candidates from the other cells in the row
                                changed |= EliminateCandidatesFromOtherCellsInRowQ(Board[quads[i][0], quads[i][1]].Candidates, quads[i], quads[j], quads[k], quads[l]);
                            }
                        }
                    }
                }
            }

            return changed;
        }

        private bool EliminateCandidatesFromOtherCellsInRowQ(HashSet<int> candidates, int[] coord1, int[] coord2, int[] coord3, int[] coord4)
        {
            bool changed = false;

            // Get the row of the first cell
            int row = coord1[0];

            // Go through each cell in the row
            for (int i = 0; i < Size; i++)
            {
                // Check if the cell is not one of the cells to ignore
                if (coord1[0] == row && coord1[1] == i || coord2[0] == row && coord2[1] == i || coord3[0] == row && coord3[1] == i || coord4[0] == row && coord4[1] == i)
                {
                    continue;
                }

                // Eliminate the candidates from the cell
                changed |= Board[row, i].Candidates.RemoveWhere(x => candidates.Contains(x)) > 0;
            }

            return changed;
        }

        private bool CheckColumnForNakedQuads(int col)
        {
            bool changed = false;

            // Find all cells in the column that have 4 candidates
            List<int[]> quads = new();
            for (int i = 0; i < Size; i++)
            {
                if (Board[i, col].Candidates.Count == 4)
                {
                    quads.Add(new int[] { i, col });
                }
            }

            // Check if any of the quads have the same candidates
            for (int i = 0; i < quads.Count; i++)
            {
                for (int j = i + 1; j < quads.Count; j++)
                {
                    for (int k = j + 1; k < quads.Count; k++)
                    {
                        for (int l = k + 1; l < quads.Count; l++)
                        {
                            if (HaveSameCandidates(quads[i], quads[j], quads[k], quads[l]))
                            {
                                // If the quads have the same candidates, eliminate those candidates from the other cells in the column
                                changed |= EliminateCandidatesFromOtherCellsInColumnQ(Board[quads[i][0], quads[i][1]].Candidates, quads[i], quads[j], quads[k], quads[l]);
                            }
                        }
                    }
                }
            }

            return changed;
        }

        private bool EliminateCandidatesFromOtherCellsInColumnQ(HashSet<int> candidates, int[] coord1, int[] coord2, int[] coord3, int[] coord4)
        {
            bool changed = false;

            // Get the column of the first cell
            int col = coord1[1];

            // Go through each cell in the column
            for (int i = 0; i < Size; i++)
            {
                // Check if the cell is not one of the cells to ignore
                if (coord1[0] == i && coord1[1] == col || coord2[0] == i && coord2[1] == col || coord3[0] == i && coord3[1] == col || coord4[0] == i && coord4[1] == col)
                {
                    continue;
                }

                // Eliminate the candidates from the cell
                changed |= Board[i, col].Candidates.RemoveWhere(x => candidates.Contains(x)) > 0;
            }

            return changed;
        }

        private bool CheckBlockForNakedQuads(int rowStart, int colStart)
        {
            bool changed = false;

            // Find all cells in the block that have 4 candidates
            List<int[]> quads = new();
            for (int i = rowStart; i < rowStart + BlockSize; i++)
            {
                for (int j = colStart; j < colStart + BlockSize; j++)
                {
                    if (Board[i, j].Candidates.Count == 4)
                    {
                        quads.Add(new int[] { i, j });
                    }
                }
            }

            // Check if any of the quads have the same candidates
            for (int i = 0; i < quads.Count; i++)
            {
                for (int j = i + 1; j < quads.Count; j++)
                {
                    for (int k = j + 1; k < quads.Count; k++)
                    {
                        for (int l = k + 1; l < quads.Count; l++)
                        {
                            if (HaveSameCandidates(quads[i], quads[j], quads[k], quads[l]))
                            {
                                // If the quads have the same candidates, eliminate those candidates from the other cells in the block
                                changed |= EliminateCandidatesFromOtherCellsInBlockQ(Board[quads[i][0], quads[i][1]].Candidates, quads[i], quads[j], quads[k], quads[l], rowStart, colStart);
                            }
                        }
                    }
                }
            }

            return changed;
        }

        private bool EliminateCandidatesFromOtherCellsInBlockQ(HashSet<int> candidates, int[] coord1, int[] coord2, int[] coord3,
            int[] coord4, int rowStart, int colStart)
        {
            bool changed = false;

            // Go through each cell in the block
            for (int i = rowStart; i < rowStart + BlockSize; i++)
            {
                for (int j = colStart; j < colStart + BlockSize; j++)
                {
                    // Check if the cell is not one of the cells to ignore
                    if (coord1[0] == i && coord1[1] == j || coord2[0] == i && coord2[1] == j || coord3[0] == i && coord3[1] == j || coord4[0] == i && coord4[1] == j)
                    {
                        continue;
                    }

                    // Eliminate the candidates from the cell
                    changed |= Board[i, j].Candidates.RemoveWhere(x => candidates.Contains(x)) > 0;
                }
            }

            return changed;
        }


        // Check if the given cells have the same candidates
        private bool HaveSameCandidates(int[] coord1, int[] coord2, int[] coord3, int[] coord4)
        {
            // Get the candidates for each cell
            HashSet<int> candidates1 = new(Board[coord1[0], coord1[1]].Candidates);
            HashSet<int> candidates2 = new(Board[coord2[0], coord2[1]].Candidates);
            HashSet<int> candidates3 = new(Board[coord3[0], coord3[1]].Candidates);
            HashSet<int> candidates4 = new(Board[coord4[0], coord4[1]].Candidates);

            // Check if the candidates are the same for all cells
            return candidates1.SetEquals(candidates2) && candidates2.SetEquals(candidates3) && candidates3.SetEquals(candidates4);
        }


        // implementing intersection - pointining doubles, triples and box line reduction

        // this is the implementation of the poiniting doubles algortim
        public bool PointingDoubles()
        {
            bool changed = false;

            for (int i = 0; i < Size; i++)
            {
                // check rows for pointing doubles
                changed |= CheckRowForPointingDoubles(i);

                // check columns for pointing doubles
                changed |= CheckColumnForPointingDoubles(i);

                // check blocks for pointing doubles
                int rowStart = i / BlockSize * BlockSize;
                int colStart = i % BlockSize * BlockSize;
                changed |= CheckBlockForPointingDoubles(rowStart, colStart);
            }

            return changed;
        }

        // this is a helper function that will go over the given row and check for pointing doubles
        private bool CheckRowForPointingDoubles(int row)
        {
            bool changed = false;

            List<int[]> doubles = new();

            // find all doubles in the row
            for (int i = 0; i < Size; i++)
            {
                if (Board[row, i].Candidates.Count == 2)
                {
                    doubles.Add(new int[] { row, i });
                }
            }

            // check if any of the doubles belong to the same column as another double
            for (int i = 0; i < doubles.Count; i++)
            {
                for (int j = i + 1; j < doubles.Count; j++)
                {
                    if (doubles[i][1] == doubles[j][1])
                    {
                        // if the doubles belong to the same column, eliminate those candidates from the other cells in the column
                        changed |= EliminateCandidatesFromOtherCellsInCol(Board[doubles[i][0], doubles[i][1]].Candidates, doubles[i], doubles[j]);
                    }
                }
            }

            return changed;
        }

        // this is a helper function that will go over the given col and check for pointing doubles
        private bool CheckColumnForPointingDoubles(int col)
        {
            bool changed = false;

            // create a dictionary to store the candidate values and the cells that contain them
            Dictionary<int, List<int[]>> candidatesToCells = new();

            // iterate through the cells in the column
            for (int i = 0; i < Size; i++)
            {
                // get the candidates of the current cell
                HashSet<int> candidates = Board[i, col].Candidates;

                // iterate through the candidates
                foreach (int candidate in candidates)
                {
                    // add the current cell to the list of cells that contain the current candidate
                    if (!candidatesToCells.ContainsKey(candidate))
                    {
                        candidatesToCells[candidate] = new List<int[]>();
                    }
                    candidatesToCells[candidate].Add(new int[] { i, col });
                }
            }

            // iterate through the dictionary
            foreach (KeyValuePair<int, List<int[]>> entry in candidatesToCells)
            {
                // check if there are exactly two cells that contain the current candidate
                if (entry.Value.Count == 2)
                {
                    // eliminate the candidate from the other cells in the column
                    changed |= EliminateCandidatesFromOtherCellsInCol(new HashSet<int> { entry.Key }, entry.Value[0], entry.Value[1]);
                }
            }

            return changed;
        }

        // this is a helper function that will go over the given block and check for pointing doubles
        private bool CheckBlockForPointingDoubles(int rowStart, int colStart)
        {
            bool changed = false;

            List<int[]> pairs = new();

            // find all pairs in the block
            for (int i = rowStart; i < rowStart + BlockSize; i++)
            {
                for (int j = colStart; j < colStart + BlockSize; j++)
                {
                    if (Board[i, j].Candidates.Count == 2)
                    {
                        pairs.Add(new int[] { i, j });
                    }
                }
            }

            // check if any of the pairs have the same candidates
            for (int i = 0; i < pairs.Count; i++)
            {
                for (int j = i + 1; j < pairs.Count; j++)
                {
                    if (HaveSameCandidates(pairs[i], pairs[j]))
                    {
                        // if the pairs have the same candidates, eliminate those candidates from the other cells in the block
                        changed |= EliminateCandidatesFromOtherCellsInBlock(Board[pairs[i][0], pairs[i][1]].Candidates, pairs[i], pairs[j], rowStart, colStart);
                    }
                }
            }

            return changed;
        }

        // this is the implementation of the poiniting doubles algortim
        public bool PointingTriples()
        {
            bool changed = false;

            // check rows for pointing triples
            for (int row = 0; row < Size; row++)
            {
                changed |= CheckRowForPointingTriples(row);
            }

            // check columns for pointing triples
            for (int col = 0; col < Size; col++)
            {
                changed |= CheckColumnForPointingTriples(col);
            }

            // check blocks for pointing triples
            for (int row = 0; row < Size; row += BlockSize)
            {
                for (int col = 0; col < Size; col += BlockSize)
                {
                    changed |= CheckBlockForPointingTriples(row, col);
                }
            }

            return changed;
        }

        // this is a helper function that will go over the given row and check for pointing triples
        private bool CheckRowForPointingTriples(int row)
        {
            bool changed = false;

            List<int[]> triples = new();

            // find all triples in the row
            for (int i = 0; i < Size; i++)
            {
                if (Board[row, i].Candidates.Count == 3)
                {
                    triples.Add(new int[] { row, i });
                }
            }

            // check if any of the triples have the same candidates
            for (int i = 0; i < triples.Count; i++)
            {
                for (int j = i + 1; j < triples.Count; j++)
                {
                    if (HaveSameCandidates(triples[i], triples[j]))
                    {
                        // if the triples have the same candidates, eliminate those candidates from the other cells in the row
                        HashSet<int> candidates = new(Board[triples[i][0], triples[i][1]].Candidates);
                        candidates.IntersectWith(Board[triples[j][0], triples[j][1]].Candidates);
                        changed |= EliminateCandidatesFromOtherCellsInRow(candidates, triples[i], triples[j]);
                    }
                }
            }

            return changed;
        }

        // this is a helper function that will go over the given col and check for pointing triples
        private bool CheckColumnForPointingTriples(int col)
        {
            bool changed = false;

            List<int[]> triples = new();

            // find all triples in the column
            for (int i = 0; i < Size; i++)
            {
                if (Board[i, col].Candidates.Count == 3)
                {
                    triples.Add(new int[] { i, col });
                }
            }

            // check if any of the triples have the same candidates
            for (int i = 0; i < triples.Count; i++)
            {
                for (int j = i + 1; j < triples.Count; j++)
                {
                    if (HaveSameCandidates(triples[i], triples[j]))
                    {
                        // if the triples have the same candidates, eliminate those candidates from the other cells in the column
                        HashSet<int> candidates = new(Board[triples[i][0], triples[i][1]].Candidates);
                        candidates.UnionWith(Board[triples[j][0], triples[j][1]].Candidates);
                        changed |= EliminateCandidatesFromOtherCellsInCol(candidates, triples[i], triples[j]);
                    }
                }
            }

            return changed;
        }

        // this is a helper function that will go over the given box and check for pointing triples
        private bool CheckBlockForPointingTriples(int rowStart, int colStart)
        {
            bool changed = false;

            List<int[]> triples = new();

            // find all triples in the block
            for (int i = rowStart; i < rowStart + BlockSize; i++)
            {
                for (int j = colStart; j < colStart + BlockSize; j++)
                {
                    if (Board[i, j].Candidates.Count == 3)
                    {
                        triples.Add(new int[] { i, j });
                    }
                }
            }

            // check if any of the triples have the same candidates
            for (int i = 0; i < triples.Count; i++)
            {
                for (int j = i + 1; j < triples.Count; j++)
                {
                    if (HaveSameCandidates(triples[i], triples[j]))
                    {
                        // if the triples have the same candidates, check if they point to a cell in a row or column
                        bool rowChanged = CheckTripleForPointingRow(triples[i], triples[j]);
                        bool colChanged = CheckTripleForPointingCol(triples[i], triples[j]);
                        if (rowChanged || colChanged)
                        {
                            // if the triples point to a cell in a row or column, eliminate those candidates from that cell
                            changed |= EliminateCandidatesFromPointedCell(triples[i], triples[j], rowStart, colStart);
                        }
                    }
                }
            }

            return changed;
        }

        // This function checks if a pair of cells in a triple share candidates with a cell in the same row as the triple.
        // If they do, it eliminates those shared candidates from the other cells in the row.
        private bool CheckTripleForPointingRow(int[] cell1, int[] cell2)
        {
            // create a hashset to store the candidates of the two cells
            HashSet<int> candidates = new();
            candidates.UnionWith(Board[cell1[0], cell1[1]].Candidates);
            candidates.UnionWith(Board[cell2[0], cell2[1]].Candidates);

            // check if the hashset has three candidates
            if (candidates.Count == 3)
            {
                // find the third cell in the row with the same candidates
                for (int i = 0; i < Size; i++)
                {
                    if (Board[cell1[0], i].Candidates.SetEquals(candidates))
                    {
                        // if the third cell is found, eliminate the candidates from other cells in the row
                        return EliminateCandidatesFromOtherCellsInRow(candidates, cell1, cell2);
                    }
                }
            }

            // if the third cell is not found or the hashset does not have three candidates, return false
            return false;
        }

        // Check if the given triple of cells in the same column points to a candidate that can be eliminated from other cells in the column.
        // If so, eliminate the candidate and return true. Otherwise, return false.
        private bool CheckTripleForPointingCol(int[] cell1, int[] cell2)
        {
            // Get the common candidates of the two cells
            HashSet<int> commonCandidates = new(Board[cell1[0], cell1[1]].Candidates);
            commonCandidates.IntersectWith(Board[cell2[0], cell2[1]].Candidates);

            // If the common candidates form a triple with the candidates of the third cell in the column,
            // eliminate the common candidates from the other cells in the column
            int[] cell3 = new int[] { cell1[0], cell1[1] == 0 ? cell1[1] + 2 : cell1[1] - 2 };
            if (commonCandidates.IsSubsetOf(Board[cell3[0], cell3[1]].Candidates))
            {
                return EliminateCandidatesFromOtherCellsInCol(commonCandidates, cell1, cell2);
            }
            return false;
        }

        // This function removes candidates from the cells indicated by the input triples (cell1 and cell2)
        // if they are part of a pointing triple in the block with top-left corner at (rowStart, colStart).
        // It returns a boolean indicating whether any candidates were removed.
        private bool EliminateCandidatesFromPointedCell(int[] triple1, int[] triple2, int rowStart, int colStart)
        {
            bool changed = false;

            // find the cell that the two triples point to
            int pointedRow = -1, pointedCol = -1;
            if (triple1[0] == triple2[0])
            {
                pointedRow = triple1[0];
                pointedCol = triple1[1] == colStart ? triple2[1] : triple1[1];
            }
            else if (triple1[1] == triple2[1])
            {
                pointedRow = triple1[0] == rowStart ? triple2[0] : triple1[0];
                pointedCol = triple1[1];
            }

            // eliminate candidates from the pointed cell
            if (pointedRow != -1 && pointedCol != -1)
            {
                HashSet<int> candidates = new();
                candidates.UnionWith(Board[triple1[0], triple1[1]].Candidates);
                candidates.UnionWith(Board[triple1[0], triple1[1]].Candidates);
                candidates.UnionWith(Board[triple2[0], triple2[1]].Candidates);
                changed |= Board[pointedRow, pointedCol].Candidates.RemoveWhere(x => candidates.Contains(x)) > 0;
            }

            return changed;
        }

        // this is the implemention of the box line reduction algoritm
        public bool BoxLineReduction()
        {
            bool changed = false;

            // check each block
            for (int i = 0; i < Size; i += BlockSize)
            {
                for (int j = 0; j < Size; j += BlockSize)
                {
                    // check rows in the block
                    for (int row = i; row < i + BlockSize; row++)
                    {
                        // get the candidates in the row
                        HashSet<int> rowCandidates = new();
                        for (int col = j; col < j + BlockSize; col++)
                        {
                            rowCandidates.UnionWith(Board[row, col].Candidates);
                        }

                        // check columns in the block
                        for (int col = j; col < j + BlockSize; col++)
                        {
                            // get the candidates in the column
                            HashSet<int> colCandidates = new();
                            for (int r = i; r < i + BlockSize; r++)
                            {
                                colCandidates.UnionWith(Board[r, col].Candidates);
                            }

                            // if the intersection of the row candidates and column candidates has size 2 or 3,
                            // eliminate those candidates from the other cells in the row or column
                            HashSet<int> intersection = new(rowCandidates);
                            intersection.IntersectWith(colCandidates);
                            if (intersection.Count == 2 || intersection.Count == 3)
                            {
                                for (int r = 0; r < Size; r++)
                                {
                                    if (r >= i && r < i + BlockSize)
                                    {
                                        // eliminate candidates from the other cells in the column
                                        changed |= Board[r, col].Candidates.RemoveWhere(x => intersection.Contains(x)) > 0;
                                    }
                                    else
                                    {
                                        // eliminate candidates from the other cells in the row
                                        changed |= Board[row, r].Candidates.RemoveWhere(x => intersection.Contains(x)) > 0;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return changed;
        }

        // --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                       DANCING LINKS
        // --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        // dancing linsk algorithm
        public bool DancingLinks(Cell[,] board)
        {
            // convert the board of cells to a matrix of nodes
            Node[][] matrix = ConvertBoard(board, Size);

            // print the right value and left value and the up and down value of each node
            //for (int i = 0; i < size; i++)
            //{
            //    for (int j = 0; j < size; j++)
            //    {
            //        Console.WriteLine("Node " + i + " " + j + " right: " + matrix[i][j].Right.Value +
            //            " left: " + matrix[i][j].Left.Value + " up: " + matrix[i][j].Up.Value + " down: " + matrix[i][j].Down.Value);
            //    }
            //}

            //Console.ReadLine();

            // find a solution to the board
            if (DLX(matrix[0][0]))
            {
                // a solution was found, update the board with the solution
                for (int row = 0; row < matrix.Length; row++)
                {
                    for (int col = 0; col < matrix[row].Length; col++)
                    {
                        Node node = matrix[row][col];
                        if (node.Right == node)
                        {
                            // the node is "on", set its value in the board
                            board[row, col].Value = node.Value;
                            board[row, col].Solved = true;
                        }
                    }
                }

                return true;
            }

            // no solution was found
            return false;
        }

        public bool DLX(Node root)
        {

            // check if the root node has no right or down pointers
            if (root.Right == root || root.Down == root)
            {
                // a solution has been found
                return true;
            }

            // choose a column with the fewest "on" cells
            Node col = ChooseColumn(root);

            // if there are no columns with "on" cells, return false
            if (col == null)
            {
                return false;
            }

            // cover the column
            CoverColumn(col);

            // iterate through the "on" cells in the column
            for (Node row = col.Down; row != col; row = row.Down)
            {
                // save the original left and right pointers of the row
                Node originalLeft = row.Left;
                Node originalRight = row.Right;

                // remove the row from the matrix
                row.Left.Right = row.Right;
                row.Right.Left = row.Left;

                // iterate through the "on" cells in the row
                for (Node cell = row.Right; cell != row; cell = cell.Right)
                {
                    // cover the column
                    cell.Up.Down = cell.Down;
                    cell.Down.Up = cell.Up;
                }

                // recursively solve the board with the new constraints
                if (DLX(root))
                {
                    return true;
                }

                // add the row and columns back to the matrix
                row.Left = originalLeft;
                row.Right = originalRight;
                for (Node cell = row.Right; cell != row; cell = cell.Right)
                {
                    cell.Up.Down = cell;
                    cell.Down.Up = cell;
                }
            }

            // uncover the column
            col.Left.Right = col;
            col.Right.Left = col;
            for (Node row = col.Up; row != col; row = row.Up)
            {
                row.Left.Right = row;
                row.Right.Left = row;
            }

            // no solution was found
            return false;
        }


    }

}

