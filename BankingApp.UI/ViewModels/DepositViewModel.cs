using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankingApp.UI.ViewModels
{
    public class DepositViewModel
    {
        [Required]
        [DataType(DataType.Currency)]
        [Range(0,Double.MaxValue, ErrorMessage = "Can't enter negative value.")]
        public decimal Amount { get; set; }
        public int AccountId { get; set; }
    }
}
