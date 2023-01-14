using SodukoSolver.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.

namespace SodukoSolver.Algoritms
{
    /// <summary>
    /// this is a module that will contain the functions used to convert
    /// the board to the dancing links format and handling the result stack
    /// </summary>
    public static class DancingLinksCreatingFunctions
    {
        /// <summary>
        /// function that will get a SudokuBoard that represents the values of the cells 
        /// and the size of the SudokuBoard and will return a cover Matrix for the SudokuBoard
        /// that will be of size (size^3) * (constrains* size^2)
        /// </summary>
        /// <param name="board">the SudokuBoard</param>
        /// <param name="size">the size of the SudokuBoard</param>
        /// <param name="blockSize">the squared size of the SudokuBoard</param>
        /// <param name="constrains">the amount of constraints</param>
        /// <param name="coverMatrix">the result Matrix of 0'es and 1's</param>
        public static void ConvertMatrixToExactCoverMatrix(byte[,] board, int size, int blockSize, int constrains,
            out byte[,] coverMatrix)
        {
            // initialize the new cover Matrix
            coverMatrix = new byte[size * size * size, constrains * size * size];

            // current row indicator
            int currentRow = 0;

            // current value indicator
            byte currentValue;

            // please note that the next part is hard coded for 4 constraints and for this exact order,
            // this is the order described in the thesis 'Solving Sudoku efficiently with Dancing Links'
            // by MATTIAS HARRYSSON and HJALMAR LAESTANDER and for a diffrent number of constraints or 
            // for a diffrent order, this function would need to be modified and so will the other functions
            // that acsess the cover Matrix that is created by this function

            // indicator for each constraint
            // first 'size' portion of the array is for cell constraints
            int currentCellConstraint = 0;
            // second 'size' portion of the array is for col constraints
            int currentColConstraint; // is set to 'size * size' in the loop
            // third 'size' portion of the array is for row constraints
            int currentRowConstraint = size * size * 2;
            // fourth 'size' portion of the array is for block constraints
            int currentBlockConstraint = size * size * 3;

            // current candidate row index
            // a-symetry shifting 
            int candidateRowIndex;

            // current candidate block index;
            int candidateBlockIndex;

            // go over the SudokuBoard
            for (int row = 0; row < size; row++)
            {
                // reset the indicator for the current col constraint
                currentColConstraint = size * size;

                for (int col = 0; col < size; col++)
                {
                    // the current value at row,col
                    currentValue = board[row, col];

                    // calcualte the block index
                    candidateBlockIndex = (row / blockSize) * blockSize + col / blockSize;

                    // try all possible candidates for this size to see which one's fit and which ones dont
                    for (int currentCandidate = 1; currentCandidate <= size; currentCandidate++)
                    {
                        // if the current value is 0 or if the current candidate IS the current value, then
                        // we update the cover Matrix to contain 1's at the constraints
                        // if not then we just move to the next row and the next ColConstraint indicator
                        if (currentValue == 0 || currentValue == currentCandidate)
                        {
                            // update the current cell constraint to be 1 at the appropriate location
                            coverMatrix[currentRow, currentCellConstraint] = 1;

                            // update the current col constraint to be 1 at the appropriate location
                            coverMatrix[currentRow, currentColConstraint] = 1;

                            // update the current row constraint to be 1 at the appropriate location
                            candidateRowIndex = currentCandidate - 1;
                            coverMatrix[currentRow, currentRowConstraint + candidateRowIndex] = 1;

                            // update the current block constraint to be 1 at the appropriate location
                            coverMatrix[currentRow, currentBlockConstraint + candidateBlockIndex * size + candidateRowIndex] = 1;
                        }
                        // continue to the next row
                        currentRow++;
                        // continue to the next col constraint indicator
                        currentColConstraint++;
                    }
                    // continue to the next cell constraint indicator
                    currentCellConstraint++;
                }
                // continue to the next row constraint indicator
                // this is done becaue every constraint take up 'size' amount in the array
                // so we skip to next one by adding 'size' to the row constraint indicator
                currentRowConstraint += size;
            }
            // return the ready cover Matrix
            return;
        }

