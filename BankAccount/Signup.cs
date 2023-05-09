using System.Net.Mail;

namespace BankApp
{
    public class User
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }

    public class Signup
    {
        public SessionState Register(List<BankAccount> accounts)
        {
            int currentAttempt = 0;
            const int maxAttempts = 3;
            string password = "";
            string name = string.Empty;
            string input = string.Empty;
            decimal initialBalance = 0;

            Console.WriteLine("\nSign up");
            Console.WriteLine("Type in 'Exit' at any time to return to main menu.");
            Console.WriteLine("\nPlease enter your email:");
            string email = Console.ReadLine() ?? string.Empty;
            email = email.Trim();

            while (email == "" && currentAttempt < maxAttempts || IsValid(email) == false && currentAttempt < maxAttempts)
            {
                if (InputHelper.IsEscapeWord(email))
                {
                    Console.WriteLine("Registration cancelled.");
                    return SessionState.Default;
                }

                Console.WriteLine("Invalid email. Please try again.");
                currentAttempt++;

                if (currentAttempt < maxAttempts)
                {
                    Console.WriteLine("\nPlease enter your email:");
                    email = Console.ReadLine() ?? string.Empty;
                    email = email.Trim();
                }
            }

            if (accounts.Any(u => u.Email == email))
            {
                Console.WriteLine("\nUser with this email already exists.");
                Console.WriteLine("Press 1 to return to main menu.");
                Console.WriteLine("Press 2 to log in.");
                string choice = Console.ReadLine() ?? string.Empty;
                choice = choice.Trim();

                while (choice != "1" && choice != "2" && !InputHelper.IsEscapeWord(choice))
                {
                    Console.WriteLine("\nInvalid choice.");
                    Console.WriteLine("Press 1 to return to main menu.");
                    Console.WriteLine("Press 2 to log in.");
                    choice = Console.ReadLine() ?? string.Empty;
                    choice = choice.Trim();
                }

                if (choice == "1" || InputHelper.IsEscapeWord(choice)) { return SessionState.Default; }
                else if (choice == "2") { return SessionState.ExistingUser; } 
            }

            if (currentAttempt < maxAttempts)
            {
                Console.WriteLine("\nPlease create a password:");
                password = Console.ReadLine() ?? string.Empty;
                password = password.Trim();
            }

            while (string.IsNullOrEmpty(password) && currentAttempt < maxAttempts)
            {
                Console.WriteLine("Password cannot be blank.");
                currentAttempt++;

                if (string.IsNullOrEmpty(password)) {
                    Console.WriteLine("Would you like to cancel account creation and return to the main menu? \n1. Exit to main menu \n2. Try password creation again");
                    string endAccountCreation = Console.ReadLine()!;
                    if (endAccountCreation == "1" || endAccountCreation == "exit" || endAccountCreation == "yes") {
                        return SessionState.Default;
                    }
                }

                if (currentAttempt < maxAttempts)
                {
                    Console.WriteLine("\nPlease create a password:");
                    password = Console.ReadLine() ?? string.Empty;
                    password = password.Trim();
                }
            }

            if (currentAttempt < maxAttempts)
            {
                Console.WriteLine("\nPlease enter your name:");
                name = Console.ReadLine()!;
                name = name.Trim();

                if (InputHelper.IsEscapeWord(name)) { return SessionState.Default; }
            }

            while (string.IsNullOrEmpty(name) && currentAttempt < maxAttempts)
            {
                Console.WriteLine("Name cannot be blank.");
                currentAttempt++;

                if (currentAttempt < maxAttempts)
                {
                    Console.WriteLine("\nPlease enter your name:");
                    name = Console.ReadLine() ?? string.Empty;
                    name = name.Trim();

                    if (InputHelper.IsEscapeWord(name)) 
                    {
                        Console.WriteLine("Registration cancelled.");
                        return SessionState.Default;
                    }
                }
            }

            if (currentAttempt < maxAttempts)
            {
                Console.WriteLine("\nPlease enter your initial deposit:");
                Console.WriteLine("\t(Note: Initial deposit must be positive number)");
                Console.Write("$");
                input = Console.ReadLine()!;
            }

            while (!Decimal.TryParse(input, out initialBalance) && initialBalance <= 0 && currentAttempt < maxAttempts)
            {
                if (InputHelper.IsEscapeWord(input))
                {
                    return SessionState.Default;
                }

                Console.Write("Initial deposit must be a positive number.");
                currentAttempt++;
                if (currentAttempt < maxAttempts)
                {
                    Console.WriteLine("\nPlease enter your initial deposit:");
                    Console.Write("$");
                    input = Console.ReadLine()!;
                }
            }

            if (currentAttempt >= maxAttempts)
            {
                Console.WriteLine("\nRegistration unsuccessful.");
                return SessionState.Default;
            }
            else 
            {
                BankAccount newUser = new BankAccount(email, password, name, initialBalance);
                accounts.Add(newUser);

                Console.WriteLine("\nRegistration successful!");
                Console.WriteLine("\nEmail is: " + email);
                Console.WriteLine("Password has been set.");
                Console.WriteLine("\nPlease log in to start banking.");
                Console.ReadKey();
                return SessionState.Default;
            }
        }

        private bool IsValid(string email)
        {
            var valid = true;

            try
            {
                var emailAddress = new MailAddress(email); 
            }
            catch
            {
                valid = false;
            }

            return valid;
        }
    }
}