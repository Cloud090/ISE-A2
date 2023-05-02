using System.Text;

namespace BankApp
{
    public class Login
    {
        //Authenticating user session
        public SessionState Authenticate(List<BankAccount> users, out BankAccount? confirmedUser)
        {
            int currentAttempt = 0;
            int maxAttempts = 3;
            string escapeWord = "exit"; // Input that triggers return to main menu
            confirmedUser = null;       // Initialising confirmed user as null

            Console.WriteLine("\nLog in.");
            Console.WriteLine("Type in 'Exit' at any time to return to main menu.");

            while (currentAttempt < maxAttempts)
            {
                Console.WriteLine("\nPlease enter your email address:"); // Prompting and taking inputs
                string email = Console.ReadLine()!;

                if (string.IsNullOrWhiteSpace(email))
                {
                    Console.WriteLine("Email cannot be blank.");
                    currentAttempt++;
                    continue;
                }
                else if (email.Equals(escapeWord, StringComparison.OrdinalIgnoreCase)) 
                { 
                    return SessionState.Default; 
                }

                Console.WriteLine("\nEnter your password:");
                string password = Console.ReadLine()!;

                if (string.IsNullOrWhiteSpace(password)) // Denying blank attempts
                {
                    Console.WriteLine("Password cannot be blank.");
                    currentAttempt++;
                    continue;
                }
                else if (password.Equals(escapeWord, StringComparison.OrdinalIgnoreCase)) 
                {
                    return SessionState.Default;
                }

                BankAccount existingUser = users.Find(user => user.Email == email && user.Password == password)!; // Checking if user exits and matches inputs

                if (existingUser == null) // If it does not alert user and give another attempt
                {
                    Console.WriteLine("Invalid email or password. Please try again.");
                    currentAttempt++;
                }
                else // If user exists
                {
                    Console.WriteLine("\nLogin successful!");
                    Console.WriteLine($"\nWelcome, {existingUser.Owner}!");
                    Console.WriteLine($"Account number: \t{existingUser.NumberID}");
                    confirmedUser = existingUser;
                    return SessionState.Authenticated;
                }
            }

            Console.WriteLine("Login unsuccessful.");
            Console.WriteLine("\nForgotten your email or password? Contact our support!");
            return SessionState.SessionEnded; // Ends session once reached max attempts
        }
    }
}