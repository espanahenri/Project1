using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BankingApp.Models
{
    public class CheckingAccount : IAccount
    {
        public int Id { get; set; }
        [Display(Name = "Balance")]
        public decimal Balance { get; set; }
        public virtual ApplicationUser Customer { get; set; }
        public virtual string ApplicationUserId { get; set; }
        public List<Transaction> Transactions { get; set; }
    }
}
