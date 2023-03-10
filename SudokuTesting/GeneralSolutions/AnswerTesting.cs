using NUnit.Framework;
using SodukoSolver.BoardSolvers;
using System.Drawing;
using static SodukoSolver.Algoritms.BoardConvertors;
using static SodukoSolver.Algoritms.ValidatingFunctions;
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

namespace SudokuTesting.GeneralSolutions
{
    /// <summary>
    /// this is class that is used to test that the algorithms give off 
    /// the correct answers for diffrent boards
    /// this is the only test class that will contain any tests for the
    /// backtracking algorithm since it is not the main algorithm used in the program
    /// and it is included as a side algorithm that is only capable of solving less complex boards
    /// </summary>
    public class AnswerTesting
    {
        // the board strings
        string easyTestString;
        string mediumTestString;
        string hardTestString;
        string unsolvableTestString;

        // the answer board strings
        string correctAnswerEasy;
        string correctAnswerMedium;
        string correctAnswerHard;
        string correctAnswerHardBacktracking;

        // the sizes
        int sizeEasy;
        int sizeMedium;
        int sizeHard;
        int sizeUnsolvable;

        // the boards in int form
        int[,] easyBoard;
        int[,] mediumBoard;
        int[,] hardBoard;
        int[,] unsolvableBoard;

        // the boards in byte form
        byte[,] easyMatrix;
        byte[,] mediumMatrix;
        byte[,] hardMatrix;
        byte[,] unsolvableMatrix;

        // the solvers
        BoardSolver dancingLinksSolver;
        BoardSolver backtrackingSolver;

        // the result of the test
        bool result;
        string resultString;

        [SetUp]
        public void Setup()
        {
            // create new board of ints that will be tested
            easyTestString = "800000070006010053040600000000080400003000700020005038000000800004050061900002000";
            mediumTestString = "10023400<06000700080007003009:6;0<00:0010=0;00>0300?200>000900<0=000800:0<201?000;76000@000?005=000:05?0040800;0@0059<00100000800200000=00<580030=00?0300>80@000580010002000=9?000<406@0=00700050300<0006004;00@0700@050>0010020;1?900=002000>000>000;0200=3500<";
            hardTestString = "0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000";
            unsolvableTestString = "100000000000100000000000005000000100000000000000000000000000000000000010000000000";

            // give the correct answers to each board
            correctAnswerEasy = "831529674796814253542637189159783426483296715627145938365471892274958361918362547";
            correctAnswerMedium = "15:2349;<@6>?=78>@8=5?7<43129:6;9<47:@618=?;35>236;?2=8>75:94@<1=4>387;:5<261?@98;76412@9:>?<35=<91:=5?634@8>2;7@?259<>31;7=:68462@>;94=?1<587:37=91?235;>8:@<46583;1:<7264@=9?>?:<4>6@8=9372;152358<>:?6794;1=@:7=<@359>8;1642?;1?968=4@25<7>3:4>6@7;12:?=3589<";
            correctAnswerHard = "123456789:;<=>?@ABCDEFGHIDBGH<15;FI48:EC3?>962@=7AF;8EI2?@CG15DAB4=H7<39:6>9:=>@3DA<H276GF15I8E4?B;C?A67C4>E=B39IH@2:F;G158<DE1234IH5896D<:>;G@A?7BC=FCDIF>@16G?=357H829<B;4AE:=@9BH:27;>F1A8G645ECD3I?<;<AG?D3=EFC4B9I71:H>6258@5678:<4BAC?2@;ED3=IFG1>9H4I123?B958DE><:GCA6;@H7F=GH@<9FI16=BC357ED2?8:>4A;:E?CA>@27;HF1=8BI459<G3D6>8F=;CG3D<IA469H71@:?E25B75D6BHE4:A@G2?;<>3F=I81C934E1289?H5:=;F<A6CB@>IDG7AFCIDG=>167@E35:982HB;<4?H>:@GBFD278I?1A=;<45C693E<=B?8;CI3E9HG46>FD17A:@25695;7A:<4@>BC2D?EG3IF=H182349156F?D<:8C=I@;>AH7EBGIC>AE78GB15;9D3FH6:2=<?@4BGHDF9;CI2E>7@15<?=48A6:3@?<:=EAH>3G6FB4987D15C;I287;56=<:@4A?HI2CBEG39DF>1";

            // this is done because backtrcking solver solves using a diffrent method and more then one answer is possible
            // for and empty board
            correctAnswerHardBacktracking = "123456789:;<=>?@ABCDEFGHI6789:EFGHI12345;<=>?@ABCD;<=>?12345@ABCDEFGHI6789:@ABCD;<=>?EFGHI6789:12345EFGHI@ABCD6789:12345;<=>?25134:;<=6BE>?7FGC@8IHDA9ADEFGH>517I3689<=;?2:@4BC:@C69?GEI4F=<DH>BA137528;HI><B28A@F4C;:G79D563=1?E8?7;=CD93B215A@H:EI4<>6FG31?@2>9CA8=DFE4:57BGH;I<6FGHDC456<1?IA;3=>28@9:E7BI4A8<=@7B2>9:56?HF;EDGC137;5BEFH:D3C@1G<I496A?8>2=9=6:>GI?E;78H2BC31D<A4@5F49253A:16HG;C<8DI>=FBE?@7D>:E7I4@?C359F=A86<BG1H;2=8@GH53;F9:67B12?4EC>DAI<<BI16D=>8GA?E@25;:7HC9F34?C;AF<B27EDH4I>31@G956:=856<7;3?F2>8:D1EBCHA=4I9G@>:9I@7CDG<H423;86?F1=B5EAB3D=18645@9>?7AGEI2;FC<:HCE42AB1H:=5GI6F9@<378?;D>GHF?89EI;A<B@=C4D5:>23761";

            // calculate the sizes
            sizeEasy = (int)Math.Sqrt(easyTestString.Length);
            sizeMedium = (int)Math.Sqrt(mediumTestString.Length);
            sizeHard = (int)Math.Sqrt(hardTestString.Length);
            sizeUnsolvable = (int)Math.Sqrt(unsolvableTestString.Length);
        }


