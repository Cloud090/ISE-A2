
namespace BankApp
{
    public class Login
    {
        //Authenticating user session
        public SessionState Authenticate(List<BankAccount> users, out BankAccount? confirmedUser)
        {
            int currentAttempt = 0;
            int maxAttempts = 3;
            confirmedUser = null; // Pre-setting confirmed user to null/empty

            Console.WriteLine("\nLog in.");

            while (currentAttempt < maxAttempts)
            {
                Console.WriteLine("\nPlease enter your email address:"); // Prompting and taking inputs
                string email = Console.ReadLine()!;

                if (string.IsNullOrWhiteSpace(email))
                {
                    Console.WriteLine("Please enter an email address.");
                    currentAttempt++;
                    continue;
                }

                Console.WriteLine("\nEnter your password:");
                string password = Console.ReadLine()!;

                if (string.IsNullOrWhiteSpace(password)) // Denying blank attempts
                {
                    Console.WriteLine("Password cannot be blank.");
                    currentAttempt++;
                    continue;
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

            Console.WriteLine("Login unsuccessful. \nForgotten the email or password? Contact our support!");
            return SessionState.SessionEnded; // Ends session once reached max attempts
        }
    }
}