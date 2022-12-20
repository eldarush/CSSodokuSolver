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
        
        // public boolean that keeps track if this cell is solved
        public bool Solved { get; set; }

        // Constructor that gets size and initializes the possible Candidates 1-size
        public Cell(int size)
        {
            Candidates = Enumerable.Range(1, size).ToList();
            Solved = false;
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
