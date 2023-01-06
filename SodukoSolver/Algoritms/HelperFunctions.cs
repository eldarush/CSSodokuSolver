using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static SodukoSolver.Exceptions.CustomExceptions;
using static SodukoSolver.Algoritms.ValidatingFunctions;
using System.Diagnostics;
using SodukoSolver.DataStructures;

namespace SodukoSolver.Algoritms
{
    public static class HelperFunctions
    {
        /// <summary>
        /// Function that gets a Board string and returns a Board of cells
        /// </summary>
        /// <param name="size"> the Size of the Board</param>
        /// <param name="boardString">the string that represents the Board</param>
        /// <returns>the Board of cells</returns>
        public static int[,] CreateBoard(int size, string boardString, out int[] rowValues, out int[] colValues, out int[] blockValues, out int[] HelperMask)
        {
            // create a new Board
            int[,] board = new int[size, size];

            // copy the Board string to the Board
            CopyBoardStringToBoard(size, boardString, board);

            // create the row, col, and block values
            rowValues = new int[size];
            colValues = new int[size];
            blockValues = new int[size];
            // create the mask
            HelperMask = new int[size];

            // initialize the mask 
            for (int index = 0; index < size; index++)
            {
                // Board values at each index will be 2 to the power of the index
                // HelperMask[0] = 1 << 0 = 1, HelperMask[1] = 1 << 1 = 2, HelperMask[2] = 1 << 2 = 4 and so on...
                HelperMask[index] = 1 << index;
            }

            // initialize the row, col, and block values to be all 0s
            for (int index = 0; index < size; index++)
            {
                rowValues[index] = 0;
                colValues[index] = 0;
                blockValues[index] = 0;
            }

            // return the Board
            return board;
        }

        /// <summary>
        /// Function that copies the Board string to the Board
        /// </summary>
        /// <param name="size"> the Size of the Board</param>
        /// <param name="boardString">the string that represents the Board</param>
        /// <param name="board">the Board to copy the string to</param>
        private static void CopyBoardStringToBoard(int size, string boardString, int[,] board)
        {
            // create a counter for the Board string
            int counter = 0;

            // loop through the rows
            for (int i = 0; i < size; i++)
            {
                // loop through the columns
                for (int j = 0; j < size; j++)
                {
                    // copy the Board string to the Board
                    board[i, j] = Convert.ToInt32(boardString[counter]) - '0';
                    // increment the counter
                    counter++;
                }
            }
        }

        /// <summary>
        /// function that gets a boardstring and a size and converts it to a board of bytes
        /// </summary>
        /// <param name="boardstring"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static void ConvertStringToByteMatrix(string boardstring, int size, out byte[,] board)
        {
            board = new byte[size, size];
            int count = 0;
            for(int i =0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    board[i,j] = (byte)(boardstring[count] - '0');
                    count++;
                }
            }
        }

