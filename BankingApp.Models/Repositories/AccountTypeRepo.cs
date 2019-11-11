using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp.Models.Repositories
{
    
    public class AccountTypeRepo
    {
        private BankingAppDbContext _context;
        public AccountTypeRepo(BankingAppDbContext ctx)
        {
            _context = ctx;
        }
        public async Task<AccountType> SelectById(int? id)
        {
            var accountT = await _context.AccountTypes.FirstOrDefaultAsync(m => m.Id == id);
            return accountT;
        }
    }
}
