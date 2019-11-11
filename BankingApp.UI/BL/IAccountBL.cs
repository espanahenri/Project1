using BankingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingApp.UI.BL
{
    public interface IAccountBL
    {
        Task<bool> deposit(Account account, decimal amount);
        
    }
}
