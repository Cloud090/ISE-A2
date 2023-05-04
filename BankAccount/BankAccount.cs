
namespace BankApp
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
            NumberID = random.Next(100000, 999999).ToString(); // Generates a random 6-digit number between 100000 and 999999

            Owner = name;
            Email = email;
            Password = password;
            MakeDeposit(initialBalance, DateTime.Now, "Initial balance");
        }

        public void MakeDeposit(decimal amount, DateTime date, string note)
        {
            if (amount <= -1)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Deposit amount must be positive.");
            }

            var deposit = new Transaction
            {
                Amount = amount,
                Date = date,
                TargetAccount = NumberID,
                Note = note
            };

            allTransactions.Add(deposit);
        }
        public void MakeDeposit()
        {
            Console.WriteLine("\nPlease enter the amount to deposit:");
            Console.WriteLine("\t(Note: Deposit must be a positive number)");
            Console.Write("$");
            decimal amount = 0; // Initialises variable outside of while loop
            string input = Console.ReadLine() ?? string.Empty;
            input = input.Trim(); // Trims whitespace from beginning and end of string

            // Loops until positive number is given or escape word is used
            while (!InputHelper.IsEscapeWord(input) && (!Decimal.TryParse(input, out amount) || amount <=0)) 
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

        public void MakeWithdrawal()
        {
            Console.WriteLine("\nPlease enter the amount to withdraw:");
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
        {
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
                return;
            }

            Console.WriteLine("\nPlease enter the account number to receive the funds:");
            string targetAccount = Console.ReadLine() ?? string.Empty;
            targetAccount = targetAccount.Trim(); // Trims whitespace from beginning and end of string

            // Loops until acceptable account number is entered or escape word is used
            while (!InputHelper.IsEscapeWord(targetAccount) && (!long.TryParse(targetAccount, out long result) || targetAccount.Equals(NumberID) || string.IsNullOrWhiteSpace(targetAccount))) 
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
        public void Settings(ref SessionState currentSessionState, BankAccount user)
        {
            Console.WriteLine("\nISE Bank App User Settings");
            Console.WriteLine("\t1. Change Password \n\t2. Exit user settings");
            Console.Write("Option: ");
            string settingChoice = Console.ReadLine()!;

            switch (settingChoice) {
                case "1":
                case "change password":
                case "password":
                case "change":
                    NewPassword(ref currentSessionState, user);
                    break;
                case "2":
                case "exit settings":
                case "exit":
                case "back":
                    return;
                    
            }
        }

        public void NewPassword(ref SessionState currentSessionState, BankAccount user)
        {
            string email = user.Email;
            Console.WriteLine(user.Email);
            Console.WriteLine("\nEnter current password: ");
            string password = Console.ReadLine()!;
            int maxAttempts = 3;
            int currentAttempt = 0;

            while (string.IsNullOrWhiteSpace(password) && currentAttempt < maxAttempts || password != user.Password && currentAttempt < maxAttempts) {
                currentAttempt++;
                Console.WriteLine("\nPassword is blank or incorrect. Try Again.");
                Console.WriteLine("\nEnter current password: ");
                password = Console.ReadLine()!;
            }

            if (currentAttempt >= maxAttempts) {
                Console.WriteLine("\nInvalid password. Press enter to return to the main menu.");
                Console.ReadLine();
                return; 
            }

            if (password == user.Password) {
                Console.WriteLine("\nEnter new password:");
                string newPassword = Console.ReadLine()!;
                Console.WriteLine("\nPlease confirm your new password:");
                string confirmNewPassword = Console.ReadLine()!;

                if (newPassword != confirmNewPassword || string.IsNullOrWhiteSpace(newPassword) || string.IsNullOrWhiteSpace(confirmNewPassword))
                {
                    Console.WriteLine("\nThe new passwords you entered do not match or were left blank. \nPress enter to return to the main menu.");
                    Console.ReadLine();
                    return;
                }
                if (confirmNewPassword == user.Password) {
                    Console.WriteLine("\nNew password must be different to current password. \nPress enter to return to the main menu.");
                    Console.ReadLine();
                    return;
                }

                Password = newPassword;
                Console.WriteLine("\nYour password has been updated successfully. \nPress enter to return to the main menu.");
                Console.ReadLine();
            }
            
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
                report.AppendLine($"{item.Date.ToShortDateString()}\t{item.Amount}\t{balance}\t\t{item.TargetAccount}\t{item.Note}");
            }

            return report.ToString();
        }
    }
}
