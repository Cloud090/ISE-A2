using System.Text;

namespace BankApp
{
    public class Login
    {
        //Authenticating user session
        public SessionState Authenticate(List<BankAccount> accounts, out BankAccount? confirmedUser)
        {
            int currentAttempt = 0;
            int maxAttempts = 3;
            confirmedUser = null;       // Initialising confirmed user as null

            Console.WriteLine("\nLog in.");
            Console.WriteLine("Type in 'Exit' at any time to return to main menu.");

            while (currentAttempt < maxAttempts)
            {
                Console.WriteLine("\nPlease enter your email address:"); // Prompting and taking inputs
                string email = Console.ReadLine()!;

                if (string.IsNullOrWhiteSpace(email)) // Denying blank attempts
                {
                    Console.WriteLine("Email cannot be blank.");
                    currentAttempt++;
                    continue;
                }
                else if (InputHelper.IsEscapeWord(email)) // If input is 'exit'
                {
                    Console.WriteLine("Log in cancelled.");
                    return SessionState.Default; // Return to main menu
                }

                Console.WriteLine("\nEnter your password:");
                string password = Console.ReadLine()!;

                if (string.IsNullOrEmpty(password)) // Denying blank attempts, offering escape option
                { 
                    Console.WriteLine("Password Cannot be blank. \nWould you like to cancel account login and return to the main menu?");
                    Console.WriteLine("\t1. Exit to main menu \n\t2. Continue");
                    string endAccountCreation = Console.ReadLine()!;
                    if (endAccountCreation == "1" || endAccountCreation == "exit" || endAccountCreation == "yes") {
                        Console.WriteLine("Log in cancelled.");
                        return SessionState.Default;
                    }
                    currentAttempt++;
                }

                BankAccount existingUser = accounts.Find(user => user.Email == email && user.Password == password)!; // Checking if user exists and matches inputs

                if (existingUser == null) // If it does not alert user and give another attempt
                {
                    if (InputHelper.IsEscapeWord(password)) { 
                        Console.WriteLine("Log in cancelled.");
                        return SessionState.Default; 
                    }
                    else {
                        Console.WriteLine("Invalid email or password. Please try again.");
                        currentAttempt++;
                    }
                    
                }
                else // If user exists
                {
                    Console.WriteLine("\nLogin successful!");
                    Console.WriteLine($"\nWelcome, {existingUser.Owner}!");
                    Console.WriteLine($"Account number: \t{existingUser.NumberID}");
                    Console.WriteLine("\nType in 'Exit' at any time to return to previous menu.");
                    confirmedUser = existingUser;
                    return SessionState.Authenticated; // Go to account menu
                }
            }

            Console.WriteLine("Login unsuccessful.");
            Console.WriteLine("\nForgotten your email or password? Contact our support!");
            return SessionState.SessionEnded; // Ends session once reached max attempts
        }
    }
}