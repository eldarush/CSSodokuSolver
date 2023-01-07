using SodukoSolver.BoardSolvers;
using static SodukoSolver.Algoritms.HelperFunctions;
using static SodukoSolver.Algoritms.ValidatingFunctions;

namespace SudokuTesting
{
    /// <summary>
    /// this is class that is used to test that the algorithms give off 
    /// the correct answers for diffrent boards
    /// </summary>
    public class AnswerTesting
    {
        string testString;
        int size;
        int[,] solvableBoard;
        byte[,] matrix;
        BoardSolver dancingLinksSolver;
        BoardSolver backtrackingSolver;

        [SetUp]
        public void Setup()
        {
            // create new board of ints that will be tested
            testString = "800000070006010053040600000000080400003000700020005038000000800004050061900002000";

            // calculate the size 
            size = (int)Math.Sqrt(testString.Length);

            // run the validation
            IsTheBoardValid(size, testString);

            // copy the board into the array
            solvableBoard = Vboard;

            // convert to matrux
            matrix = IntBoardToByteMatrix(solvableBoard, size);

        }

        // check if an easy board is solvable using dancing links
        [Test]
        public void TestEasyBoardSolvableDancingLinks()
        {
            // try and solve using backtracking
            dancingLinksSolver = new DancingLinks(matrix, size);
            Assert.That(dancingLinksSolver.Solve(), Is.EqualTo(true));
        }

        // test if an easy board returns the correct asnwer using dancing links
        [Test]
        public void TestEasyBoardCorrectAsnwerDancingLinks()
        {
            // the correct asnwer string to the easy board
            string correctAsnwer = "831529674796814253542637189159783426483296715627145938365471892274958361918362547";

            // try and fet the correct solution string
            dancingLinksSolver = new DancingLinks(matrix, size);
            Assert.That(correctAsnwer, Is.EqualTo(dancingLinksSolver.GetSolutionString()));
        }

        // check if an easy board is solvable using backtracking
        [Test]
        public void TestEasyBoardSolvableBacktracking()
        {
            // try and solve using backtracking
            backtrackingSolver = new BackTracking(solvableBoard, size);
            Assert.That(backtrackingSolver.Solve(), Is.EqualTo(true));
        }

        // test if an easy board returns the correct asnwer using backtracking
        [Test]
        public void TestEasyBoardCorrectAsnwerBacktracking()
        {
            // the correct asnwer string to the easy board
            string correctAsnwer = "831529674796814253542637189159783426483296715627145938365471892274958361918362547";

            // try and fet the correct solution string
            backtrackingSolver = new BackTracking(solvableBoard, size);
            Assert.That(correctAsnwer, Is.EqualTo(backtrackingSolver.GetSolutionString()));
        }

    }
}