using NUnit.Framework;
using SodukoSolver.BoardSolvers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SodukoSolver.Algoritms.BoardConvertors;
using static SodukoSolver.Algoritms.ValidatingFunctions;

namespace SudokuTesting.ExtremeCases
{
    /// <summary>
    /// this is a test class for the DancingLinks class
    /// this class containts extreme cases and tests that the algorithm can solve them
    /// this class contains boards that are 9 by 9 in size
    /// </summary>
    public class Extreme9By9Testing
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

        // testing for diffrent extreme cases of boards

        // unsolvable board - very hard for a normal algorithm
        [Test]
        public void Unsolvable9By9()
        {
            // arrange test
            boardString = "000005080000601043000000000010500000000106000300000005530000061000000004000000000";
            size = (int)Math.Sqrt(boardString.Length);
            IsTheBoardValid(size, boardString);
            board = Vboard;
            matrix = IntBoardToByteMatrix(board, size);

            // act test
            solver = new DancingLinks(matrix, size);
            result = solver.Solve();

            // assert test
            Assert.That(result, Is.EqualTo(false));
        }

        // very hard 9 by 9 board
        [Test]
        public void ExtremeBoard2()
        {
            // arrange test
            boardString = "400000805030000000000700000020000060000080400000010000000603070500200000104000000";
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

        // very hard 9 by 9 board
        [Test]
        public void ExtremeBoard3()
        {
            // arrange test
            boardString = "000000000000003085001020000000507000004000100090000000500000073002010000000040009";
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

        // very hard 9 by 9 board
        [Test]
        public void ExtremeBoard4()
        {
            // arrange test
            boardString = "000006000059000008200008000045000000003000000006003054000325006000000000000000000";
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

        // very hard 9 by 9 board
        [Test]
        public void ExtremeBoard5()
        {
            // arrange test
            boardString = "000700000100000000000430200000000006000509000000000418000081000002000050040000300";
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

        // very hard 9 by 9 board
        [Test]
        public void ExtremeBoard6()
        {
            // arrange test
            boardString = "900800000000000500000000000020010003010000060000400070708600000000030100400000200";
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

        // this is considered "the world hardest soduko board" according to the internet
        [Test]
        public void WorldHardestSudoku()
        {
            // arrange test
            boardString = "800000000003600000070090200050007000000045700000100030001000068008500010090000400";
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
