using System;
using System.Collections.Generic;
using System.Text;

namespace BankingApp.Models
{
    public class TermDeposit
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public int Term { get; set; }
        public bool isMatured { get; set; }
        public decimal TotalDue { get; set; }
        public virtual Account Account { get; set; }
    }
}
