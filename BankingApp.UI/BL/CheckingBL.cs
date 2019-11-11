using BankingApp.Models;
using BankingApp.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingApp.UI.BL
{
    public class CheckingBL:IAccountBL,IAccountV1BL
    {
        private readonly AccountRepo _accountRepo;
        private readonly TransactionRepo _transactionRepo;
        public CheckingBL(AccountRepo accountRepo,TransactionRepo tranRepo)
        {
            _transactionRepo = tranRepo;
            _accountRepo = accountRepo;
        }
        public async Task<bool> deposit(Account account, decimal amount)
        {
            account.Balance += amount;
            await _accountRepo.Update(account);
            var tran = new Transaction()
            {
                AccountId = account.Id,
                Amount = amount,
                TransactionType = "Deposit",
                DateStamp = DateTime.Now
            };
            await _transactionRepo.Add(tran);
            return true;
        }
        public async Task<bool> withdraw(Account account, decimal amount)
        {
            if (account.Balance < amount)
            {
                return false;
            }
            account.Balance -= amount;
            await _accountRepo.Update(account);
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
