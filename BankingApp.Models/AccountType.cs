using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BankingApp.Models
{
    public class AccountType
    {
        public int Id { get; set; }
        public string Type { get; set; }
        [DisplayFormat(DataFormatString = "{0:P2}", ApplyFormatInEditMode = true)]
        public decimal InterestRate { get; set; }
        public List<Account> Accounts { get; set; }
    }
}
