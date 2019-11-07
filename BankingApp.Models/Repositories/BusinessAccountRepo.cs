using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp.Models.Repositories
{
    public class BusinessAccountRepo
    {

        private BankingAppDbContext _context;
        public BusinessAccountRepo(BankingAppDbContext ctx)
        {
            _context = ctx;
        }
        public async Task<BusinessAccount> GetAccount(int? id)
        {
            var account = await _context.BusinessAccounts.FirstOrDefaultAsync(m => m.Id == id);
            return account;
        }
        public async Task<BusinessAccount> GetAccount(ApplicationUser user, int? id)
        {
            var account = await _context.BusinessAccounts.FirstOrDefaultAsync(m => m.ApplicationUserId == user.Id && m.Id == id);
            return account;
        }
        public async Task<List<BusinessAccount>> GetAllAccounts(ApplicationUser user)
        {
            var accounts = await _context.BusinessAccounts.Where(x => x.ApplicationUserId == user.Id).ToListAsync();
            return accounts;
        }
        public async Task<bool> Add(BusinessAccount account)
        {
            _context.Add(account);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Update(BusinessAccount account)
        {
            _context.Update(account);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Remove(BusinessAccount account)
        {
            _context.Remove(account);
            await _context.SaveChangesAsync();
            return true;
        }
        public bool AccountExists(int id)
        {
            return _context.BusinessAccounts.Any(e => e.Id == id);
        }

    }
}
