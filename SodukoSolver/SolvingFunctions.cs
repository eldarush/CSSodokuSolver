using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SodukoSolver.ValidatingFunctions;
using static SodukoSolver.HelperFunctions;


namespace SodukoSolver
{
    internal class SolvingFunctions
    {
        // size of the board
        public int size { get; set; }

        // blockSize is the size of the block that the board is divided into
        public int blockSize { get; set; }

        // the board
        public Cell[,] board { get; set; }

        // Constructor that gets the size and the board
        public SolvingFunctions(int size, Cell[,] board)
        {
            this.size = size;
            this.board = board;
            this.blockSize = (int)Math.Sqrt(size);
        }

        // this is the implementation of the backtracking algorithm
        // that goes over the board and tries to solve it
        // this function goes from left to right and top to bottom
        public bool Backtracking(CancellationToken token)
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


            //// TODO: work out why cells arent working properly - possible problem in board creation
            //// get the current cell 
            //Cell currentCell = board[row, col];
            //currentCell.printCandidates();
            //Console.WriteLine("row: " + row + " col: " + col);
            //Console.WriteLine("current cell value: " + currentCell.Value + "is Solved: \n" + currentCell.Solved);

            //// for each possible candidate in the cell
            //foreach (int i in currentCell.Candidates)
            for(int i=0; i<=size; i++)
            {
                // if the candidate is valid
                if (isValidCandidate(row, col, i))
                {
                    // set the value of the cell to the candidate
                    board[row, col].Value = i;


                    // if the board is solved then return true
                    if (Backtracking(token))
                    {
                        return true;
                    }
                    board[row, col].Value = 0;
                }
            }

