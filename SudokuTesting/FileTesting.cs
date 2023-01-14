using NUnit.Framework;
using SodukoSolver;
using SodukoSolver.BoardSolvers;
using SodukoSolver.Interfaces;
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
    /// this is a class that tests the reading and writing of files
    /// works
    /// </summary>
    public class FileTesting
    {
        // the files paths
        string path;
        string resultPath;

        // file reader
        FileReader reader;

        // the board size
        int size;

        // the board string
        string boardString;

        // the result string
        string resultString;
        string expectedResult;
        string actualResult;

        // the board in int form
        int[,] board;

        // the board in byte form
        byte[,] matrix;

        // the solver
        BoardSolver solver;

        // the result
        bool result;

        // set up the tests
        [SetUp]
        public void Setup()
        {
            // set the file paths
            path = "C:\\Users\\eldar\\source\\repos\\eldarush\\CSSodokuSolver\\SodukoSolver\\TextFiles\\testboard.txt";
            resultPath = "C:\\Users\\eldar\\source\\repos\\eldarush\\CSSodokuSolver\\SodukoSolver\\TextFiles\\testboardSOLVED.txt";

            // create new file reader with the given path
            reader = new FileReader(path);

            // get the board string
            boardString = reader.Read();

            // set the expected result string
            expectedResult = "15:2349;<@6>?=78>@8=5?7<43129:6;9<47:@618=?;35>236;?2=8>75:94@<1=4>387;:5<261?@98;76412@9:>?<35=<91:=5?634@8>2;7@?259<>31;7=:68462@>;94=?1<587:37=91?235;>8:@<46583;1:<7264@=9?>?:<4>6@8=9372;152358<>:?6794;1=@:7=<@359>8;1642?;1?968=4@25<7>3:4>6@7;12:?=3589<";
        }

        // test that the areading from files Works
        [Test]
        public void TestFileReading()
        {
            // arrange test
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

        // test that the writing to files Works
        [Test]
        public void TestFileWriting()
        {
            // arrange test
            size = (int)Math.Sqrt(boardString.Length);
            IsTheBoardValid(size, boardString);
            board = Vboard;
            matrix = IntBoardToByteMatrix(board, size);

            // act test
            solver = new DancingLinks(matrix, size);
            resultString = resultString = solver.GetSolutionString();
            reader.Write(resultString);
            actualResult = File.ReadAllText(resultPath);

            // assert test
            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }
    }
}
