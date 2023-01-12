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
    public class EmptyBoardTesting
    { 
        int size;
        string boardString;
        int[,] board;
        byte[,] matrix;
        BoardSolver solver;

        // test that the algorithm can solve an empty 1 by 1 Board
        [Test]
        public void Allzeroes1By1()
        {
            boardString = "0";
            size = (int)Math.Sqrt(boardString.Length);
            IsTheBoardValid(size, boardString);
            board = Vboard;
            matrix = IntBoardToByteMatrix(board, size);
            solver = new DancingLinks(matrix, size);
            Assert.That(solver.Solve(), Is.EqualTo(true));
        }


        // test that the algorithm can solve an empty 4 by 4 Board
        [Test]
        public void Allzeroes4By4()
        {
            boardString = "0000000000000000";
            size = (int)Math.Sqrt(boardString.Length);
            IsTheBoardValid(size, boardString);
            board = Vboard;
            matrix = IntBoardToByteMatrix(board, size);
            solver = new DancingLinks(matrix, size);
            Assert.That(solver.Solve(), Is.EqualTo(true));
        }

        // test that the algorithm can solve an empty 9 by 9 Board
        [Test]
        public void Allzeroes9By9()
        {
            boardString = "000000000000000000000000000000000000000000000000000000000000000000000000000000000";
            size = (int)Math.Sqrt(boardString.Length);
            IsTheBoardValid(size, boardString);
            board = Vboard;
            matrix = IntBoardToByteMatrix(board, size);
            solver = new DancingLinks(matrix, size);
            Assert.That(solver.Solve(), Is.EqualTo(true));
        }

        // test that the algorithm can solve an empty 16 by 16 Board
        [Test]
        public void Allzeroes16By16()
        {
            boardString = "0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000";
            size = (int)Math.Sqrt(boardString.Length);
            IsTheBoardValid(size, boardString);
            board = Vboard;
            matrix = IntBoardToByteMatrix(board, size);
            solver = new DancingLinks(matrix, size);
            Assert.That(solver.Solve(), Is.EqualTo(true));
        }

        // test that the algorithm can solve an empty 25 by 25 Board
        [Test]
        public void Allzeroes25By25()
        {
            boardString = "0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000";
            size = (int)Math.Sqrt(boardString.Length);
            IsTheBoardValid(size, boardString);
            board = Vboard;
            matrix = IntBoardToByteMatrix(board, size);
            solver = new DancingLinks(matrix, size);
            Assert.That(solver.Solve(), Is.EqualTo(true));
        }
    }
}
