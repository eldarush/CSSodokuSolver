using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodukoSolver.DataStructures
{
    /// <summary>
    /// class for a node that will fill the sparse Matrix
    /// </summary>
    public class Node
    {
        // pointers to each direction
        public Node Left { get; set; }
        public Node Right { get; set; }
        public Node Up { get; set; }
        public Node Down { get; set; }

        // pointer to the header node that represents the column that this node is in
        public HeaderNode Header { get; set; }

        /// <summary>
        /// counstructor for the node that links it to itself
        /// </summary>
        /// <param name="header">a header for the node</param>
        public Node(HeaderNode header)
        {
            // make the node point to itself in all directions
            Left = this;
            Right = this;
            Up = this;
            Down = this;
            // set the header to the passed header
            Header = header;
        }

        // attach the passed node between this node and the node to the right of this node
        public void AttachRight(Node node)
        {
            node.Right = Right;
            Right.Left = node;
            Right = node;
            node.Left = this;
        }

        // attach the passed node between this node and the node below this node
        public void AttachLeft(Node node)
        {
            node.Left = Left;
            Left.Right = node;
            Left = node;
            node.Right = this;
        }

        // attach the passed node between this node and the node above this node
        public void AttachDown(Node node)
        {
            node.Down = Down;
            Down.Up = node;
            Down = node;
            node.Up = this;
        }

        // attach the passed node between this node and the node below this node
        public void AttachUp(Node node)
        {
            node.Up = Up;
            Up.Down = node;
            Up = node;
            node.Down = this;
        }

        // remove the node from the row
        public void RemoveLeftRight()
        {
            Right.Left = Left;
            Left.Right = Right;
        }

        // remove the node from the column
        public void RemoveUpDown()
        {
            Down.Up = Up;
            Up.Down = Down;         
        }

        // reinsert this node into the row it originally was in
        public void ReAttachRow()
        {
            Right.Left = this;
            Left.Right = this;
        }

        // reinsert this node into the col it originally was in
        public void ReAttachCol()
        {
            Up.Down = this;
            Down.Up = this;
        }
    }
}
