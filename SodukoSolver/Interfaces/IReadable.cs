using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodukoSolver.Interfaces
{
    public interface IReadable
    {
        /// <summary>
        /// read function that will be implemented in the classes that implement this interface
        /// </summary>
        /// <returns>the board string</returns>
        string Read();
    }
}
