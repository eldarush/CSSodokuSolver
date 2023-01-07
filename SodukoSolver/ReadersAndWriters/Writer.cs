using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SodukoSolver.Interfaces;

namespace SodukoSolver
{
    /// <summary>
    /// abstract class that will be inherited by the classes that will read the soduko board
    /// </summary>
    abstract class Writer
    {
        // an instance of a class that implements the IWritable interface
        private readonly IWritable _writer;

        /// <summary>
        /// construcntor that takes an instance of a class that implements the IWritable interface
        /// </summary>
        /// <param name="writer">the _writer implemetation</param>
        public Writer(IWritable writer)
        {
            this._writer = writer;
        }

        /// <summary>
        /// write function that will write a Board string to the file
        /// </summary>
        /// <param name="boardstring"></param>
        /// <returns></returns>
        public bool Write(string boardstring)
        {
            return _writer.Write(boardstring);
        }

    }
}
