using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodukoSolver.Interfaces
{
    public interface Isolvable
    {
        /// <summary>
        /// solve function that will be implemented in the calsses that implement this interface
        /// </summary>
        /// <returns>tif solved or not</returns>
        bool Solve();

        /// <summary>
        /// function that solves the board and returns the solution string
        /// </summary>
        /// <returns></returns>
        string GetSolutionString();
    }
}
