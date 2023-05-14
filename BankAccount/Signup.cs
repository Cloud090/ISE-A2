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
            int currentAttempt = 0;     // Tracking attempts
            const int maxAttempts = 3;  // Max attempts to signup if required
            string password = "";       // Sets a blank password so password is not null
            string name = string.Empty; // Sets a blank name so name is not null
            string input = string.Empty;// Sets blank input variable for deposit & to catch escape word
            decimal initialBalance = 0; // Initialises starting balance to $0 to avoid null

            Console.WriteLine("\nSign up");
            Console.WriteLine("Type in 'Exit' at any time to return to main menu.");
            Console.WriteLine("\nPlease enter your email:");
            string email = Console.ReadLine() ?? string.Empty;
            email = email.Trim();

            // If email input is blank, max attempts have been reached &/or email is an invalid format it will enter the loop
            while (email == "" && currentAttempt < maxAttempts || IsValid(email) == false && currentAttempt < maxAttempts)
            {
                if (InputHelper.IsEscapeWord(email)) // Checking for escape word
                {
                    Console.WriteLine("Registration cancelled.");
                    return SessionState.Default; // Returns to main menu
                }

                Console.WriteLine("Invalid email. Please try again.");
                currentAttempt++;

                if (currentAttempt < maxAttempts) // If not escape word then notify user to reattempt entering a valid email
                {
                    Console.WriteLine("\nPlease enter your email:");
                    email = Console.ReadLine() ?? string.Empty;
                    email = email.Trim();
                }
            }

            // Check if user with email already exists
            if (accounts.Any(u => u.Email == email))
            {
                Console.WriteLine("\nUser with this email already exists.");
                Console.WriteLine("Press 1 to return to main menu.");
                Console.WriteLine("Press 2 to log in.");
                string choice = Console.ReadLine() ?? string.Empty;
                choice = choice.Trim();

                // Loops until valid choice is made to determine if they wish to login or go back to main menu
                while (choice != "1" && choice != "2" && !InputHelper.IsEscapeWord(choice))
                {
                    Console.WriteLine("\nInvalid choice.");
                    Console.WriteLine("Press 1 to return to main menu.");
                    Console.WriteLine("Press 2 to log in.");
                    choice = Console.ReadLine() ?? string.Empty;
                    choice = choice.Trim();
                }

                if (choice == "1" || InputHelper.IsEscapeWord(choice)) { return SessionState.Default; } // Returns to main menu
                else if (choice == "2") { return SessionState.ExistingUser; } // Enters log in menu
            }

            if (currentAttempt < maxAttempts) // Only allowing inputs if attempt limit isn't reached
            {
                Console.WriteLine("\nPlease create a password:");
                password = Console.ReadLine() ?? string.Empty;
                password = password.Trim();
            }

            while (string.IsNullOrEmpty(password) && currentAttempt < maxAttempts) // Enters here if attempts exist & password input was left blank
            {
                Console.WriteLine("Password cannot be blank.");
                currentAttempt++;

                if (string.IsNullOrEmpty(password)) { // If password is left empty enters here
                    Console.WriteLine("Would you like to cancel account creation and return to the main menu? \n1. Exit to main menu \n2. Try password creation again");
                    string endAccountCreation = Console.ReadLine()!;
                    if (endAccountCreation == "1" || endAccountCreation == "exit" || endAccountCreation == "yes") { // Enters if user wishes to cancel account creation
                        return SessionState.Default;
                    }
                }

                if (currentAttempt < maxAttempts) // Enters when attempts are left & user didn't want to cancel signup
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

            while (string.IsNullOrEmpty(name) && currentAttempt < maxAttempts) // Enters here if attempts exist & password input was left blank
            {
                Console.WriteLine("Name cannot be blank.");
                currentAttempt++;

                if (currentAttempt < maxAttempts)
                {
                    Console.WriteLine("\nPlease enter your name:");
                    name = Console.ReadLine() ?? string.Empty;
                    name = name.Trim();

                    if (InputHelper.IsEscapeWord(name)) // checking escape word before proceeding
                    {
                        Console.WriteLine("Registration cancelled.");
                        return SessionState.Default;
                    }
                }
            }

            if (currentAttempt < maxAttempts) // Taking a deposit to be inputted to the account at creation time
            {
                Console.WriteLine("\nPlease enter your initial deposit:");
                Console.WriteLine("\t(Note: Initial deposit must be positive number)");
                Console.Write("$");
                input = Console.ReadLine()!;
            }

            while (!Decimal.TryParse(input, out initialBalance) && initialBalance <= 0 && currentAttempt < maxAttempts)
            {
                if (InputHelper.IsEscapeWord(input)) // checking escape word
                {
                    return SessionState.Default;
                }

                Console.Write("Initial deposit must be a positive number."); // Disallowed input taken, alerting user & allowing another attempt
                currentAttempt++;
                if (currentAttempt < maxAttempts)
                {
                    Console.WriteLine("\nPlease enter your initial deposit:");
                    Console.Write("$");
                    input = Console.ReadLine()!;
                }
            }

            if (currentAttempt >= maxAttempts)  // If the max attempts are reached registration is unsuccessful & session ends
            {
                Console.WriteLine("\nRegistration unsuccessful.");
                return SessionState.Default;
            }
            else  // Otherwise new user will be created
            {
                BankAccount newUser = new BankAccount(email, password, name, initialBalance);
                accounts.Add(newUser);

                Console.WriteLine("\nRegistration successful!");
                Console.WriteLine("\nEmail is: " + email);
                Console.WriteLine("Password has been set.");
                Console.WriteLine("\nPlease log in to start banking.");
                Console.ReadKey(); // Allows a break for user to read info before going back to main menu
                return SessionState.Default;
            }
        }

        private bool IsValid(string email) // This method checks if emails passed by accounts are a valid format 
        {
            var valid = true; // Assumes email is valid unless found otherwise

            try
            {
                var emailAddress = new MailAddress(email); // passes to Net.Mail
            }
            catch
            {
                valid = false; // If the email is invalid false is set and user will be prompted to enter a valid email format
            }

            return valid;
        }
    }
}