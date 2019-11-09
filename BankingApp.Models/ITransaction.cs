using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BankingApp.Models
{
    public interface ITransaction
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        [Display(Name = "Transaction Type")]
        public string TransactionType { get; set; }
        [Display(Name = "Date")]
        public DateTime DateStamp { get; set; }
    }
}
