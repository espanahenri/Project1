using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankingApp.Models;
using BankingApp.Models.Repositories;

namespace BankingApp.UI.BL
{
    public class TermBL : IAccountV1BL
    {
        private readonly AccountRepo _accountRepo;
        private readonly TransactionRepo _transactionRepo;
        private readonly TermDepositRepo _termRepo;
        public TermBL(AccountRepo accountRepo, TransactionRepo tranRepo, TermDepositRepo termDepositRepo)
        {
            _termRepo = termDepositRepo;
            _transactionRepo = tranRepo;
            _accountRepo = accountRepo;
        }
        public async Task<bool> withdraw(Account account, decimal amount)
        {
            var td = await _termRepo.SelectByAccountId(account.Id);
            DateTime matureDate = account.DateCreated.AddMonths(td.Term);
            if (matureDate > DateTime.Now)
            {
                return false;
            }
            if (amount != td.TotalDue) 
            {
                return false;
            }
            account.isClosed = true;
            td.isMatured = true;
            td.TotalDue -= amount;
            await _accountRepo.Update(account);
            await _termRepo.Update(td);
            var tran = new Transaction()
            {
                AccountId = account.Id,
                Amount = amount,
                TransactionType = "Withdraw",
                DateStamp = DateTime.Now
            };
            await _transactionRepo.Add(tran);
            return true;
        }
    }
}
