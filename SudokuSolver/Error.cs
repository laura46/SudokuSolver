namespace SudokuSolver
{
    /// <summary>
    /// Handles the error message on the index.cshtml page
    /// </summary>
    public static class Error
    {   
        /// <summary>
        /// The error message to be shown to the user for feedback
        /// </summary>
        private static string ErrorMessage { get; set; }

        /// <summary>
        /// This function retrieves the error message
        /// </summary>
        /// <returns>If the error message is set it will return this message and otherwise an empty string</returns>
        public static string GetMessage() {
            return ErrorMessage ?? "";
        }

        /// <summary>
        /// Sets the error messages based on the supplied type
        /// </summary>
        /// <param name="type">Type of message that should be shown to the user</param>
        public static void SetMessage(MESSAGE_TYPE type) {
            switch (type)
            {
                case MESSAGE_TYPE.INVALID_DIGIT:
                    ErrorMessage = "You can only use digits 1-9";
                    break;
                case MESSAGE_TYPE.BREAKS_SUDOKU_RULES:
                    ErrorMessage = "You can not repeat the same digit in a row, column or block";
                    break;
                case MESSAGE_TYPE.NO_ERROR:
                    ErrorMessage = "";
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Enum of message types, in the function SetMessage these are used to decide what message to show
        /// </summary>
        public enum MESSAGE_TYPE { 
            INVALID_DIGIT,
            BREAKS_SUDOKU_RULES,
            NO_ERROR
        }

    }
}
