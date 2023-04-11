using System.Net.Mail;
using static BankApp.Signup; 

namespace BankApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool shouldContinueBanking = true;
            //bool shouldContinueAsking = true;
            Console.WriteLine("Welcome to ISE Banking App.");

            List<User> users = new List<User>();

            while (shouldContinueBanking)
            {
                Console.WriteLine("\nPlease enter the number code of the operation to perform.");
                Console.WriteLine("\t1. Log in");
                Console.WriteLine("\t2. Sign up");
                Console.WriteLine("\t3. Exit");
                string choice = Console.ReadLine()!;
                choice = choice?.Trim().ToLower() ?? string.Empty;

                
                

                switch (choice)
                {
                    case "1":
                    case "l":
                    case "log in":
                    case "login":
                        Login(users);
                        break;

                    case "2":
                    case "s":
                    case "sign up":
                    case "signup":
                        Signup.Register(users);
                        break;
                    case "3":
                    case "Exit":
                    case "exit":
                        shouldContinueBanking = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        continue;
                }

                
            }

            Console.WriteLine("\nThank you for banking with ISE Bank.");
            Console.ReadKey();
            return;
        }
        
        
    }
}