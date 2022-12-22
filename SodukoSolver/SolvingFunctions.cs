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

        // the board
        private static Cell[,] board;

        // setter for the size
        public static void setSize(int size)
        {
            SolvingFunctions.size = size;
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

        // solving function that solves the soduko board
        // and returns boolean value indicating if the board was solved
        // or not
        public static bool Solve()
        {
            var find = FindEmpty();
            if (find == null)
            {
                return true;
            }
            
            int row = find[0];
            int col = find[1];

            for (int i = 1; i <= size; i++)
            {
                if (ValidateCell(board,row,col,size,i))
                {
                    board[row,col].Value = i;

                    if (Solve())
                    {
                        return true;
                    }

                    board[row,col].Value = 0;
                }
            }

            return false;
            
        }

        // function that find the first empty cell, and returns its col and row
        // empty cell is represented by 0
        private static int[] FindEmpty()
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (board[i,j].Value == 0)
                    {
                        return new int[] { i, j };
                    }
                }
            }

            return null;
        }

    }
}
