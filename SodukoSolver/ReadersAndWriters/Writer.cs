using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SodukoSolver.Interfaces;

namespace SodukoSolver
{
    abstract class Writer
    {
        // an instance of a class that implements the IWritable interface
        protected IWritable writer;

        /// <summary>
        /// construcntor that takes an instance of a class that implements the IWritable interface
        /// </summary>
        /// <param name="writer">the writer implemetation</param>
        public Writer(IWritable writer)
        {
            this.writer = writer;
        }

        /// <summary>
        /// write function that will write a Board string to the file
        /// </summary>
        /// <param name="boardstring"></param>
        /// <returns></returns>
        public bool Write(string boardstring)
        {
            return writer.Write(boardstring);
        }

    }
}
