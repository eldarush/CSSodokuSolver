using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodukoSolver
{
    public class Node
    {
        // properties for the row, column, and value of the cell
        public int Row { get; set; }
        public int Col { get; set; }
        public int Value { get; set; }

        // pointers to the previous and next nodes in the row and column linked lists
        public Node Left { get; set; }
        public Node Right { get; set; }
        public Node Up { get; set; }
        public Node Down { get; set; }

        // reference to the column header node
        public Node ColumnHeader { get; set; }

        // constructor
        public Node()
        {
            // set all pointers to point to itself
            Up = this;
            Down = this;
            Left = this;
            Right = this;
        }
    }

}
