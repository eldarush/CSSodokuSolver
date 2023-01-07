using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SodukoSolver.Algoritms.HelperFunctions;
using static SodukoSolver.Algoritms.ValidatingFunctions;

namespace SodukoSolver.BoardSolvers
{
    /// <summary>
    /// class that inherits from the BoardSolver class and solves the given SudokuBoard using backtracking
    /// </summary>
    public class BackTracking : BoardSolver
    {

        // the helper mask that will be needed to modify valid candidates in the arrays
        public int[] HelperMask;

        // the allowed values for each cell in a row
        public int[] RowValues;

        // the allowed values for each cell in a column
        public int[] ColumnValues;

        // the allowed values for each cell in a block
        public int[] BlockValues;

        #region backtracking functions

        /// <summary>
        /// constructor that gets the SudokuBoard and the size of the SudokuBoard
        /// and initialzes the helper mask and the allowed values for each cell
        /// </summary>
        /// <param name="board">the SudokuBoard</param>
        /// <param name="size">the size</param>
        public BackTracking(int[,] board, int size) : base(board, size)
        {
            // initialize the masks and the allowed values for each cell
            InitializeMasks();

            // copy the bit masks from the validating class because if the SudokuBoard
            // is valid then they are correct
            CopyBitMasks(VRowValues, VColValues, VBlockValues, VHelperMask,
                     out RowValues, out ColumnValues, out BlockValues, out HelperMask);
        }

        /// <summary>
        /// initialize the masks that are used in this class
        /// </summary>
        private void InitializeMasks()
        {
            // create new array with the Size of one row
            HelperMask = new int[Size];
            RowValues = new int[Size];
            ColumnValues = new int[Size];
            BlockValues = new int[Size];
        }

        /// <summary>
        /// update the valid candidates for row, col and block
        /// </summary>
        /// <param name="row">row of value</param>
        /// <param name="col">col of value</param>
        /// <param name="value">the value itself</param>
        private void UpdateCandidateValues(int row, int col, int value)
        {
            //Use the bitwise OR operator (|) to add the mask at index value - 1 in the masks array to the element at index row in the RowValues array.
            //This has the effect of setting the bit corresponding to value in the binary representation of the element to 1,
            //indicating that the value is valid for the row.
            RowValues[row] |= HelperMask[value - 1];
            ColumnValues[col] |= HelperMask[value - 1];
            int squareLocation = row / BlockSize * BlockSize + col / BlockSize;
            BlockValues[squareLocation] |= HelperMask[value - 1];
        }

        /// <summary>
        /// This function determines the valid values for a cell at a specific row and column int the Board
        /// </summary>
        /// <param name="row">the row</param>
        /// <param name="col">the col</param>
        /// <returns>the amount of possible candidate values this cell has</returns>
        private int GetPossibleValuesForGivenCell(int row, int col)
        {
            // Calculate the index of the block that the cell belongs to by using the formula (row / BlockSize) * BlockSize + col / BlockSize.
            int squareLocation = row / BlockSize * BlockSize + col / BlockSize;

            // Use the bitwise OR operator (|) to compute the sum of the non-valid values for the row, column, and box containing the cell.
            // This is done by performing a bitwise OR operation between the element at index row in the RowValues array,
            // the element at index col in the ColumnValues array,
            // and the element at index squareLocation in the BlockValues array.
            int invalidCandidates = RowValues[row] | ColumnValues[col] | BlockValues[squareLocation];

            // Use the bitwise NOT operator (~) to negate all of the non-valid values,
            // effectively turning the 0 bits into 1 bits and the 1 bits into 0 bits. 
            int validCandidates = ~invalidCandidates;

            // Use the bitwise AND operator (&) to compute the intersection between the int
            // of valid values and the range of possible values for the cell.
            // This is done by performing a bitwise AND operation between the int and((1 << Size - 1),
            // which represents a bitmask with all bits set to 1 up to the Size of the Board array.
            validCandidates &= (1 << Size) - 1;

            // Return the result of the bitwise AND operation.
            return validCandidates;
        }

