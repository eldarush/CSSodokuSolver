using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodukoSolver.DataStructures
{
    // class for a node that will fill the sparse matrix
    public class Node
    {
        // pointers to each direction
        public Node Left { get; set; }
        public Node Right { get; set; }
        public Node Up { get; set; }
        public Node Down { get; set; }

        // pointer to the header node that represents the column that this node is in
        public HeaderNode Header { get; set; }

        // counstructor for the node that links it to itself
        public Node(HeaderNode header)
        {
            Left = this;
            Right = this;
            Up = this;
            Down = this;
            Header = header;
        }

        // attach this node to the right of the node passed in
        public void AttachRight(Node node)
        {
            node.Right = Right;
            Right.Left = node;
            Right = node;
            node.Left = this;
        }

        // attach this node to the left of the node passed in
        public void AttachLeft(Node node)
        {
            node.Left = Left;
            Left.Right = node;
            Left = node;
            node.Right = this;
        }

        // attach this node to the down of the node passed in
        public void AttachDown(Node node)
        {
            node.Down = Down;
            Down.Up = node;
            Down = node;
            node.Up = this;
        }

        // attach this node to the up of the node passed in
        public void AttachUp(Node node)
        {
            node.Up = Up;
            Up.Down = node;
            Up = node;
            node.Down = this;
        }

        // remove this node from the column it is in
        public void RemoveLeftRight()
        {
            Right.Left = Left;
            Left.Right = Right;
        }

        // remove this node from the row it is in
        public void RemoveUpDown()
        {
            Up.Down = Down;
            Down.Up = Up;
        }

        // reinsert this node into the column it is in
        public void ReinsertLeftRight()
        {
            Right.Left = this;
            Left.Right = this;
        }

        // reinsert this node into the row it is in
        public void ReinsertUpDown()
        {
            Up.Down = this;
            Down.Up = this;
        }
    }
}
