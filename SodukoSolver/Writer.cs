using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        // write function that will write a board of cells
        public bool Write(Cell[,] board)
        {
            return writer.Write(board);
        }

    }
}
