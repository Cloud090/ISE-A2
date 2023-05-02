
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

        public BankAccount(string email, string password, string name = "User", decimal initialBalance = 100)   // Default values to be changed later
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
            if (amount <= 0)
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
            Console.Write("$ ");

            if (!Decimal.TryParse(Console.ReadLine(), out decimal amount)) 
            {
                throw new Exception("Deposit amount must be a decimal number.");
            }

            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Deposit amount must be positive.");
            }

            Console.WriteLine("\nPlease enter transaction description (optional):");
            string? note = Console.ReadLine();

            // Gives transaction description if user gives no description
            if (string.IsNullOrEmpty(note)) { note = "Deposit"; }

            note = note.Trim();

            var deposit = new Transaction
            {
                Amount = amount, 
                Date = DateTime.Now, 
                TargetAccount = NumberID,
                Note = note
            };

            allTransactions.Add(deposit);
            Console.WriteLine($"\nDeposit made: $ {deposit.Amount}");
        }

        public void MakeWithdrawal(decimal amount, DateTime date, string note)
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Withdrawal amount must be positive.");
            }

            if (Balance - amount < 0)
            {
                throw new InvalidOperationException("Insufficient funds for requested withdrawal.");
            }

            var withdrawal = new Transaction 
            {
                Amount = -amount, 
                Date = date, 
                Note = note
            };

            allTransactions.Add(withdrawal);
        }

        private List<Transaction> allTransactions = new List<Transaction>();

        public string GetAccountHistory()
        {
            var report = new System.Text.StringBuilder();

            decimal balance = 0;
            report.AppendLine("\nDate\t\tAmount\tBalance\tNote");

            foreach (var item in allTransactions)
            {
                balance += item.Amount;
                report.AppendLine($"{item.Date.ToShortDateString()}\t{item.Amount}\t{balance}\t{item.Note}");
            }

            return report.ToString();
        }
    }
}
