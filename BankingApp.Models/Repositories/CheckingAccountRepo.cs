using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp.Models.Repositories
{
    public class CheckingAccountRepo
    {
        
        private BankingAppDbContext _context;
        public CheckingAccountRepo(BankingAppDbContext ctx)
        {
            _context = ctx;
        }
        public async Task<CheckingAccount> GetAccount(int? id)
        {
            var account = await _context.CheckingAccounts.FirstOrDefaultAsync(m => m.Id == id);
            return account;
        }
        public async Task<CheckingAccount> GetAccount(ApplicationUser user,int? id)
        {
            var account = await _context.CheckingAccounts.FirstOrDefaultAsync(m => m.ApplicationUserId == user.Id && m.Id == id);
            return account;
        }
        public async Task<List<CheckingAccount>> GetAllAccounts(ApplicationUser user)
        {
            var accounts = await _context.CheckingAccounts.Where(x => x.ApplicationUserId == user.Id).ToListAsync();
            return accounts;
        }
        public async Task<bool> Add(CheckingAccount account)
        {
            _context.Add(account);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Update(CheckingAccount account)
        {
            _context.Update(account);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Remove(CheckingAccount account)
        {
            _context.Remove(account);
            await _context.SaveChangesAsync();
            return true;
        }
        public bool AccountExists(int id)
        {
            return _context.CheckingAccounts.Any(e => e.Id == id);
        }
        
    }
}
