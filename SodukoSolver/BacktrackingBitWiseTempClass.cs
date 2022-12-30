using System;
using System.Diagnostics;

namespace SodukoSolver
{
    class Program
    {
        public static int size = 16;
        public static int blockSize = 4;

        public static int[][] ConvertTo2DArray(string boardstring, int[][] array)
        {
            for (int i = 0; i < size; i++)
            {
                array[i] = new int[size];
                for (int j = 0; j < size; j++)
                {
                    array[i][j] = boardstring[i * size + j] - '0';
                }
            }
            return array;
        }

        static void Main(string[] args)
        {
            int[][] grid = new int[size][];
            grid = ConvertTo2DArray("0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000", grid);
            // print original grid
            PrintGrid(grid, size);

            Console.WriteLine("");

            var watch = new Stopwatch();
            watch.Start();
            // solve sudoku
            if (SolveSudoku(grid, size, blockSize))
            {
                watch.Stop();
                // print solved grid
                PrintGrid(grid, size);
            }
            else
            {
                watch.Stop();
                Console.WriteLine("No solution exists");
            }
            Console.WriteLine("");
            Console.WriteLine("\nElapsed time: {0} seconds", watch.Elapsed.TotalSeconds);
            Console.WriteLine("Elapsed time: {0} milliseconds", watch.Elapsed.TotalMilliseconds);
        }

        /* A utility function to print grid */
        private static void PrintGrid(int[][] grid, int size)
        {
            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    Console.Write(grid[row][col] + " ");
                }
                Console.WriteLine();
            }
        }

        /* Takes a partially filled-in grid and attempts
        to assign values to all unassigned locations in
        such a way to meet the requirements for
        Sudoku solution (non-duplication across rows,
        columns, and boxes) */
        private static bool Solve(int r, int c, int[][] board,
            int[][] submatrixDigits,
            int[] rowDigits,
            int[] columnDigits,
            int size,
            int subSize)
        {
            if (r == size)
            {
                return true;
            }
            if (c == size)
            {
                return Solve(r + 1, 0, board, submatrixDigits,
                            rowDigits, columnDigits, size, subSize);
            }

            if (board[r][c] == 0)
            {
                for (int i = 1; i <= size; i++)
                {
                    int digit = 1 << (i - 1);

                    if (!IsDigitUsed(submatrixDigits, rowDigits, columnDigits, r, c, digit, size, subSize))
                    {
                        // set digit
                        SetDigit(submatrixDigits, rowDigits, columnDigits, r, c, digit, size, subSize);
                        board[r][c] = i;

                        if (Solve(r, c + 1, board, submatrixDigits,
                                    rowDigits, columnDigits, size, subSize))
                        {
                            return true;
                        }
                        else
                        {
                            // unset digit
                            UnsetDigit(submatrixDigits, rowDigits, columnDigits, r, c, digit, size, subSize);
                            board[r][c] = 0;
                        }
                    }
                }
                return false;
            }
            return Solve(r, c + 1, board, submatrixDigits,
                        rowDigits, columnDigits, size, subSize);
        }

        // Function checks if the given digit is used in
        // the given submatrix, row, and column
        private static bool IsDigitUsed(int[][] submatrixDigits, int[] rowDigits, int[] columnDigits, int r, int c, int digit, int size, int submatrixSize)
        {
            return (submatrixDigits[r / submatrixSize][c / submatrixSize] & digit) != 0
                || (rowDigits[r] & digit) != 0
                || (columnDigits[c] & digit) != 0;

        }
        // Function sets the given digit in the given
        // submatrix, row, and column
        private static void SetDigit(int[][] submatrixDigits, int[] rowDigits, int[] columnDigits, int r, int c, int digit, int size, int submatrixSize)
        {
            submatrixDigits[r / submatrixSize][c / submatrixSize] |= digit;
            rowDigits[r] |= digit;
            columnDigits[c] |= digit;
        }

        // Function unsets the given digit in the given
        // submatrix, row, and column
        private static void UnsetDigit(int[][] submatrixDigits, int[] rowDigits, int[] columnDigits, int r, int c, int digit, int size, int submatrixSize)
        {
            submatrixDigits[r / submatrixSize][c / submatrixSize] &= ~digit;
            rowDigits[r] &= ~digit;
            columnDigits[c] &= ~digit;
        }

        // Function checks if Sudoku can be
        // solved or not
        public static bool SolveSudoku(int[][] board, int size, int submatrixSize)
        {
            int[][] submatrixDigits = new int[submatrixSize][];
            int[] columnDigits = new int[size];
            int[] rowDigits = new int[size];

            for (int i = 0; i < submatrixSize; i++)
                submatrixDigits[i] = new int[submatrixSize];

            // get submatrix, row, and column digits
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    if (board[i][j] > 0)
                    {
                        int value = 1 << (board[i][j] - 1);
                        submatrixDigits[i / submatrixSize][j / submatrixSize] |= value;
                        rowDigits[i] |= value;
                        columnDigits[j] |= value;
                    }
            // backtrack
            return Solve(0, 0, board, submatrixDigits, rowDigits, columnDigits, size, submatrixSize);
        }
    }
}
