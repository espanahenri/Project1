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
        
        public int AccountFrom { get; set; }

        public List<IAccount> EligibleAccounts { get; set; }
        
        public int AccountTo { get; set; }
        [Required]
        [Range(0, Double.MaxValue, ErrorMessage = "Can't enter negative value.")]
        public decimal Amount { get; set; }
    }
}