            // if the board is not solved then return false
            return false;
        }

   
        // this is an implementation of backtracking to solve the board
        // that goes in reversed order - from the last empty cell to the first
        public bool BacktrackingR(CancellationToken token)
        {
            // get the next empty cell
            int[] lastEmptyCell = getLastEmptyCell();

            // if there is no empty cell then the board is solved
            if (lastEmptyCell == null)
            {
                return true;
            }

            // get the row and column of the next empty cell
            int row = lastEmptyCell[0];
            int col = lastEmptyCell[1];

            //// for each possible candidate in the cell
            //foreach (int i in currentCell.Candidates)
            for (int i = 0; i <= size; i++)
            {
                // if the candidate is valid
                if (isValidCandidate(row, col, i))
                {
                    // set the value of the cell to the candidate
                    board[row, col].Value = i;


                    // if the board is solved then return true
                    if (BacktrackingR(token))
                    {
                        return true;
                    }
                    board[row, col].Value = 0;
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

        // this is an implementation of backtracking to solve the board
        // that goes in a spiral order from the center of the board to the edges
        public bool BacktrackingSpiral(CancellationToken token, int row, int col, Direction dir, int visited)
        {
            // Continue the spiral traversal until all cells have been visited
            while (visited < size * size)
            {
                // Check if the current cell is empty
                if (board[row, col].Value == 0)
                {
                    // Check if the cell has any valid candidates
                    bool hasCandidates = false;
                    for (int k = 1; k <= size; k++)
                    {
                        if (isValidCandidate(row, col, k))
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
                    for (int k = 1; k <= size; k++)
                    {
                        // Check if the value is valid for the cell
                        if (isValidCandidate(row, col, k))
                        {
                            // Set the value for the cell and recursively solve the rest of the board
                            board[row, col].Value = k;
                            if (BacktrackingSpiral(token, row, col, dir, visited))
                            {
                                return true;
                            }
                            else
                            {
                                // Backtrack and try the next value
                                board[row, col].Value = 0;
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
                        if (col == size || board[row, col].Value != 0)
                        {
                            dir = Direction.Down;
                            col--;
                            row++;
                        }
                        break;
                    case Direction.Down:
                        row++;
                        if (row == size || board[row, col].Value != 0)
                        {
                            dir = Direction.Left;
                            row--;
                            col--;
                        }
                        break;
                    case Direction.Left:
                        col--;
                        if (col < 0 || board[row, col].Value != 0)
                        {
                            dir = Direction.Up;
                            col++;
                            row--;
                        }
                        break;
                    case Direction.Up:
                        row--;
                        if (row < 0 || board[row, col].Value != 0)
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

        private void updateAffectedCells(int row, int col)
        {
            int value = board[row, col].Value;

            // update the candidates of the cells in the same row
            for (int i = 0; i < size; i++)
            {
                if (i != col)
                {
                    board[row, i].Candidates.Remove(value);
                }
            }

            // update the candidates of the cells in the same column
            for (int i = 0; i < size; i++)
            {
                if (i != row)
                {
                    board[i, col].Candidates.Remove(value);
                }
            }

            // update the candidates of the cells in the same block
            int blockRowStart = row - row % blockSize;
            int blockColStart = col - col % blockSize;
            for (int i = blockRowStart; i < blockRowStart + blockSize; i++)
            {
                for (int j = blockColStart; j < blockColStart + blockSize; j++)
                {
                    if (i != row && j != col)
                    {
                        board[i, j].Candidates.Remove(value);
                    }
                }
            }
        }

        // returns the next empty cell on the board, or null if there are no more empty cells
        private int[] getNextEmptyCell()
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

        // returns the last empty cell on the board, or null if there are no more empty cells
        private int[] getLastEmptyCell()
        {
            for (int i = size - 1; i >= 0; i--)
            {
                for (int j = size - 1; j >= 0; j--)
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
        private bool isValidCandidate(int row, int col, int candidate)
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
        public bool Eliminate()
        {
            bool changesMade = false;

            // create a HashSet to store the candidates of each cell
            HashSet<int> candidates;

            // loop through all the cells of the board
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    // if the cell is empty
                    if (board[i, j].Value == 0)
                    {
                        // initialize the HashSet with the candidates of the cell
                        candidates = new HashSet<int>(board[i, j].Candidates);

                        // remove the values of the cells in the same row, column, and block
                        for (int k = 0; k < size; k++)
                        {
                            if (k != j && board[i, k].Value != 0)
                            {
                                candidates.Remove(board[i, k].Value);
                            }
                            if (k != i && board[k, j].Value != 0)
                            {
                                candidates.Remove(board[k, j].Value);
                            }
                        }
                        int blockRowStart = i - i % blockSize;
                        int blockColStart = j - j % blockSize;
                        for (int k = blockRowStart; k < blockRowStart + blockSize; k++)
                        {
                            for (int l = blockColStart; l < blockColStart + blockSize; l++)
                            {
                                if (k != i && l != j && board[k, l].Value != 0)
                                {
                                    candidates.Remove(board[k, l].Value);
                                }
                            }
                        }

                        // if the cell has only one candidate left
                        if (candidates.Count == 1)
                        {
                            // set the value of the cell to the candidate
                            board[i, j].Value = candidates.First();
                            board[i, j].Candidates.Clear();

                            // update the candidates of the affected cells
                            updateAffectedCells(i, j);

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
        public bool hiddenSingles()
        {
            // flag to check if any changes have been made to the board
            bool changesMade = false;

            // for each row
            for (int row = 0; row < size; row++)
            {
                // for each value
                for (int value = 1; value <= size; value++)
                {
                    // count the number of cells in the row that have the value as a candidate
                    int count = 0;
                    int col = 0;
                    for (int i = 0; i < size; i++)
                    {
                        if (board[row, i].Candidates.Contains(value))
                        {
                            count++;
                            col = i;
                        }
                    }

                    // if there is only one cell in the row that has the value as a candidate
                    // then set the value of that cell to the value
                    if (count == 1)
                    {
                        board[row, col].Value = value;
                        board[row, col].Candidates.Clear();
                        updateAffectedCells(row, col);
                        changesMade = true;
                    }
                }
            }

            // for each column
            for (int col = 0; col < size; col++)
            {
                // for each value
                for (int value = 1; value <= size; value++)
                {
                    // count the number of cells in the column that have the value as a candidate
                    int count = 0;
                    int row = 0;
                    for (int i = 0; i < size; i++)
                    {
                        if (board[i, col].Candidates.Contains(value))
                        {
                            count++;
                            row = i;
                        }
                    }

                    // if there is only one cell in the column that has the value as a candidate
                    // then set the value of that cell to the value
                    if (count == 1)
                    {
                        board[row, col].Value = value;
                        updateAffectedCells(row, col);
                        changesMade = true;
                    }
                }
            }

            // for each block
            for (int blockRow = 0; blockRow < blockSize; blockRow++)
            {
                for (int blockCol = 0; blockCol < blockSize; blockCol++)
                {
                    // for each value
                    for (int value = 1; value <= size; value++)
                    {
                        // count the number of cells in the block that have the value as a candidate
                        int count = 0;
                        int row = 0;
                        int col = 0;
                        for (int i = blockRow * blockSize; i < blockRow * blockSize + blockSize; i++)
                        {
                            for (int j = blockCol * blockSize; j < blockCol * blockSize + blockSize; j++)
                            {
                                if (board[i, j].Candidates.Contains(value))
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
                            board[row, col].Value = value;
                            updateAffectedCells(row, col);
                            changesMade = true;
                        }
                    }
                }
            }
            // return true if any changes were made, false otherwise
            return changesMade;
        }

        // checks for and eliminates naked pairs in the given row
        public bool checkRowForNakedPairs(int row)
        {
            bool changesMade = false;

            for (int j = 0; j < size; j++)
            {
                // check for naked pairs in the row
                if (board[row, j].Value == 0 && board[row, j].Candidates.Count == 2)
                {
                    for (int k = j + 1; k < size; k++)
                    {
                        if (board[row, k].Value == 0 && board[row, k].Candidates.Count == 2 && board[row, j].Candidates.SetEquals(board[row, k].Candidates))
                        {
                            // eliminate the candidates of the naked pair from the other cells in the row
                            for (int l = 0; l < size; l++)
                            {
                                if (l != j && l != k)
                                {
                                    if (board[row, l].Candidates.Remove(board[row, j].Candidates.ElementAt(0)))
                                    {
                                        changesMade = true;
                                    }
                                    if (board[row, l].Candidates.Remove(board[row, j].Candidates.ElementAt(1)))
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
        public bool checkColumnForNakedPairs(int col)
        {
            bool changesMade = false;

            for (int j = 0; j < size; j++)
            {
                // check for naked pairs in the column
                if (board[j, col].Value == 0 && board[j, col].Candidates.Count == 2)
                {
                    for (int k = j + 1; k < size; k++)
                    {
                        if (board[k, col].Value == 0 && board[k, col].Candidates.Count == 2 && board[j, col].Candidates.SetEquals(board[k, col].Candidates))
                        {
                            // eliminate the candidates of the naked pair from the other cells in the column
                            for (int l = 0; l < size; l++)
                            {
                                if (l != j && l != k)
                                {
                                    if (board[l, col].Candidates.Remove(board[j, col].Candidates.ElementAt(0)))
                                    {
                                        changesMade = true;
                                    }
                                    if (board[l, col].Candidates.Remove(board[j, col].Candidates.ElementAt(1)))
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
        public bool checkBlockForNakedPairs(int blockRowStart, int blockColStart)
        {
            bool changesMade = false;

            for (int j = blockRowStart; j < blockRowStart + blockSize; j++)
            {
                for (int k = blockColStart; k < blockColStart + blockSize; k++)
                {
                    if (board[j, k].Value == 0 && board[j, k].Candidates.Count == 2)
                    {
                        for (int l = blockRowStart; l < blockRowStart + blockSize; l++)
                        {
                            for (int m = blockColStart; m < blockColStart + blockSize; m++)
                            {
                                if (l != j && m != k && board[l, m].Value == 0 && board[l, m].Candidates.Count == 2
                                    && board[j, k].Candidates.SetEquals(board[l, m].Candidates))
                                {
                                    // eliminate the candidates of the naked pair from the other cells in the block
                                    for (int n = blockRowStart; n < blockRowStart + blockSize; n++)
                                    {
                                        for (int p = blockColStart; p < blockColStart + blockSize; p++)
                                        {
                                            if (n != j && n != l && p != k && p != m)
                                            {
                                                if (board[n, p].Candidates.Remove(board[j, k].Candidates.ElementAt(0)))
                                                {
                                                    changesMade = true;
                                                }
                                                if (board[n, p].Candidates.Remove(board[j, k].Candidates.ElementAt(1)))
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
        public bool nakedPairs()
        {
            bool changesMade = false;

            // check for and eliminate naked pairs in each row
            for (int i = 0; i < size; i++)
            {
                changesMade |= checkRowForNakedPairs(i);
            }

            // check for and eliminate naked pairs in each column
            for (int i = 0; i < size; i++)
            {
                changesMade |= checkColumnForNakedPairs(i);
            }

            // check for and eliminate naked pairs in each block
            for (int i = 0; i < size; i += blockSize)
            {
                for (int j = 0; j < size; j += blockSize)
                {
                    changesMade |= checkBlockForNakedPairs(i, j);
                }
            }

            return changesMade;
        }

        // check for and elimiantes naked triples in th given block
        private bool checkBlockForNakedTriples(int rowStart, int colStart)
        {
            bool changed = false;

            List<int[]> triples = new List<int[]>();

            // find all triples in the block
            for (int i = rowStart; i < rowStart + blockSize; i++)
            {
                for (int j = colStart; j < colStart + blockSize; j++)
                {
                    if (board[i, j].Candidates.Count == 3)
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
                    if (haveSameCandidates(triples[i], triples[j]))
                    {
                        // if the triples have the same candidates, eliminate those candidates from the other cells in the block
                        changed |= eliminateCandidatesFromOtherCellsInBlock(board[triples[i][0], triples[i][1]].Candidates, triples[i], triples[j], rowStart, colStart);
                    }
                }
            }

            return changed;
        }

        // Check if the given row has any naked triples
        // A naked triple is 3 cells in a row that have 3 candidates each and those candidates are the same for all 3 cells
        private bool checkRowForNakedTriples(int row)
        {
            bool changed = false;

            // Find all cells in the row that have 3 candidates
            List<int[]> triples = new List<int[]>();
            for (int i = 0; i < size; i++)
            {
                if (board[row, i].Candidates.Count == 3)
                {
                    triples.Add(new int[] { row, i });
                }
            }

            // Check if any of the triples have the same candidates
            for (int i = 0; i < triples.Count; i++)
            {
                for (int j = i + 1; j < triples.Count; j++)
                {
                    if (haveSameCandidates(triples[i], triples[j]))
                    {
                        // If the triples have the same candidates, eliminate those candidates from the other cells in the row
                        changed |= eliminateCandidatesFromOtherCellsInRow(board[triples[i][0], triples[i][1]].Candidates, triples[i], triples[j]);
                    }
                }
            }

            return changed;
        }

        private bool checkColumnForNakedTriples(int col)
        {
            // flag to track if any changes have been made
            bool changed = false;

            // list to store all cells with 3 candidates in the given column
            List<int[]> triples = new List<int[]>();

            // find all triples in the column
            for (int i = 0; i < size; i++)
            {
                if (board[i, col].Candidates.Count == 3)
                {
                    triples.Add(new int[] { i, col });
                }
            }

            // check if any of the triples have the same candidates
            for (int i = 0; i < triples.Count; i++)
            {
                for (int j = i + 1; j < triples.Count; j++)
                {
                    if (haveSameCandidates(triples[i], triples[j]))
                    {
                        // if the triples have the same candidates, eliminate those candidates from the other cells in the column
                        changed |= eliminateCandidatesFromOtherCellsInCol(board[triples[i][0], triples[i][1]].Candidates, triples[i], triples[j]);
                    }
                }
            }

            return changed;
        }

        // helper function to check if two cells have the same candidates
        private bool haveSameCandidates(int[] cell1, int[] cell2)
        {
            HashSet<int> candidates1 = board[cell1[0], cell1[1]].Candidates;
            HashSet<int> candidates2 = board[cell2[0], cell2[1]].Candidates;

            return candidates1.SetEquals(candidates2);
        }

        // helper function to eliminate candidates from other cells in a block
        private bool eliminateCandidatesFromOtherCellsInBlock(HashSet<int> candidates, int[] cell1,
            int[] cell2, int rowStart, int colStart)
        {
            bool changed = false;
            for (int i = rowStart; i < rowStart + blockSize; i++)
            {
                for (int j = colStart; j < colStart + blockSize; j++)
                {
                    if (board[i, j].Equals(cell1) || board[i, j].Equals(cell2))
                    {
                        continue;
                    }
                    changed |= board[i, j].Candidates.RemoveWhere(x => candidates.Contains(x)) > 0;
                }
            }
            return changed;
        }


        // helper function to eliminate candidates from other cells in a row
        private bool eliminateCandidatesFromOtherCellsInRow(HashSet<int> candidates, int[] cell1, int[] cell2)
        {
            bool changed = false;
            for (int j = 0; j < size; j++)
            {
                if (board[cell1[0], j].Equals(cell1) || board[cell1[0], j].Equals(cell2))
                {
                    continue;
                }
                changed |= board[cell1[0], j].Candidates.RemoveWhere(x => candidates.Contains(x)) > 0;
            }
            return changed;
        }

        // helper function to eliminate candidates from other cells in a column
        private bool eliminateCandidatesFromOtherCellsInCol(HashSet<int> candidates, int[] cell1, int[] cell2)
        {
            bool changed = false;
            for (int i = 0; i < size; i++)
            {
                if (board[i, cell1[1]].Equals(cell1) || board[i, cell1[1]].Equals(cell2))
                {
                    continue;
                }
                changed |= board[i, cell1[1]].Candidates.RemoveWhere(x => candidates.Contains(x)) > 0;
            }
            return changed;
        }

        // this is an implementation of naked triples algoritm 
        public bool nakedTriples()
        {
            bool changed = false;

            // check each block for naked triples
            for (int row = 0; row < size; row += blockSize)
            {
                for (int col = 0; col < size; col += blockSize)
                {
                    changed |= checkBlockForNakedTriples(row, col);
                }
            }

            // check each row for naked triples
            for (int row = 0; row < size; row++)
            {
                changed |= checkRowForNakedTriples(row);
            }

            // check each column for naked triples
            for (int col = 0; col < size; col++)
            {
                changed |= checkColumnForNakedTriples(col);
            }

            return changed;
        }

        // checks for and eliminates naked quads in the board
        public bool nakedQuads()
        {
            bool changesMade = false;

            // check for and eliminate hidden quads in each row
            for (int i = 0; i < size; i++)
            {
                changesMade |= checkRowForNakedQuads(i);
            }

            // check for and eliminate hidden quads in each column
            for (int i = 0; i < size; i++)
            {
                changesMade |= checkColumnForNakedQuads(i);
            }

            // check for and eliminate hidden quads in each block
            for (int i = 0; i < size; i += blockSize)
            {
                for (int j = 0; j < size; j += blockSize)
                {
                    changesMade |= checkBlockForNakedQuads(i, j);
                }
            }

            return changesMade;
        }

        // Check if the given row has any naked quads
        // A naked quad is 4 cells in a row that have 4 candidates each and those candidates are the same for all 4 cells
        private bool checkRowForNakedQuads(int row)
        {
            bool changed = false;

            // Find all cells in the row that have 4 candidates
            List<int[]> quads = new List<int[]>();
            for (int i = 0; i < size; i++)
            {
                if (board[row, i].Candidates.Count == 4)
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
                            if (haveSameCandidates(quads[i], quads[j], quads[k], quads[l]))
                            {
                                // If the quads have the same candidates, eliminate those candidates from the other cells in the row
                                changed |= eliminateCandidatesFromOtherCellsInRowQ(board[quads[i][0], quads[i][1]].Candidates, quads[i], quads[j], quads[k], quads[l]);
                            }
                        }
                    }
                }
            }

            return changed;
        }

        private bool eliminateCandidatesFromOtherCellsInRowQ(HashSet<int> candidates, int[] coord1, int[] coord2, int[] coord3, int[] coord4)
        {
            bool changed = false;

            // Get the row of the first cell
            int row = coord1[0];

            // Go through each cell in the row
            for (int i = 0; i < size; i++)
            {
                // Check if the cell is not one of the cells to ignore
                if ((coord1[0] == row && coord1[1] == i) || (coord2[0] == row && coord2[1] == i) || (coord3[0] == row && coord3[1] == i) || (coord4[0] == row && coord4[1] == i))
                {
                    continue;
                }

                // Eliminate the candidates from the cell
                changed |= board[row, i].Candidates.RemoveWhere(x => candidates.Contains(x)) > 0;
            }

            return changed;
        }

        private bool checkColumnForNakedQuads(int col)
        {
            bool changed = false;

            // Find all cells in the column that have 4 candidates
            List<int[]> quads = new List<int[]>();
            for (int i = 0; i < size; i++)
            {
                if (board[i, col].Candidates.Count == 4)
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
                            if (haveSameCandidates(quads[i], quads[j], quads[k], quads[l]))
                            {
                                // If the quads have the same candidates, eliminate those candidates from the other cells in the column
                                changed |= eliminateCandidatesFromOtherCellsInColumnQ(board[quads[i][0], quads[i][1]].Candidates, quads[i], quads[j], quads[k], quads[l]);
                            }
                        }
                    }
                }
            }

            return changed;
        }

        private bool eliminateCandidatesFromOtherCellsInColumnQ(HashSet<int> candidates, int[] coord1, int[] coord2, int[] coord3, int[] coord4)
        {
            bool changed = false;

            // Get the column of the first cell
            int col = coord1[1];

            // Go through each cell in the column
            for (int i = 0; i < size; i++)
            {
                // Check if the cell is not one of the cells to ignore
                if ((coord1[0] == i && coord1[1] == col) || (coord2[0] == i && coord2[1] == col) || (coord3[0] == i && coord3[1] == col) || (coord4[0] == i && coord4[1] == col))
                {
                    continue;
                }

                // Eliminate the candidates from the cell
                changed |= board[i, col].Candidates.RemoveWhere(x => candidates.Contains(x)) > 0;
            }

            return changed;
        }

        private bool checkBlockForNakedQuads(int rowStart, int colStart)
        {
            bool changed = false;

            // Find all cells in the block that have 4 candidates
            List<int[]> quads = new List<int[]>();
            for (int i = rowStart; i < rowStart + blockSize; i++)
            {
                for (int j = colStart; j < colStart + blockSize; j++)
                {
                    if (board[i, j].Candidates.Count == 4)
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
                            if (haveSameCandidates(quads[i], quads[j], quads[k], quads[l]))
                            {
                                // If the quads have the same candidates, eliminate those candidates from the other cells in the block
                                changed |= eliminateCandidatesFromOtherCellsInBlockQ(board[quads[i][0], quads[i][1]].Candidates, quads[i], quads[j], quads[k], quads[l], rowStart, colStart);
                            }
                        }
                    }
                }
            }

            return changed;
        }

        private bool eliminateCandidatesFromOtherCellsInBlockQ(HashSet<int> candidates, int[] coord1, int[] coord2, int[] coord3,
            int[] coord4, int rowStart, int colStart)
        {
            bool changed = false;

            // Go through each cell in the block
            for (int i = rowStart; i < rowStart + blockSize; i++)
            {
                for (int j = colStart; j < colStart + blockSize; j++)
                {
                    // Check if the cell is not one of the cells to ignore
                    if ((coord1[0] == i && coord1[1] == j) || (coord2[0] == i && coord2[1] == j) || (coord3[0] == i && coord3[1] == j) || (coord4[0] == i && coord4[1] == j))
                    {
                        continue;
                    }

                    // Eliminate the candidates from the cell
                    changed |= board[i, j].Candidates.RemoveWhere(x => candidates.Contains(x)) > 0;
                }
            }

            return changed;
        }


        // Check if the given cells have the same candidates
        private bool haveSameCandidates(int[] coord1, int[] coord2, int[] coord3, int[] coord4)
        {
            // Get the candidates for each cell
            HashSet<int> candidates1 = new HashSet<int>(board[coord1[0], coord1[1]].Candidates);
            HashSet<int> candidates2 = new HashSet<int>(board[coord2[0], coord2[1]].Candidates);
            HashSet<int> candidates3 = new HashSet<int>(board[coord3[0], coord3[1]].Candidates);
            HashSet<int> candidates4 = new HashSet<int>(board[coord4[0], coord4[1]].Candidates);

            // Check if the candidates are the same for all cells
            return candidates1.SetEquals(candidates2) && candidates2.SetEquals(candidates3) && candidates3.SetEquals(candidates4);
        }


        // implementing intersection - pointining doubles, triples and box line reduction

        // this is the implementation of the poiniting doubles algortim
        public bool PointingDoubles()
        {
            bool changed = false;

            for (int i = 0; i < size; i++)
            {
                // check rows for pointing doubles
                changed |= checkRowForPointingDoubles(i);

                // check columns for pointing doubles
                changed |= checkColumnForPointingDoubles(i);

                // check blocks for pointing doubles
                int rowStart = (i / blockSize) * blockSize;
                int colStart = (i % blockSize) * blockSize;
                changed |= checkBlockForPointingDoubles(rowStart, colStart);
            }

            return changed;
        }

        // this is a helper function that will go over the given row and check for pointing doubles
        private bool checkRowForPointingDoubles(int row)
        {
            bool changed = false;

            List<int[]> doubles = new List<int[]>();

            // find all doubles in the row
            for (int i = 0; i < size; i++)
            {
                if (board[row, i].Candidates.Count == 2)
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
                        changed |= eliminateCandidatesFromOtherCellsInCol(board[doubles[i][0], doubles[i][1]].Candidates, doubles[i], doubles[j]);
                    }
                }
            }

            return changed;
        }

        // this is a helper function that will go over the given col and check for pointing doubles
        private bool checkColumnForPointingDoubles(int col)
        {
            bool changed = false;

            // create a dictionary to store the candidate values and the cells that contain them
            Dictionary<int, List<int[]>> candidatesToCells = new Dictionary<int, List<int[]>>();

            // iterate through the cells in the column
            for (int i = 0; i < size; i++)
            {
                // get the candidates of the current cell
                HashSet<int> candidates = board[i, col].Candidates;

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
                    changed |= eliminateCandidatesFromOtherCellsInCol(new HashSet<int> { entry.Key }, entry.Value[0], entry.Value[1]);
                }
            }

            return changed;
        }

        // this is a helper function that will go over the given block and check for pointing doubles
        private bool checkBlockForPointingDoubles(int rowStart, int colStart)
        {
            bool changed = false;

            List<int[]> pairs = new List<int[]>();

            // find all pairs in the block
            for (int i = rowStart; i < rowStart + blockSize; i++)
            {
                for (int j = colStart; j < colStart + blockSize; j++)
                {
                    if (board[i, j].Candidates.Count == 2)
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
                    if (haveSameCandidates(pairs[i], pairs[j]))
                    {
                        // if the pairs have the same candidates, eliminate those candidates from the other cells in the block
                        changed |= eliminateCandidatesFromOtherCellsInBlock(board[pairs[i][0], pairs[i][1]].Candidates, pairs[i], pairs[j], rowStart, colStart);
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
            for (int row = 0; row < size; row++)
            {
                changed |= checkRowForPointingTriples(row);
            }

            // check columns for pointing triples
            for (int col = 0; col < size; col++)
            {
                changed |= checkColumnForPointingTriples(col);
            }

            // check blocks for pointing triples
            for (int row = 0; row < size; row += blockSize)
            {
                for (int col = 0; col < size; col += blockSize)
                {
                    changed |= checkBlockForPointingTriples(row, col);
                }
            }

            return changed;
        }

        // this is a helper function that will go over the given row and check for pointing triples
        private bool checkRowForPointingTriples(int row)
        {
            bool changed = false;

            List<int[]> triples = new List<int[]>();

            // find all triples in the row
            for (int i = 0; i < size; i++)
            {
                if (board[row, i].Candidates.Count == 3)
                {
                    triples.Add(new int[] { row, i });
                }
            }

            // check if any of the triples have the same candidates
            for (int i = 0; i < triples.Count; i++)
            {
                for (int j = i + 1; j < triples.Count; j++)
                {
                    if (haveSameCandidates(triples[i], triples[j]))
                    {
                        // if the triples have the same candidates, eliminate those candidates from the other cells in the row
                        HashSet<int> candidates = new HashSet<int>(board[triples[i][0], triples[i][1]].Candidates);
                        candidates.IntersectWith(board[triples[j][0], triples[j][1]].Candidates);
                        changed |= eliminateCandidatesFromOtherCellsInRow(candidates, triples[i], triples[j]);
                    }
                }
            }

            return changed;
        }

        // this is a helper function that will go over the given col and check for pointing triples
        private bool checkColumnForPointingTriples(int col)
        {
            bool changed = false;

            List<int[]> triples = new List<int[]>();

            // find all triples in the column
            for (int i = 0; i < size; i++)
            {
                if (board[i, col].Candidates.Count == 3)
                {
                    triples.Add(new int[] { i, col });
                }
            }

            // check if any of the triples have the same candidates
            for (int i = 0; i < triples.Count; i++)
            {
                for (int j = i + 1; j < triples.Count; j++)
                {
                    if (haveSameCandidates(triples[i], triples[j]))
                    {
                        // if the triples have the same candidates, eliminate those candidates from the other cells in the column
                        HashSet<int> candidates = new HashSet<int>(board[triples[i][0], triples[i][1]].Candidates);
                        candidates.UnionWith(board[triples[j][0], triples[j][1]].Candidates);
                        changed |= eliminateCandidatesFromOtherCellsInCol(candidates, triples[i], triples[j]);
                    }
                }
            }

            return changed;
        }

        // this is a helper function that will go over the given box and check for pointing triples
        private bool checkBlockForPointingTriples(int rowStart, int colStart)
        {
            bool changed = false;

            List<int[]> triples = new List<int[]>();

            // find all triples in the block
            for (int i = rowStart; i < rowStart + blockSize; i++)
            {
                for (int j = colStart; j < colStart + blockSize; j++)
                {
                    if (board[i, j].Candidates.Count == 3)
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
                    if (haveSameCandidates(triples[i], triples[j]))
                    {
                        // if the triples have the same candidates, check if they point to a cell in a row or column
                        bool rowChanged = checkTripleForPointingRow(triples[i], triples[j]);
                        bool colChanged = checkTripleForPointingCol(triples[i], triples[j]);
                        if (rowChanged || colChanged)
                        {
                            // if the triples point to a cell in a row or column, eliminate those candidates from that cell
                            changed |= eliminateCandidatesFromPointedCell(triples[i], triples[j], rowStart, colStart);
                        }
                    }
                }
            }

            return changed;
        }

        // This function checks if a pair of cells in a triple share candidates with a cell in the same row as the triple.
        // If they do, it eliminates those shared candidates from the other cells in the row.
        private bool checkTripleForPointingRow(int[] cell1, int[] cell2)
        {
            // create a hashset to store the candidates of the two cells
            HashSet<int> candidates = new HashSet<int>();
            candidates.UnionWith(board[cell1[0], cell1[1]].Candidates);
            candidates.UnionWith(board[cell2[0], cell2[1]].Candidates);

            // check if the hashset has three candidates
            if (candidates.Count == 3)
            {
                // find the third cell in the row with the same candidates
                for (int i = 0; i < size; i++)
                {
                    if (board[cell1[0], i].Candidates.SetEquals(candidates))
                    {
                        // if the third cell is found, eliminate the candidates from other cells in the row
                        return eliminateCandidatesFromOtherCellsInRow(candidates, cell1, cell2);
                    }
                }
            }

            // if the third cell is not found or the hashset does not have three candidates, return false
            return false;
        }

        // Check if the given triple of cells in the same column points to a candidate that can be eliminated from other cells in the column.
        // If so, eliminate the candidate and return true. Otherwise, return false.
        private bool checkTripleForPointingCol(int[] cell1, int[] cell2)
        {
            // Get the common candidates of the two cells
            HashSet<int> commonCandidates = new HashSet<int>(board[cell1[0], cell1[1]].Candidates);
            commonCandidates.IntersectWith(board[cell2[0], cell2[1]].Candidates);

            // If the common candidates form a triple with the candidates of the third cell in the column,
            // eliminate the common candidates from the other cells in the column
            int[] cell3 = new int[] { cell1[0], cell1[1] == 0 ? cell1[1] + 2 : cell1[1] - 2 };
            if (commonCandidates.IsSubsetOf(board[cell3[0], cell3[1]].Candidates))
            {
                return eliminateCandidatesFromOtherCellsInCol(commonCandidates, cell1, cell2);
            }
            return false;
        }

        // This function removes candidates from the cells indicated by the input triples (cell1 and cell2)
        // if they are part of a pointing triple in the block with top-left corner at (rowStart, colStart).
        // It returns a boolean indicating whether any candidates were removed.
        private bool eliminateCandidatesFromPointedCell(int[] triple1, int[] triple2, int rowStart, int colStart)
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
                HashSet<int> candidates = new HashSet<int>();
                candidates.UnionWith(board[triple1[0], triple1[1]].Candidates);
                candidates.UnionWith(board[triple1[0], triple1[1]].Candidates);
                candidates.UnionWith(board[triple2[0], triple2[1]].Candidates);
                changed |= board[pointedRow, pointedCol].Candidates.RemoveWhere(x => candidates.Contains(x)) > 0;
            }

            return changed;
        }

        // this is the implemention of the box line reduction algoritm
        public bool boxLineReduction()
        {
            bool changed = false;

            // check each block
            for (int i = 0; i < size; i += blockSize)
            {
                for (int j = 0; j < size; j += blockSize)
                {
                    // check rows in the block
                    for (int row = i; row < i + blockSize; row++)
                    {
                        // get the candidates in the row
                        HashSet<int> rowCandidates = new HashSet<int>();
                        for (int col = j; col < j + blockSize; col++)
                        {
                            rowCandidates.UnionWith(board[row, col].Candidates);
                        }

                        // check columns in the block
                        for (int col = j; col < j + blockSize; col++)
                        {
                            // get the candidates in the column
                            HashSet<int> colCandidates = new HashSet<int>();
                            for (int r = i; r < i + blockSize; r++)
                            {
                                colCandidates.UnionWith(board[r, col].Candidates);
                            }

                            // if the intersection of the row candidates and column candidates has size 2 or 3,
                            // eliminate those candidates from the other cells in the row or column
                            HashSet<int> intersection = new HashSet<int>(rowCandidates);
                            intersection.IntersectWith(colCandidates);
                            if (intersection.Count == 2 || intersection.Count == 3)
                            {
                                for (int r = 0; r < size; r++)
                                {
                                    if (r >= i && r < i + blockSize)
                                    {
                                        // eliminate candidates from the other cells in the column
                                        changed |= board[r, col].Candidates.RemoveWhere(x => intersection.Contains(x)) > 0;
                                    }
                                    else
                                    {
                                        // eliminate candidates from the other cells in the row
                                        changed |= board[row, r].Candidates.RemoveWhere(x => intersection.Contains(x)) > 0;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return changed;
        }



        // dancing linsk algorithm
        public bool DancingLinks(Cell[,] board)
        {
            // convert the board of cells to a matrix of nodes
            Node[][] matrix = ConvertBoard(board, size);

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
                            board[row,col].Value = node.Value;
                            board[row,col].Solved = true;
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

