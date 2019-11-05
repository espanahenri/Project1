using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankingApp.UI.ViewModels
{
    public class WithdrawViewModel
    {
        [Required]
        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }
    }
}
