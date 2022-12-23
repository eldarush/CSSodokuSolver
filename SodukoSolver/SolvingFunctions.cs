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
        public static bool Eliminate()
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
        public static bool hiddenSingles()
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
        public static bool checkRowForNakedPairs(int row)
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
        public static bool checkColumnForNakedPairs(int col)
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
        public static bool checkBlockForNakedPairs(int blockRowStart, int blockColStart)
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
        public static bool nakedPairs()
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

        private static bool checkBlockForNakedTriples(int rowStart, int colStart)
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
        private static bool checkRowForNakedTriples(int row)
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

        private static bool checkColumnForNakedTriples(int col)
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
        private static bool haveSameCandidates(int[] cell1, int[] cell2)
        {
            HashSet<int> candidates1 = board[cell1[0], cell1[1]].Candidates;
            HashSet<int> candidates2 = board[cell2[0], cell2[1]].Candidates;

            return candidates1.SetEquals(candidates2);
        }

        // helper function to eliminate candidates from other cells in a block
        private static bool eliminateCandidatesFromOtherCellsInBlock(HashSet<int> candidates, int[] cell1,
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
        private static bool eliminateCandidatesFromOtherCellsInRow(HashSet<int> candidates, int[] cell1, int[] cell2)
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
        private static bool eliminateCandidatesFromOtherCellsInCol(HashSet<int> candidates, int[] cell1, int[] cell2)
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
        public static bool nakedTriples()
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

    }

}

