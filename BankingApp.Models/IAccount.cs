using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BankingApp.Models
{
    public interface IAccount
    {
        public int Id { get; set; }
        [Display(Name = "Balance")]
        public decimal Balance { get; set; }
        public ApplicationUser Customer { get; set; }
        public string ApplicationUserId { get; set; }
        public List<Transaction> Transactions { get; set; }
        public float InterestRate { get; set; }
    }
}