        /// <summary>
        /// this is a function that takes in the cover Matrix as a Matrix of 0'es and one's and converts it to a new 
        /// node Matrix that will be used by the dancing links algorithm
        /// this function just returns the root of the new Matrix, of which the Matrix is represented by the connections
        /// between the nodes themselves
        /// </summary>
        /// <param name="coverMatrix">the Matrix of 0'es and 1's</param>
        /// <param name="rootOfNewMatrix">the root of the new node Matrix, root is connected to the left of the row of headers</param>
        public static void ConvertCoverMatrixToNodeMatrix(byte[,] coverMatrix, out HeaderNode rootOfNewMatrix)
        {
            // create the root of the Matrix that will be returned
            rootOfNewMatrix = new HeaderNode("root");

            // calcualte the amount of rows and cols
            // rows = n^3
            int rowAmount = coverMatrix.GetLength(0);
            // cols = constraints*n^2
            int colAmount = coverMatrix.GetLength(1);

            // create the row of headers (A-H) but for diffrent sizes that will be at the top of the 
            // node Matrix, will all be linked to one another and will all have link to the col that they
            // are 'heading', the headeers value will later be used to indentify the col and the row
            // of the current cell to be placed in the final SudokuBoard that will represt the answer to the SudokuBoard
            HeaderNode[] headersRow = new HeaderNode[colAmount];

            // initialize the headers row with the given row index as the name 
            for (int currentRowIndex = 0; currentRowIndex < colAmount; currentRowIndex++)
            {
                // create new header and insert it into the headers row
                // note that when debugging the names of the row will just be the index of the place
                // where they are located 0 -> (size-1) and not A-Z letters as described
                HeaderNode CurrentHeaderNode = new(currentRowIndex.ToString());
                headersRow[currentRowIndex] = CurrentHeaderNode;

                // attach the header node to the root (root will be to the left of the headers node and will point
                // to to first header node at the index 0 at the headers row)
                rootOfNewMatrix.AttachRight(CurrentHeaderNode);

                // change the root to be the current header so that we can link all the nodes together
                rootOfNewMatrix = (HeaderNode)CurrentHeaderNode;
            }

            // change the root back to what it was originally (the node to the left of all headers)
            rootOfNewMatrix = (HeaderNode)rootOfNewMatrix.Right.Header;

            // keep track of the location of the node that we inserted last so that when we want
            // to insert a new node, we can link the last inserted node to the new node
            Node lastInsertedNode;

            // keep track of the current value at the 0,1 Matrix
            byte currentValue;

            // keep track of the current header node of this row
            HeaderNode currentHeader;

            // new node to be inserted
            Node nodeToBeInserted;

            // go over all the rows in the 0,1 Matrix and where there is a '1', insert a new node
            for (int row = 0; row < rowAmount; row++)
            {
                // reset the last inserted node with every passing row
                lastInsertedNode = null;

                // go over the cols in the current row index
                for (int col = 0; col < colAmount; col++)
                {
                    // update the current value
                    currentValue = coverMatrix[row, col];

                    // if current value is 1, new node
                    // if current value 0, do nothing
                    if (currentValue == 0) continue;

                    // if we reached this place, the current value is 1
                    // get the coorespoinding header node
                    currentHeader = headersRow[col];

                    nodeToBeInserted = new Node(currentHeader);

                    // attach the header node to the node to be inserted
                    currentHeader.Up.AttachDown(nodeToBeInserted);

                    // if it is the first inserted node in this col, make it the last inserted node
                    // if not, attach the last inserted node to this node and then update the last inserted
                    if (lastInsertedNode != null)
                    {
                        lastInsertedNode.AttachRight(nodeToBeInserted);
                        lastInsertedNode = lastInsertedNode.Right;
                    }
                    // if no last inserted node exists, make the current one the last inserted
                    else
                    {
                        lastInsertedNode = nodeToBeInserted;
                    }
                    // update the size of the current header node after every insertion
                    currentHeader.Size++;
                }
            }
            // return the root of the new node Matrix
            return;
        }

