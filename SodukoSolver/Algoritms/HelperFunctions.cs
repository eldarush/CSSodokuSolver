﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static SodukoSolver.Exceptions.CustomExceptions;
using static SodukoSolver.Algoritms.ValidatingFunctions;
using System.Diagnostics.Metrics;

namespace SodukoSolver.Algoritms
{
    public static class HelperFunctions
    {
        /// <summary>
        /// Function that gets a board string and returns a board of cells
        /// </summary>
        /// <param name="size"> the size of the board</param>
        /// <param name="boardString">the string that represents the board</param>
        /// <returns>the board of cells</returns>
        public static Cell[,] CreateBoard(int size, string boardString)
        {
            // create a new board
            Cell[,] board = new Cell[size, size];

            // copy the board string to the board
            CopyBoardStringToBoard(size, boardString, board);

            // return the board
            return board;
        }

        /// <summary>
        /// Function that copies the board string to the board
        /// </summary>
        /// <param name="size"> the size of the board</param>
        /// <param name="boardString">the string that represents the board</param>
        /// <param name="board">the board to copy the string to</param>
        private static void CopyBoardStringToBoard(int size, string boardString, Cell[,] board)
        {
            // create a counter for the board string
            int counter = 0;

            // loop through the rows
            for (int i = 0; i < size; i++)
            {
                // loop through the columns
                for (int j = 0; j < size; j++)
                {
                    // copy the board string to the board
                    board[i, j] = new Cell(size, Convert.ToInt32(boardString[counter]) - '0');
                    // increment the counter
                    counter++;
                }
            }
        }

        /// <summary>
        /// function taht updates the board string
        /// </summary>
        /// <param name="EmptyCells">the array of empty cells</param>
        /// <param name="board">the board of cells</param>
        /// <param name="size">the size</param>
        public static int[] UpdateEmptyCells(int[] EmptyCellsArray, Cell[,] board, int size, float emptyCellsCount)
        {
            EmptyCellsArray = new int[(int)emptyCellsCount];

            // create a counter for the empty cells
            int counter = 0;
            // loop through the rows
            for (int i = 0; i < size; i++)
            {
                // loop through the columns
                for (int j = 0; j < size; j++)
                {
                    // if the cell is empty
                    // then add its index to the empty cells array
                    if (board[i, j].Value == 0)
                    {
                        EmptyCellsArray[counter] = i * size + j;
                        counter++;
                    }
                }
            }
            return EmptyCellsArray;
        }

        /// <summary>
        /// Function that chekcs if the board is valid
        /// </summary>
        /// <param name="size">the size of the board</param>
        /// <param name="boardString">the string that represents the board</param>
        /// <returns>if the board is valid or not</returns>
        public static bool IsTheBoardValid(int size, string boardString)
        {
            bool valid = false;
            try
            {
                valid = Validate(size, boardString);
            }
            // catch the custom exceptions, print their error messages and exit
            catch (SizeException se)
            {
                Console.WriteLine(se.Message);
                Environment.Exit(0);
            }
            catch (InvalidCharacterException ice)
            {
                Console.WriteLine(ice.Message);
                Environment.Exit(0);
            }
            catch (BoardCellsNotValidException bcne)
            {
                Console.WriteLine(bcne.Message);
                Environment.Exit(0);
            }
            catch (NullBoardException nbe)
            {
                Console.WriteLine(nbe.Message);
                Environment.Exit(0);
            }
            return valid;
        }

