﻿using System.Net.Mail;
using static BankApp.Signup; 

namespace BankApp
{
    internal class Program
    {
        // Session independent variables
        private static readonly Login _loginProcessor = new Login();
        private static readonly Signup _signupProcessor = new Signup();

        static void Main(string[] args)
        {

            // Session specific variables
            var currentSessionState = SessionState.Unknown;
            var AccountsList = new List<BankAccount> { new BankAccount("johndoe@email.com", "JDPassword", "John Doe", 985) };
            BankAccount? user = null;

            Console.WriteLine("Welcome to ISE Banking App.");

            while (currentSessionState != SessionState.SessionEnded)
            {
                if (currentSessionState == SessionState.Unknown)
                {
                    HandleLogin(ref currentSessionState, ref user, AccountsList);
                }

                if (currentSessionState == SessionState.Authenticated && user != null)
                {
                    HandleBankServices(ref currentSessionState, user);
                }
            }

            Console.WriteLine("\nThank you for banking with ISE Bank.");
            Console.ReadKey();
            return;
        }


        public static void HandleLogin(ref SessionState currentSessionState, ref BankAccount? user, List<BankAccount> accountsList)
        {
            Console.WriteLine("\nPlease enter the number code of the operation to perform.");
            Console.WriteLine("\t1. Log in");
            Console.WriteLine("\t2. Sign up");
            Console.WriteLine("\t3. Exit");
            var choice = Console.ReadLine();
            choice = choice?.Trim().ToLower() ?? string.Empty;      // Trim and ToLower reduce possible string permutations

            switch (choice)
            {
                // Log in permutations
                case "1":
                case "l":
                case "log in":
                case "login":
                    currentSessionState = _loginProcessor.Authenticate(accountsList, out user);
                    break;

                // Sign up permutations
                case "2":
                case "s":
                case "sign up":
                case "signup":
                    currentSessionState = _signupProcessor.Register(accountsList);
                    break;

                // Exit permutations
                case "3":
                case "e":
                case "x":
                case "exit":
                    currentSessionState = SessionState.SessionEnded;
                    break;

                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }

        private static void HandleBankServices(ref SessionState currentSessionState, BankAccount user)
        {
            Console.WriteLine($"\nCurrent Balance: \t${user.Balance}.");
            Console.WriteLine("\nPlease enter the number code of the operation to perform.");
            Console.WriteLine("\t1. Deposit");
            Console.WriteLine("\t2. Withdraw");
            Console.WriteLine("\t3. Transaction History");
            Console.WriteLine("\t4. Log out");
            Console.WriteLine("\t5. Exit");
            var choice = Console.ReadLine();
            choice = choice?.Trim().ToLower() ?? string.Empty;      // Trim and ToLower reduce possible string permutations

            switch (choice)
            {
                // Deposit permutations
                case "1":
                case "d":
                case "deposit":
                    Console.WriteLine("\nYou have made a deposit!");
                    break;

                // Withdrawal permutations
                case "2":
                case "w":
                case "withdraw":
                case "withdrawal":
                    Console.WriteLine("\nYou have made a withdrawal!");
                    break;

                // Transaction History permutations
                case "3":
                case "t":
                case "h":
                case "transaction":
                case "history":
                case "transaction history":
                case "transactionhistory":
                    Console.WriteLine(user.GetAccountHistory());
                    break;

                // Log out permutations
                case "4":
                case "l":
                case "log out":
                case "logout":
                    Console.WriteLine("\nYou've logged out.");
                    currentSessionState = SessionState.Unknown;
                    break;

                // Exit permutations
                case "5":
                case "e":
                case "x":
                case "exit":
                    currentSessionState = SessionState.SessionEnded;
                    break;

                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }

    }
}