        /// <summary>
        /// function that will get a board that represents the values of the cells 
        /// and the size of the board and will return a cover matrix for the board
        /// that will be of size (size^3) * (constrains* size^2)
        /// </summary>
        /// <param name="board"></param>
        /// <param name="size"></param>
        /// <param name="coverMatrix"></param>
        public static void ConvertMatrixToExactCoverMatrix(byte[,] board, int size, int blockSize, int constrains, out byte[,] coverMatrix)
        {
            // initialize the new cover matrix
            coverMatrix = new byte[size * size * size, constrains * size * size];

            // current row indicator
            int CurrentRow = 0;

            // current value indicator
            int CurrentValue;

            // please note that the next part is hard coded for 4 constraints and for this exact order,
            // this is the order described in the thesis 'Solving Sudoku efficiently with Dancing Links'
            // by MATTIAS HARRYSSON and HJALMAR LAESTANDER and for a diffrent number of constraints or 
            // for a diffrent order, this function would need to be modified and so will the other functions
            // that acsess the cover matrix that is created by this function

            // indicator for each constraint
            // first 'size' portion of the array is for cell constraints
            int CurrentCellConstraint = 0;
            // second 'size' portion of the array is for col constraints
            int CurrentColConstraint = size * size;
            // third 'size' portion of the array is for row constraints
            int CurrentRowConstraint = size * size * 2;
            // fourth 'size' portion of the array is for block constraints
            int CurrentBlockConstraint = size * size * 3;

            // current candidate row index
            int CandidateRowIndex;

            // current candidate block index;
            int CandidateBlockIndex;

            // go over the board
            for(int row =0; row<size; row++)
            {
                // reset the indicator for the current col constraint
                CurrentColConstraint = size * size;
                
                for (int col =0; col<size; col++)
                {
                    // the current value at row,col
                    CurrentValue = board[row, col];

                    // calcualte the block index
                    CandidateBlockIndex = (row / blockSize) * blockSize + col / blockSize;

                    // try all possible candidates for this size to see which one's fit and which ones dont
                    for (int CurrentCandidate=1; CurrentCandidate<=size; CurrentCandidate++)
                    {
                        // if the current value is 0 or if the current candidate IS the current value, then
                        // we update the cover matrix to contain 1's at the constraints
                        // if not then we just move to the next row and the next ColConstraint indicator
                        if(CurrentValue==0 || CurrentValue == CurrentCandidate)
                        {
                            // update the current cell constraint to be 1 at the appropriate location
                            coverMatrix[CurrentRow, CurrentCellConstraint] = 1;

                            // update the current col constraint to be 1 at the appropriate location
                            coverMatrix[CurrentRow, CurrentColConstraint] = 1;

                            // update the current row constraint to be 1 at the appropriate location
                            CandidateRowIndex = CurrentCandidate - 1;
                            coverMatrix[CurrentRow, CurrentRowConstraint + CandidateRowIndex] = 1;

                            // update the current block constraint to be 1 at the appropriate location
                            coverMatrix[CurrentRow, CurrentBlockConstraint + CandidateBlockIndex * size + CandidateRowIndex] = 1;
                        }
                        // OUTSIDE THE IF STATEMENT IF VALUE IS 0 OR IS CURRENT CANDIDATE

                        // continue to the next row
                        CurrentRow++;
                        // continue to the next col constraint indicator
                        CurrentColConstraint++;
                    }
                    // OUTSIDE THE CANDIDATE LOOPING

                    // continue to the next cell constraint indicator
                    CurrentCellConstraint++;
                }
                // OUTSIDE THE COL LOOPING

                // continue to the next row constraint indicator
                // this is done becaue every constraint take up 'size' amount in the array
                // so we skip to next one by adding 'size' to the row constraint indicator
                CurrentRowConstraint += size;
            }
            // OUTSIDE THE ROW LOOPING

            // return the ready cover matrix
            return;
        }

