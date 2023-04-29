
namespace BankApp
{
    public class BankAccount
    {
        private static long accountNumberSeed = 147258369;
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
            NumberID = accountNumberSeed.ToString();
            accountNumberSeed++;

            Owner = name;
            Email = email;
            Password = password;
            MakeDeposit(initialBalance, DateTime.Now, "Initial balance");
        }

        public void MakeDeposit(decimal amount, DateTime date, string note = "Deposit")
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Deposit amount must be positive");
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

        public void MakeWithdrawal(decimal amount, DateTime date, string note = "Withdrawal")
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Withdrawal amount must be positive");
            }

            if (Balance - amount < 0)
            {
                throw new InvalidOperationException("Insufficient funds for requested withdrawal");
            }

            var withdrawal = new Transaction 
            {
                Amount = -amount, 
                Date = date, 
                TargetAccount = "None\t",
                Note = note
            };

            allTransactions.Add(withdrawal);
        }

        public void MakeTransfer (decimal amount, long targetAccount, DateTime date, string note = "Transfer") 
        {
            if (amount <= 0) 
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Transfer amount must be positive");
            }

            if (Balance - amount < 0) 
            {
                throw new InvalidOperationException("Insufficient funds for requested transfer");
            }

            if (NumberID == targetAccount.ToString()) 
            {
                throw new InvalidOperationException("Cannot transfer to own account");
            }

            var transfer = new Transaction
            {
                Amount = -amount,
                Date = date,
                TargetAccount = targetAccount.ToString(),
                Note = note
            };

            allTransactions.Add(transfer);
        }

        private List<Transaction> allTransactions = new List<Transaction>();

        public string GetAccountHistory()
        {
            var report = new System.Text.StringBuilder();

            decimal balance = 0;
            report.AppendLine("\nDate\t\tAmount\tBalance\tTo Account:\tNote");

            foreach (var item in allTransactions)
            {
                balance += item.Amount;
                report.AppendLine($"{item.Date.ToShortDateString()}\t{item.Amount}\t{balance}\t{item.TargetAccount}\t{item.Note}");
            }

            return report.ToString();
        }
    }
}
