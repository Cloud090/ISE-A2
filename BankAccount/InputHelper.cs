
namespace BankApp
{ 
    public static class InputHelper
    {
        public static bool IsEscapeWord(string input) // Checks if input is escape word to return to main menu 
        {
            string escapeWord = "exit"; // Input that triggers return to main menu
            if (input == null) { return false; }
            return input.Equals(escapeWord, StringComparison.OrdinalIgnoreCase); // Compares input to escape word, ignoring case, and returns Boolean
        }
    }
}
