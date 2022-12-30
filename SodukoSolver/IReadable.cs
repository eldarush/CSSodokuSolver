using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodukoSolver
{
    public interface IReadable
    {
        // read Function that will return a board string
        string Read();
    }
}
