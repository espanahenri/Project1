using BankingApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankingApp.UI.ViewModels
{
    public class TransferViewModel
    {
        public Account accountFrom { get; set; }
        public int? AccountFromId { get; set; }

        public List<Account> EligibleAccounts { get; set; }
        public Account accountTo { get; set; }
        public int? AccountToId { get; set; }
        [Required]
        [Range(0, Double.MaxValue, ErrorMessage = "Can't enter negative value.")]
        public decimal Amount { get; set; }
    }
}