        /// <summary>
        /// function that gets a location in the Board and a value and returns if it valid to put 
        /// value inside the Board in this location, this is done using the possible values held in the arrays 
        /// for row, col and block
        /// </summary>
        /// <param name="row">row of value</param>
        /// <param name="col">col of value</param>
        /// <param name="value">the value</param>
        /// <returns>if the value is valid to be insrted in the current place</returns>
        private bool IsValidBits(int row, int col, int value)
        {
            int squareLocation = row / BlockSize * BlockSize + col / BlockSize;
            // Use the bitwise AND operator (&) to check if the value is valid for the row, col and block
            // This is done by performing a bitwise AND operation between the element at index row in the RowValues array
            // and the mask at index value in the masks array.
            // If the result is 0, it means that the value is valid for the row
            // (i.e., the bit corresponding to value is not set in the binary representation of the element).
            return (RowValues[row] & HelperMask[value]) == 0
                && (ColumnValues[col] & HelperMask[value]) == 0
                && (BlockValues[squareLocation] & HelperMask[value]) == 0;

        }

        /// <summary>
        /// function that goes over the Board and find the next cell that has the lesat possible candidates
        /// </summary>
        /// <returns>the row and col of the cell</returns>
        private (int bestRow, int BestCol) GetNextBestCell()
        {
            // row and col are set by deafult to -1, so that we know that if this function
            // returned -1, it means all the cells are filled
            int bestRow = -1;
            int bestCol = -1;

            // amount of valid candidates for each location
            int amountOfValidCandidates;

            // the current lowest amount of valid candidates
            int lowestAmountOfValidCandidates = Size + 1;

            // go over the Board and find the cell with the least valid candidates for it in the corresponding arrays
            // this search is done from top left to bottom right and if two cells have the same candidates, the one closest to the top left
            // will get picked, this is done to make the algorithm more efficiant
            for (int row = 0; row < Size; row++)
            {
                for (int col = 0; col < Size; col++)
                {
                    // if the vlaue at the current cell isn't 0, move on
                    if (BoardInts[row, col] != 0) continue;
                    // otherwise, get the amount of number candidates for this location
                    amountOfValidCandidates = GetPossibleValuesForGivenCell(row, col);

                    // if the amount of valid candidates for this cell is lower the the lowest amount, change the bestRow and bestCol
                    // and the current lowest amount of valid candidates 
                    if (GetActivatedBits(amountOfValidCandidates) < lowestAmountOfValidCandidates)
                    {
                        bestRow = row;
                        bestCol = col;
                        lowestAmountOfValidCandidates = GetActivatedBits(amountOfValidCandidates);
                    }

                    // if the cell has only one valid candidate, return it, as it is the best cell to start with
                    // and closest to the top left corner
                    if (lowestAmountOfValidCandidates == 1)
                    {
                        // return that cell that can only be filled with one possible value
                        return (bestRow, bestCol);
                    }
                }
            }
            // return the found best row and col
            return (bestRow, bestCol);
        }

        /// <summary>
        /// function that copies the original values of the arrays into the new one's
        /// </summary>
        /// <param name="copyRowValues">original row values</param>
        /// <param name="copyColValues">original col values</param>
        /// <param name="copyBlockValues">original block values</param>
        private void CopyOriginalArraysIntoNewOnes(int[] copyRowValues, int[] copyColValues, int[] copyBlockValues)
        {
            Array.Copy(RowValues, copyRowValues, Size);
            Array.Copy(ColumnValues, copyColValues, Size);
            Array.Copy(BlockValues, copyBlockValues, Size);
        }

        /// <summary>
        /// function that copies the pointers of the affected values to the old saved one's
        /// this also saves the Board that we need to come back to - what the Board looked like
        /// before the failed branch of backtracking messed up it's values
        /// </summary>
        /// <param name="oldBoard">the old Board</param>
        /// <param name="copyRowValues">the copy of the row values</param>
        /// <param name="copyColValues">the copy of the col values</param>
        /// <param name="copyBlockValues">the copy of the block values</param>
        private void RestoreAffectedValuesAndBoard(int[,] oldBoard, int[] copyRowValues, int[] copyColValues, int[] copyBlockValues)
        {
            BoardInts = oldBoard;
            RowValues = copyRowValues;
            ColumnValues = copyColValues;
            BlockValues = copyBlockValues;
        }

