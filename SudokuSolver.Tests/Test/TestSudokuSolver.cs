using System;
using Xunit;
using SudokuSolver.Pages;
using System.Collections.Generic;

namespace SudokuSolver.Tests
{
    /// <summary>
    /// This class tests the sudoku solver capability by checking if solved field is
    /// according to sudoku rules.
    /// </summary>
    public class TestSudokuSolver
         
    {
        /// <summary>
        /// Gets the correct theories, to be tested
        /// </summary>
        public static IEnumerable<object[]> SolverTheories { get {
                var theory = new TheoryData<int?[][]>();
                theory.Add(SudokuTheories.GetTheory(SudokuTheories.THEORY_TYPE.NULL_FIELD));
                theory.Add(SudokuTheories.GetTheory(SudokuTheories.THEORY_TYPE.VALID_FIELD));
                return theory;
            }
        }
        /// <summary>
        /// Check if provided fields are valid sudoku fields after solving
        /// </summary>
        /// <param name="field">Sudoku field to check, is supplied by static Data attribute</param>
        [Theory]
        [MemberData(nameof(SolverTheories))]
        public void TestNullOrNormalField(int?[][] field)
        {
            //arange
            Solver solver = new Solver(field);
            //act
            solver.Solve();
            bool isValidField = solver.FieldIsAccordingToSudokuRules();
            //assert
            Assert.All(solver.Field, x => Assert.All(x, i => Assert.NotNull(i)));
            Assert.All(solver.Field, x => Assert.All(x, i => Assert.InRange((int)i, 1, 9)));
            Assert.True(isValidField);
        }
    }
}
