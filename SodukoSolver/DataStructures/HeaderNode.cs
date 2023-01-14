using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodukoSolver.DataStructures
{
    /// <summary>
    /// this class represents the header node for each column
    /// it is a subclass of the node class
    /// </summary>
    public class HeaderNode : Node
    {
        // the number of nodes in the column
        public int Size { get; set; }

        // the name of the column
        public string Name { get; set; }

        /// <summary>
        /// constructor for the header node
        /// </summary>
        /// <param name="name">the name of the class</param>
        public HeaderNode(string name) : base(null)
        {
            // set the name to the passed name, the header to itself
            // and the size to 0
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
            RemoveFromRow();

            // keep track of what row we are on
            Node currentRow = Down;
            // keep track of what node we are on
            Node currentNode;

            // while there are still rows to be proccesed
            while (currentRow != this)
            {
                // get the current node and delete all the nodes on the same row
                currentNode = currentRow.Right;
                while (currentNode != currentRow)
                {
                    // remove the current node from the col and change the size 
                    currentNode.RemoveFromCol();
                    currentNode.Header.Size--;
                    // continue to the next node in this row
                    currentNode = currentNode.Right;
                }
                // continue to the next row
                currentRow = currentRow.Down;
            }
        }

        /// <summary>
        /// this is a mirrored function to the CoverCol that re-attaches the uncovered nodes
        /// in this col
        /// </summary>
        public void UncoverCol()
        {
            // keep track of what row we are on
            Node currentRow = Up;
            // keep track of what node we re on
            Node currentNode;

            // while there are still rows to be proccesed
            while (currentRow != this)
            {
                // get the current node and add back all the affected nodes
                currentNode = currentRow.Left;
                while (currentNode != currentRow)
                {
                    // re-attach the current node to its col and change the size
                    currentNode.ReAttachCol();
                    currentNode.Header.Size++;
                    // continue to the next node in this row
                    currentNode = currentNode.Left;
                }
                // continue to the next row
                currentRow = currentRow.Up;
            }
            // re-attach the header node to the row of header nodes
            ReAttachRow();
        }
    }
}