        /// <summary>
        /// this is the implementation of the simple elimination algorithm,
        /// what this algorithm does is that it goes over all the possible candidates for each cell
        /// in each house in the Board, and if it is found that in a house there is only one possible
        /// cell with that value, this cell will be filled with that value
        /// </summary>
        private void SimpleElimination()
        {
            // counter for how many possible candidates this cell has
            int possibleCandidates;
            // the amount of activated bits in the value
            int activatedBits;
            // the value
            int value;
            // go over the Board and for each cell, check if there is only one possible candidate for it
            for (int row = 0; row < Size; row++)
            {
                for (int col = 0; col < Size; col++)
                {
                    // if the value at the current cell isn't 0, move on
                    if (BoardInts[row, col] != 0) continue;
                    // otherwise, get the amount of number candidates for this location
                    possibleCandidates = GetPossibleValuesForGivenCell(row, col);
                    // get the amount of activated bits in the value
                    activatedBits = GetActivatedBits(possibleCandidates);
                    // if there is only one possible candidate for this cell, fill it with that value
                    if (activatedBits == 1)
                    {
                        // get the value
                        value = GetIndexOfMostSignificantActivatedBit(possibleCandidates);
                        // fill the cell with that value and update the affected values
                        BoardInts[row, col] = value;
                        UpdateCandidateValues(row, col, value);
                    }
                }
            }
            // return the updated Board
            return;
        }


        // TODO: implement this hidden singles optimization

        /// <summary>
        /// this is an implementation of the hidden singles algorithm,
        /// what this algorithm does is that it goes over all cells in each house and checks if there
        /// is a cell in a house where one of it's candidates only appears once in that cell, if that is
        /// the case then the cell's value automatically becomes that value and the other cell's candidates are updated
        /// to no longer have that candidate
        /// </summary>
        private void HiddenSingles()
        {
            
        }

        /// <summary>
        /// function that solves a sudoku using backtracking algorithm with candidate values updating in real time
        /// </summary>
        /// <returns>if the backtracking managed to solve the Board or not</returns>
        public bool Backtracking()
        {
            // store the original values for the cell and the origianl Board
            int[,] copyBoard = GetBoardIntsCopy(BoardInts,Size);
            int[] copyRowValues = new int[Size];
            int[] copyColValues = new int[Size];
            int[] copyBlockValues = new int[Size];
            // copy the old values into the copy values
            CopyOriginalArraysIntoNewOnes(copyRowValues, copyColValues, copyBlockValues);

            // run the hidden singles algorithm that will fill in as many cells as possible
            SimpleElimination();

            // the row and col of the cell that we are analyzing
            int currentRow, currentCol;
            // get the next best cell
            (currentRow, currentCol) = GetNextBestCell();

            // if the value of one of the row or col was -1, return true because that means that there are no more
            // empty cells that the function can find inside the Board
            if (currentRow == -1 || currentCol == -1) return true;

            // go over the possible values and check if the value is valid or not to put in the cell
            for (int currentValue = 0; currentValue < Size; currentValue++)
            {
                if (IsValidBits(currentRow, currentCol, currentValue))
                {
                    // if the value is valid, the set the Board at this location to be the value and update the affected cells
                    BoardInts[currentRow, currentCol] = currentValue + 1;
                    UpdateCandidateValues(currentRow, currentCol, currentValue + 1);

                    // run the backtracking again, if it works, then return true, else restore the original values
                    if (Backtracking()) return true;

                    // if the current backtracking branch failed, restore the original values
                    RestoreAffectedValuesAndBoard(copyBoard, copyRowValues, copyColValues, copyBlockValues);
                }
            }
            // if the backtrackig failed completelly, return false
            return false;
        }

        /// <summary>
        /// implementation of the solve function, simply returns the result of the backtracking algorithm
        /// and the SudokuBoard will update itself because the algoriths uses the SudokuBoard
        /// </summary>
        /// <returns>if the SudokuBoard is solved or not</returns>
        public override bool Solve()
        {
            return Backtracking();
        }

        /// <summary>
        /// implementation of the get SudokuBoard function, run the solving function and return the SudokuBoard
        /// if it is solved, if not return empty string
        /// </summary>
        /// <returns>the SudokuBoard of the solved SudokuBoard</returns>
        public override string GetSolutionString()
        {
            bool solved = Backtracking();
            if (solved)
            {
                return ConvertToString(BoardInts, Size);
            }
            return "";
        }

        #endregion backtracking functions
    }
}
