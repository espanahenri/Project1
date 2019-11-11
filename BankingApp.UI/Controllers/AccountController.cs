using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BankingApp.Models;
using BankingApp.Models.Repositories;
using Microsoft.AspNetCore.Identity;
using BankingApp.UI.ViewModels;
using BankingApp.UI.BL;

namespace BankingApp.UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly AccountRepo _accountRepo;
        private readonly AccountTypeRepo _typeRepo;
        private readonly LoanRepo _loanRepo;
        private readonly TermDepositRepo _tdRepo;
        private readonly TransactionRepo _transactionRepo;
        private readonly UserManager<ApplicationUser> _userManager;
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
        public AccountController(AccountRepo rep, UserManager<ApplicationUser> userManager,
                                AccountTypeRepo typeRepo, LoanRepo loanRepo, TermDepositRepo tdRepo, TransactionRepo transactionRepo)
        {
            _transactionRepo = transactionRepo;
            _tdRepo = tdRepo;
            _loanRepo = loanRepo;
            _accountRepo = rep;
            _typeRepo = typeRepo;
            _userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var accounts = await _accountRepo.SelectAll(await GetCurrentUserAsync());
            foreach (var at in accounts)
            {
                at.AccountType = await _typeRepo.SelectById(at.AccountTypeId);
                at.Loans = await _loanRepo.SelectAll(at.Id);
                at.TermDeposits = await _tdRepo.SelectAll(at.Id);
            }
            return View(accounts);
        }
        [HttpGet]
        public async Task<IActionResult> Transfer(int id)
        {
            var model = new TransferViewModel()
            {
                AccountFromId = id
            };
            model.EligibleAccounts = await _accountRepo.SelectAll(await GetCurrentUserAsync());
            model.EligibleAccounts = model.EligibleAccounts.Where(x => x.Id != id).ToList();
            model.EligibleAccounts = model.EligibleAccounts.Where(x => x.AccountTypeId == 1 || x.AccountTypeId == 2).ToList();
            model.EligibleAccounts = model.EligibleAccounts.Where(x => x.isClosed == false).ToList();
            model.accountFrom = await _accountRepo.SelectById(id);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Transfer(TransferViewModel model, int id)
        {
            model.EligibleAccounts = await _accountRepo.SelectAll(await GetCurrentUserAsync());
            model.EligibleAccounts = model.EligibleAccounts.Where(x => x.Id != id).ToList();
            model.EligibleAccounts = model.EligibleAccounts.Where(x => x.AccountTypeId == 1 || x.AccountTypeId == 2).ToList();
            model.EligibleAccounts = model.EligibleAccounts.Where(x => x.isClosed == false).ToList();
            model.accountFrom = await _accountRepo.SelectById(id);
            if (model.AccountToId == null)
            {
                return RedirectToAction(nameof(Index));
            }
            model.AccountFromId = id;
            if (ModelState.IsValid)
            {
                var accountfrom = await _accountRepo.SelectById(id);
                var accountto = await _accountRepo.SelectById(model.AccountToId);
                if (accountfrom.AccountTypeId == 1)
                {
                    var checkingBl = new CheckingBL(_accountRepo, _transactionRepo);
                    if (await checkingBl.withdraw(accountfrom, model.Amount) == false)
                    {

                        ModelState.AddModelError(string.Empty, "Insuffient Funds to complete transaction.");
                        return View(model);
                    }
                }
                if (accountfrom.AccountTypeId == 2)
                {
                    accountfrom.AccountType = await _typeRepo.SelectById(2);
                    var businessBl = new BusinessBL(_accountRepo, _transactionRepo);
                    await businessBl.withdraw(accountfrom, model.Amount);
                }
                if (accountto.AccountTypeId == 1)
                {
                    var checkingBl = new CheckingBL(_accountRepo, _transactionRepo);
                    await checkingBl.deposit(accountto, model.Amount);
                }
                if (accountto.AccountTypeId == 2)
                {
                    var businessBl = new BusinessBL(_accountRepo, _transactionRepo);
                    await businessBl.deposit(accountto, model.Amount);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Withdraw(int id)
        {
            var depositmodel = new DepositViewModel()
            {
                AccountId = id
            };
            depositmodel.td = await _tdRepo.SelectByAccountId(id);
            depositmodel.account = await _accountRepo.SelectById(id);
            return View(depositmodel);
        }
        [HttpPost]
        public async Task<IActionResult> Withdraw(DepositViewModel model, int id)
        {
            model.td = await _tdRepo.SelectByAccountId(id);
            model.account = await _accountRepo.SelectById(id);
            if (ModelState.IsValid)
            {
                var account = await _accountRepo.SelectById(id);
                if (account.AccountTypeId == 1)
                {
                    var checkingBl = new CheckingBL(_accountRepo, _transactionRepo);
                    if (await checkingBl.withdraw(account, model.Amount))
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    ModelState.AddModelError(string.Empty, "Insuffient Funds to complete transaction.");
                    return View(model);
                }
                if (account.AccountTypeId == 2)
                {
                    account.AccountType = await _typeRepo.SelectById(2);
                    var businessBl = new BusinessBL(_accountRepo, _transactionRepo);
                    if (await businessBl.withdraw(account, model.Amount))
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    ModelState.AddModelError(string.Empty, "Insuffient Funds to complete transaction.");
                    return View(model);
                }
                if (account.AccountTypeId == 4)
                {
                    account.AccountType = await _typeRepo.SelectById(4);
                    var termBl = new TermBL(_accountRepo, _transactionRepo,_tdRepo);
                    if (await termBl.withdraw(account, model.Amount))
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    ModelState.AddModelError(string.Empty, "Transaction cancelled you have to withdraw the full amount or you have not reached maturity.");
                    return View(model);
                }
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Deposit(int id)
        {
            var depositmodel = new DepositViewModel()
            {
                AccountId = id
            };
            return View(depositmodel);
        }
        [HttpPost]
        public async Task<IActionResult> Deposit(DepositViewModel model,int id)
        {
            if (ModelState.IsValid)
            {
                var account = await _accountRepo.SelectById(id);
                if (account.AccountTypeId == 1)
                {
                    var checkingBl = new CheckingBL(_accountRepo,_transactionRepo);
                    if (await checkingBl.deposit(account, model.Amount))
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    ModelState.AddModelError(string.Empty, "Insuffient Funds to complete transaction.");
                    return View(model);
                }
                if (account.AccountTypeId == 2)
                {
                    var businessBl = new BusinessBL(_accountRepo, _transactionRepo);
                    if (await businessBl.deposit(account, model.Amount))
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    ModelState.AddModelError(string.Empty, "Insuffient Funds to complete transaction.");
                    return View(model);
                }
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var account = await _accountRepo.SelectById(id);
            return View(account);
        }
        [HttpPost]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var account = await _accountRepo.SelectById(id);
            if (account.Balance != 0)
            {
                return View("Delete", account);
            }
            account.isClosed = true;
            await _accountRepo.Update(account);
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> PayLoan(int id)
        {
            var model = new TransferViewModel()
            {
                AccountToId = id
            };
            model.EligibleAccounts = await _accountRepo.SelectAll(await GetCurrentUserAsync());
            model.EligibleAccounts = model.EligibleAccounts.Where(x => x.Id != id).ToList();
            model.EligibleAccounts = model.EligibleAccounts.Where(x => x.AccountTypeId == 1 || x.AccountTypeId == 2).ToList();
            model.EligibleAccounts = model.EligibleAccounts.Where(x => x.isClosed == false && x.Balance > 0).ToList();
            model.accountTo = await _accountRepo.SelectById(id);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> PayLoan(TransferViewModel model, int id)
        {
            model.EligibleAccounts = await _accountRepo.SelectAll(await GetCurrentUserAsync());
            //model.EligibleAccounts = model.EligibleAccounts.Where(x => x.Id != id).ToList();
            model.EligibleAccounts = model.EligibleAccounts.Where(x => x.AccountTypeId == 1 || x.AccountTypeId == 2).ToList();
            model.EligibleAccounts = model.EligibleAccounts.Where(x => x.isClosed == false && x.Balance > 0).ToList();
            if (model.AccountFromId == null)
            {
                return RedirectToAction(nameof(Index));
            }
            if (ModelState.IsValid)
            {
                model.AccountToId = id;
                model.accountFrom = await _accountRepo.SelectById(model.AccountFromId);
                var loan = await _loanRepo.SelectByAccountId(id);
                if (model.Amount > loan.TotalDue)
                {
                    ModelState.AddModelError(string.Empty, "You cannot pay more than what you owe.");
                    return View(model);
                }
                if (model.accountFrom.AccountTypeId == 1)
                {
                    var checkingBl = new CheckingBL(_accountRepo, _transactionRepo);
                    if (await checkingBl.withdraw(model.accountFrom, model.Amount) == false)
                    {
                        ModelState.AddModelError(string.Empty, "Insuffient Funds to complete transaction.");
                        return View(model);
                    }
                }
                if (model.accountFrom.AccountTypeId == 2)
                {
                    model.accountFrom.AccountType = await _typeRepo.SelectById(2);
                    var businessBl = new BusinessBL(_accountRepo, _transactionRepo);
                    await businessBl.withdraw(model.accountFrom, model.Amount);
                }

                loan.TotalDue -= model.Amount;
                await _loanRepo.Update(loan);
                var account = await _accountRepo.SelectById(id);
                if (loan.TotalDue == 0)
                {
                    account.isClosed = true;
                    await _accountRepo.Update(account);
                }
                if (account.Transactions == null)
                {
                    account.Transactions = new List<Transaction>();
                }
                var tran = new Transaction()
                {
                    AccountId = id,
                    Amount = model.Amount,
                    TransactionType = "Payment",
                    DateStamp = DateTime.Now
                };
                account.Transactions.Add(tran);
                await _transactionRepo.Add(tran);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Transactions(int id)
        {
            var model = new TransactionViewModel()
            {
                currentAccountId = id,
            };
            var transactions = await _transactionRepo.SelectAll(id);
            transactions = transactions.Skip(Math.Max(0, transactions.Count - 10)).ToList();
            transactions.Reverse();
            model.Transactions = transactions;
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Transactions(TransactionViewModel model,int id)
        {
            var transactions = await _transactionRepo.SelectAll(id);
            model.currentAccountId = id;
            
            if (model.StartDate == DateTime.MinValue || model.EndDate == DateTime.MinValue)
            {
                transactions = transactions.Skip(Math.Max(0, transactions.Count - 10)).ToList();
                transactions.Reverse();
                model.Transactions = transactions;
                ModelState.AddModelError(string.Empty, "Please enter two dates");
                return View(model);
            }
            if (model.StartDate > model.EndDate)
            {
                transactions = transactions.Skip(Math.Max(0, transactions.Count - 10)).ToList();
                transactions.Reverse();
                model.Transactions = transactions;
                ModelState.AddModelError(string.Empty, "Date are in incorrect order.");
                return View(model);
            }
            model.Transactions = transactions.Where(x => x.DateStamp > model.StartDate && x.DateStamp < model.EndDate).ToList();
            model.Transactions.Reverse();
            return View(model);
        }
        [HttpGet]
        public IActionResult CreateTD()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateTD(LoanViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await GetCurrentUserAsync();
                AccountType accountType = await _typeRepo.SelectById(4);
                var account = new Account()
                {
                    ApplicationUserId = user.Id,
                    AccountTypeId = 4,
                    AccountType = accountType,
                    Customer = user,
                    Balance = model.Amount,
                    isClosed = false,
                    DateCreated = DateTime.Now,
                    Transactions = new List<Transaction>()
                };
                if (account.Customer.Accounts == null)
                {
                    account.Customer.Accounts = new List<Account>();
                }
                if (account.AccountType.Accounts == null)
                {
                    account.AccountType.Accounts = new List<Account>();
                }
                account.AccountType.Accounts.Add(account);
                account.Customer.Accounts.Add(account);
                await _accountRepo.Add(account);
                var lastid = (await _accountRepo.SelectAll(user)).Count;
                var td = new TermDeposit()
                {
                    Account = account,
                    AccountId = lastid,
                    Term = model.Term,
                    TotalDue = model.Amount + ((model.Amount * accountType.InterestRate) * model.Term),
                    isMatured = false
                };
                if (account.TermDeposits == null)
                {
                    account.TermDeposits = new List<TermDeposit>();
                }
                account.TermDeposits.Add(td);
                await _tdRepo.Add(td);
                var tran = new Transaction()
                {
                    AccountId = account.Id,
                    Amount = model.Amount,
                    TransactionType = "Deposit",
                    DateStamp = DateTime.Now
                };
                account.Transactions.Add(tran);
                await _transactionRepo.Add(tran);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult CreateLoan()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateLoan(LoanViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await GetCurrentUserAsync();
                AccountType accountType = await _typeRepo.SelectById(3);
                var account = new Account()
                {
                    ApplicationUserId = user.Id,
                    AccountTypeId = 3,
                    AccountType = accountType,
                    Customer = user,
                    Balance = model.Amount,
                    isClosed = false,
                    DateCreated = DateTime.Now,
                    Transactions = new List<Transaction>()
                };
                if (account.Customer.Accounts == null)
                {
                    account.Customer.Accounts = new List<Account>();
                }
                if (account.AccountType.Accounts == null)
                {
                    account.AccountType.Accounts = new List<Account>();
                }
                account.AccountType.Accounts.Add(account);
                account.Customer.Accounts.Add(account);
                await _accountRepo.Add(account);
                var lastid = (await _accountRepo.SelectAll(user)).Count;
                var loan = new Loan()
                {
                    Account = account,
                    AccountId = lastid,
                    Term = model.Term,
                    TotalDue = model.Amount + ((model.Amount * accountType.InterestRate) * model.Term)
                };
                if (account.Loans == null)
                {
                    account.Loans = new List<Loan>();
                }
                account.Loans.Add(loan);
                await _loanRepo.Add(loan);
                var tran = new Transaction()
                {
                    AccountId = account.Id,
                    Amount = model.Amount,
                    TransactionType = "Loan Issue",
                    DateStamp = DateTime.Now
                };
                account.Transactions.Add(tran);
                await _transactionRepo.Add(tran);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult CreateBusiness()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateBusiness([Bind("Balance")]Account model)
        {
            if (ModelState.IsValid)
            {
                if (model.Balance < 1)
                {
                    ModelState.AddModelError(string.Empty, "You must deposit at least one dollar.");
                    return View(model);
                }
                ApplicationUser user = await GetCurrentUserAsync();
                AccountType accountType = await _typeRepo.SelectById(2);
                var account = new Account()
                {
                    ApplicationUserId = user.Id,
                    AccountTypeId = 2,
                    AccountType = accountType,
                    Customer = user,
                    Balance = model.Balance,
                    isClosed = false,
                    DateCreated = DateTime.Now,
                    Transactions = new List<Transaction>()
                };
                if (account.Customer.Accounts == null)
                {
                    account.Customer.Accounts = new List<Account>();
                }
                if (account.AccountType.Accounts == null)
                {
                    account.AccountType.Accounts = new List<Account>();
                }
                account.AccountType.Accounts.Add(account);
                account.Customer.Accounts.Add(account);
                await _accountRepo.Add(account);
                var tran = new Transaction()
                {
                    AccountId = account.Id,
                    Amount = model.Balance,
                    TransactionType = "Deposit",
                    DateStamp = DateTime.Now
                };
                account.Transactions.Add(tran);
                await _transactionRepo.Add(tran);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult CreateChecking()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateChecking([Bind("Balance")]Account model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await GetCurrentUserAsync();
                AccountType accountType = await _typeRepo.SelectById(1);
                var account = new Account()
                {
                    ApplicationUserId = user.Id,
                    AccountTypeId = 1,
                    AccountType = accountType,
                    Customer = user,
                    Balance = model.Balance,
                    isClosed = false,
                    DateCreated = DateTime.Now,
                    Transactions = new List<Transaction>()
                };
                if (account.Customer.Accounts == null)
                {
                    account.Customer.Accounts = new List<Account>();
                }
                if (account.AccountType.Accounts == null)
                {
                    account.AccountType.Accounts = new List<Account>();
                }
               
                account.AccountType.Accounts.Add(account);
                account.Customer.Accounts.Add(account);
                await _accountRepo.Add(account);
                var tran = new Transaction()
                {
                    AccountId = account.Id,
                    Amount = model.Balance,
                    TransactionType = "Deposit",
                    DateStamp = DateTime.Now
                };
                account.Transactions.Add(tran);
                await _transactionRepo.Add(tran);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
    }
}
