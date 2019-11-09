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
        [Range(1, Int16.MaxValue, ErrorMessage = "Can't enter negative value.")]
        public decimal Balance { get; set; }
        public virtual ApplicationUser Customer { get; set; }
        public virtual string ApplicationUserId { get; set; }
        public List<CheckingTransaction> Transactions { get; set; }
        [Display(Name = "Interest Rate")]
        [DisplayFormat(DataFormatString = "{0:P2}", ApplyFormatInEditMode = true)]
        public float InterestRate { get; set; }
    }
}
