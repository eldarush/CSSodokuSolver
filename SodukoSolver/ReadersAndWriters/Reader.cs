using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodukoSolver.Interfaces
{
    /// <summary>
    /// abstract class that will be inherited by the classes that will read the soduko board
    /// </summary>
    abstract class Reader
    {
        // an instance of a class that implements the IReadable interface
        private readonly IReadable _reader;

        /// <summary>
        /// constructor that takes an instance of a class that implements the IReadable interface
        /// </summary>
        /// <param name="reader">the _reader implementation</param>
        public Reader(IReadable reader)
        {
            this._reader = reader;
        }

        /// <summary>
        /// reads the input 
        /// </summary>
        /// <returns></returns>
        public string Read()
        {
            return _reader.Read();
        }
    }
}