        /// <summary>
        /// function that prints the ascci art in the console
        /// </summary>
        public static void PrintAsciiArt()
        {
            // the most beautiful art known to mankind
            var arr = new[] {
            @"┌────────────────────────────────────────────────────────────────────────────────────────────────────┐",
            @"│ ╔═══╗────╔╗──╔╗───────╔═══╗──╔╗──────────╔══╗───────╔═══╦╗──╔╗───────╔═══╗──╔╗──────╔╗─────╔╗───── │",
            @"│ ║╔═╗║────║║──║║───────║╔═╗║──║║──────────║╔╗║───────║╔══╣║──║║───────║╔═╗║──║║──────║║─────║║───── │",
            @"│ ║╚══╦══╦═╝╠╗╔╣║╔╦╗╔╗──║╚══╦══╣╠╗╔╦══╦═╗──║╚╝╚╦╗─╔╗──║╚══╣║╔═╝╠══╦═╗──║║─║╠══╣║╔══╦═╗║╚═╦══╦╣║╔╗─╔╗ │",
            @"│ ╚══╗║╔╗║╔╗║║║║╚╝╣║║║──╚══╗║╔╗║║╚╝║║═╣╔╝──║╔═╗║║─║║──║╔══╣║║╔╗║╔╗║╔╝──║╚═╝║══╣║║╔╗║╔╗╣╔╗║║═╬╣║║║─║║ │",
            @"│ ║╚═╝║╚╝║╚╝║╚╝║╔╗╣╚╝║──║╚═╝║╚╝║╚╗╔╣║═╣║───║╚═╝║╚═╝║──║╚══╣╚╣╚╝║╔╗║║───║╔═╗╠══║╚╣╔╗║║║║╚╝║║═╣║╚╣╚═╝║ │",
            @"│ ╚═══╩══╩══╩══╩╝╚╩══╝──╚═══╩══╩═╩╝╚══╩╝───╚═══╩═╗╔╝──╚═══╩═╩══╩╝╚╩╝───╚╝─╚╩══╩═╩╝╚╩╝╚╩══╩══╩╩═╩═╗╔╝ │",
            @"│ ─────────────────────────────────────────────╔═╝║────────────────────────────────────────────╔═╝║─ │",
            @"│ ─────────────────────────────────────────────╚══╝────────────────────────────────────────────╚══╝─ │",
            @"└────────────────────────────────────────────────────────────────────────────────────────────────────┘"
            };
            Console.WriteLine("\n");
            foreach (string line in arr)
                Console.WriteLine(line);
            Console.WriteLine("\n");
            // set the console title
            Console.Title = "Sudoku Solver by @Eldar Aslanbeily";
        }

        /// <summary>
        /// function that prints the board
        /// </summary>
        /// <param name="board">the board</param>
        /// <param name="size">the size of the board</param>
        public static void PrintBoard(Cell[,] board, int size)
        {
            // Find the maximum length of the string representation of any element in the board
            int maxLength = 0;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    int length = board[i, j].Value.ToString().Length;
                    if (length > maxLength)
                    {
                        maxLength = length;
                    }
                }
            }

            // Calculate the width of each square
            int squareWidth = maxLength + 2;

            // Calculate the size of the sub-squares (if any)
            int subSquareSize = (int)Math.Sqrt(size);

