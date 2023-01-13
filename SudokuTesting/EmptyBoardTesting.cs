using SodukoSolver.BoardSolvers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SodukoSolver.Algoritms.BoardConvertors;
using static SodukoSolver.Algoritms.ValidatingFunctions;

namespace SudokuTesting
{
    /// <summary>
    /// this is a class that tests the DancingLinks class
    /// this class contains boards of all sizes and tests that the algorithm can solve them
    /// when the boards are empty
    /// </summary>
    public class EmptyBoardTesting
    {
        // the board size
        int size;
        
        // the board string
        string boardString;

        // the board in int form
        int[,] board;

        // the board in byte form
        byte[,] matrix;

        // the solver
        BoardSolver solver;

        // the result
        bool result;

        // test that the algorithm can solve an empty 1 by 1 Board
        [Test]
        public void Allzeroes1By1()
        {
            // arrange test
            boardString = "0";
            size = (int)Math.Sqrt(boardString.Length);
            IsTheBoardValid(size, boardString);
            board = Vboard;
            matrix = IntBoardToByteMatrix(board, size);
            
            // act test
            solver = new DancingLinks(matrix, size); 
            result = solver.Solve();
            
            // assert test
            Assert.That(result, Is.EqualTo(true));
        }


        // test that the algorithm can solve an empty 4 by 4 Board
        [Test]
        public void Allzeroes4By4()
        {
            // arrange test
            boardString = "0000000000000000";
            size = (int)Math.Sqrt(boardString.Length);
            IsTheBoardValid(size, boardString);
            board = Vboard;
            matrix = IntBoardToByteMatrix(board, size);

            // act test
            solver = new DancingLinks(matrix, size);
            result = solver.Solve();

            // assert test
            Assert.That(result, Is.EqualTo(true));
        }

        // test that the algorithm can solve an empty 9 by 9 Board
        [Test]
        public void Allzeroes9By9()
        {
            // arrange test
            boardString = "000000000000000000000000000000000000000000000000000000000000000000000000000000000";
            size = (int)Math.Sqrt(boardString.Length);
            IsTheBoardValid(size, boardString);
            board = Vboard;
            matrix = IntBoardToByteMatrix(board, size);

            // act test
            solver = new DancingLinks(matrix, size);
            result = solver.Solve();

            // assert test
            Assert.That(result, Is.EqualTo(true));
        }

        // test that the algorithm can solve an empty 16 by 16 Board
        [Test]
        public void Allzeroes16By16()
        {
            // arrange test
            boardString = "0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000";
            size = (int)Math.Sqrt(boardString.Length);
            IsTheBoardValid(size, boardString);
            board = Vboard;
            matrix = IntBoardToByteMatrix(board, size);

            // act test
            solver = new DancingLinks(matrix, size);
            result = solver.Solve();

            // assert test
            Assert.That(result, Is.EqualTo(true));
        }

        // test that the algorithm can solve an empty 25 by 25 Board
        [Test]
        public void Allzeroes25By25()
        {
            // arrange test
            boardString = "0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000";
            size = (int)Math.Sqrt(boardString.Length);
            IsTheBoardValid(size, boardString);
            board = Vboard;
            matrix = IntBoardToByteMatrix(board, size);

            // act test
            solver = new DancingLinks(matrix, size);
            result = solver.Solve();

            // assert test
            Assert.That(result, Is.EqualTo(true));
        }
    }
}
