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

        // constructor
        public Writer(IWritable writer)
        {
            this.writer = writer;
        }

        // write function that will write a Board string
        public bool Write(string boardstring)
        {
            return writer.Write(boardstring);
        }

    }
}
