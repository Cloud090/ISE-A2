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
            int currentAttempt = 0; // Tracking attempts
            int maxAttempts = 3; // Max attempts to signup if required
            string password = "";       // Sets a blank password so password is not null

            Console.WriteLine("\nSign up");
            Console.WriteLine("\nPlease enter your email:");
            string email = Console.ReadLine()!;

            // If email input is blank, max attempts have been reached &/or email is an invalid format it will enter the loop
            while (email == "" && currentAttempt < maxAttempts || IsValid(email) == false && currentAttempt < maxAttempts)
            {
                Console.WriteLine("Invalid email. Please try again.");
                Console.WriteLine("\nPlease enter your email:");
                email = Console.ReadLine()!;
                currentAttempt++;
            }

            if (currentAttempt <= maxAttempts) // Only allowing inputs if attempt limit isn't reached
            {
                Console.WriteLine("\nPlease create a password:");
                password = Console.ReadLine()!;
            }

            while (password == "" && currentAttempt < maxAttempts ) // Enters here if attempts exist & password input was left blank
            {
                Console.WriteLine("Password cannot be blank.");
                Console.WriteLine("\nPlease create a password:");
                password = Console.ReadLine()!;
                currentAttempt++;
            }

            if (currentAttempt >= maxAttempts)  // If the max attempts are reached registration is unsuccessful & session ends
            {
                Console.WriteLine("Registration unsuccessful.");
                return SessionState.SessionEnded;
            }
            else  // Otherwise new user will be created
            {
                BankAccount newUser = new BankAccount(email, password);
                users.Add(newUser);

                Console.WriteLine("\nRegistration successful!");
                Console.WriteLine("\nEmail is: " + email);
                Console.WriteLine("Password has been set.");
                Console.WriteLine("\nPlease log in to start banking.");
                return SessionState.Unknown;
            }
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