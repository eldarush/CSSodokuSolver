using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodukoSolver.Interfaces
{
    /// <summary>
    /// interface for the classes that will read the soduko board
    /// </summary>
    public interface IReadable
    {
        /// <summary>
        /// read function that will be implemented in the classes that implement this interface
        /// </summary>
        /// <returns>the SudokuBoard string</returns>
        string Read();
    }
}
