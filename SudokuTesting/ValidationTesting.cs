using SodukoSolver.BoardSolvers;
using System.Drawing;
using static SodukoSolver.Algoritms.HelperFunctions;
using static SodukoSolver.Algoritms.ValidatingFunctions;
using static SodukoSolver.Exceptions.CustomExceptions;

namespace SudokuTesting
{
    /// <summary>
    /// this is the class that is used to check that the validation
    /// function works properly
    /// </summary>
    public class ValidationTesting
    {
        string validMatix;
        string invalidMatrix;
        string invalidChars;
        string invalidSize;
        string invalidCells;

        int sizeValidMatrix;
        int sizeInvalidMatrix;
        int sizeInvalidChars;
        int sizeInvalidSize;
        int sizeInvalidCells;

        [SetUp]
        public void Setup()
        {
            validMatix = "400000805030000000000700000020000060000080400000010000000603070500200000104000000";
            invalidMatrix = "4000008050300000000007000000200000600000804000000100000006030705002000001040000001";
            invalidChars = "40000080503000000000070000002000006000008040000001000000060307050020000010400000a";
            invalidSize = "00";
            invalidCells = "400000805030000000000700000020000060000080400000010000000603070500200000104000088";

            sizeValidMatrix = (int)Math.Sqrt(validMatix.Length);
            sizeInvalidMatrix = (int)Math.Sqrt(invalidMatrix.Length);
            sizeInvalidChars = (int)Math.Sqrt(invalidChars.Length);
            sizeInvalidSize = (int)Math.Sqrt(invalidSize.Length);
            sizeInvalidCells = (int)Math.Sqrt(invalidCells.Length);
        }

        // tests that the validation returns true for a simple valid matrix
        [Test]
        public void TestValidMatrix()
        {
            bool passedValidation = IsTheBoardValid(sizeValidMatrix, validMatix);
            Assert.IsTrue(passedValidation);
        }

        // test that the validation returns false for a matrix that is not valid
        [Test]
        public void TestInvalidMatrix()
        {
            bool passedValidation = IsTheBoardValid(sizeInvalidMatrix, invalidMatrix);
            Assert.IsFalse(passedValidation);
        }

        // test that the validation returns false for a matrix that has invalid characters
        [Test]
        public void TestInvalidChars()
        {
            // check that the validation function throws the correct exception
            Assert.Throws<InvalidCharacterException>(() => Validate(sizeInvalidChars, invalidChars));
        }

        // test that the validation returns false for a matrix that has invalid size
        [Test]
        public void TestInvalidSize()
        {
            // check that the validation function throws the correct exception
            Assert.Throws<SizeException>(() => Validate(sizeInvalidSize, invalidSize));
        }

        // test that the validation returns false for a matrix that has invalid cells
        [Test]
        public void TestInvalidCells()
        {
            // check that the validation function throws the correct exception
            Assert.Throws<BoardCellsNotValidException>(() => Validate(sizeInvalidCells, invalidCells));
        }
    }
}
