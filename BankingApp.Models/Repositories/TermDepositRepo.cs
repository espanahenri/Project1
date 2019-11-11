using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp.Models.Repositories
{
    public class TermDepositRepo
    {
        private BankingAppDbContext _context;
        public TermDepositRepo(BankingAppDbContext ctx)
        {
            _context = ctx;
        }
        public async Task<TermDeposit> SelectById(int? id)
        {
            var account = await _context.TermDeposits.FirstOrDefaultAsync(m => m.Id == id);
            return account;
        }
        public async Task<TermDeposit> SelectByAccountId(int? accountid)
        {
            var account = await _context.TermDeposits.FirstOrDefaultAsync(m => m.AccountId == accountid);
            return account;
        }
        public async Task<TermDeposit> SelectById(int accountId, int? id)
        {
            var account = await _context.TermDeposits.FirstOrDefaultAsync(m => m.AccountId == accountId && m.Id == id);
            return account;
        }
        public async Task<List<TermDeposit>> SelectAll(int accountId)
        {
            var accounts = await _context.TermDeposits.Where(x => x.AccountId == accountId).ToListAsync();
            return accounts;
        }
        public async Task<bool> Add(TermDeposit account)
        {
            _context.Add(account);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Update(TermDeposit account)
        {
            _context.Update(account);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Remove(TermDeposit account)
        {
            _context.Remove(account);
            await _context.SaveChangesAsync();
            return true;
        }
        public bool AccountExists(int id)
        {
            return _context.TermDeposits.Any(e => e.Id == id);
        }
    }
}