        /// <summary>
        /// this is a function that takes in the cover matrix as a matrix of 0'es and one's and converts it to a new 
        /// node matrix that will be used by the dancing links algorithm
        /// this function just returns the root of the new matrix, of which the matrix is represented by the connections
        /// between the nodes themselves
        /// </summary>
        /// <param name="coverMatrix"></param>
        /// <param name="size"></param>
        /// <param name="blockSize"></param>
        /// <param name="rootOfNewMatrix"></param>
        public static void ConvertCoverMatrixToNodeMatrix(byte[,] coverMatrix, out HeaderNode rootOfNewMatrix)
        {
            // create the root of the matrix that will be returned
            rootOfNewMatrix = new HeaderNode("root");

            // calcualte the amount of rows and cols
            // rows = n^3
            int RowAmount = coverMatrix.GetLength(0);
            // cols = constraints*n^2
            int ColAmount = coverMatrix.GetLength(1);

            // create the row of headers (A-H) but for diffrent sizes that will be at the top of the 
            // node matrix, will all be linked to one another and will all have link to the col that they
            // are 'heading'
            HeaderNode[] headersRow = new HeaderNode[ColAmount];
            
            // initialize the headers row with the given row index as the name 
            for(int CurrentRowIndex =0; CurrentRowIndex<ColAmount; CurrentRowIndex++)
            {
                // create new header and insert it into the headers row
                // note that when debugging the names of the row will just be the index of the place
                // where they are located 0 -> (size-1) and not A-Z letters as described
                HeaderNode CurrentHeaderNode = new HeaderNode(CurrentRowIndex.ToString());
                headersRow[CurrentRowIndex] = CurrentHeaderNode;

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
            Node LastInsertedNode;

            // keep track of the current value at the 0,1 matrix
            byte CurrentValue;

            // keep track of the current header node of this row
            HeaderNode CurrentHeader;

            // new node to be inserted
            Node NodeToBeInserted;

            // go over all the rows in the 0,1 matrix and where there is a '1', insert a new node
            for(int CurrentRowIndex =0; CurrentRowIndex< RowAmount; CurrentRowIndex++)
            {
                // reset the last inserted node with every passing row
                LastInsertedNode = null;

                // go over the cols in the current row index
                for(int CurrentColIndex =0; CurrentColIndex<ColAmount; CurrentColIndex++)
                {
                    // update the current value
                    CurrentValue = coverMatrix[CurrentRowIndex, CurrentColIndex];

                    // if current value is 1, new node
                    // if current value 0, do nothing
                    if (CurrentValue == 0) continue;

                    // if we reached this place, the current value is 1
                    // get the coorespoinding header node
                    CurrentHeader = headersRow[CurrentColIndex];

                    NodeToBeInserted = new Node(CurrentHeader);

                    // attach the header node to the node to be inserted
                    CurrentHeader.Up.AttachDown(NodeToBeInserted);

                    // if it is the first inserted node in this col, make it the last inserted node
                    // if not, attach the last inserted node to this node and then update the last inserted
                    if(LastInsertedNode != null)
                    {
                        LastInsertedNode.AttachRight(NodeToBeInserted);
                        LastInsertedNode = LastInsertedNode.Right;
                    }
                    // if no last inserted node exists, make the current one the last inserted
                    else
                    {
                        LastInsertedNode = NodeToBeInserted;
                    }
                    // update the size of the current header node after every insertion
                    CurrentHeader.Size++;
                }
            }
            // return the root of the new node matrix
            return;
        }

        /// <summary>
        /// this is a fuction that gets the root of the matrix of nodes, and goes over
        /// all the header nodes and compares their sizes and 
        /// it picks the header node with the smallest size
        /// </summary>
        /// <param name="root"></param>
        /// <param name="colWithMinSize"></param>
        public static void FindHeaderWithLeastNodes(HeaderNode root, out HeaderNode colWithMinSize)
        {
            // current header node 
            // by deafult set to the first header node
            HeaderNode CurrentHeaderNode = (HeaderNode)root.Right;

            // the header node with the samllest size 
            // by deafult set to the first header node
            colWithMinSize = (HeaderNode)root.Right;

            // go over all the header nodes and change the min node if needed
            // while we havent reached the root
            while (CurrentHeaderNode != root)
            {
                // comapre the two sizes and change the min if 
                // the current size is smaller then min node size
                if (CurrentHeaderNode.Size < colWithMinSize.Size) colWithMinSize = CurrentHeaderNode;

                // continue going right untill we reach the root again
                CurrentHeaderNode = (HeaderNode)CurrentHeaderNode.Right;
            }
            // return the heder node witht eh smallest size
            return;
        }


        /// <summary>
        /// function that will get the row, col, and block and helper mask and will copy them into new copies of them
        /// </summary>
        /// <param name="RowValues">row bitmask</param>
        /// <param name="ColValues">col bitmask</param>
        /// <param name="BlockValues">block bitmask</param>
        /// <param name="HelperMask">helper bitmask</param>
        /// <param name="newRowValues">new row bitmask</param>
        /// <param name="newColValues">new col bitmask</param>
        /// <param name="newBlockValues">new block bitmask</param>
        /// <param name="newHelperMask">new helper bitmask</param>
        public static void CopyBitMasks(int[] RowValues, int[] ColValues, int[] BlockValues, int[] HelperMask,
             out int[] newRowValues, out int[] newColValues, out int[] newBlockValues, out int[] newHelperMask)
        {
            // make the new row, col, and block values use the old one's memeory space
            newRowValues = RowValues;
            newColValues = ColValues;
            newBlockValues = BlockValues;
            newHelperMask = HelperMask;
        }


        /// <summary>
        /// function that gets a stop watch and prints out the seconds and ms in the watch
        /// </summary>
        /// <param name="watch">the stopwatch</param>
        public static void PrintOutTime(Stopwatch watch)
        {
            // print the elapsed times in seconds, milliseconds
            Console.WriteLine("\nElapsed time: {0} seconds", watch.Elapsed.TotalSeconds);
            Console.WriteLine("Elapsed time: {0} milliseconds", watch.Elapsed.TotalMilliseconds);
        }

        /// <summary>
        /// Function that chekcs if the Board is valid
        /// </summary>
        /// <param name="size">the Size of the Board</param>
        /// <param name="boardString">the string that represents the Board</param>
        /// <returns>if the Board is valid or not</returns>
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
        /// function that print the welcome message that greets the user
        /// </summary>
        public static void PrintWelcomeMessage()
        {
            // function that will ask the user for input and will perform the actions
            Console.WriteLine("Welcome to the Soduko Solver! \n" +
                "This is a program written in c# by @Eldar Aslanbeily \n" +
                "For more information about the program, please visit: \n" +
                "https://github.com/eldarush/CSSodokuSolver.git \n" +
                "This is a program that will solve any soduko Board \n");
        }

        /// <summary>
        /// function that converts a boardstring to a Board of ints
        /// </summary>
        /// <param name="boardstring">the string that represents the Board</param>
        /// <param name="array">the 2d array where the Board will be stored</param>
        /// <param name="size">the Size of the Board</param>
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
        /// function that converts Board of ints to string
        /// </summary>
        /// <param name="array">the 2d array where the Board will be stored</param>
        /// <param name="size">the Size of the Board</param>
        /// <returns>the string that represents the Board</returns>
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
        /// function that prints the board 
        /// </summary>
        /// <param name="board">the board</param>
        /// <param name="size">the size of the board</param>
        public static void PrintBoard(int[,] board, int size)
        {
            {
                // Find the maximum length of the string representation of any element in the Board
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

                // Calculate the Size of the sub-squares (if any)
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
                        // if the Size more then 9, print the value with a 0 in front
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

        /// <summary>
        /// function that gets a value and counts the amount of activated bits in the numbe
        /// Equal to writing CountOnes from System.Numerics.BitOperations
        /// </summary>
        /// <param name="value">thevalue to be converted</param>
        /// <returns>counts the amount of activated bits in the number</returns>
        public static int GetActivatedBits(int value)
        {
            // set value to the result of a bitwise AND operation between value and value - 1 and increment the counter
            int bits = 0;
            while (value > 0)
            {
                value &= value - 1;
                bits++;
            }
            return bits;
        }

        /// <summary>
        /// function that determines the index of the most significant bit that is set to 1 in a binary representation of a given integer value.
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>the index of the most significant bit that is set to 1</returns>
        public static int GetIndexOfMostSignificantActivatedBit(int value)
        {
            int bits = 0;
            while (value != 0)
            {
                // divide value by 2 using right shift and increment the counter
                value >>= 1;
                bits++;
            }
            return bits;
        }
    }
}