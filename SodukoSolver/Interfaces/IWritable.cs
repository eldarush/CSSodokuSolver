using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodukoSolver.Interfaces
{
    public interface IWritable
    {
        // write function that will write a Board string
        bool Write(string boardstring);
    }
}
