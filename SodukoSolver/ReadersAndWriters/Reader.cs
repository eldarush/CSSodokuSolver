using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodukoSolver.Interfaces
{
    abstract class Reader
    {
        // an instance of a class that implements the IReadable interface
        protected IReadable reader;

        /// <summary>
        /// constructor that takes an instance of a class that implements the IReadable interface
        /// </summary>
        /// <param name="reader">the reader implementation</param>
        public Reader(IReadable reader)
        {
            this.reader = reader;
        }

        /// <summary>
        /// reads the input 
        /// </summary>
        /// <returns></returns>
        public string Read()
        {
            return reader.Read();
        }
    }
}
