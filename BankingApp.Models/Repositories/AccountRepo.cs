using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp.Models.Repositories
{
    public class AccountRepo
    {
        private BankingAppDbContext _context;
        public AccountRepo(BankingAppDbContext ctx)
        {
            _context = ctx;
        }
        public async Task<Account> SelectById(int? id)
        {
            var account = await _context.Accounts.FirstOrDefaultAsync(m => m.Id == id);
            return account;
        }
        public async Task<Account> SelectById(ApplicationUser user, int? id)
        {
            var account = await _context.Accounts.FirstOrDefaultAsync(m => m.ApplicationUserId == user.Id && m.Id == id);
            return account;
        }
        public async Task<List<Account>> SelectAll(ApplicationUser user)
        {
            var accounts = await _context.Accounts.Where(x => x.ApplicationUserId == user.Id).ToListAsync();
            return accounts;
        }
        public async Task<bool> Add(Account account)
        {
            
            _context.Add(account);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Update(Account account)
        {
            _context.Update(account);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Remove(Account account)
        {
            _context.Remove(account);
            await _context.SaveChangesAsync();
            return true;
        }
        public bool AccountExists(int id)
        {
            return _context.Accounts.Any(e => e.Id == id);
        }
    }
}
