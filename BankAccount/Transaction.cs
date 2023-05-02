using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp
{
    public class Transaction
    {
        public decimal Amount { get; init; }
        public DateTime Date { get; init; }
        public string Note { get; init; } = string.Empty;
        public string TargetAccount { get; init; } = string.Empty;
    }
}