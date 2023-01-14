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
    /// this class contains boards that are 16 by 16 in size
    /// </summary>
    public class Extreme16By16Testing
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

        // very hard 16 by 16 board
        [Test]
        public void ExtremeBoard1()
        {
            // arrange test
            boardString = ":24000<003=06000070000000<00=08000@00070:00000500<0080=0160024>:>00000000030060900700:2450<03000830001070:0400;5000;@8000067000>;0?<0@0000000:0000:00;5?00000007910000:2;000030@@00=000600020?00=000670104>:00?000500=0007004>:004>00<;0000800160900000:000?0800";
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

        // very hard 16 by 16 board
        [Test]
        public void ExtremeBoard2()
        {
            // arrange test
            boardString = "00000000000140004900000@:006<7050<70000308@;:0060:>000070002080000003040000@000000:0005<04000?0000?0>06000000490009000000:0=0<00004900;?00:00000010003200;?0000>0000<70030400;?88@00:>00700<0200032008@;>0007100071509008@00>=60:>=05000002000;008000:00<0159000";
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

        // very hard 16 by 16 board
        [Test]
        public void ExtremeBoard3()
        {
            // arrange test
            boardString = "000>0?095;000000009000;06<@004000000=0<000:>0000000=0070000035;80@>047001000508=0:200090;0006<00100?500=<@000000;0=560000:000003020010008=0;00009050;0000000002?80000@>402?009000>0<0:0?0001;0000?00905;00080>0735098=00040000?0=6<00>07000003000470:00130008000";
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

        // very hard 16 by 16 board
        [Test]
        public void ExtremeBoard4()
        {
            // arrange test
            boardString = "001@;000:70?000=0<00072?00000000:72004050010000090=5030000000:0201000000020000=0700;400:0=50000042?:005000@0;7000009<1@60080:000=0001000006<02800500>00<00;000?0000028;7=?00300900;00000150300@6?0000:00@930>00<5000@03000000000@901000>0070=50406<00002004=1093";
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

        // very hard 16 by 16 board
        [Test]
        public void ExtremeBoard5()
        {
            // arrange test
            boardString = "0>905000400<@732000?0;04002080>900003000:000000=730@00800000000010<00@000=8005;?0007=8:000000020000000601204000@50060<0109@70>00=00006002@000000;00004020800>0000001000900:>5;<00870?00=0<651000<05070200039=000@0000000?00=00408:000000040;0@01?000050007109800";
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

        // very hard 16 by 16 board
        [Test]
        public void ExtremeBoard6()
        {
            // arrange test
            boardString = "00:=1?2000000400?070003000>40:008350@000=000000?0000=;9:1020000000108>00<00@:0;00500094@000000?39400000=0001000020=;?000000040095000>4680:@00;00068>0:@000=000300@<92700301000>07000000000600<0008>4000000;00306000:70;000?00>00100000?0008000:=0?004@000=09;270";
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

        // very hard 16 by 16 board
        [Test]
        public void ExtremeBoard7()
        {
            // arrange test
            boardString = "0000010070?=;0000000?00@00030>00000080;00>000<0000080950:<200@?=50090:004007>8300@21=000003;009000?=0;00060502100>00950000004?07000500018=0460;00010700=0000200<080700002900000@>03;0<00?000000000;000000000004800000?0:304800>00=0@003700061002000000900502=:00";
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
