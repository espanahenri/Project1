using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BankingApp.Models
{
    public class Loan : IAccount
    {
        public int Id { get; set; }
        [Display(Name = "Loan Amount")]
        [Required]
        [Range(1, Int16.MaxValue, ErrorMessage = "Can't enter negative value.")]
        public decimal Balance { get; set; }
        public ApplicationUser Customer { get; set; }
        public virtual string ApplicationUserId { get; set; }
        public List<CheckingTransaction> Transactions { get; set; }
        [Display(Name = "Interest Rate")]
        [DisplayFormat(DataFormatString = "{0:P2}", ApplyFormatInEditMode = true)]
        public float InterestRate { get; set; }
        [Required]
        [Range(1, Int16.MaxValue, ErrorMessage = "Can't enter negative value.")]
        public int Term { get; set; }
        [Display(Name = "Total Amount Due")]
        
        public decimal TotalBalance { get; set; }
        [Display(Name ="Due per month")]
        public decimal PaymentInstallment { get; set; }
        
        public bool isPayed { get; set; }

    }
}
