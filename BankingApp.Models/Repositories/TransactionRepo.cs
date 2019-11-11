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
        public async Task<Transaction> SelectById(int? id)
        {
            var tran = await _context.Transactions.FirstOrDefaultAsync(m => m.Id == id);
            return tran;
        }
        public async Task<Transaction> SelectById(int accountId, int? id)
        {
            var tran = await _context.Transactions.FirstOrDefaultAsync(m => m.AccountId == accountId && m.Id == id);
            return tran;
        }
        public async Task<List<Transaction>> SelectAll(int accountId)
        {
            var trans = await _context.Transactions.Where(x => x.AccountId == accountId).ToListAsync();
            return trans;
        }
        public async Task<bool> Add(Transaction tran)
        {
            _context.Add(tran);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Update(Transaction tran)
        {
            _context.Update(tran);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Remove(Transaction tran)
        {
            _context.Remove(tran);
            await _context.SaveChangesAsync();
            return true;
        }
        public bool AccountExists(int id)
        {
            return _context.Transactions.Any(e => e.Id == id);
        }
    }
}
