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
    public class DancingLinks : BoardSolver
    {
        #region new dancing links functions

        // the matrix that will represent the exact cover problem
        // that will be solved by the dancing links algorithm
        public byte[,] CoverMatrix;

        // root of the header node matrix 
        public HeaderNode Root;

        // the number of constarinsts in the exact cover problem
        // 4 because each cell has 4 constraints, one for each house
        public const int NUMBER_OF_CONSTRAINTS = 4;

        // stack of Nodes that represent the current solution
        public Stack<Node> DLX_Solution;

        // constructor that gets in a string and converts the string into a matrix
        public DancingLinks(byte[,] matrix, int size) : base(matrix, size)
        {
            
            // convert the matrix into the exact cover problem matrix
            ConvertMatrixToExactCoverMatrix(matrix, Size, BlockSize, NUMBER_OF_CONSTRAINTS, out CoverMatrix);

            // convert the 0,1 matrix into a linked list matrix
            ConvertCoverMatrixToNodeMatrix(CoverMatrix, out Root);

            // create new list that will hold the solution nodes
            DLX_Solution = new Stack<Node>();
        }

        /// <summary>
        /// searching function that will search for the solutions for this node matrix
        /// all the solutions will be stored in the solutions list
        /// Explanation to how the function works:
        /// 
        /// The reduction from the grid G must preserve the constraints in a binary matrix
        /// M. In M there must then exist a selection of rows such that a union between them
        /// covers all columns, otherwise there is no solution.
        /// </summary>
        /// <returns></returns>
        public bool Search()
        {
            // stoppig condition, if the root is the only node in the matrix, then we have found a solution
            if (Root.Right == Root)
            {
                return true;
            }

            // get the col with the least amount of nodes in it and cover it
            HeaderNode LeastPopulatedCol;
            FindHeaderWithLeastNodes(Root, out LeastPopulatedCol);
            LeastPopulatedCol.CoverCol();

            // go over the nodes in the current col and cover all the other cols that 
            // have nodes on the same row as the given nodes in this col
            Node CurrentRow = LeastPopulatedCol.Down;

            // current node in the row that is being covered
            Node NodeToBeCovered;

            // while the current row is not empty
            while (CurrentRow != LeastPopulatedCol)
            {
                // add the current row to the stack of nodes that represent the solution
                DLX_Solution.Push(CurrentRow);

                // cover all the cols that have nodes in the same rows as the col
                // that is currently being covered
                NodeToBeCovered = CurrentRow.Right;

                // while the node doesnt point to itelf, cover all the header cols
                while (NodeToBeCovered != CurrentRow)
                {
                    // cover the header col of this current node
                    NodeToBeCovered.Header.CoverCol();
                    // move on to the next node in the row
                    NodeToBeCovered = NodeToBeCovered.Right;
                }

                // call the function recursivally and return true in this call
                // of the function if a solution is found
                if (Search()) return true;

                // if the recursion failed and couldnt find a solution, undo all the changes
                // meaning that every node that was added to the solution list, remove it
                // and every col that was covered, uncover it
                CurrentRow = DLX_Solution.Pop();
                LeastPopulatedCol = CurrentRow.Header;

                // uncover all the cols that were covered during the process if the recursion
                // returned a 'false' result
                NodeToBeCovered = CurrentRow.Left;
                while (NodeToBeCovered != CurrentRow)
                {
                    // go over all the affected cols and uncover them
                    NodeToBeCovered.Header.UncoverCol();
                    NodeToBeCovered = NodeToBeCovered.Left;
                }
                // go to the next row in the current row
                CurrentRow = CurrentRow.Down;
            }
            // uncover the current col that was covered in the beggining and return
            // false if the search function couldnt find a result 
            LeastPopulatedCol.UncoverCol();
            return false;
        }

        /// <summary>
        /// implementation of the solve function that solves the board and 
        /// returns if managed to solve or not,
        /// board is updated if there was a viable solution
        /// </summary>
        /// <returns>if the board is solved or not</returns>
        public override bool Solve()
        {
            bool Solved =  Search();
            BoardInts = ConvertSolutionStackToBoard(DLX_Solution, Size);
            return Solved;
        }

        /// <summary>
        /// implementation of the string getter function that updates the solved board
        /// if the board is solved
        /// </summary>
        /// <returns>the board of the solved board</returns>
        public override string GetSolutionString()
        {
            bool Solved = Search();
            BoardInts = ConvertSolutionStackToBoard(DLX_Solution, Size);
            if (Solved)
            {
                return ConvertToString(BoardInts, Size);
            }
            return "";
        }

        #endregion new dancing links functions

    }
}
