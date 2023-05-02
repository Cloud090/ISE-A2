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
        public SessionState Register (List<BankAccount> users)
        {
            int currentAttempt = 0;     // Tracking attempts
            int maxAttempts = 3;        // Max attempts to signup if required
            string password = "";       // Sets a blank password so password is not null
            string escapeWord = "exit"; // Input that triggers return to main menu

            Console.WriteLine("\nSign up");
            Console.WriteLine("Type in 'Exit' at any time to return to main menu.");
            Console.WriteLine("\nPlease enter your email:");
            string email = Console.ReadLine()!;

            // If email input is blank, max attempts have been reached &/or email is an invalid format it will enter the loop
            while (email == "" && currentAttempt < maxAttempts || IsValid(email) == false && currentAttempt < maxAttempts)
            {
                if (email.Equals(escapeWord, StringComparison.OrdinalIgnoreCase))
                {
                    return SessionState.Default;
                }

                Console.WriteLine("Invalid email. Please try again.");
                currentAttempt++;

                if (currentAttempt < maxAttempts)
                {
                    Console.WriteLine("\nPlease enter your email:");
                    email = Console.ReadLine()!;
                }
            }

            bool alreadyRegistered = false;
            // Check if user with email already exists
            if (users.Any(u => u.Email == email))
            {
                alreadyRegistered = true;
                Console.WriteLine("\nUser with this email already exists.");
                Console.WriteLine("Press 1 to return to main menu.");
                Console.WriteLine("Press 2 to log in.");

                string choice = Console.ReadLine()!;
                while (choice != "1" && choice != "2")
                {
                    Console.WriteLine("\nInvalid choice.");
                    Console.WriteLine("Press 1 to return to main menu.");
                    Console.WriteLine("Press 2 to log in.");
                    choice = Console.ReadLine()!;
                }

                if (choice == "1")
                {
                    return SessionState.Default;
                }
                else if (choice == "2")
                {
                    return SessionState.ExistingUser;
                }
            }

            if (currentAttempt < maxAttempts && alreadyRegistered == false) // Only allowing inputs if attempt limit isn't reached
            {
                Console.WriteLine("\nPlease create a password:");
                password = Console.ReadLine()!;
            }

            if (password.Equals(escapeWord, StringComparison.OrdinalIgnoreCase))
            {
                return SessionState.Default;
            }

            while (password == "" && currentAttempt < maxAttempts && alreadyRegistered == false) // Enters here if attempts exist & password input was left blank
            {
                Console.WriteLine("Password cannot be blank.");
                currentAttempt++;

                if (currentAttempt < maxAttempts)
                {
                    Console.WriteLine("\nPlease create a password:");
                    password = Console.ReadLine()!;

                    if (password.Equals(escapeWord, StringComparison.OrdinalIgnoreCase))
                    {
                        return SessionState.Default;
                    }
                }
            }

            if (currentAttempt >= maxAttempts && alreadyRegistered == false)  // If the max attempts are reached registration is unsuccessful & session ends
            {
                Console.WriteLine("Registration unsuccessful.");
                return SessionState.Default;
            }
            else if (alreadyRegistered == false)  // Otherwise new user will be created
            {
                BankAccount newUser = new BankAccount(email, password);
                users.Add(newUser);

                Console.WriteLine("\nRegistration successful!");
                Console.WriteLine("\nEmail is: " + email + "\nPassword has been set.");
                Console.WriteLine("\nPlease log in to start banking.");
                return SessionState.Default;
            }

            return SessionState.Default;
        }

         private static bool IsValid(string email) // This class checks if emails passed by users are a valid format 
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