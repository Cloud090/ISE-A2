using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankApp;

namespace BankApp
{
    public class Signup
    {
        static void Register(List<User> users)
        {
            Console.WriteLine("Enter a username:");
            string username = Console.ReadLine();

            Console.WriteLine("Enter a password:");
            string password = Console.ReadLine();

            User newUser = new User { Username = username, Password = password };
            users.Add(newUser);

            Console.WriteLine("Registration successful!");
        }
    }
}