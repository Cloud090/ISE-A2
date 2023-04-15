
namespace BankApp
{
    public class Login
    {
        public SessionState Authenticate(List<BankAccount> accountsList, out BankAccount? confirmedUser)
        {
            int currentAttempt = 0;
            int maxAttempts = 3;
            confirmedUser = null;

            Console.WriteLine("\nLog in.");

            while (currentAttempt < maxAttempts)
            {
                Console.WriteLine("\nPlease enter your email address:");
                string email = Console.ReadLine()!;

                if (string.IsNullOrWhiteSpace(email))
                {
                    Console.WriteLine("Please enter an email address.");
                    currentAttempt++;
                    continue;
                }

                Console.WriteLine("\nEnter your password:");
                string password = Console.ReadLine()!;

                if (string.IsNullOrWhiteSpace(password))
                {
                    Console.WriteLine("Password cannot be blank.");
                    currentAttempt++;
                    continue;
                }

                BankAccount existingUser = accountsList.Find(user => user.Email == email && user.Password == password);

                if (existingUser == null)
                {
                    Console.WriteLine("Invalid email or password. Please try again.");
                    currentAttempt++;
                }
                else
                {
                    Console.WriteLine("\nLogin successful!");
                    Console.WriteLine($"\nWelcome, {existingUser.Owner}!");
                    Console.WriteLine($"Account number: \t{existingUser.NumberID}");
                    confirmedUser = existingUser;
                    return SessionState.Authenticated;
                }
            }

            Console.WriteLine("Login unsuccessful. \nForgotten the email or password? Contact our support!");
            return SessionState.SessionEnded;
        }
    }
}