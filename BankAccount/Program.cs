namespace BankAccount
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
                choice = choice?.Trim().ToLower() ?? "";

                switch (choice)
                {
                    case "1":
                    case "l":
                    case "log in":
                    case "login":
                        Login();
                        break;

                    case "2":
                    case "s":
                    case "sign up":
                    case "signup":
                        Signup();
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        continue;
                }

                while (shouldContinueAsking)
                {
                    Console.WriteLine("\nWould you like to continue banking?");
                    Console.WriteLine("\t1. Yes\n\t2. No");
                    choice = Console.ReadLine();
                    choice = choice?.Trim().ToLower() ?? "";

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
                            break;
                    }
                }
                shouldContinueAsking = true;
            }

            Console.WriteLine("\nThank you for banking with ISE Bank.");
            Console.ReadKey();
            return;
        }

        public static void Login()
        {
            Console.WriteLine("\nPlaceholder");
        }

        public static void Signup()
        {
            Console.WriteLine("\nPlaceholder");
        }
    }

    public class BankAccount
    {
        private static int accountNumberSeed = 147258369;
        public string NumberID { get; }
        public string Owner { get; set; }
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

        public BankAccount(string name, decimal initialBalance)
        {
            this.NumberID = accountNumberSeed.ToString();
            accountNumberSeed++;

            this.Owner = name;
            MakeDeposit(initialBalance, DateTime.Now, "Initial balance");
        }

        public void MakeDeposit(decimal amount, DateTime date, string note)
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Deposit amount must be positive");
            }
            var deposit = new Transaction(amount, date, note);
            allTransactions.Add(deposit);
        }

        public void MakeWithdrawal(decimal amount, DateTime date, string note)
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Withdrawal amount must be positive");
            }

            if (Balance - amount < 0)
            {
                throw new InvalidOperationException("Insufficient funds for requested withdrawal");
            }
            var withdrawal = new Transaction(-amount, date, note);
            allTransactions.Add(withdrawal);
        }

        private List<Transaction> allTransactions = new List<Transaction>();

        public string GetAccountHistory()
        {
            var report = new System.Text.StringBuilder();

            decimal balance = 0;
            report.AppendLine("Date\t\tAmount\tBalance\tNote");
            foreach (var item in allTransactions)
            {
                balance += item.Amount;
                report.AppendLine($"{item.Date.ToShortDateString()}\t{item.Amount}\t{balance}\t{item.Note}");
            }
            return report.ToString();
        }
    }

    public class Transaction
    {
        public decimal Amount { get; }
        public DateTime Date { get; }
        public string Note { get; }

        public Transaction(decimal amount, DateTime date, string note)
        {
            this.Amount = amount;
            this.Date = date;
            this.Note = note;
        }
    }
}