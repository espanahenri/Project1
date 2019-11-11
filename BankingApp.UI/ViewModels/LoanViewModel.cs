using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankingApp.UI.ViewModels
{
    public class LoanViewModel
    {
        [Required]
        [Range(1, 60, ErrorMessage = "1-60 months(5 years) most and least your term can be.")]
        [Display(Name = "Term (Months)")]
        public int Term { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        [Range(0, Double.MaxValue, ErrorMessage = "Can't enter negative value.")]
        public decimal Amount { get; set; }
        
    }
}
