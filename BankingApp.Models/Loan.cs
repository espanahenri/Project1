using System;
using System.Collections.Generic;
using System.Text;

namespace BankingApp.Models
{
    public class Loan
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public int Term { get; set; }
        public decimal TotalDue { get; set; }
        public virtual Account Account { get; set; }
    }
}
