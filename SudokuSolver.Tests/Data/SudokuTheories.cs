namespace SudokuSolver.Tests
{/// <summary>
/// This class returns sudoku fields for theory data
/// </summary>
    public static class SudokuTheories 
    {
        /// <summary>
        /// Function to get theory based on THEORY_TYPE
        /// </summary>
        /// <param name="theory">Theory type that needs to be added to theories</param>
        /// <returns>A new theory of a field</returns>
        public static int?[][] GetTheory(THEORY_TYPE theory)
        {
            int?[][] field = new int?[9][];
            for (int i = 0; i < 9; i++)
            {
                
                switch (theory)
                {
                    case THEORY_TYPE.NULL_FIELD:
                        field[i] =  new int?[9] { null, null, null, null, null, null, null, null, null };
                        break;
                    case THEORY_TYPE.VALID_FIELD:
                        field[i] = new int?[9] { null, null, null, (i + 1), null, null, null, null, null };
                        break;
                    case THEORY_TYPE.INPUT_OUTSIDE_RANGE:
                        field[i] = new int?[9] { null, 22, null, null, null, null, null, -5, null };
                        break;
                    case THEORY_TYPE.BREAKING_SUDOKU_RULES_ROW:
                        field[i] = new int?[9] { null, null, (i + 1), null, null, null, (i + 1), null, null };
                        break;
                    case THEORY_TYPE.BREAKING_SUDOKU_RULES_COLUMN:
                        int number = 4;
                        field[i] = new int?[9] { null, null, number, null, null, null, null, null, null };
                        break;
                    case THEORY_TYPE.BREAKING_SUDOKU_RULES_BLOCK:
                        field[i] = new int?[9] { null, null, null, null, null, null, null, null, null };
                        field[i][i] = 5;
                        break;
                    default:
                        break;
                }
                
            }
            return field;
        }

        /// <summary>
        /// Theory/ sudoku field types
        /// </summary>
        public enum THEORY_TYPE
        {
            NULL_FIELD,
            VALID_FIELD,
            INPUT_OUTSIDE_RANGE,
            BREAKING_SUDOKU_RULES_ROW,
            BREAKING_SUDOKU_RULES_COLUMN,
            BREAKING_SUDOKU_RULES_BLOCK
        }
    }
}
