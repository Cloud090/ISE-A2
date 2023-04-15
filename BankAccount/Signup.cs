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

        public static SessionState Register (List<BankAccount> accountsList)
        {
            int currentAttempt = 0;
            int maxAttempts = 3;
            string password = "";       // Sets a blank password so password is not null

            Console.WriteLine("\nSign up");
            Console.WriteLine("\nPlease enter your email:");
            string email = Console.ReadLine()!;

            while (email == "" && currentAttempt < maxAttempts || IsValid(email) == false && currentAttempt < maxAttempts)
            {
                Console.WriteLine("Invalid email. Please try again.");
                Console.WriteLine("\nPlease enter your email:");
                email = Console.ReadLine()!;
                currentAttempt++;
            }

            if (currentAttempt <= maxAttempts) 
            {
                Console.WriteLine("\nPlease create a password:");
                password = Console.ReadLine()!;
            }

            while (password == "" && currentAttempt <= maxAttempts )
            {
                Console.WriteLine("Password cannot be blank.");
                Console.WriteLine("\nPlease create a password:");
                password = Console.ReadLine()!;
                currentAttempt++;
            }

            if (currentAttempt > maxAttempts) 
            {
                Console.WriteLine("Registration unsucessful.");
                return SessionState.SessionEnded;
            }
            else 
            {
                BankAccount newUser = new BankAccount(email, password);
                accountsList.Add(newUser);

                Console.WriteLine("\nRegistration successful!");
                Console.WriteLine("\nEmail is: " + email + "\nPassword has been set.");
                Console.WriteLine("\nPlease log in to start banking.");
                return SessionState.Unknown;

        
            }
        }

         private static bool IsValid(string email)
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