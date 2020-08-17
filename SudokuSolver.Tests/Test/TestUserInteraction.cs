using System.Collections.Generic;
using Xunit;
using SudokuSolver.Pages;

namespace SudokuSolver.Tests
{
    /// <summary>
    /// Tests the UI handling for the Index.cshtml page where sudoku field is placed
    /// </summary>
    public class TestUserInteraction
    {
        /// <summary>
        /// Gets the correct theories, to be tested for the duplicate value test
        /// </summary>
        public static IEnumerable<object[]> DuplicateTheories
        {
            get
            {
                var theory = new TheoryData<int?[][]>();
                theory.Add(SudokuTheories.GetTheory(SudokuTheories.THEORY_TYPE.BREAKING_SUDOKU_RULES_ROW));
                theory.Add(SudokuTheories.GetTheory(SudokuTheories.THEORY_TYPE.BREAKING_SUDOKU_RULES_COLUMN));
                theory.Add(SudokuTheories.GetTheory(SudokuTheories.THEORY_TYPE.BREAKING_SUDOKU_RULES_BLOCK));
                return theory;
            }
        }
        /// <summary>
        /// Tests if the clear field function, clears the field
        /// </summary>
        [Fact]
        public void TestClearField()
        {
            //arrange
            IndexModel page = new IndexModel();

            //act
            page.OnPostClear();

            //assert
            Assert.Equal(SudokuTheories.GetTheory(SudokuTheories.THEORY_TYPE.NULL_FIELD), page.Field);
            Assert.True(page.FieldIsValid);
        }
        /// <summary>
        /// Tests how a duplicate value in row, column or block is handled
        /// </summary>
        /// <param name="field"></param>
        [Theory]
        [MemberData(nameof(DuplicateTheories))]
        public void TestDuplicateValuesInField(int?[][] field) {
            //arrange
            IndexModel page = new IndexModel
            {
                Field = field
            };

            //act
            page.OnPostSolve();

            //assert
            Assert.False(page.FieldIsValid);
            Assert.Equal("You can not repeat the same digit in a row, column or block", Error.GetMessage());
        }

        /// <summary>
        /// Tests if a field with values outside 1-9 is handled correctly
        /// </summary>
        [Fact]
        public void TestDigitOutsideRange()
        {
            //arrange
            IndexModel page = new IndexModel
            {
                Field = SudokuTheories.GetTheory(SudokuTheories.THEORY_TYPE.INPUT_OUTSIDE_RANGE)
            };

            //act
            page.OnPostSolve();

            //assert
            Assert.False(page.FieldIsValid);
            Assert.Equal("You can only use digits 1-9", Error.GetMessage());
        }
    }
}