        #region easy boards
        // -------------------------------------------------------------------------------------------EASY-------------------------------------------------------------------------------------

        // check if an easy board is solvable using dancing links
        [Test]
        public void TestEasyBoardSolvableDancingLinks()
        {
            // arrange test
            // run the validation
            IsTheBoardValid(sizeEasy, easyTestString);

            // get the board
            easyBoard = Vboard;

            // convert to matrix
            easyMatrix = IntBoardToByteMatrix(easyBoard, sizeEasy);

            // act test
            // try and solve using backtracking
            dancingLinksSolver = new DancingLinks(easyMatrix, sizeEasy);
            result = dancingLinksSolver.Solve();

            // assert test
            Assert.That(result, Is.EqualTo(true));
        }

        // test if an easy board returns the correct asnwer using dancing links
        [Test]
        public void TestEasyBoardCorrectAsnwerDancingLinks()
        {
            // arrange test
            // run the validation
            IsTheBoardValid(sizeEasy, easyTestString);

            // get the board
            easyBoard = Vboard;

            // convert to matrix
            easyMatrix = IntBoardToByteMatrix(easyBoard, sizeEasy);

            // act test
            // try and fet the correct solution string
            dancingLinksSolver = new DancingLinks(easyMatrix, sizeEasy);
            resultString = dancingLinksSolver.GetSolutionString();

            // assert test
            Assert.That(correctAnswerEasy, Is.EqualTo(resultString));
        }

        // check if an easy board is solvable using backtracking
        [Test]
        public void TestEasyBoardSolvableBacktracking()
        {
            // arrange test
            // run the validation
            IsTheBoardValid(sizeEasy, easyTestString);

            // get the board
            easyBoard = Vboard;

            // act test
            // try and solve using backtracking
            backtrackingSolver = new BackTracking(easyBoard, sizeEasy);
            result = backtrackingSolver.Solve();

            // assert test
            Assert.That(result, Is.EqualTo(true));
        }

        // test if an easy board returns the correct asnwer using backtracking
        [Test]
        public void TestEasyBoardCorrectAsnwerBacktracking()
        {
            // arrange test
            // run the validation
            IsTheBoardValid(sizeEasy, easyTestString);

            // get the board
            easyBoard = Vboard;

            // act test
            // try and fet the correct solution string
            backtrackingSolver = new BackTracking(easyBoard, sizeEasy);
            resultString = backtrackingSolver.GetSolutionString();

            // assert test
            Assert.That(correctAnswerEasy, Is.EqualTo(resultString));
        }

        #endregion easy boards

        #region medium boards
        //-----------------------------------------------------------------------------------------------MEDIUM-------------------------------------------------------------------------------------

        // check if an easy board is solvable using dancing links
        [Test]
        public void TestMediumBoardSolvableDancingLinks()
        {
            // arrange test
            // run the validation
            IsTheBoardValid(sizeMedium, mediumTestString);

            // get the board
            mediumBoard = Vboard;

            // convert to matrix
            mediumMatrix = IntBoardToByteMatrix(mediumBoard, sizeMedium);

            // act test
            // try and solve using backtracking
            dancingLinksSolver = new DancingLinks(mediumMatrix, sizeMedium);
            result = dancingLinksSolver.Solve();

            // assert test
            Assert.That(result, Is.EqualTo(true));
        }

        // test if an easy board returns the correct asnwer using dancing links
        [Test]
        public void TestMediumBoardCorrectAsnwerDancingLinks()
        {
            // arrange test
            // run the validation
            IsTheBoardValid(sizeMedium, mediumTestString);

            // get the board
            mediumBoard = Vboard;

            // convert to matrix
            mediumMatrix = IntBoardToByteMatrix(mediumBoard, sizeMedium);

            // act test
            // try and fet the correct solution string
            dancingLinksSolver = new DancingLinks(mediumMatrix, sizeMedium);
            resultString = dancingLinksSolver.GetSolutionString();

            // assert test
            Assert.That(correctAnswerMedium, Is.EqualTo(resultString));
        }

