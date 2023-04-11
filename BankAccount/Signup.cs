using System.Net.Mail;

namespace BankApp
{
    public class User
    {
        public string Email { get; set; }
        public string Password { get; set; }
    } 
    public class Signup
    {
        
        public static void Register(List<User> users)
        {
            int currentAttempt = 0;
            int maxAttempts = 3;
            Console.WriteLine("Enter your email:");
            string email = Console.ReadLine()!;
            currentAttempt++;
            

            while (email == "" && currentAttempt < maxAttempts || IsValid(email) == false && currentAttempt < maxAttempts){
                Console.WriteLine("Invalid Email. Try again.");
                Console.WriteLine("Enter Email:");
                email = Console.ReadLine()!;
                currentAttempt++;
            }
            string password = "";
            if (currentAttempt <= maxAttempts) {
                Console.WriteLine("Create a password:");
                password = Console.ReadLine()!;
                currentAttempt++;
            }
            
            
            while (password == "" && currentAttempt <= maxAttempts ){
                Console.WriteLine("Invalid Password. Try again.");
                Console.WriteLine("Create a password:");
                password = Console.ReadLine()!;
                currentAttempt++;
            }
            if (currentAttempt > maxAttempts) {
                Console.WriteLine("Registration Unsucessful. Try again later.");
            }
            else {
                User newUser = new User { Email = email, Password = password };
                users.Add(newUser);

                Console.WriteLine("Registration successful!");
                Console.WriteLine("\nEmail is: " + email + "\n Password: Set");
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