        /// <summary>
        /// this is a fuction that gets the root of the Matrix of nodes, and goes over
        /// all the header nodes and compares their sizes and 
        /// it picks the header node with the smallest size
        /// </summary>
        /// <param name="root">the root of the node Matrix</param>
        /// <param name="colWithMinSize">the col with the least nodes in it</param>
        public static void FindHeaderWithLeastNodes(HeaderNode root, out HeaderNode colWithMinSize)
        {
            // current header node 
            // by deafult set to the first header node
            HeaderNode currentHeaderNode = (HeaderNode)root.Right;

            // the header node with the samllest size 
            // by deafult set to the first header node
            colWithMinSize = (HeaderNode)root.Right;

            // go over all the header nodes and change the min node if needed
            // while we havent reached the root
            while (currentHeaderNode != root)
            {
                // comapre the two sizes and change the min if 
                // the current size is smaller then min node size
                if (currentHeaderNode.Size < colWithMinSize.Size) colWithMinSize = currentHeaderNode;

                // continue going right untill we reach the root again
                currentHeaderNode = (HeaderNode)currentHeaderNode.Right;
            }
            // return the heder node witht eh smallest size
            return;
        }

        /// <summary>
        /// this is a function that will get the stack of nodes that represent the solution
        /// and will convert this stack to a SudokuBoard of bytes that willl visibally represent
        /// the correct solution of the SudokuBoard
        /// </summary>
        /// <param name="solutionStack">the stack of nodes that make out the solution of the SudokuBoard</param>
        /// <param name="size">the size of the new SudokuBoard</param>
        public static int[,] ConvertSolutionStackToBoard(Stack<Node> solutionStack, int size)
        {
            // initialize the SudokuBoard
            int[,] board = new int[size, size];

            // keep track of the current node, the first node and the temp firstNode
            Node currentNode, firstNode, tempFirstNode;

            // save the current header's name value and the min header value
            int currentHeaderValue, minHeaderValue;

            // The right node is needed to determine the value that should be placed in the SudokuBoard at the position represented by the first node.
            // The name of the header node of the first node is used to calculate the row and column indices of this position,
            // and the name of the header node of the right node is used to calculate the value to be placed at this position.

            // the right node's name's value
            int rightHeaderValue;

            // the row, col and the value that will be placed in the SudokuBoard
            int row, col, value;

            // go over all the nodes in the stack
            while (solutionStack.Count != 0)
            {
                // get the current node by poping the stack
                currentNode = solutionStack.Pop();
                // set the first node to be the current node and the temp to be the right 
                // of he current node
                firstNode = currentNode;
                tempFirstNode = firstNode.Right;

                // set the value of the name of the header col of the first node
                minHeaderValue = Convert.ToInt32(firstNode.Header.Name);

                // go over all the header's names of the nodes in the same row as the current node
                // and find the header with the smallest name 
                while (tempFirstNode != currentNode)
                {
                    // get the value the name of the header col of the current node
                    currentHeaderValue = (int)Int64.Parse(tempFirstNode.Header.Name);

                    // if the current vlaue is smaller then the min value
                    if (currentHeaderValue < minHeaderValue)
                    {
                        // change the current value and the first node
                        firstNode = tempFirstNode;
                        minHeaderValue = (int)Int64.Parse(firstNode.Header.Name);
                    }
                    // go to the next node in the current row
                    tempFirstNode = tempFirstNode.Right;
                }
                // set the value of the node to the right that will be used to set the value
                rightHeaderValue = (int)Int64.Parse(firstNode.Right.Header.Name);

                // the name represents the inhe index from the start of the array of how many
                // cells we need to move to rach the curent cell so for example in a 9 by 9 SudokuBoard
                // index of 64 means row = (64/9) = 7 and col = (64%9) = 1

                // In this particular implementation, the names of the header nodes are integers that represent indices in a linear array.
                // The row and column indices of a position in the Matrix are calculated by dividing and modding this integer by the size of the Matrix.
                // The value to be placed at this position is calculated by modding the name of the header node of the right node by the size of the Matrix and adding 1.

                // get the row, col from the Min value
                row = minHeaderValue / size;
                col = minHeaderValue % size;

                // calculate the value from the value of the node to the right of the first node
                value = rightHeaderValue % size + 1;

                // insert the value into the SudokuBoard
                board[row, col] = value;

                // continue the recursion for all nodes in the stack
            }

            // return the final SudokuBoard as a 2d array of ints
            return board;
        }
    }
}
