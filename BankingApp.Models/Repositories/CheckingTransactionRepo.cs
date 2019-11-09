using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp.Models.Repositories
{
    public class CheckingTransactionRepo
    {

        private BankingAppDbContext _context;
        public CheckingTransactionRepo(BankingAppDbContext ctx)
        {
            _context = ctx;
        }
        public async Task<CheckingTransaction> GetTransaction(int? id)
        {
            var trans = await _context.CheckingAccountTransactions.FirstOrDefaultAsync(m => m.Id == id);
            return trans;
        }
        public async Task<CheckingTransaction> GetTransaction(IAccount account, int? id)
        {
            var trans = await _context.CheckingAccountTransactions.FirstOrDefaultAsync(m => m.CheckingAccountId == account.Id && m.Id == id);
            return trans;
        }
        public async Task<List<CheckingTransaction>> GetAllTransactions(int id)
        {
            var trans = await _context.CheckingAccountTransactions.Where(x => x.CheckingAccountId == id).ToListAsync();
            return trans;
        }
        public async Task<bool> Add(CheckingTransaction trans)
        {
            _context.Add(trans);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Update(CheckingTransaction trans)
        {
            _context.Update(trans);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Remove(CheckingTransaction trans)
        {
            _context.Remove(trans);
            await _context.SaveChangesAsync();
            return true;
        }
        public bool AccountExists(int id)
        {
            return _context.CheckingAccountTransactions.Any(e => e.Id == id);
        }

    }
}
