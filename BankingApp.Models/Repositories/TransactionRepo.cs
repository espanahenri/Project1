using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp.Models.Repositories
{
    public class TransactionRepo
    {

        private BankingAppDbContext _context;
        public TransactionRepo(BankingAppDbContext ctx)
        {
            _context = ctx;
        }
        public async Task<Transaction> GetTransaction(int? id)
        {
            var trans = await _context.Transactions.FirstOrDefaultAsync(m => m.Id == id);
            return trans;
        }
        public async Task<Transaction> GetTransaction(IAccount account, int? id)
        {
            var trans = await _context.Transactions.FirstOrDefaultAsync(m => m.AccountNumber == account.Id && m.Id == id);
            return trans;
        }
        public async Task<List<Transaction>> GetAllTransactions(int id)
        {
            var trans = await _context.Transactions.Where(x => x.AccountNumber == id).ToListAsync();
            return trans;
        }
        public async Task<bool> Add(Transaction trans)
        {
            _context.Add(trans);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Update(Transaction trans)
        {
            _context.Update(trans);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Remove(Transaction trans)
        {
            _context.Remove(trans);
            await _context.SaveChangesAsync();
            return true;
        }
        public bool AccountExists(int id)
        {
            return _context.Transactions.Any(e => e.Id == id);
        }

    }
}
