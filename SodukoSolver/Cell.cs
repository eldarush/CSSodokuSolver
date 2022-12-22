using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodukoSolver
{
    public class Cell
    {
        // in each cell there is a hashset of possible candidates
        public HashSet<int> Candidates { get; set; }

        // the value of the cell
        public int Value { get; set; }

        // public boolean that keeps track if this cell is solved
        public bool Solved { get; set; }

        // Constructor that gets size and initializes the possible Candidates 1-size
        public Cell(int size, int value)
        {
            // if the value is 0 then the cell is not solved
            // and we need to initialize the possible candidates
            if (value == 0)
            {
                Solved = false;
                Candidates = new HashSet<int>();
                for (int i = 1; i <= size; i++)
                {
                    Candidates.Add(i);
                }
            } else
            {
                // if the value is not 0 then the cell is solved
                // and we can leave the candidates as just the cell value
                Solved = true;
                Candidates = new HashSet<int>();
                Candidates.Add(value);
            }
            // set the value of the cell to the given value
            Value = value;
        }

        // function that checks if the list of candidates only contains one 
        // candidate, if it is returns true and updates the Solved boolean
        public bool isSolved()
        {
            if (Candidates.Count == 1)
            {
                Solved = true;
                return Solved;
            }
            else return false;
        }    

       // function to print all candidates
       public void printCandidates()
       {
            Console.WriteLine(string.Join(", ", Candidates));
       }


        // function that updates the possible candidates of the cell
        public void UpdatePossibleCandidates(Cell[,] board, int size, int currentCol, int currentRow)
        {
            // if the cell is solved then we don't need to update the candidates
            if (Solved) return;

            // if the cell is not solved then we need to update the candidates
            // we need to check the row, column and the box of the cell
            // and remove the values that are already in the row, column and box
            // from the list of candidates
            // we need to check the row
            for (int i = 0; i < size; i++)
            {
                // if the board in the current row has a final value and the value is in the list of candidates
                // then we need to remove it from the list of candidates
                if (board[currentRow, i].Solved && Candidates.Contains(board[currentRow, i].Value))
                {
                    Candidates.Remove(board[currentRow, i].Value);
                }
            }

            // we need to check the column
            for (int i = 0; i < size; i++)
            {
                // if the board in the current column has a final value and the value is in the list of candidates
                // then we need to remove it from the list of candidates
                if (board[i,currentCol].Solved && Candidates.Contains(board[i, currentCol].Value))
                {
                    Candidates.Remove(board[i, currentCol].Value);
                }
            }

            // we need to check the box
            // first we need to find the top left corner of the box
            int boxRow = currentRow - currentRow % (int)Math.Sqrt(size);
            int boxCol = currentCol - currentCol % (int)Math.Sqrt(size);

            // then we need to check the box
            for (int i = boxRow; i < boxRow + (int)Math.Sqrt(size); i++)
            {
                for (int j = boxCol; j < boxCol + (int)Math.Sqrt(size); j++)
                {
                    // if the board in the current box has a final value and the value is in the list of candidates
                    // then remove the current value from the list of candidates
                    if (board[i, j].Solved && Candidates.Contains(board[i, j].Value))
                    {
                        Candidates.Remove(board[i, j].Value);
                    }
                }
            }
        }
    }
}
