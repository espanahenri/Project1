using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BankingApp.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }
        public int AccountTypeId { get; set; }
        [Display(Name = "Balance")]
        public decimal Balance { get; set; }
        public DateTime DateCreated { get; set; }
        public bool isClosed { get; set; }
        public virtual AccountType AccountType { get; set; }
        public virtual ApplicationUser Customer { get; set; }
        public List<Loan> Loans { get; set; }
        public List<TermDeposit> TermDeposits { get; set; }
        public List<Transaction> Transactions { get; set; }
    }
}
