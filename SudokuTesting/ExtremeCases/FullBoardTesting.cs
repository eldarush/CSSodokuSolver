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
    /// this is a class that tests the DancingLinks class
    /// this class contains boards of all sizes and tests that the algorithm can solve them
    /// when the boards are already full
    /// </summary>
    public class FullBoardTesting
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

        // test that the algorithm can solve a Complete 1 by 1 Board
        [Test]
        public void Complete1By1()
        {
            // arrange test
            boardString = "1";
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


        // test that the algorithm can solve a Complete 4 by 4 Board
        [Test]
        public void Complete4By4()
        {
            // arrange test
            boardString = "1234341221434321";
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

        // test that the algorithm can solve a Complete 9 by 9 Board
        [Test]
        public void Complete9By9()
        {
            // arrange test
            boardString = "123456789687139254495278136712893465956714823348625917261347598879561342534982671";
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

        // test that the algorithm can solve a Complete 16 by 16 Board
        [Test]
        public void Complete16By16()
        {
            // arrange test
            boardString = "123456789:;<=>?@?=@714;<368>29:5<;:>2=9?145@368768593:>@27?=14;<9123?@475<>8:;6=:6?;815=@34972<>>@<=926;?17:53484578>3<:=26;@19?3?127<84:>@6;=598<=6@?15;934>72:79>@:;268=15?<3454;:=>39<?278@16236145=>78<?9:@;=:8?67@14;93<5>2@>9<;8:265=14?73;745<9?3>@:268=1";
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

        // test that the algorithm can solve a Complete 25 by 25 Board
        [Test]
        public void Complete25By25()
        {
            // arrange test
            boardString = "123456789:;<=>?@ABCDEFGHIDBGH<15;FI48:EC3?>962@=7AF;8EI2?@CG15DAB4=H7<39:6>9:=>@3DA<H276GF15I8E4?B;C?A67C4>E=B39IH@2:F;G158<DE1234IH5896D<:>;G@A?7BC=FCDIF>@16G?=357H829<B;4AE:=@9BH:27;>F1A8G645ECD3I?<;<AG?D3=EFC4B9I71:H>6258@5678:<4BAC?2@;ED3=IFG1>9H4I123?B958DE><:GCA6;@H7F=GH@<9FI16=BC357ED2?8:>4A;:E?CA>@27;HF1=8BI459<G3D6>8F=;CG3D<IA469H71@:?E25B75D6BHE4:A@G2?;<>3F=I81C934E1289?H5:=;F<A6CB@>IDG7AFCIDG=>167@E35:982HB;<4?H>:@GBFD278I?1A=;<45C693E<=B?8;CI3E9HG46>FD17A:@25695;7A:<4@>BC2D?EG3IF=H182349156F?D<:8C=I@;>AH7EBGIC>AE78GB15;9D3FH6:2=<?@4BGHDF9;CI2E>7@15<?=48A6:3@?<:=EAH>3G6FB4987D15C;I287;56=<:@4A?HI2CBEG39DF>1";
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
