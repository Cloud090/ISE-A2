using System.Net.Mail;
using static BankApp.Signup; 
using static BankApp.Login;

namespace BankApp
{
    public class UserArea
    {

        public static SessionState Welcome (BankAccount confirmedUser)
        {
            Console.WriteLine($"Welcome {confirmedUser.Email}!");
            Console.WriteLine($"Account Number: {confirmedUser.NumberID}");
            Console.WriteLine($"Balance: ${confirmedUser.Balance}\n");
            Console.WriteLine("Please choose an option:");
            Console.WriteLine("\t1. Withdrawal");
            Console.WriteLine("\t2. Transfer");
            Console.WriteLine("\t3. Deposit");
            Console.WriteLine("\t4. Quit");
            Console.ReadLine();

            return SessionState.Authenticated;
        }
    }
}