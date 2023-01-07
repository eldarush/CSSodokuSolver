using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodukoSolver.Interfaces
{
    /// <summary>
    /// interface for board solvers that will have to implement those functions
    /// </summary>
    public interface Isolvable
    {
        /// <summary>
        /// solve function that will be implemented in the calsses that implement this interface
        /// </summary>
        /// <returns>tif solved or not</returns>
        bool Solve();

        /// <summary>
        /// function that solves the SudokuBoard and returns the solution string
        /// </summary>
        /// <returns></returns>
        string GetSolutionString();
    }
}
