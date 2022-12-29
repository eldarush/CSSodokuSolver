using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodukoSolver
{
    public interface IWritable
    {
        // write function that will write a board of cells
        bool Write(Cell[,] board);
    }
}