        // check if an easy board is solvable using backtracking
        [Test]
        public void TestMediumBoardSolvableBacktracking()
        {
            // arrange test
            // run the validation
            IsTheBoardValid(sizeMedium, mediumTestString);

            // get the board
            mediumBoard = Vboard;

            // act test
            // try and solve using backtracking
            backtrackingSolver = new BackTracking(mediumBoard, sizeMedium);
            result = backtrackingSolver.Solve();

            // assert test
            Assert.That(result, Is.EqualTo(true));
        }

        // test if an easy board returns the correct asnwer using backtracking
        [Test]
        public void TestMediumBoardCorrectAsnwerBacktracking()
        {
            // arrange test
            // run the validation
            IsTheBoardValid(sizeMedium, mediumTestString);

            // get the board
            mediumBoard = Vboard;

            // act test
            // try and fet the correct solution string
            backtrackingSolver = new BackTracking(mediumBoard, sizeMedium);
            resultString = backtrackingSolver.GetSolutionString();

            // assert test
            Assert.That(correctAnswerMedium, Is.EqualTo(resultString));
        }

        #endregion medium boards

        #region hard boards
        //-----------------------------------------------------------------------------------------------HARD-------------------------------------------------------------------------------------

        // check if an easy board is solvable using dancing links
        [Test]
        public void TestHardBoardSolvableDancingLinks()
        {
            // arrange test
            // run the validation
            IsTheBoardValid(sizeHard, hardTestString);

            // get the board
            hardBoard = Vboard;

            // convert to matrix
            hardMatrix = IntBoardToByteMatrix(hardBoard, sizeHard);

            // act test
            // try and solve using backtracking
            dancingLinksSolver = new DancingLinks(hardMatrix, sizeHard);
            result = dancingLinksSolver.Solve();

            // assert test
            Assert.That(result, Is.EqualTo(true));
        }

        // test if an easy board returns the correct asnwer using dancing links
        [Test]
        public void TestHardBoardCorrectAsnwerDancingLinks()
        {
            // arrange test
            // run the validation
            IsTheBoardValid(sizeHard, hardTestString);

            // get the board
            hardBoard = Vboard;

            // convert to matrix
            hardMatrix = IntBoardToByteMatrix(hardBoard, sizeHard);

            // act test
            // try and fet the correct solution string
            dancingLinksSolver = new DancingLinks(hardMatrix, sizeHard);
            resultString = dancingLinksSolver.GetSolutionString();

            // assert test
            Assert.That(correctAnswerHard, Is.EqualTo(resultString));
        }

        // check if an easy board is solvable using backtracking
        [Test]
        public void TestHardBoardSolvableBacktracking()
        {
            // arrange test
            // run the validation
            IsTheBoardValid(sizeHard, hardTestString);

            // get the board
            hardBoard = Vboard;

            // act test
            // try and solve using backtracking
            backtrackingSolver = new BackTracking(hardBoard, sizeHard);
            result = backtrackingSolver.Solve();

            // assert test
            Assert.That(result, Is.EqualTo(true));
        }

        // test if an easy board returns the correct asnwer using backtracking
        [Test]
        public void TestHardBoardCorrectAsnwerBacktracking()
        {
            // arrange test
            // run the validation
            IsTheBoardValid(sizeHard, hardTestString);

            // get the board
            hardBoard = Vboard;

            // act test
            // try and fet the correct solution string
            backtrackingSolver = new BackTracking(hardBoard, sizeHard);
            resultString = backtrackingSolver.GetSolutionString();

            // assert test
            Assert.That(correctAnswerHardBacktracking, Is.EqualTo(resultString));
        }

        #endregion hard boards

        #region unsolvable boards
        //--------------------------------------------------------------------------------UNSOLVABLE-----------------------------------------------------------------------------------------------

        // test that an unsolvable board is not solvable using dancing links
        [Test]
        public void TestUnsolvableBoardDancingLinks()
        {
            // arrange test
            // run the validation
            IsTheBoardValid(sizeUnsolvable, unsolvableTestString);

            // get the board
            unsolvableBoard = Vboard;

            // convert to matrix
            unsolvableMatrix = IntBoardToByteMatrix(unsolvableBoard, sizeUnsolvable);

            // act test
            // try and solve using backtracking
            dancingLinksSolver = new DancingLinks(unsolvableMatrix, sizeUnsolvable);
            result = dancingLinksSolver.Solve();

            // assert test
            Assert.That(result, Is.EqualTo(false));
        }

        // test that an unsolvable board is not solvable using backtracking
        [Test]
        public void TestUnsolvableBoardBacktracking()
        {
            // arrange test
            // run the validation
            IsTheBoardValid(sizeUnsolvable, unsolvableTestString);

            // get the board
            unsolvableBoard = Vboard;

            // act test
            // try and solve using backtracking
            backtrackingSolver = new BackTracking(unsolvableBoard, sizeUnsolvable);
            result = backtrackingSolver.Solve();

            // assert test
            Assert.That(result, Is.EqualTo(false));
        }

        #endregion unsolvable boards
    }
}