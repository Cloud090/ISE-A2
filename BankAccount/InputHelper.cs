
namespace BankApp
{ 
    public static class InputHelper
    {
        public static bool IsEscapeWord(string input)
        {
            string escapeWord = "exit";
            if (input == null) { return false; }
            return input.Equals(escapeWord, StringComparison.OrdinalIgnoreCase);
        }
    }
}
