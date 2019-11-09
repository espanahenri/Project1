using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BankingApp.Models
{
    public class BusinessAccount : IAccount
    {
        public int Id { get; set; }
        public decimal Balance { get; set ; }
        public ApplicationUser Customer { get ; set ; }
        public string ApplicationUserId { get; set ; }
        public List<CheckingTransaction> Transactions { get; set; }
        public decimal Overdraft { get; set; }
        [Display(Name = "Interest Rate")]
        [DisplayFormat(DataFormatString = "{0:P2}", ApplyFormatInEditMode = true)]
        public float InterestRate { get; set; }
    }
}
