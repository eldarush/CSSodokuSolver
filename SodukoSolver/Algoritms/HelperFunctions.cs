using System.Threading.Tasks.Dataflow;
using System.Xml;
using System.Numerics;
using System.Reflection.Metadata;
using System.Collections.Specialized;
using System.Text;
using System.Reflection.Metadata.Ecma335;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using static SodukoSolver.Exceptions.CustomExceptions;
using static SodukoSolver.Algoritms.ValidatingFunctions;
using static SodukoSolver.UserInterface;
using SodukoSolver.DataStructures;
using SodukoSolver.BoardSolvers;
using System.Drawing;

namespace SodukoSolver.Algoritms
{
    /// <summary>
    /// class that contains general helpful functions that are used in other classes
    /// </summary>
    public static class HelperFunctions
    {

        /// <summary>
        /// function that returns a copy of the given Board
        /// </summary>
        /// <param name="board">the Board</param>
        /// <returns>the copied Board</returns>
        public static int[,] GetBoardIntsCopy(int[,] board, int size)
        {
            // craete copy of the Board
            int[,] boardCopy = new int[size, size];
            // go over the current Board and copy each value to the corresponding value in the new Board
            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    boardCopy[row, col] = board[row, col];
                }
            }
            // return the copied Board
            return boardCopy;
        }

        /// <summary>
        /// Function that gets a Board string and returns a Board of cells
        /// </summary>
        /// <param name="size"> the Size of the Board</param>
        /// <param name="boardString">the string that represents the Board</param>
        /// <returns>the Board of cells</returns>
        public static int[,] CreateBoard(int size, string boardString, out int[] rowValues, out int[] colValues, out int[] blockValues, out int[] helperMask)
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
            helperMask = new int[size];

            // initialize the mask 
            for (int index = 0; index < size; index++)
            {
                // Board values at each index will be 2 to the power of the index
                // helperMask[0] = 1 << 0 = 1, helperMask[1] = 1 << 1 = 2, helperMask[2] = 1 << 2 = 4 and so on...
                helperMask[index] = 1 << index;
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
        public static void CopyBoardStringToBoard(int size, string boardString, int[,] board)
        {
            // create a location for the Board string
            int location = 0;

            // loop through the rows
            for (int row = 0; row < size; row++)
            {
                // loop through the columns
                for (int col = 0; col < size; col++)
                {
                    // copy the Board string to the Board
                    board[row, col] = Convert.ToInt32(boardString[location]) - '0';
                    // increment the location
                    location++;
                }
            }
        }

        /// <summary>
        /// function that gets a SudokuBoard solver, ask the user using what algorith, he wants to solve the SudokuBoard,
        /// creates new solver using the input from the user
        /// </summary>
        /// <param name="board">the SudokuBoard</param>
        /// <param name="boardSolver">the returned SudokuBoard solver</param>
        public static void GetTypeOfBoardSolver(SudokuBoard board, out BoardSolver boardSolver)
        {
            // create input string
            string userInput;

            // ask the user in which way does he want to solve the Board
            Console.WriteLine("\nPlease choose the way you want to solve the Board: \n" +
                "\t d: using the Dancing Links algorithm (Highly Reccommended) \n" +
                "\t b: using the Backtracking algorithm \n\n" +
                "\t Dancing Links works better for bigger and more complicated SudokuBoard but takes up more memory. \n" +
                "\t Backtracking works better with simpler and smaller boards and takes up less memory, \n" +
                "\t But may struggle with more complicated boards. \n");

            // get the user input
            Console.Write("Please enter your choice: ");
            userInput = Console.ReadKey().KeyChar.ToString();

            // while the user doesnt enter a valid charachter, put him in a while loop
            while (userInput != "d" && userInput != "b" && userInput != "D" && userInput != "B")
            {
                Console.Write("\nPlease Enter a valid character (d or b): ");
                userInput = Console.ReadKey().KeyChar.ToString(); ;
            }

            // if the user wanted to solve using dancing links
            if (userInput == "d" || userInput == "D")
            {
                byte[,] matrix = IntBoardToByteMatrix(board.Board, board.Size);

                boardSolver = new DancingLinks(matrix, board.Size);
                return;
            }
            // if the user wanted to solve using backtracking
            else if (userInput == "b" || userInput == "B")
            {
                boardSolver = new BackTracking(board.Board, board.Size);
                return;
            }

            // this part will never be reached
            boardSolver = new BoardSolver(board.Board, board.Size);
        }

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
            int currentColConstraint = size * size;
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
            for(int row = 0; row < size; row++)
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
                    for (int currentCandidate=1; currentCandidate<=size; currentCandidate++)
                    {
                        // if the current value is 0 or if the current candidate IS the current value, then
                        // we update the cover Matrix to contain 1's at the constraints
                        // if not then we just move to the next row and the next ColConstraint indicator
                        if(currentValue == 0 || currentValue == currentCandidate)
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
            for(int currentRowIndex = 0; currentRowIndex < colAmount; currentRowIndex++)
            {
                // create new header and insert it into the headers row
                // note that when debugging the names of the row will just be the index of the place
                // where they are located 0 -> (size-1) and not A-Z letters as described
                HeaderNode CurrentHeaderNode = new HeaderNode(currentRowIndex.ToString());
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
            for(int row = 0; row < rowAmount; row++)
            {
                // reset the last inserted node with every passing row
                lastInsertedNode = null;

                // go over the cols in the current row index
                for(int col = 0; col < colAmount; col++)
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
                    if(lastInsertedNode != null)
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
            while (solutionStack.Count!= 0)
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

        /// <summary>
        /// function that will get the row, col, and block and helper mask and will copy them into new copies of them
        /// </summary>
        /// <param name="oldRowValues">row bitmask</param>
        /// <param name="oldColValues">col bitmask</param>
        /// <param name="oldBlockValues">block bitmask</param>
        /// <param name="oldHelperMask">helper bitmask</param>
        /// <param name="newRowValues">new row bitmask</param>
        /// <param name="newColValues">new col bitmask</param>
        /// <param name="newBlockValues">new block bitmask</param>
        /// <param name="newHelperMask">new helper bitmask</param>
        public static void CopyBitMasks(int[] oldRowValues, int[] oldColValues, int[] oldBlockValues, int[] oldHelperMask,
             out int[] newRowValues, out int[] newColValues, out int[] newBlockValues, out int[] newHelperMask)
        {
            // make the new row, col, and block values use the old one's memeory space
            newRowValues = oldRowValues;
            newColValues = oldColValues;
            newBlockValues = oldBlockValues;
            newHelperMask = oldHelperMask;
        }


        /// <summary>
        /// function that gets a stop watch and prints out the seconds and ms in the watch
        /// </summary>
        /// <param name="watch">the stopwatch</param>
        public static void PrintOutTime(Stopwatch watch)
        {
            // print the elapsed times in seconds and milliseconds (1000 ms =  1 sec)
            Console.WriteLine("\nElapsed time: {0} seconds", watch.Elapsed.TotalSeconds);
            Console.WriteLine("Elapsed time: {0} milliseconds", watch.Elapsed.TotalMilliseconds);
        }

        /// <summary>
        /// function that takes in the SudokuBoard solver that contains the result and
        /// prints out the solved SudokuBoard and the SudokuBoard string
        /// </summary>
        /// <param name="solver">the solved SudokuBoard</param>
        public static void OutputBoard(BoardSolver solver)
        {
            // print the solved Board
            Console.WriteLine("\nSolved Board is: \n");
            PrintBoard(solver.BoardInts, solver.Size);

            // print the solved Board string
            Console.WriteLine("\nSolved Board string is: \n");
            Console.WriteLine(ConvertToString(solver.BoardInts, solver.Size));
        }

        /// <summary>
        /// Function that chekcs if the Board is valid
        /// </summary>
        /// <param name="size">the Size of the Board</param>
        /// <param name="boardString">the string that represents the Board</param>
        /// <returns>if the Board is valid or not</returns>
        public static bool IsTheBoardValid(int size, string boardString)
        {
            // to keep track if the SudokuBoard is valid or not
            bool valid = false;
            // try and validate, if any exceptions appear, catch them
            try
            {
                valid = Validate(size, boardString);
            }
            // catch the custom exceptions, print their error messages and run the function
            // that gets the input from user again
            catch (SizeException se)
            {
                Console.WriteLine("\nERROR: " + se.Message);
                AskUserForInput();
            }
            catch (InvalidCharacterException ice)
            {
                Console.WriteLine("\nERROR: " + ice.Message);
                AskUserForInput();
            }
            catch (BoardCellsNotValidException bcne)
            {
                Console.WriteLine("\nERROR: " + bcne.Message);
                AskUserForInput();
            }
            catch (NullBoardException nbe)
            {
                Console.WriteLine("\nERROR: " + nbe.Message);
                AskUserForInput();
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
            // print out the welcome message
            Console.WriteLine("Welcome to the Soduko Solver! \n" +
                "This is a program written in c# by @Eldar Aslanbeily \n" +
                "For more information about the program, please visit: \n" +
                "https://github.com/eldarush/CSSodokuSolver.git \n" +
                "This is a program that will solve any soduko Board \n" +
                "The compatable sizes are (1x1 4x4 9x9 16x16 25x25) \n");
        }

        /// <summary>
        /// function that converts Board of ints to string
        /// </summary>
        /// <param name="array">the 2d array where the Board will be stored</param>
        /// <param name="size">the Size of the Board</param>
        /// <returns>the string that represents the Board</returns>
        public static string ConvertToString(int[,] array, int size)
        {
            // the string that will hold the SudokuBoard representation
            string boardstring = "";
            // go over the SudokuBoard
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    // add the current value in string format
                    boardstring += (char)(array[i, j] + '0');
                }
            }
            // return the string
            return boardstring;
        }
    
        /// <summary>
        /// function that converts a int SudokuBoard to a byte Matrix
        /// </summary>
        /// <param name="board">the SudokuBoard of ints</param>
        /// <param name="size">size of each dimention</param>
        /// <returns>tje converted Matrix</returns>
        public static byte[,] IntBoardToByteMatrix(int[,] board, int size)
        {
            // create new Matrix of bytes of the same size as the int Matrix
            byte[,] matrix = new byte[size, size];
            // go over the SudokuBoard
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    // convert to byte and insert into Matrix
                    matrix[i, j] = (byte)board[i, j];   
                }
            }
            // return the resulted Matrix
            return matrix;
        }
        
        /// <summary>
        /// function that prints the SudokuBoard 
        /// </summary>
        /// <param name="board">the SudokuBoard</param>
        /// <param name="size">the size of the SudokuBoard</param>
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
                        // if the current length is bigger then the max length
                        if (length > maxLength)
                        {
                            // make the amx size the current size
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
                            // add leading zero for single digit values
                            element = board[i, j].ToString().PadLeft(2, '0'); 
                        }
                        else
                        {
                            element = board[i, j].ToString();
                        }
                        // calculate the padding and print it
                        int padding = (squareWidth - element.Length) / 2;
                        for (int k = 0; k < padding; k++)
                        {
                            Console.Write(" ");
                        }

                        // print the next element
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
            // set value to the result of a bitwise AND operation between value and value - 1 and increment the location
            int numActivatedBits = 0;
            while (value > 0)
            {
                value &= value - 1;
                numActivatedBits++;
            }
            return numActivatedBits;
        }

        /// <summary>
        /// function that determines the index of the most significant bit that is set to 1 in a binary representation of a given integer value.
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>the index of the most significant bit that is set to 1</returns>
        public static int GetIndexOfMostSignificantActivatedBit(int value)
        {
            int msbIndex = 0;
            while (value != 0)
            {
                // divide value by 2 using right shift and increment the location
                value >>= 1;
                msbIndex++;
            }
            return msbIndex;
        }
    }
}