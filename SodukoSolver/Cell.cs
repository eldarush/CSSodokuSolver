using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodukoSolver
{
    public class Cell
    {
        // in each cell there is a list of possible candidates
        public List<int> Candidates { get; set; }

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
                Candidates = new List<int>();
                for (int i = 1; i <= size; i++)
                {
                    Candidates.Add(i);
                }
            } else
            {
                // if the value is not 0 then the cell is solved
                // and we can leave the candidates as just the cell value
                Solved = true;
                Candidates = new List<int>();
                Candidates.Add(Value);
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

    }
}
