using BankingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingApp.UI.BL
{
    public interface IAccountV1BL
    {
        Task<bool> withdraw(Account account, decimal amount);
    }
}
