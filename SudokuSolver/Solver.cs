using SudokuSolver.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SudokuSolver
{
    /// <summary>
    /// This class solves the sudoku field given on index.cshtml. It first validates if the user input was
    /// correct and not breaking any sudoku rules. After it solves the field and returns it to the index.cshtml view.
    /// </summary>
    public class Solver
    {
        /// <summary>
        /// The sudoku field that needs to be solved in this class.
        /// </summary>
        public int?[][] Field { get; set; }

        /// <summary>
        /// Constructor for Solver class
        /// </summary>
        /// <param name="field">Sudoku field that needs to be validated and solved</param>
        public Solver(int?[][] field)
        {
            Field = field;
        }

        /// <summary>
        /// Checks if the current Field property doesn't break any sudoku rules, by checking if there are any double digits in
        /// a row, column or block.
        /// </summary>
        /// <returns>Returns true if all digits are between 1-9 and don't break any sudoku rules</returns>
        public bool FieldIsAccordingToSudokuRules()
        {
            for (int i = 0; i < 9; i++)
            {
                int?[] row = new int?[9];
                int?[] col = new int?[9];
                int?[] block = GetBlock(i);
                for (int j = 0; j < 9; j++)
                {
                    row[j] = Field[i][j];
                    col[j] = Field[j][i];
                }
                //Group the values by themselves, and check if the group key is not null, or has a count of over 1
                if (row.GroupBy(x => x).Any(g => g.Key != null && g.Count() > 1) || col.GroupBy(x => x).Any(g => g.Key != null && g.Count() > 1) ||
                    block.GroupBy(x => x).Any(g => g.Key != null && g.Count() > 1))
                {
                    //Set the error message to notify the user about the rules being broken.
                    Error.SetMessage(Error.MESSAGE_TYPE.BREAKS_SUDOKU_RULES);
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Gets all the values for the block number.
        /// </summary>
        /// <param name="blockNumber">The number of the block to get between 0 - 8</param>
        /// <returns>An int array with all the values for the block</returns>
        private int?[] GetBlock(int blockNumber) 
        {
            int?[] block = new int?[9];
            //Manipulate row and column value to get correct block
            int row = blockNumber - blockNumber % 3;
            int col = blockNumber % 3 * 3;
            //Keep a counter to iterate 0-8 for block variable
            int counter = 0;
            for (int i = row; i < row + 3; i++)
            {
                for (int j = col; j < col + 3; j++)
                {
                    block[counter] = Field[i][j];
                    counter++;
                }
            }
            return block;
        }

        /// <summary>
        /// Solves the sudoku field by recursively checking if the digit is allowed to be placed
        /// on the field. The function will re-enter the for loops ensuring all previous steps are
        /// correct before returning true.
        /// </summary>
        /// <returns>Returns true once the field is full by checking if row 8 and column 8 have been reached</returns>
        public bool Solve()
        {
            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    //Check if field is empty, so it can be filled
                    if (Field[row][col] == null)
                    {
                        for (int digit = 1; digit <= 9; digit++)
                        {
                            //Check if digit is not already in row, column or block
                            if (!IsInRowOrColumnOrBlock(row, col, digit))
                            {
                                //Place correct digit
                                Field[row][col] = digit;
                                //Continue to solve field until field is full aka row = 8 & col = 8
                                if ((row == 8 && col == 8) || Solve())
                                {
                                    return true;
                                }
                                else
                                {
                                    //If solve is false, set value to null and increase the digit and try again
                                    Field[row][col] = null;
                                }
                            }
                        }
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Checks if a digit is present in the corresponding row, column and block.
        /// </summary>
        /// <param name="row">Row to see if digit is in it</param>
        /// <param name="digit">Digit to check</param>
        /// <returns>Returns false if digit is not present</returns>
        private bool IsInRowOrColumnOrBlock(int row, int col, int digit)
        {
            //First check column and row
            for (int i = 0; i < 9; i++)
            {
                if (Field[row][i] == digit || Field[i][col] == digit) return true;
            }

            //Then use values of row and column to check block
            int r = row - row % 3;
            int c = col - col % 3;
            for (int j = r; j < r + 3; j++)
            {
                for (int k = c; k < c + 3; k++)
                {
                    if (Field[j][k] == digit) return true;
                }
            }

            //If true was never returned the digit is not present in row block or column
            return false;
        }
    }
}
