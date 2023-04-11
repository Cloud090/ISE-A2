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
            int registerAttempts = 0;
            int attempts = 3;
            Console.WriteLine("Enter your email:");
            string email = Console.ReadLine()!;
            registerAttemps++;
            

            while (email == "" && registerAttempts <= attempts || IsValid(email) == false && registerAttempts <= attempts){
                Console.WriteLine("Invalid Email. Try again.");
                Console.WriteLine("Enter Email:");
                email = Console.ReadLine()!;
                registerAttempts++;
            }
            string password = "";
            if (registerAttempts <= attempts) {
                Console.WriteLine("Create a password:");
                password = Console.ReadLine()!;
                registerAttempts++;
            }
            
            
            while (password == "" && registerAttempts <= attempts ){
                Console.WriteLine("Invalid Password. Try again.");
                Console.WriteLine("Create a password:");
                password = Console.ReadLine()!;
                registerAttempts++;
            }
            if (registerAttempts > attempts) {
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