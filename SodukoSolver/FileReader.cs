using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SodukoSolver
{
    public class FileReader : IReadable, IWritable
    {
        // the name of the file that will contain the soduko puzzle
        private string filename;

        // constructor that will set the filename
        public FileReader(string filename)
        {
            this.filename = filename;
        }

        public Cell[,] Read()
        {
            throw new NotImplementedException();
        }

        public bool Write(Cell[,] board)
        {
            throw new NotImplementedException();
        }
    }
}
