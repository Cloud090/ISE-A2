﻿namespace BankApp
{
    public class BankAccount
    {
        public string NumberID { get; }
        public string Owner { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public decimal Balance
        {
            get
            {
                decimal balance = 0;

                foreach (var item in allTransactions)
                {
                    balance += item.Amount;
                }

                return balance;
            }
        }

        public BankAccount(string email, string password, string name, decimal initialBalance)   // Default values to be changed later
        {
            var random = new Random();
            NumberID = random.Next(100000, 999999).ToString(); // Generates a random 6-digit number between 100000 and 999999 after new account creation- Sets it as the account number

            Owner = name;
            Email = email;
            Password = password;
            MakeDeposit(initialBalance, DateTime.Now, "Initial balance");
        }

        public void MakeDeposit(decimal amount, DateTime date, string note)
        { // Inputs passed in during a deposit
            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Deposit amount must be positive."); // Throws error if invalid input
            }

            var deposit = new Transaction
            { // Creates transaction
                Amount = amount,
                Date = date,
                TargetAccount = NumberID,
                Note = note
            };

            allTransactions.Add(deposit);
        }
        public void MakeDeposit()
        { // Entered to create deposit
            Console.WriteLine("\nPlease enter the amount to deposit:"); // Taking user inputs
            Console.WriteLine("\t(Note: Deposit must be a positive number)");
            Console.Write("$");
            decimal amount = 0; // Initialises variable outside of while loop
            string input = Console.ReadLine() ?? string.Empty;
            input = input.Trim(); // Trims whitespace from beginning and end of string

            // Loops until positive number is given or escape word is used
            while (!InputHelper.IsEscapeWord(input) && (!Decimal.TryParse(input, out amount) || amount <= 0))
            {
                Console.WriteLine("Deposit amount must be a positive decimal number.");
                Console.Write("$");
                input = Console.ReadLine() ?? string.Empty;
                input = input.Trim(); // Trims whitespace from beginning and end of string
            }

            if (InputHelper.IsEscapeWord(input)) { return; } // Return to account menu if 'exit' is entered

            Console.WriteLine("\nPlease enter transaction description (optional):");
            string note = Console.ReadLine() ?? string.Empty;
            note = note.Trim(); // Trims whitespace from beginning and end of string

            // Gives transaction description if user gives no description
            if (string.IsNullOrEmpty(note)) { note = "Deposit"; }

            if (InputHelper.IsEscapeWord(note)) { return; } // Return to account menu if 'exit' is entered

            var deposit = new Transaction
            {
                Amount = amount,
                Date = DateTime.Now,
                TargetAccount = NumberID,
                Note = note
            };

            allTransactions.Add(deposit); // Adds transaction to list
            Console.WriteLine($"\nDeposit made: ${deposit.Amount}");
        }

        public void WithdrawAccountBalance() 
        { // Takes action on withdrawals
            var withdrawal = new Transaction 
            {
                Amount = -Balance,
                Date = DateTime.Now,
                TargetAccount = "None",
                Note = "Closing account"
            };

            allTransactions.Add(withdrawal);
            Console.WriteLine($"Balance of ${-withdrawal.Amount} withdrawn for account closure");
        }

        public void MakeWithdrawal()
        { // Entered when user wishes to withdraw funds
            Console.WriteLine("\nPlease enter the amount to withdraw:"); // User inputs
            Console.WriteLine("\t(Note: Withdrawal must be a positive number)");
            Console.Write("$");
            decimal amount = 0; // Initialises variable outside of while loop
            string input = Console.ReadLine() ?? string.Empty;
            input = input.Trim(); // Trims whitespace from beginning and end of string

            // Loops until positive number is given or escape word is used
            while (!InputHelper.IsEscapeWord(input) && (!Decimal.TryParse(input, out amount) || amount <= 0))
            {
                Console.WriteLine("Withdrawal amount must be a positive decimal number.");
                Console.Write("$");
                input = Console.ReadLine() ?? string.Empty;
                input = input.Trim(); // Trims whitespace from beginning and end of string
            }

            if (InputHelper.IsEscapeWord(input)) { return; } // Return to account menu if 'exit' is entered

            if (Balance - amount < 0) // Disallows overdrawn balance
            {
                Console.WriteLine("Insufficient funds for requested withdrawal.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("\nPlease enter transaction description (optional):");
            string note = Console.ReadLine() ?? string.Empty;
            note = note.Trim(); // Trims whitespace from beginning and end of string

            // Gives transaction description if user gives no description
            if (string.IsNullOrEmpty(note)) { note = "Withdrawal"; }

            if (InputHelper.IsEscapeWord(note)) { return; } // Return to account menu if 'exit' is entered

            var withdrawal = new Transaction
            {
                Amount = -amount,
                Date = DateTime.Now,
                TargetAccount = "None\t",
                Note = note
            };

            allTransactions.Add(withdrawal); // Adds transaction to list
            Console.WriteLine($"\nWithdrawal made: ${-withdrawal.Amount}");
        }

        public void MakeTransfer()
        { // Allows user to send money to external accounts
            Console.WriteLine("\nPlease enter the amount to be transferred:");
            Console.WriteLine("\t(Note: Transfer must be a positive number)");
            Console.Write("$");
            decimal amount = 0; // Initialises variable outside of while loop
            string input = Console.ReadLine() ?? string.Empty;
            input = input.Trim(); // Trims whitespace from beginning and end of string

            // Loops until positive number is given or escape word is used
            while (!InputHelper.IsEscapeWord(input) && (!Decimal.TryParse(input, out amount) || amount <= 0))
            {
                Console.WriteLine("Transfer amount must be a positive decimal number.");
                Console.Write("$");
                input = Console.ReadLine() ?? string.Empty;
                input = input.Trim(); // Trims whitespace from beginning and end of string
            }

            if (InputHelper.IsEscapeWord(input)) { return; } // Return to account menu if 'exit' is entered

            if (Balance - amount < 0) // Disallows overdrawn balance
            {
                Console.WriteLine("Insufficient funds for requested transfer.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("\nPlease enter the account number to receive the funds:");
            string targetAccount = Console.ReadLine() ?? string.Empty;
            targetAccount = targetAccount.Trim(); // Trims whitespace from beginning and end of string

            // Loops until acceptable account number is entered or escape word is used
            while (!InputHelper.IsEscapeWord(targetAccount) && (!long.TryParse(targetAccount, out _) || targetAccount.Equals(NumberID) || string.IsNullOrWhiteSpace(targetAccount)))
            {
                if (targetAccount.Equals(NumberID)) { Console.WriteLine("Cannot transfer to yourself!"); }
                else { Console.WriteLine("Target account must be an integer."); }

                Console.WriteLine("\nPlease enter the account number to receive the funds:");
                targetAccount = Console.ReadLine() ?? string.Empty;
                targetAccount = targetAccount.Trim(); // Trims whitespace from beginning and end of string
            }

            if (InputHelper.IsEscapeWord(targetAccount)) { return; } // Return to account menu if 'exit' is entered

            Console.WriteLine("\nPlease enter transaction description (optional):");
            string note = Console.ReadLine() ?? string.Empty;
            note = note.Trim(); // Trims whitespace from beginning and end of string

            // Gives transaction description if user gives no description
            if (string.IsNullOrEmpty(note)) { note = "Transfer"; }

            if (InputHelper.IsEscapeWord(note)) { return; } // Return to account menu if 'exit' is entered

            var transfer = new Transaction
            {
                Amount = -amount,
                Date = DateTime.Now,
                TargetAccount = targetAccount,
                Note = note
            };

            allTransactions.Add(transfer); // Adds transaction to list
            Console.WriteLine($"\nTransfer made: ${-transfer.Amount}\nTo account no.: {transfer.TargetAccount}");
        }
        public BankAccount? Settings(ref SessionState currentSessionState, List<BankAccount> accounts, BankAccount user)
        { // User Settings area - To allow basic functions & controls of accounts
            while (true)
            {
                Console.WriteLine("\nISE Bank App User Settings"); // Gives options
                Console.WriteLine("\t1. Change Password");
                Console.WriteLine("\t2. Update name");
                Console.WriteLine("\t3. Delete account");
                Console.WriteLine("\t4. Exit user settings");
                Console.Write("Option: ");
                string settingChoice = Console.ReadLine()!;

                switch (settingChoice)
                { 
                    // Change password permutations
                    case "1":
                    case "c":
                    case "p":
                    case "cp":
                    case "change password":
                    case "change":
                    case "password":
                        NewPassword(user); // Allows user to change their password
                        continue;

                    // Update name
                    case "2":
                    case "u":
                    case "n":
                    case "name":
                    case "update name":
                    case "updatename":
                        NewName(user); // Allows user to change their name
                        continue;

                    // Delete account
                    case "3":
                    case "d":
                    case "del":
                    case "delete":
                    case "delete account":
                    case "deleteaccount":
                        Console.WriteLine("3. Delete Account"); // Allows account deletion - All data will be deleted & funds forced to be withdrawn once deleted
                        Console.WriteLine("Enter Password:");
                        string password = Console.ReadLine()!;
                        Console.WriteLine("\nAre you sure you want to delete your account? This cannot be undone.");
                        Console.WriteLine("\t1. Yes\n\t2. No");
                        string input = Console.ReadLine() ?? string.Empty;
                        input = input.Trim().ToLower();
                        if ((input != "yes" && input != "y" && input != "1") || password != user.Password) 
                        {
                            continue;
                        }
                        user.WithdrawAccountBalance();
                        try 
                        { // Attempting to delete account
                            var accountToDelete = accounts.Single(u => u.Email == user.Email); // Verifying account to delete
                            accounts.Remove(accountToDelete);
                        }
                        catch 
                        {
                            Console.WriteLine("Error: Account does not exist. Ending session."); // Should never be hit, ends session if it is
                            currentSessionState = SessionState.SessionEnded;
                            return null;
                        }
                        Console.WriteLine("\nAccount deleted."); // Account deleted & returns to banks main menu
                        currentSessionState = SessionState.Default;
                        Console.ReadKey();
                        return null;

                    // Exit settings
                    case "4":
                    case "e":
                    case "s":
                    case "x":
                    case "exit":
                    case "back":
                    case "exit settings":
                    case "exitsettings":
                        return user; // exits to account area
                }
            }
        }

        private void NewPassword(BankAccount user) // Method to change owner password
        {
            Console.WriteLine($"\nAccount email: {user.Email}");
            Console.WriteLine("Enter current password: ");
            string password = Console.ReadLine()!; // Verifies current password
            int maxAttempts = 3;
            int currentAttempt = 0;

            while ((string.IsNullOrWhiteSpace(password) || password != user.Password && !InputHelper.IsEscapeWord(password)) && currentAttempt < maxAttempts)
            {
                currentAttempt++;
                Console.WriteLine("\nPassword is blank or incorrect. Try Again.");
                Console.WriteLine("\nEnter current password: ");
                password = Console.ReadLine()!;
            }

            if (currentAttempt > maxAttempts || InputHelper.IsEscapeWord(password) && password != user.Password)
            {
                Console.WriteLine("\nInvalid password or attempt limit reached or process exited. \nPress enter to return to the settings menu.");
                Console.ReadLine();
                return;
            }

            if (password == user.Password)
            {
                Console.WriteLine("\nEnter new password:"); // Takes new password twice
                string newPassword = Console.ReadLine()!;
                Console.WriteLine("\nPlease confirm your new password:");
                string confirmNewPassword = Console.ReadLine()!;

                if (newPassword != confirmNewPassword || string.IsNullOrWhiteSpace(newPassword) || string.IsNullOrWhiteSpace(confirmNewPassword))
                { // Checking that inputs match & are valid
                    Console.WriteLine("\nThe new passwords you entered do not match or were left blank. \nPress enter to return to the settings menu.");
                    Console.ReadLine();
                    return;
                }
                if (confirmNewPassword == user.Password)
                { // checking that the new password is not the current one
                    Console.WriteLine("\nNew password must be different to current password. \nPress enter to return to the settings menu.");
                    Console.ReadLine();
                    return;
                }

                Password = newPassword; // Setting password as the new password
                Console.WriteLine("\nYour password has been updated successfully. \nPress enter to return to the settings menu.");
                Console.ReadLine();
            }
        }

        private void NewName(BankAccount user) // Method to change owner name
        {
            Console.WriteLine($"\nAccount email: {user.Email}");
            Console.WriteLine("Enter updated name, or type 'Exit' to return to previous menu.");
            string name = Console.ReadLine() ?? string.Empty;
            name = name.Trim();

            if (InputHelper.IsEscapeWord(name)) { return; }

            Owner = name; // Setting new name
            Console.WriteLine($"Name updated to: {user.Owner}");
            Console.ReadKey();
        }

        private List<Transaction> allTransactions = new List<Transaction>();

        public string GetAccountHistory()
        {
            var report = new System.Text.StringBuilder();

            decimal balance = 0;
            report.AppendLine("\nDate\t\tAmount\tBalance\t\tTo Account:\tNote");

            foreach (var item in allTransactions)
            {
                balance += item.Amount;
                report.AppendLine($"{item.Date.ToShortDateString()}\t{item.Amount}\t{balance}\t\t{item.TargetAccount}\t\t{item.Note}");
            }

            return report.ToString();
        }
    }
}
