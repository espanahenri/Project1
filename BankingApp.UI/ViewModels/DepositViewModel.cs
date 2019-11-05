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
        public decimal Amount { get; set; }
    }
}
