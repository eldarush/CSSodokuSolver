using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SodukoSolver
{
    public static class HelperFunctions
    {
        // function that gets rows, columns, and board string and creates the board
        public static Cell[,] CreateBoard(int size, string boardString)
        {
            // create a new board
            Cell[,] board = new Cell[size, size];

            // copy the board string to the board
            CopyBoardStringToBoard(size, boardString, board);

            // return the board
            return board;
        }

        // CopyBoardStringToBoard function that copies the board string to the board
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

        //TODO: fix this function so that it prints the board correctly (currently missing the right border)
        // PrintBoard method that prints the board to the console
        // PrintBoard method that prints the board to the console
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

        //TODO: add validation here 
        // GetBoardString method that gets the board string from the user
        public static string GetBoardString()
        {
            // create a new string builder
            StringBuilder boardString = new StringBuilder();

            // get the board string from the user
            Console.WriteLine("Please enter the board string (81 digits):");
            boardString.Append(Console.ReadLine());

            // return the board string
            return boardString.ToString();
        }

        // fumction that gets a board and counts the amount of empty cells
        // empty cells are cells with value 0
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

        // function that gets a board and returns an exact copy of it
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
                    newBoard[i, j] = new Cell(size, board[i, j].Value);
                    // copy the candidates
                    newBoard[i, j].Candidates = new HashSet<int>(board[i, j].Candidates);
                    // copy the solved boolean
                    newBoard[i, j].Solved = board[i, j].Solved;
                }
            }

            // return the new board
            return newBoard;
        }

        // function that gets a board and checks if it is solved
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

        // function that gets a board and print the candidates of each cell
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


        public static Node[][] ConvertBoard(Cell[,] board, int size)
        {
            // create a matrix of nodes with the same dimensions as the board
            Node[][] matrix = new Node[size * size][];
            for (int i = 0; i < size * size; i++)
            {
                matrix[i] = new Node[size];
            }

            // create a dictionary to map cell values to column indices
            Dictionary<int, int> valueToCol = new Dictionary<int, int>();
            int nextCol = 0;

            // iterate through the cells in the board
            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    // create a new node for the cell
                    Node node = new Node();
                    node.Row = row;
                    node.Col = col;
                    node.Value = board[row, col].Value;

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
            Node root = new Node();
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

        public static Node ChooseColumn(Node root)
        {
            // start with the root node's right pointer
            Node col = root.Right;

            // keep track of the column with the fewest "on" cells
            Node chosenCol = col;
            int minOnCells = int.MaxValue;

            // iterate through the columns
            while (col != root)
            {
                // count the number of "on" cells in the column
                int onCells = 0;
                for (Node cell = col.Down; cell != col; cell = cell.Down)
                {
                    onCells++;
                }

                // if the number of "on" cells is smaller than the current minimum, update the chosen column
                if (onCells < minOnCells)
                {
                    chosenCol = col;
                    minOnCells = onCells;
                }

                // move to the next column
                col = col.Right;
            }

            // if no columns have "on" cells, return the root node
            if (minOnCells == 0)
            {
                return root;
            }

            return chosenCol;
        }


        public static void CoverColumn(Node col)
        {
            // update the left and right pointers of the nodes to the left and right of the column
            col.Left.Right = col.Right;
            col.Right.Left = col.Left;

            // iterate through the "on" cells in the column
            for (Node row = col.Down; row != col; row = row.Down)
            {
                // update the up and down pointers of the nodes in the column
                row.Up.Down = row.Down;
                row.Down.Up = row.Up;
            }
        }

        // very simple printBoard function for board of nodes
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


        // function that gets a boarf of cells and its size and returns the string representation of the board
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

        // function that gets a string representation of a board and its size and returns the board of nodes
        public static Node[][] ConvertStringBoard(string board, int size)
        {
            // create a matrix of nodes with the same dimensions as the board
            Node[][] matrix = new Node[size * size][];
            for (int i = 0; i < size * size; i++)
            {
                matrix[i] = new Node[size];
            }

            // create a dictionary to map cell values to column indices
            Dictionary<int, int> valueToCol = new Dictionary<int, int>();
            int nextCol = 0;

            // iterate through the cells in the board
            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    // create a new node for the cell
                    Node node = new Node();
                    node.Row = row;
                    node.Col = col;
                    node.Value = int.Parse(board[row * size + col].ToString());

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
            Node root = new Node();
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

        // function that gets a board of nodes and its size and returns the board of cells
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

        // function that gets a board of cells and a value and wil return how many cells have its value as one of their candidates
        public static int GetNumOfCellsThatHaveCandidate(Cell[,] board, int size, int value)
        {
            // check if the board is not null
            if (board == null)
            {
                throw new ArgumentNullException("board");
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
    }
}