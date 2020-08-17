using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SudokuSolver.Pages
{
    /// <summary>
    /// Logic behind Index.cshtml, generates an empty sudoku field, and handles post requests from Index.cshtml
    /// </summary>
    public class IndexModel : PageModel
    {
        /// <summary>
        /// The sudoku field that is shown to the user, binding the user input for solve
        /// </summary>
        [BindProperty]
        public int?[][] Field { get; set; }

        /// <summary>
        /// Multidimensional array that keeps track of all the positions where a user has filled out the field
        /// </summary>
        public bool[,] UserInputPosition { get; set; }

        /// <summary>
        /// Is false when the field is not valid for solving
        /// </summary>
        public bool FieldIsValid { get; set; }

        /// <summary>
        /// An instance of the solver that will solve the sudoku field
        /// </summary>
        private Solver Solver { get; set; }

        /// <summary>
        /// Constructor, generates an empty sudoku field 
        /// </summary>
        public IndexModel()
        {
            GenerateEmptyField();
            FieldIsValid = true;
        }

        /// <summary>
        /// Post method for handling the clear button, with this a new empty field is generated
        /// </summary>
        public void OnPostClear() {
            //Reset the UI
            ModelState.Clear();
            GenerateEmptyField();
            Error.SetMessage(Error.MESSAGE_TYPE.NO_ERROR);
        }

        /// <summary>
        /// Post method for handling the solve button, uses the solver instance to solve the field
        /// </summary>
        public void OnPostSolve() 
        {
            //Instantiate solver with the current field
            Solver = new Solver(Field);
            //First check if the given board is correct and doesn't break any of the rules
            if (ValidateAndRememberUserInput() && Solver.FieldIsAccordingToSudokuRules())
            {
                //Reset the UI
                Error.SetMessage(Error.MESSAGE_TYPE.NO_ERROR);
                ModelState.Clear();

                //Solve the field and show solution
                Solver.Solve();
                Field = Solver.Field;
            }
            else {
                //Set the valid state to false to show correct UI
                ModelState.Clear();
                FieldIsValid = false;
            }
        }

        /// <summary>
        /// Checks if user input digits are between 1-9 and remembers the position of user input
        /// </summary>
        /// <returns>Returns true if all digits are between 1-9</returns>
        private bool ValidateAndRememberUserInput() 
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (Field[i][j] != null) 
                    {
                        UserInputPosition[i, j] = true;
                        if (!(Field[i][j] > 0 && Field[i][j] < 10))
                        {
                            Error.SetMessage(Error.MESSAGE_TYPE.INVALID_DIGIT);
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Generates a new empty sudoku field, and resets all the user input indicators to false
        /// </summary>
        public void GenerateEmptyField() 
        {
            Field = new int?[9][];
            //Remember where user input is fo clear visual of result
            UserInputPosition = new bool[9, 9];
            for (int i = 0; i < 9; i++)
            {
                Field[i] = new int?[9];
                for (int j = 0; j < 9; j++)
                {
                    //Set everything to null/false for a fresh start
                    Field[i][j] = null;
                    UserInputPosition[i, j] = false;
                }
            }
        }
    }
}
