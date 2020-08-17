using System.Collections;
using System.Collections.Generic;

namespace SudokuSolver.Tests
{
    /// <summary>
    /// Implementation of abstract theorydata case, to use for enumerating theories
    /// </summary>
    public abstract class TheoryData : IEnumerable<int?[][]>
    {
        /// <summary>
        /// List of fields to be tested
        /// </summary>
        public readonly List<int?[][]> data = new List<int?[][]>();

        /// <summary>
        /// Adds a new field to the data list for testing
        /// </summary>
        /// <param name="values">Sudoku field that needs to be tested</param>
        protected void AddRow(params int?[][] field)
        {
            data.Add(field);
        }

        public IEnumerator<int?[][]> GetEnumerator()
        {
            return data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
