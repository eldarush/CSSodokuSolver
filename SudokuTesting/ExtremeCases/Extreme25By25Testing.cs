using NUnit.Framework;
using SodukoSolver.BoardSolvers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SodukoSolver.Algoritms.BoardConvertors;
using static SodukoSolver.Algoritms.ValidatingFunctions;
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

namespace SudokuTesting.ExtremeCases
{
    /// <summary>
    /// this is a test class for the DancingLinks class
    /// this class containts extreme cases and tests that the algorithm can solve them
    /// this class contains boards that are 25 by 25 in size
    /// </summary>
    public class Extreme25By25Testing
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

        // very hard 25 by 25 board
        [Test]
        public void ExtremeBoard1()
        {
            // arrange test
            boardString = "<:;090BA000I0E203C0FHD0>G070000I0E03C@0=06>GH900;02504E30@F0600H0<00?0100BA0000F0>G0D00?9<870A0E05I406>00:;?007000020I00F=0C03C@00>G000;01<:00A0825I00000900?0<00AE070I0F2=00@00;010BA000I40050C00=D0>090BA0004020C@H036>G9000001500F200000>00D6:00008700E0@003G0<00?18:00AE07000F=0G900?10:;AE200000=000@HD;?18:00200400500@0036>G0<B0E204F05I@0000>G0<6:;018I000000D0009<6>000800BA024000I0000090:>G0180;0AE05@H060000>G087000E25B00F=000<000800?025B04003IC0H06?100;E25B00=304@HD00000<00E250F030000600G9<0>;01870=0C006>@H<:009180B0A005IH060@0:;0007B?0E05I04F00C9<:0080B002500E0=3C40H06>00700200AE=3000HD00@0000000000=0040D0>00900;G?1000";
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

        // very hard 25 by 25 board
        [Test]
        public void ExtremeBoard2()
        {
            // arrange test
            boardString = "005DEI0000;9800:070F00000000I0B098G0@F:H0>3?0E0600080B;000F03?0<>5DE20A100I00:0003?=00200000000090G0?=<00D0265A10C00B0900000HF0000000<0005IEBA04C9000;=003?0265I00C0A0098000:00000E2A04C098GH;00@F00=<0000BA0;080H@F0>7030=0200IE000;07@F:0?0<D3I006010CB00DE?0260I00C001090G0F:>0000A2004C000GH003@0:0=0D0?0000098GH0F0>3@E?=0065002G07980F:>3=<D0?A265000000:>00F?0000000A2;040B8G070>0?00=00E050016040B;G00@0D020<65I01CB094@00H7:>0?0IA1654C000G00@8?00000000=0094C8007@:00002=<DE0IA00H700G00>0?<DE0=100IAC0;900@FGH:00?=DE20000IA00;08C0?0:>000000A1408C0;0H7@00E00<D0IA14B;9000G07@030=00145I0B;9800@0G0000?0E00<0080BGH7@F000=:60DE000100";
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

        // very hard 25 by 25 board
        [Test]
        public void ExtremeBoard3()
        {
            // arrange test
            boardString = "7<00AH0D0000E:F80;00I?1>0090BDG000@06;300?000<05070000:6;008I00>0<05A09HB0240603?0>00<=5000HB02@0E0FCI?0>05A009HBD20G00F80000800:F030I0?1>0<=5A790000006;041>C<000079H002@0E0080?100507900BD0@G0:006;04I000A7B0200GE0086;30I?10C<@H0D000F8G60340?10C<=5A79=0>0<00000000@000F80;34I0H5009D0@G000F86;300?000<=G0000:F86E000I?00C<00009H0000034I?00>C<=50700B02@00;300000=000700BD2@GE:F0603400C0=0>07000000G00086;0>C<=09H0AD200E0000;000?1B00000@GE0:086030I01>0000E02@GF06;0340?100<=5A0900;:00040?130C0=5000HB02@0000@G0860004I?10C000A790B03F8600?0>40<05079H0D20GE:04I00<00A0090B000GE0F0003A0005000D02@000F80030I010079HB0GE00080;30001000000";
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

        // very hard 25 by 25 board
        [Test]
        public void ExtremeBoard4()
        {
            // arrange test
            boardString = "0IFG1=0<0:6490D50@780003EE0020B0F01000=0D049H050@0@000000;000FG00000009HD6004000C008000A23000F0>:=00000:=0009H0780023E0AF01BI=?<>0H0040C0080A230000010000FG:000>D0000000070;A20300021BIF0?<>0=H0600000000@080200;A0IFG00=?00000D60640H5C@0000;A0G0B0F<>0=?0C@78A23E0100FG>0=?0640H0HD6000500703E;00G00I0<>:000?<>9H0005C008;A03EB0001G10IF>:=0<0D049000C000;020000A010000?0>0000000785C001BI0>0=09HD60@705C20E;0A03E;FG10I0=0<009H000078580C@00A03EG10I0<>:=?D64000HD0000000A20000FG1B0?<>:0:0?0400D0000@7E000000IF0080C0E;0230G0BI?<>0=H000040006078500000EBI001:0?0>00000640HD085C030;A0G100F000100<>:=49000C@780000E;0A200I0G000:00<040HD0C@78";
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

        // very hard 25 by 25 board
        [Test]
        public void ExtremeBoard5()
        {
            // arrange test
            boardString = "?906:8300000GE0>@700=FA0B083100H0IGD>@70F000=0:?00GEH0I70D>@000;0:00C000080@05D>00=F00:?00<08312IG0000B=F900:01048000EH0D>@057020G00000F000=08C0:04E01;500000000000C64E31<I00H09B=0A06008<4030G002I00000006:031040I0002@050>FA000E0104020G7>@;5000B=0:00C0B0>@000A0C0030:0010007500C=0A9600804E010750IG0;00>00:000000HG002I;BD00A0C=0H1<40200750;00>9C00A0836050000D>0;0A0C0F0360?4E00<0008300EH270D0G0=>@0006F0200EHI075D;B=>@C0F00001:0D007500;B=006FA000?8E0004=0@0B0A9C0831:000<4000D0G00A0C00000000<050I0000=>@0@00000C6001<?80000H5D0070A00000000H200ED0G7000F@;<0031400000D0G700@;BC6:0000EH20050>B0F@06:A0001<08007500;0=0C6:001<08002I0E";
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

        // very hard 25 by 25 board
        [Test]
        public void ExtremeBoard6()
        {
            // arrange test
            boardString = "D0402?AI00CG0070:;010@0H0?0I00G00700E1000>050000000000C00=:0H08@>40203A000?E1=:;580>02030F090?A<B0CG08@0HD340060009B0000100000@>H5A40200<I0000G10=:;E0A002D<096?01B0C:;0000>H50<09601B0CGE0=0;>05300000A1B7000000E00@00F2D04I00?000:000@>000A0F000?0I0700000000000?<1=7CG0E80000500006?<=00010@0;0H534>0200I070G00:;E834>050DAIF060<B0:0E80>H030I0206?0B07000=4>0500F00A0090?CG1=00008@>0E0@00500I900000000C01=0FH530000A0B06?<010:00E80000DAI000<B=00G1E0000H004F000<B0CG0=0>;E85300H2DA00:C010>;00@4F003DA0900?<B0H08@>2034096000<000?G00:;0530F6D0000C?0B0=:00E000H6DAI00?<B70;00080>0E534F000007000=0>008000000D0I96000000E00002000A090D?<B00";
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

        // very hard 25 by 25 board
        [Test]
        public void ExtremeBoard7()
        {
            // arrange test
            boardString = "F100?0E2<=006>G0I@007B000E00=000056I0D0900000000130H00>A900D:;7B8F000?00E000I0DAB800703000E20=00>GH50:;0B?00342<=0000500D090@I00>0000DA;0080034000EH<=000A90100B04?020<=CE00I00000B802300<0C00I00>G00:0D234?00H00C560G000D0900100H00CEGI560@0A901;008?020430081000?0000H50000090000<0?F2H000E00GI0;DA0:0037B5000HI000GD000037B01F2000000GI0;DA97B010<40F2005=C;0A9:030B80?02<5=CEHG0@600>0I@;7A00B0104=000<0500E7000034B810F20=00E00000000B80000?F2CE050D>00@:07A000F0<50C0H>G00D0090;104B06C0H5@D00I09000000002<=?0CF00=60EH5G00DAB9:0700000>0056D00I00:;7008134<=C020GI@07000;8130?C00<=0600HB00;700800F200C>0H060D000?0034=000<0H50>0G00D00B0:";
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
