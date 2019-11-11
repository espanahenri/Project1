using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp.Models.Repositories
{
    public class LoanRepo
    {
        private BankingAppDbContext _context;
        public LoanRepo(BankingAppDbContext ctx)
        {
            _context = ctx;
        }
        public async Task<Loan> SelectById(int? id)
        {
            var account = await _context.Loans.FirstOrDefaultAsync(m => m.Id == id);
            return account;
        }
        public async Task<Loan> SelectByAccountId(int accountId)
        {
            var account = await _context.Loans.FirstOrDefaultAsync(m => m.AccountId == accountId);
            return account;
        }
        public async Task<List<Loan>> SelectAll(int accountId)
        {
            var accounts = await _context.Loans.Where(x => x.AccountId == accountId).ToListAsync();
            return accounts;
        }
        public async Task<bool> Add(Loan account)
        {
            _context.Add(account);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Update(Loan account)
        {
            _context.Update(account);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Remove(Loan account)
        {
            _context.Remove(account);
            await _context.SaveChangesAsync();
            return true;
        }
        public bool AccountExists(int id)
        {
            return _context.Loans.Any(e => e.Id == id);
        }
    }
}
