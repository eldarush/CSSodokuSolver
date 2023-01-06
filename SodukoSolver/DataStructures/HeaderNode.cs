using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodukoSolver.DataStructures
{
    // this class represents the header node for each column
    // it is a subclass of the node class
    public class HeaderNode : Node
    {
        // the number of nodes in the column
        public int Size { get; set; }

        // the name of the column
        public string Name { get; set; }

        // constructor for the header node
        public HeaderNode(string name) : base(null)
        {
            Header = this;
            Name = name;
            Size = 0;
        }

        /// <summary>
        /// cover the column by removing the column and all the rows that are 
        /// at the same row as the nodes in the column
        /// </summary>
        public void CoverCol()
        {
            // remove the header node from the row of header nodes
            RemoveLeftRight();

            // keep track of what row we are on
            Node CurrentRow = Down;
            // keep track of what node we re on
            Node CurrentNode;

            // while there are still rows to be proccesed
            while (CurrentRow != this)
            {
                // get the current node and delete all the nodes on the same row
                CurrentNode = CurrentRow.Right;
                while (CurrentNode != CurrentRow)
                {
                    // remove the current node from the col and change the size 
                    CurrentNode.RemoveUpDown();
                    CurrentNode.Header.Size--;
                    // continue to the next node in this row
                    CurrentNode = CurrentNode.Right;
                }
                // continue to the next row
                CurrentRow = CurrentRow.Down;
            }
        }

        /// <summary>
        /// this is a mirrored function to the CoverCol that re-attaches the uncovered nodes
        /// in this col
        /// </summary>
        public void UncoverCol()
        {
            // keep track of what row we are on
            Node CurrentRow = Down;
            // keep track of what node we re on
            Node CurrentNode;

            // while there are still rows to be proccesed
            while (CurrentRow != this)
            {
                // get the current node and add back all the affected nodes
                CurrentNode = CurrentRow.Left;
                while (CurrentNode != CurrentRow)
                {
                    // re-attach the current node to its col and change the size
                    CurrentNode.ReinsertUpDown();
                    CurrentNode.Header.Size++;
                    // continue to the next node in this row
                    CurrentNode = CurrentNode.Left;
                }
                // continue to the next row
                CurrentRow = CurrentRow.Up;
            }
            // re-attach the header node to the row of header nodes
            ReinsertLeftRight();
        }
    }
}
