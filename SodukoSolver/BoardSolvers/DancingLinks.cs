using SodukoSolver.DataStructures;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SodukoSolver.Algoritms.HelperFunctions;

namespace SodukoSolver.BoardSolvers
{
    /// <summary>
    /// this class inherits from the BoardSolver class and implements the dancing links algorithm
    /// </summary>
    public class DancingLinks : BoardSolver
    {
        #region dancing links functions

        // the number of constarinsts in the exact cover problem
        // 4 because each cell has 4 constraints, one for each house
        private const int NUMBER_OF_CONSTRAINTS = 4;

        // the Matrix that will represent the exact cover problem
        // that will be solved by the dancing links algorithm
        public byte[,] CoverMatrix;

        // root of the header node Matrix 
        private readonly HeaderNode _root;

        // stack of Nodes that represent the current solution
        private Stack<Node> _solutionStack;

        /// <summary>
        /// constructor that gets in a string and converts the string into a Matrix
        /// </summary>
        /// <param name="matrix">the martix</param>
        /// <param name="size">size of the matrix</param>
        public DancingLinks(byte[,] matrix, int size) : base(matrix, size)
        {  
            // convert the Matrix into the exact cover problem Matrix
            ConvertMatrixToExactCoverMatrix(matrix, Size, BlockSize, NUMBER_OF_CONSTRAINTS, out CoverMatrix);

            // convert the 0,1 Matrix into a linked list Matrix
            ConvertCoverMatrixToNodeMatrix(CoverMatrix, out _root);

            // create new stack that will hold the solution nodes
            _solutionStack = new Stack<Node>();
        }

        /// <summary>
        /// searching function that will search for the solutions for this node Matrix
        /// all the solutions will be stored in the solutions list
        /// Explanation to how the function works:
        /// 
        /// The reduction from the grid G must preserve the constraints in a binary Matrix
        /// M. In M there must then exist a selection of rows such that a union between them
        /// covers all columns, otherwise there is no solution.
        /// </summary>
        /// <returns></returns>
        public bool Search()
        {
            // stoppig condition, if the root is the only node in the Matrix, then we have found a solution
            if (_root.Right == _root)
            {
                return true;
            }

            // get the col with the least amount of nodes in it and cover it
            HeaderNode leastPopulatedCol;
            FindHeaderWithLeastNodes(_root, out leastPopulatedCol);
            leastPopulatedCol.CoverCol();

            // go over the nodes in the current col and cover all the other cols that 
            // have nodes on the same row as the given nodes in this col
            Node currentRow = leastPopulatedCol.Down;

            // current node in the row that is being covered
            Node nodeInGivenRow;

            // while the current row is not empty
            while (currentRow != leastPopulatedCol)
            {
                // add the current row to the stack of nodes that represent the solution
                _solutionStack.Push(currentRow);

                // cover all the cols that have nodes in the same rows as the col
                // that is currently being covered
                nodeInGivenRow = currentRow.Right;

                // while the node doesnt point to itelf, cover all the header cols
                while (nodeInGivenRow != currentRow)
                {
                    // cover the header col of this current node
                    nodeInGivenRow.Header.CoverCol();
                    // move on to the next node in the row
                    nodeInGivenRow = nodeInGivenRow.Right;
                }

                // call the function recursivally and return true in this call
                // of the function if a solution is found
                if (Search()) return true;

                // if the recursion failed and couldnt find a solution, undo all the changes
                // meaning that every node that was added to the solution list, remove it
                // and every col that was covered, uncover it
                currentRow = _solutionStack.Pop();
                leastPopulatedCol = currentRow.Header;

                // uncover all the cols that were covered during the process if the recursion
                // returned a 'false' result
                nodeInGivenRow = currentRow.Left;
                while (nodeInGivenRow != currentRow)
                {
                    // go over all the affected cols and uncover them
                    nodeInGivenRow.Header.UncoverCol();
                    nodeInGivenRow = nodeInGivenRow.Left;
                }
                // go to the next row in the current row
                currentRow = currentRow.Down;
            }
            // uncover the current col that was covered in the beggining and return
            // false if the search function couldnt find a result 
            leastPopulatedCol.UncoverCol();
            return false;
        }

        /// <summary>
        /// implementation of the solve function that solves the SudokuBoard and 
        /// returns if managed to solve or not,
        /// SudokuBoard is updated if there was a viable solution
        /// </summary>
        /// <returns>if the SudokuBoard is solved or not</returns>
        public override bool Solve()
        {
            // solve the SudokuBoard and store if managed to solve or not
            bool solved =  Search();
            // convert the solution stack to a SudokuBoard of ints
            BoardInts = ConvertSolutionStackToBoard(_solutionStack, Size);
            // return if managed to solve a SudokuBoard or not
            return solved;
        }

        /// <summary>
        /// implementation of the string getter function that updates the solved SudokuBoard
        /// if the SudokuBoard is solved
        /// </summary>
        /// <returns>the SudokuBoard of the solved SudokuBoard</returns>
        public override string GetSolutionString()
        {
            // solve the SudokuBoard and store if managed to solve or not
            bool solved = Search();
            // convert the solution stack to a SudokuBoard of ints
            BoardInts = ConvertSolutionStackToBoard(_solutionStack, Size);
            // if the SudokuBoard is solver return the SudokuBoard as a string
            if (solved)
            {
                return ConvertToString(BoardInts, Size);
            }
            // else return an empty string
            return "";
        }

        #endregion dancing links functions

    }
}
