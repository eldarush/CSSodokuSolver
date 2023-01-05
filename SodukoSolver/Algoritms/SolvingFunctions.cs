﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SodukoSolver.Algoritms.ValidatingFunctions;
using static SodukoSolver.Algoritms.HelperFunctions;
using System.Diagnostics;
#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace SodukoSolver.Algoritms
{
    public class SolvingFunctions
    {
        // Size of the Board
        public int Size { get; set; }

        // blockSize is the Size of the block that the Board is divided into
        public int BlockSize { get; set; }

        // the Board of ints
        public int[,] BoardInts;

        // the helper mask that will be needed to modify valid candidates in the arrays
        public int[] HelperMask;
        
        // the allowed values for each cell in a row
        public int[] RowValues;
        
        // the allowed values for each cell in a column
        public int[] ColumnValues;
        
        // the allowed values for each cell in a block
        public int[] BlockValues;

        #region new backtracking functions

        /// <summary>
        /// function that gets a value and counts the amount of activated bits in the numbe
        /// Equal to writing CountOnes from System.Numerics.BitOperations
        /// </summary>
        /// <param name="value">thevalue to be converted</param>
        /// <returns>counts the amount of activated bits in the number</returns>
        private int GetActivatedBits(int value)
        {
            // set value to the result of a bitwise AND operation between value and value - 1 and increment the counter
            int bits = 0;
            while (value > 0)
            {
                value &= value - 1;
                bits++;   
            }
            return bits;
        }

        /// <summary>
        /// function that determines the index of the most significant bit that is set to 1 in a binary representation of a given integer value.
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>the index of the most significant bit that is set to 1</returns>
        private int GetIndexOfMostSignificantActivatedBit(int value)
        {
            int bits = 0;
            while (value != 0)
            {
                // divide value by 2 using right shift and increment the counter
                value >>= 1;
                bits++;
            }
            return bits;
        }

        /// <summary>
        /// initialize the helperMask
        /// </summary>
        private void SetHelperMask()
        {
            // create new array with the Size of one row
            HelperMask = new int[Size];
            // go over the Board and initialize the values
            for(int index=0; index < Size; index++)
            {
                // Board values at each index will be 2 to the power of the index
                // HelperMask[0] = 1 << 0 = 1, HelperMask[1] = 1 << 1 = 2, HelperMask[2] = 1 << 2 = 4 and so on...
                HelperMask[index] = 1 << index;
            }
        }

        /// <summary>
        /// update the valid candidates for row, col and block
        /// </summary>
        /// <param name="Row">row of value</param>
        /// <param name="Col">col of value</param>
        /// <param name="Value">the value itself</param>
        private void UpdateCandidateValues(int Row, int Col, int Value)
        {
            //Use the bitwise OR operator (|) to add the mask at index value - 1 in the masks array to the element at index row in the RowValues array.
            //This has the effect of setting the bit corresponding to value in the binary representation of the element to 1,
            //indicating that the value is valid for the row.
            RowValues[Row] |= HelperMask[Value - 1];
            ColumnValues[Col] |= HelperMask[Value - 1];
            int SquareLocation = (Row / BlockSize) * BlockSize + Col / BlockSize;
            BlockValues[SquareLocation] |= HelperMask[Value - 1];
        }


        /// <summary>
        /// initializes the possible values for row, col and block
        /// </summary>
        private void SetValuesForEachHouse()
        {
            // create new arrays all with the Size of one row
            RowValues = new int[Size];
            ColumnValues = new int[Size];
            BlockValues = new int[Size];
            // go over all the values in the Board and fill the possible values accordingally for each cell in the Board
            for(int row=0; row < Size; row++)
            {
                for(int col =0; col < Size; col++)
                {
                    // the current value
                    int value = BoardInts[row, col];

                    // if the value is not 0
                    if(value != 0)
                    {
                        // update the values
                        UpdateCandidateValues(row, col, value);
                    }
                }
            }
        }

        /// <summary>
        /// This function determines the valid values for a cell at a specific row and column int the Board
        /// </summary>
        /// <param name="Row">the row</param>
        /// <param name="Col">the col</param>
        /// <returns>the amount of possible candidate values this cell has</returns>
        private int GetPossibleValuesForGivenCell(int Row, int Col)
        {
            // Calculate the index of the block that the cell belongs to by using the formula (Row / BlockSize) * BlockSize + Col / BlockSize.
            int SquareLocation = (Row / BlockSize) * BlockSize + Col / BlockSize;
            
            // Use the bitwise OR operator (|) to compute the sum of the non-valid values for the row, column, and box containing the cell.
            // This is done by performing a bitwise OR operation between the element at index row in the RowValues array,
            // the element at index col in the ColumnValues array,
            // and the element at index SquareLocation in the BlockValues array.
            int SumInvalidCandidates = RowValues[Row] | ColumnValues[Col] | BlockValues[SquareLocation];

            // Use the bitwise NOT operator (~) to negate the sum of the non-valid values,
            // effectively turning the 0 bits into 1 bits and the 1 bits into 0 bits.
            int ValidCandidates = ~SumInvalidCandidates;

            // Use the bitwise AND operator (&) to compute the intersection between the int
            // of valid values and the range of possible values for the cell.
            // This is done by performing a bitwise AND operation between the int and((1 << Size - 1),
            // which represents a bitmask with all bits set to 1 up to the Size of the Board array.
            ValidCandidates &= ((1 << Size) - 1);

            // Return the result of the bitwise AND operation.
            return ValidCandidates;
        }

        /// <summary>
        /// function that gets a location in the Board and a value and returns if it valid to put 
        /// value inside the Board in this location, this is done using the possible values held in the arrays 
        /// for row, col and block
        /// </summary>
        /// <param name="Row">row of value</param>
        /// <param name="Col">col of value</param>
        /// <param name="Value">the value</param>
        /// <returns></returns>
        private bool isValidBits(int Row, int Col, int Value)
        {
            int SquareLocation = (Row / BlockSize) * BlockSize + Col / BlockSize;
            // Use the bitwise AND operator (&) to check if the value is valid for the row, col and block
            // This is done by performing a bitwise AND operation between the element at index row in the RowValues array
            // and the mask at index value in the masks array.
            // If the result is 0, it means that the value is valid for the row
            // (i.e., the bit corresponding to value is not set in the binary representation of the element).
            return (RowValues[Row] & HelperMask[Value]) == 0
                && (ColumnValues[Col] & HelperMask[Value]) == 0
                && (BlockValues[SquareLocation] & HelperMask[Value]) == 0;

        }

        /// <summary>
        /// function that returns a copy of the current Board
        /// </summary>
        /// <returns>the copied Board</returns>
        private int[,] GetBoardIntsCopy()
        {
            // craete copy of the Board
            int[,] BoardCopy = new int[Size, Size];
            // go over the current Board and copy each value to the corresponding value in the new Board
            for(int row=0; row < Size; row++)
            {
                for(int col=0; col < Size; col++)
                {
                    BoardCopy[row, col] = BoardInts[row, col];
                }
            }
            // return the copied Board
            return BoardCopy;
        }

        /// <summary>
        /// function that returns a copy of the given Board
        /// </summary>
        /// <param name="board">the Board</param>
        /// <returns>the copied Board</returns>
        private int[,] GetBoardIntsCopy(int[,] board)
        {
            // craete copy of the Board
            int[,] BoardCopy = new int[Size, Size];
            // go over the current Board and copy each value to the corresponding value in the new Board
            for (int row = 0; row < Size; row++)
            {
                for (int col = 0; col < Size; col++)
                {
                    BoardCopy[row, col] = board[row, col];
                }
            }
            // return the copied Board
            return BoardCopy;
        }

        /// <summary>
        /// function that goes over the Board and find the next cell that has the lesat possible candidates
        /// THIS FUNCTION ALSO CONTAINS THE IMPLEMENTATION OF THE SIMPLE ELIMINATION ALGORITHM
        /// </summary>
        /// <returns>the row and col of the cell</returns>
        private (int BestRow, int BestCol) GetNextBestCell()
        {
            // row and col are set by deafult to -1, so that we know that if this function
            // returned -1, it means all the cells are filled
            int BestRow = -1;
            int BestCol = -1;

            // amount of valid candidates for each location
            int AmountOfValidCandidates;

            // the current lowest amount of valid candidates
            int LowestAmountOfValidCandidates = Size + 1;

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
                    AmountOfValidCandidates = GetPossibleValuesForGivenCell(row, col);

                    // if the amount of valid candidates for this cell is lower the the lowest amount, change the BestRow and BestCol
                    // and the current lowest amount of valid candidates 
                    if(GetActivatedBits(AmountOfValidCandidates) < LowestAmountOfValidCandidates)
                    {
                        BestRow = row;
                        BestCol = col;
                        LowestAmountOfValidCandidates = GetActivatedBits(AmountOfValidCandidates);
                    }

                    // IMPLEMENTATION OF SIMPLE ELIMINATION:
                    // Simple elimination is an algorithm that is used to improve the search time for a backtrackig algorithm
                    // over a sudoku puzzle, it works this way:
                    // go over a Board and for each cell check if there is only one possible candidate that can fit in that cell,
                    // if there is only one, then this cell has to contain that value, so fill it with that value
                    if(LowestAmountOfValidCandidates == 1)
                    {
                        // return that cell that can only be filled with one possible value
                        return (BestRow, BestCol);
                    }
                }
            }
            // return the found best row and col
            return (BestRow, BestCol);
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
        /// this is the implementation of the hidden singles algorithm,
        /// what this algorithm does is that it goes over all the possible candidates for each cell
        /// in each house in the Board, and if it is found that in a house there is only one possible
        /// cell with that value, this cell will be filled with that value
        /// </summary>
        private void HiddenSinglesAlgorithmWithBitwiseManipulation()
        {
            // counter for how many possible candidates this cell has
            int PossibleCandidates;
            // the amount of activated bits in the value
            int ActivatedBits;
            // the value
            int value;
            // go over the Board and for each cell, check if there is only one possible candidate for it
            for (int row =0; row< Size; row++)
            {
                for (int col = 0; col < Size; col++){
                    // if the value at the current cell isn't 0, move on
                    if (BoardInts[row, col] != 0) continue;
                    // otherwise, get the amount of number candidates for this location
                    PossibleCandidates = GetPossibleValuesForGivenCell(row, col);
                    // get the amount of activated bits in the value
                    ActivatedBits = GetActivatedBits(PossibleCandidates);
                    // if there is only one possible candidate for this cell, fill it with that value
                    if (ActivatedBits == 1)
                    {
                        // get the value
                        value = GetIndexOfMostSignificantActivatedBit(PossibleCandidates);
                        // fill the cell with that value and update the affected values
                        BoardInts[row, col] = value;
                        UpdateCandidateValues(row, col, value);
                    }
                }                 
            }
            // return the updated Board
            return;
        }


        /// <summary>
        /// function that solves a sudoku using backtracking algorithm with candidate values updating in real time
        /// </summary>
        /// <returns>if the backtracking managed to solve the Board or not</returns>
        public bool BacktrackingWithBitwiseManipulation()
        {
            // store the original values for the cell and the origianl Board
            int[,] copyBoard = GetBoardIntsCopy(BoardInts);
            int[] copyRowValues =  new int[Size];
            int[] copyColValues = new int[Size];
            int[] copyBlockValues = new int[Size];
            // copy the old values into the copy values
            CopyOriginalArraysIntoNewOnes(copyRowValues, copyColValues, copyBlockValues);

            // run the hidden singles algorithm that will fill in as many cells as possible
            HiddenSinglesAlgorithmWithBitwiseManipulation();

            // the row and col of the cell that we are analyzing
            int CurrentRow, CurrentCol;
            // get the next best cell
            (CurrentRow, CurrentCol) = GetNextBestCell();

            // if the value of one of the row or col was -1, return true because that means that there are no more
            // empty cells that the function can find inside the Board
            if (CurrentRow == -1 || CurrentCol == -1) return true;

            // go over the possible values and check if the value is valid or not to put in the cell
            for(int currentValue = 0; currentValue < Size; currentValue++)
            {
                if(isValidBits(CurrentRow,CurrentCol, currentValue))
                {
                    // if the value is valid, the set the Board at this location to be the value and update the affected cells
                    BoardInts[CurrentRow, CurrentCol] = currentValue+1;
                    UpdateCandidateValues(CurrentRow, CurrentCol, currentValue+1);

                    // run the backtracking again, if it works, then return true, else restore the original values
                    if (BacktrackingWithBitwiseManipulation()) return true;

                    // if the current backtracking branch failed, restore the original values
                    RestoreAffectedValuesAndBoard(copyBoard, copyRowValues, copyColValues, copyBlockValues);
                }
            }
            // if the backtrackig failed completelly, return false
            return false;
        }

        /// <summary>
        /// function that solves the Board using backtracking algorithm
        /// </summary>
        public string SolveUsingBacktracking()
        {

            // Stopwatch to measure the time it takes to solve the Board
            var Watch = new Stopwatch();

            // start the timer
            Watch.Start();
            // run the backtracking algorithm and save the result
            bool solved = BacktrackingWithBitwiseManipulation();

            // stop the timer
            Watch.Stop();
            
            // print if solved or not
            if (solved) Console.WriteLine("\nSolved!");
            else Console.WriteLine("\nNot solved!");

            // print the Board after solving
            Console.WriteLine("\nResult Board:\n");
            PrintBoard(BoardInts, Size);

            // print out the amount of time ot took the algorithm to reach a conclusion
            // print the elapsed times in seconds, milliseconds
            Console.WriteLine("\nElapsed time: {0} seconds", Watch.Elapsed.TotalSeconds);
            Console.WriteLine("Elapsed time: {0} milliseconds", Watch.Elapsed.TotalMilliseconds);

            // return the Board string
            return ConvertToString(BoardInts, Size);
        }

        public SolvingFunctions(int[,] board, int size)
        {
            BoardInts = board;
            Size = size;
            BlockSize = (int)Math.Sqrt(Size);         
            
            // set up values
            SetHelperMask();

            // set up possible values for each house
            SetValuesForEachHouse();

        }

            #endregion new backtracking functions

    }

}