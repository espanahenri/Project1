using System;
using System.Collections.Generic;
using System.Text;

namespace BankingApp.Models
{
    public class BusinessTransaction : ITransaction
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string TransactionType { get; set; }
        public DateTime DateStamp { get; set; }
        public int BusinessAccountId { get; set; }
    }
}
