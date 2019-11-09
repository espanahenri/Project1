using System;
using System.Collections.Generic;
using System.Text;

namespace BankingApp.Models
{
    public class CheckingTransaction : ITransaction
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public int? CheckingAccountId { get; set; }
        public string TransactionType { get; set; }
        public DateTime DateStamp { get; set; }
    }
}