            // Print the top border
            Console.Write(" ");
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < squareWidth; j++)
                {
                    Console.Write("-");
                }
                if ((i + 1) % subSquareSize == 0 && i != size - 1)
                {
                    Console.Write("+");
                }
            }
            Console.WriteLine();

            // Print the rows
            for (int i = 0; i < size; i++)
            {
                // Print the left border
                Console.Write("|");

                // Print the elements in the row
                for (int j = 0; j < size; j++)
                {
                    // if the size more then 9, print the value with a 0 in front
                    string element;
                    if (size >= 10)
                    {
                        element = board[i, j].Value.ToString().PadLeft(2, '0');  // add leading zero for single digit values
                    }
                    else
                    {
                        element = board[i, j].Value.ToString();
                    }
                    int padding = (squareWidth - element.Length) / 2;
                    for (int k = 0; k < padding; k++)
                    {
                        Console.Write(" ");
                    }
                    Console.Write(element);
                    for (int k = 0; k < padding; k++)
                    {
                        Console.Write(" ");
                    }
                    if ((j + 1) % subSquareSize == 0 && j != size - 1)
                    {
                        Console.Write("|");
                    }
                }

                // Print the right border
                Console.WriteLine("|");

                // Print the bottom border for the row
                if ((i + 1) % subSquareSize == 0 && i != size - 1)
                {
                    Console.Write(" ");
                    for (int j = 0; j < size; j++)
                    {
                        for (int k = 0; k < squareWidth; k++)
                        {
                            Console.Write("-");
                        }
                        if ((j + 1) % subSquareSize == 0 && j != size - 1)
                        {
                            Console.Write("+");
                        }
                    }
                    Console.WriteLine();
                }
            }

            // Print the bottom border
            Console.Write(" ");
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < squareWidth; j++)
                {
                    Console.Write("-");
                }

                if ((i + 1) % subSquareSize == 0 && i != size - 1)
                {
                    Console.Write("+");
                }
            }
            Console.WriteLine();
        }

        /// <summary>
        /// function that converts board of cells to board of ints
        /// </summary>
        /// <param name="board">the board of cells</param>
        /// <returns>the board of cells</returns>
        public static Cell[,] IntsToCells(int[,] board)
        {
            int size = board.GetLength(0);
            Cell[,] newBoard = new Cell[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    newBoard[i, j] = new Cell(size, board[i, j]);
                }
            }
            return newBoard;
        }

        /// <summary>
        /// function that converts a boardstring to a board of ints
        /// </summary>
        /// <param name="boardstring">the string that represents the board</param>
        /// <param name="array">the 2d array where the board will be stored</param>
        /// <param name="size">the size of the board</param>
        /// <returns>the 2d array of ints</returns>
        public static int[,] ConvertTo2DArray(string boardstring, int[,] array, int size)
        {
            if (array is null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            int[,] newBoard = new int[size, size];
            int counter = 0;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    newBoard[i, j] = boardstring[counter] - '0';
                    counter++;
                }
            }
            return newBoard;
        }

        /// <summary>
        /// function that converts board of ints to string
        /// </summary>
        /// <param name="array">the 2d array where the board will be stored</param>
        /// <param name="size">the size of the board</param>
        /// <returns>the string that represents the board</returns>
        public static string ConvertToString(int[,] array, int size)
        {
            string boardstring = "";
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    boardstring += (char)(array[i, j] + '0');
                }
            }
            return boardstring;
        }

        /// <summary>
        /// GetBoardString method that gets the board string from the user
        /// </summary>
        /// <returns>the string that represents the board form the console</returns>
        public static string GetBoardString()
        {
            // create a new string builder
            StringBuilder boardString = new();

            // get the board string from the user
            Console.WriteLine("Please enter the board string:");
            boardString.Append(Console.ReadLine());

            // return the board string
            return boardString.ToString();
        }

        /// <summary>
        ///  fumction that gets a board and counts the amount of empty cells
        ///   empty cells are cells with value 0
        /// </summary>
        /// <param name="board">the board</param>
        /// <param name="size">the size</param>
        /// <returns>how many empty cells are there</returns>
        public static float CountEmptyCells(Cell[,] board, int size)
        {
            // create a counter for the empty cells
            float counter = 0;

            // loop through the rows
            for (int i = 0; i < size; i++)
            {
                // loop through the columns
                for (int j = 0; j < size; j++)
                {
                    // if the cell is empty
                    if (board[i, j].Value == 0)
                    {
                        // increment the counter
                        counter++;
                    }
                }
            }

            // return the counter
            return counter;
        }

        /// <summary>
        /// function that gets a board and returns an exact copy of it
        /// </summary>
        /// <param name="board">origin board</param>
        /// <param name="size">size of the board</param>
        /// <returns>exact copy of the board</returns>
        public static Cell[,] CopyBoard(Cell[,] board, int size)
        {
            // create a new board
            Cell[,] newBoard = new Cell[size, size];

            // loop through the rows
            for (int i = 0; i < size; i++)
            {
                // loop through the columns
                for (int j = 0; j < size; j++)
                {
                    // copy the cell to the new board
                    newBoard[i, j] = new Cell(size, board[i, j].Value)
                    {
                        // copy the candidates
                        Candidates = new HashSet<int>(board[i, j].Candidates),
                        // copy the solved boolean
                        Solved = board[i, j].Solved
                    };
                }
            }

            // return the new board
            return newBoard;
        }

        /// <summary>
        /// function that gets a board and checks if it is solved
        /// </summary>
        /// <param name="board">the board</param>
        /// <param name="size">the size</param>
        /// <returns>if the board is solved</returns>
        public static bool IsSolved(Cell[,] board, int size)
        {
            // loop through the rows
            for (int i = 0; i < size; i++)
            {
                // loop through the columns
                for (int j = 0; j < size; j++)
                {
                    // if the cell is not solved
                    if (board[i, j].Value == 0)
                    {
                        // return false
                        return false;
                    }
                }
            }

            // return true
            return true;
        }

        /// <summary>
        /// function that gets a board and checks if it is solved
        /// </summary>
        /// <param name="board">the board</param>
        /// <param name="size">the size</param>
        /// <returns>if the board is solved</returns>
        public static bool IsSolvedInts(int[,] board, int size)
        {
            // loop through the rows
            for (int i = 0; i < size; i++)
            {
                // loop through the columns
                for (int j = 0; j < size; j++)
                {
                    // if the cell is not solved
                    if (board[i, j] == 0)
                    {
                        // return false
                        return false;
                    }
                }
            }

            // return true
            return true;
        }

        /// <summary>
        /// function that gets a board and print the candidates of each cell
        /// </summary>
        /// <param name="board">the board</param>
        /// <param name="size">the size</param>
        public static void PrintCandidatesInBoard(Cell[,] board, int size)
        {
            // loop through the rows
            for (int i = 0; i < size; i++)
            {
                // loop through the columns
                for (int j = 0; j < size; j++)
                {
                    // print the candidates
                    Console.Write($"Cell ({i + 1},{j + 1}): ");
                    // print the value
                    Console.Write(" Value: " + board[i, j].Value + " Candidates: ");
                    foreach (int candidate in board[i, j].Candidates)
                    {
                        Console.Write(candidate + " ");
                    }
                    Console.WriteLine();
                }
            }
        }

        # region dancing links
        // --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                 DANCING LINKS
        // --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// function taht converts the board of cells to board of nodes for the dancing links 
        /// </summary>
        /// <param name="board">board of cells</param>
        /// <param name="size">size</param>
        /// <returns>board of nodes</returns>
        public static Node[][] ConvertBoard(Cell[,] board, int size)
        {
            // create a matrix of nodes with the same dimensions as the board
            Node[][] matrix = new Node[size * size][];
            for (int i = 0; i < size * size; i++)
            {
                matrix[i] = new Node[size];
            }

            // create a dictionary to map cell values to column indices
            Dictionary<int, int> valueToCol = new();
            int nextCol = 0;

            // iterate through the cells in the board
            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    // create a new node for the cell
                    Node node = new()
                    {
                        Row = row,
                        Col = col,
                        Value = board[row, col].Value
                    };

                    // if the cell has a value, add it to the dictionary if it's not already there
                    if (node.Value > 0)
                    {
                        if (!valueToCol.ContainsKey(node.Value))
                        {
                            valueToCol[node.Value] = nextCol++;
                        }
                        node.Col = valueToCol[node.Value];
                    }

                    // add the node to the matrix
                    matrix[row][col] = node;
                }
            }

            // create a root node to represent the entire matrix
            Node root = new();
            root.Left = root;
            root.Right = root;
            root.Up = root;
            root.Down = root;

            // create linked lists for each row and column in the matrix
            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    Node node = matrix[row][col];

                    // insert the node into the row linked list
                    if (col == 0)
                    {
                        node.Left = root;
                        node.Right = matrix[row][col + 1];
                        matrix[row][col + 1].Left = node;
                    }
                    else if (col == size - 1)
                    {
                        node.Left = matrix[row][col - 1];
                        node.Right = root;
                        matrix[row][col - 1].Right = node;
                    }
                    else
                    {
                        node.Left = matrix[row][col - 1];
                        node.Right = matrix[row][col + 1];
                        matrix[row][col - 1].Right = node;
                        matrix[row][col + 1].Left = node;
                    }

                    // insert the node into the column linked list
                    if (row == 0)
                    {
                        node.Up = root;
                        node.Down = matrix[row + 1][col];
                        matrix[row + 1][col].Up = node;
                    }
                    else if (row == size - 1)
                    {
                        node.Up = matrix[row - 1][col];
                        node.Down = root;
                        matrix[row - 1][col].Down = node;
                    }
                    else
                    {
                        node.Up = matrix[row - 1][col];
                        node.Down = matrix[row + 1][col];
                        matrix[row - 1][col].Down = node;
                        matrix[row + 1][col].Up = node;
                    }

                }
            }

            // link the root node to the first and last nodes in the first and last rows
            root.Right = matrix[0][0];
            root.Left = matrix[0][size - 1];
            matrix[0][0].Left = root;
            matrix[0][size - 1].Right = root;

            root.Down = matrix[0][0];
            root.Up = matrix[size - 1][0];
            matrix[0][0].Up = root;
            matrix[size - 1][0].Down = root;

            // return the matrix of nodes
            return matrix;
        }

        ///// <summary>
        ///// function that chooses the columm with the fewest "on" cells
        ///// </summary>
        ///// <param name="root">the root of the matrix</param>
        ///// <returns>the columm with the fewest "on" cells</returns>
        //public static Node ChooseColumn(Node root)
        //{
        //    // start with the root node's right pointer
        //    Node col = root.Right;

        //    // keep track of the column with the fewest "on" cells
        //    Node chosenCol = col;
        //    int minOnCells = int.MaxValue;

        //    // iterate through the columns
        //    while (col != root)
        //    {
        //        // count the number of "on" cells in the column
        //        int onCells = 0;
        //        for (Node cell = col.Down; cell != col; cell = cell.Down)
        //        {
        //            onCells++;
        //        }

        //        // if the number of "on" cells is smaller than the current minimum, update the chosen column
        //        if (onCells < minOnCells)
        //        {
        //            chosenCol = col;
        //            minOnCells = onCells;
        //        }

        //        // move to the next column
        //        col = col.Right;
        //    }

        //    // if no columns have "on" cells, return the root node
        //    if (minOnCells == 0)
        //    {
        //        return root;
        //    }

        //    return chosenCol;
        //}

        ///// <summary>
        ///// function that updates the cover columm
        ///// </summary>
        ///// <param name="col">the columm</param>
        //public static void CoverColumn(Node col)
        //{
        //    // update the left and right pointers of the nodes to the left and right of the column
        //    col.Left.Right = col.Right;
        //    col.Right.Left = col.Left;

        //    // iterate through the "on" cells in the column
        //    for (Node row = col.Down; row != col; row = row.Down)
        //    {
        //        // update the up and down pointers of the nodes in the column
        //        row.Up.Down = row.Down;
        //        row.Down.Up = row.Up;
        //    }
        //}

        

        public static bool SolveUsingDancingLinks(Node[][] board, int size, List<Node> headers)
        {
            // add the rows to the linked lists
            var rows = AddRows(board, headers, size);

            // search for a solution
            return Search(rows, headers, 0);
        }

        private static void CoverColumn(Node column)
        {
            // remove the column from the linked list
            column.Right.Left = column.Left;
            column.Left.Right = column.Right;

            // iterate through the rows in the covered column
            for (var row = column.Down; row != column; row = row.Down)
            {
                // remove the row's right and left nodes from their respective linked lists
                var rightNode = row.Right;
                while (rightNode != row)
                {
                    rightNode.Down.Up = rightNode.Up;
                    rightNode.Up.Down = rightNode.Down;
                    rightNode = rightNode.Right;
                }
            }
        }

        private static void UncoverColumn(Node columnHeader)
        {
            // set the column header's right and left pointers to point to itself
            columnHeader.Right.Left = columnHeader;
            columnHeader.Left.Right = columnHeader;

            // set the up and down pointers of the nodes in the column to point to themselves
            for (Node row = columnHeader.Down; row != columnHeader; row = row.Down)
            {
                for (Node node = row.Right; node != row; node = node.Right)
                {
                    node.Up.Down = node;
                    node.Down.Up = node;
                    node.ColumnHeader = columnHeader;
                }
            }
        }

        //private static List<Node> InitializeHeaders(int size)
        //{
        //    var headers = new List<Node>();

        //    // create a column header node for each column
        //    for (int col = 0; col < size; col++)
        //    {
        //        var header = new Node
        //        {
        //            Col = col,
        //            Value = 0
        //        };
        //        headers.Add(header);
        //    }

        //    // link the column headers together
        //    for (int i = 0; i < headers.Count; i++)
        //    {
        //        headers[i].Right = headers[(i + 1) % headers.Count];
        //        headers[i].Left = headers[(i + headers.Count - 1) % headers.Count];
        //    }

        //    return headers;
        //}

        private static List<Node> AddRows(Node[][] board, List<Node> headers, int size)
        {
            var rows = new List<Node>();

            // create a node for each cell in the board
            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    var node = new Node
                    {
                        Row = row,
                        Col = col,
                        Value = board[row][col].Value,
                        ColumnHeader = headers[col]  // set the column header for this node
                    };
                    rows.Add(node);
                }
            }

            // link the nodes in each row together
            for (int i = 0; i < rows.Count; i++)
            {
                rows[i].Right = rows[(i + 1) % rows.Count];
                rows[i].Left = rows[(i + rows.Count - 1) % rows.Count];
            }

            // link the nodes in each column together
            for (int col = 0; col < headers.Count; col++)
            {
                var header = headers[col];
                var current = header;
                for (int row = 0; row < rows.Count; row++)
                {
                    if (rows[row].Col == col)
                    {
                        current.Down = rows[row];
                        rows[row].Up = current;
                        current = rows[row];
                    }
                }
                current.Down = header;
                header.Up = current;
            }

            return rows;
        }


        private static Node ChooseColumn(List<Node> headers)
        {
            int minCount = int.MaxValue;
            Node minColumn = null;

            // find the column with the fewest possible values
            foreach (var header in headers)
            {
                if (header.Right == header)
                    continue;

                int count = 0;
                for (var row = header.Down; row != header; row = row.Down)
                    count++;

                if (count < minCount)
                {
                    minCount = count;
                    minColumn = header;
                }
            }

            return minColumn;
        }

        private static bool Search(List<Node> rows, List<Node> headers, int depth)
        {
            // if the board is complete, return true
            if (headers[0].Right == headers[0])
                return true;

            // choose the next column with the fewest possible values
            var column = ChooseColumn(headers);

            // cover the column to remove it from the consideration
            CoverColumn(column);

            // iterate through the rows in the covered column
            for (var row = column.Down; row != column; row = row.Down)
            {
                // add the row to the solution
                rows[depth] = row;

                // cover the row's right and left nodes to remove them from the consideration
                var rightNode = row.Right;
                while (rightNode != row)
                {
                    CoverColumn(rightNode.ColumnHeader);
                    rightNode = rightNode.Right;
                }

                // search for a solution with the current row added
                if (Search(rows, headers, depth + 1))
                    return true;

                // backtrack: uncover the row's right and left nodes to restore them to the consideration
                var leftNode = row.Left;
                while (leftNode != row)
                {
                    UncoverColumn(leftNode.ColumnHeader);
                    leftNode = leftNode.Left;
                }
            }

            // uncover the column to restore it to the consideration
            UncoverColumn(column);

            return false;
        }


        /// <summary>
        /// very simple printBoard function for board of nodes
        /// </summary>
        /// <param name="board">the board</param>
        /// <param name="size">the size</param>
        public static void PrintBoardNode(Node[][] board, int size)
        {
            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    Node node = board[row][col];
                    Console.Write(node.Value + " ");
                }
                Console.WriteLine();
            }
        }


        /// <summary>
        /// function that gets a boarf of cells and its size and returns the string representation of the board
        /// </summary>
        /// <param name="board">the board</param>
        /// <param name="size">the size</param>
        /// <returns>the board string from the board</returns>
        public static string GetBoardString(Cell[,] board, int size)
        {
            string boardString = "";
            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    // convert each cell value to its ascii representation
                    boardString += (char)(board[row, col].Value + '0');
                }
            }
            return boardString;
        }

        /// <summary>
        /// function that gets a string representation of a board and its size and returns the board of nodes
        /// </summary>
        /// <param name="board">the board</param>
        /// <param name="size">the size</param>
        /// <returns>the board that was made out of the string</returns>
        public static Node[][] ConvertStringBoard(string board, int size)
        {
            // create a matrix of nodes with the same dimensions as the board
            Node[][] matrix = new Node[size * size][];
            for (int i = 0; i < size * size; i++)
            {
                matrix[i] = new Node[size];
            }

            // create a dictionary to map cell values to column indices
            Dictionary<int, int> valueToCol = new();
            int nextCol = 0;

            // iterate through the cells in the board
            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    // create a new node for the cell
                    Node node = new()
                    {
                        Row = row,
                        Col = col,
                        Value = int.Parse(board[row * size + col].ToString())
                    };

                    // if the cell has a value, add it to the dictionary if it's not already there
                    if (node.Value > 0)
                    {
                        if (!valueToCol.ContainsKey(node.Value))
                        {
                            valueToCol[node.Value] = nextCol++;
                        }
                        node.Col = valueToCol[node.Value];
                    }

                    // add the node to the matrix
                    matrix[row][col] = node;
                }
            }

            // create a root node to represent the entire matrix
            Node root = new();
            root.Left = root;
            root.Right = root;
            root.Up = root;
            root.Down = root;
            // TODO: initialize columm headers here
            // the problem is that columm headers are not initailized and there is a nullpointerexception

            // create linked lists for each row and column in the matrix
            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    Node node = matrix[row][col];

                    // insert the node into the row linked list
                    if (col == 0)
                    {
                        node.Left = root;
                        node.Right = matrix[row][col + 1];
                        matrix[row][col + 1].Left = node;
                    }
                    else if (col == size - 1)
                    {
                        node.Left = matrix[row][col - 1];
                        node.Right = root;
                        matrix[row][col - 1].Right = node;
                    }
                    else
                    {
                        node.Left = matrix[row][col - 1];
                        node.Right = matrix[row][col + 1];
                        matrix[row][col - 1].Right = node;
                        matrix[row][col + 1].Left = node;
                    }

                    // insert the node into the column linked list
                    if (row == 0)
                    {
                        node.Up = root;
                        node.Down = matrix[row + 1][col];
                        matrix[row + 1][col].Up = node;
                    }
                    // if the cell is in the last row
                    else if (row == size - 1)
                    {
                        node.Up = matrix[row - 1][col];
                        node.Down = root;
                        matrix[row - 1][col].Down = node;
                    }
                }
            }

            // link the root node to the first and last nodes in the first and last rows
            root.Right = matrix[0][0];
            root.Left = matrix[0][size - 1];
            matrix[0][0].Left = root;
            matrix[0][size - 1].Right = root;

            root.Down = matrix[0][0];
            root.Up = matrix[size - 1][0];
            matrix[0][0].Up = root;
            matrix[size - 1][0].Down = root;

            return matrix;
        }

        public static (Node[][] matrix, List<Node> headers) ConvertStringBoardFixed(string board, int size)
        {
            // create a matrix of nodes with the same dimensions as the board
            Node[][] matrix = new Node[size * size][];
            for (int i = 0; i < size * size; i++)
            {
                matrix[i] = new Node[size];
            }
            // create a list of column header nodes
            List<Node> headers = new List<Node>();

            // create a dictionary to map cell values to column indices
            Dictionary<int, int> valueToCol = new Dictionary<int, int>();
            int nextCol = 0;

            // iterate through the cells in the board
            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    // create a new node for the cell
                    Node node = new Node
                    {
                        Row = row,
                        Col = col,
                        Value = int.Parse(board[row * size + col].ToString())
                    };

                    // if the cell has a value, add it to the dictionary if it's not already there
                    if (node.Value > 0)
                    {
                        if (!valueToCol.ContainsKey(node.Value))
                        {
                            valueToCol[node.Value] = nextCol++;
                            headers.Add(new Node
                            {
                                Col = nextCol - 1
                            });
                        }
                        //node.Col = valueToCol[node.Value];
                    }
                    // add the node to the matrix
                    matrix[row][col] = node;
                }
            }

            // create a root node to represent the entire matrix
            Node root = new Node();
            root.Left = root;
            root.Right = root;
            root.Up = root;
            root.Down = root;

            // link the column headers together in a circular linked list
            for (int i = 0; i < headers.Count; i++)
            {
                headers[i].Right = headers[(i + 1) % headers.Count];
                headers[i].Left = headers[(i + headers.Count - 1) % headers.Count];
            }
            root.Left = headers[headers.Count - 1];
            root.Right = headers[0];
            headers[headers.Count - 1].Right = root;
            headers[0].Left = root;


             // create linked lists for each row and column in the matrix
            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    Node node = matrix[row][col];

                    // insert the node into the row linked list
                    if (col == 0)
                    {
                        node.Left = root;
                        node.Right = matrix[row][col + 1];
                        matrix[row][col + 1].Left = node;
                    }
                    else if (col == size - 1)
                    {
                        node.Left = matrix[row][col - 1];
                        node.Right = root;
                        matrix[row][col - 1].Right = node;
                    }
                    else
                    {
                        node.Left = matrix[row][col - 1];
                        node.Right = matrix[row][col + 1];
                        matrix[row][col - 1].Right = node;
                        matrix[row][col + 1].Left = node;
                    }
                    // insert the node into the column linked list
                    // insert the node into the column linked list
                    if (row == 0)
                    {
                        node.Up = headers[node.Col];
                        node.Down = matrix[row + 1][col];
                        matrix[row + 1][col].Up = node;

                        // set the column header's down pointer to point to this node
                        headers[node.Col].Down = node;

                    }
                    // if the cell is in the last row
                    else if (row == size - 1)
                    {
                        node.Up = matrix[row - 1][col];
                        node.Down = headers[node.Col];
                        matrix[row - 1][col].Down = node;

                        // set the column header's up pointer to point to this node
                        headers[node.Col].Up = node;

                    }
                    else
                    {
                        node.Up = matrix[row - 1][col];
                        node.Down = matrix[row + 1][col];
                        matrix[row - 1][col].Down = node;
                        matrix[row + 1][col].Up = node;

                    }

                    // set the column header for this node
                    node.ColumnHeader = headers[node.Col];
                }
            }

            // link the root node to the first and last nodes in the first and last rows
            root.Right = matrix[0][0];
            root.Left = matrix[0][size - 1];
            matrix[0][0].Left = root;
            matrix[0][size - 1].Right = root;

            return (matrix, headers);
        }



        /// <summary>
        /// function that gets a board of nodes and its size and returns the board of cells
        /// </summary>
        /// <param name="board">the board</param>
        /// <param name="size">the size</param>
        /// <returns>the created board of cells</returns>
        public static Cell[,] ConvertNodeBoardToCellBoard(Node[][] board, int size)
        {
            Cell[,] cellBoard = new Cell[size, size];
            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    cellBoard[row, col] = new Cell(size, board[row][col].Value);
                }
            }
            return cellBoard;
        }

        /// <summary>
        /// function that gets a board of cells and a value and wil return how many cells have its value as one of their candidates
        /// </summary>
        /// <param name="board">the board</param>
        /// <param name="size">the size</param>
        /// <param name="value">the value to be checked</param>
        /// <returns>how many cells have its value as one of their candidates</returns>
        public static int GetNumOfCellsThatHaveCandidate(Cell[,] board, int size, int value)
        {
            // check if the board is not null
            if (board == null)
            {
                return -1;
            }
            int count = 0;
            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    if (board[row, col].Candidates.Contains(value))
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        // --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                 DANCING LINKS
        // --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        #endregion dancing links

        
        public static void BoardStringToBoardOfInts(string boardString, int size, int[,] board)
        {
            int counter = 0;
            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    board[row, col] = Convert.ToInt32(boardString[counter]) - '0';
                    counter++;
                }
            }
        }

        public static void PrintBoardOfInts(int[,] board, int size)
        {
            {
                // Find the maximum length of the string representation of any element in the board
                int maxLength = 0;
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        int length = board[i, j].ToString().Length;
                        if (length > maxLength)
                        {
                            maxLength = length;
                        }
                    }
                }

                // Calculate the width of each square
                int squareWidth = maxLength + 2;

                // Calculate the size of the sub-squares (if any)
                int subSquareSize = (int)Math.Sqrt(size);

                // Print the top border
                Console.Write(" ");
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < squareWidth; j++)
                    {
                        Console.Write("-");
                    }
                    if ((i + 1) % subSquareSize == 0 && i != size - 1)
                    {
                        Console.Write("+");
                    }
                }
                Console.WriteLine();

                // Print the rows
                for (int i = 0; i < size; i++)
                {
                    // Print the left border
                    Console.Write("|");

                    // Print the elements in the row
                    for (int j = 0; j < size; j++)
                    {
                        // if the size more then 9, print the value with a 0 in front
                        string element;
                        if (size >= 10)
                        {
                            element = board[i, j].ToString().PadLeft(2, '0');  // add leading zero for single digit values
                        }
                        else
                        {
                            element = board[i, j].ToString();
                        }
                        int padding = (squareWidth - element.Length) / 2;
                        for (int k = 0; k < padding; k++)
                        {
                            Console.Write(" ");
                        }
                        Console.Write(element);
                        for (int k = 0; k < padding; k++)
                        {
                            Console.Write(" ");
                        }
                        if ((j + 1) % subSquareSize == 0 && j != size - 1)
                        {
                            Console.Write("|");
                        }
                    }

                    // Print the right border
                    Console.WriteLine("|");

                    // Print the bottom border for the row
                    if ((i + 1) % subSquareSize == 0 && i != size - 1)
                    {
                        Console.Write(" ");
                        for (int j = 0; j < size; j++)
                        {
                            for (int k = 0; k < squareWidth; k++)
                            {
                                Console.Write("-");
                            }
                            if ((j + 1) % subSquareSize == 0 && j != size - 1)
                            {
                                Console.Write("+");
                            }
                        }
                        Console.WriteLine();
                    }
                }

                // Print the bottom border
                Console.Write(" ");
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < squareWidth; j++)
                    {
                        Console.Write("-");
                    }

                    if ((i + 1) % subSquareSize == 0 && i != size - 1)
                    {
                        Console.Write("+");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}