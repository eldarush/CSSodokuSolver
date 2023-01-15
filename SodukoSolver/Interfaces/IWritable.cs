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
    public interface IWritable
    {
        /// <summary>
        /// write function that will be implemented in the calsses that implement this interface
        /// </summary>
        /// <param name="boardstring">the boardstring</param>
        /// <returns>if the writing sucseeded or not</returns>
        bool Write(string boardstring);
    }
}
