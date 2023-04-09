namespace BankApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool shouldContinueBanking = true;
            bool shouldContinueAsking = true;
            Console.WriteLine("Welcome to ISE Banking App.");
            while (shouldContinueBanking)
            {
                Console.WriteLine("\nPlease enter the number code of the operation to perform.");
                Console.WriteLine("\t1. Log in");
                Console.WriteLine("\t2. Sign up");
                string? choice = Console.ReadLine();
                choice = choice?.Trim().ToLower() ?? string.Empty;

                switch (choice)
                {
                    case "1":
                    case "l":
                    case "log in":
                    case "login":
                        Console.WriteLine("Log in placeholder");
                        break;

                    case "2":
                    case "s":
                    case "sign up":
                    case "signup":
                        Console.WriteLine("Sign up placeholder");
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        continue;
                }

                do
                {
                    Console.WriteLine("\nWould you like to continue banking?");
                    Console.WriteLine("\t1. Yes\n\t2. No");
                    choice = Console.ReadLine();
                    choice = choice?.Trim().ToLower() ?? string.Empty;

                    switch (choice)
                    {
                        case "1":
                        case "y":
                        case "yes":
                            shouldContinueAsking = false;
                            continue;

                        case "2":
                        case "n":
                        case "no":
                            shouldContinueBanking = false;
                            shouldContinueAsking = false;
                            continue;

                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            shouldContinueAsking = true;
                            break;
                    }
                }
                while (shouldContinueAsking);
            }

            Console.WriteLine("\nThank you for banking with ISE Bank.");
            Console.ReadKey();
            return;
        }
    }
}