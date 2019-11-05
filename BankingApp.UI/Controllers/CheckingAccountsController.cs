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

namespace BankingApp.UI.Controllers
{
    public class CheckingAccountsController : Controller
    {
        private readonly CheckingAccountRepo _repo;
        private readonly UserManager<ApplicationUser> _userManager;
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        public CheckingAccountsController(CheckingAccountRepo rep, UserManager<ApplicationUser> userManager)
        {
            _repo = rep;
            _userManager = userManager;
            
        }

        // GET: Customers
        //public async Task<IActionResult> Index()
        //{
        //return View(await _repo.GetAllAccounts());
        //}

        [HttpGet]
        public IActionResult Deposit()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Deposit(DepositViewModel model,int? id)
        {
            if (ModelState.IsValid)
            {
                var trans = new Transaction()
                {
                    AccountNumber = id,
                    DateStamp = DateTime.Now,
                    Amount = model.Amount,
                    TransactionType = "Deposit"
                };
                ApplicationUser user = await GetCurrentUserAsync();
                var account = await _repo.GetAccount(user, id);
                if (account.Transactions == null)
                {
                    account.Transactions = new List<Transaction>();
                }
                account.Balance += model.Amount;
                account.Transactions.Add(trans);
                await _repo.Update(account);
                return RedirectToAction(nameof(GetAccounts));
            }
            return View(model);
            
        }
        [HttpGet]
        public IActionResult Withdraw()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Withdraw(WithdrawViewModel model, int? id)
        {
            if (ModelState.IsValid)
            {

                ApplicationUser user = await GetCurrentUserAsync();
                var account = await _repo.GetAccount(user, id);
                if(model.Amount > account.Balance)
                {
                    ModelState.AddModelError(string.Empty, "You cannot withdraw more than what you have!");
                    return View(model);
                }
                account.Balance -= model.Amount;
                await _repo.Update(account);
                return RedirectToAction(nameof(GetAccounts));
            }
            return View(model);

        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _repo.GetAccount(id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CheckingAccount model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await GetCurrentUserAsync();
                var account = new CheckingAccount()
                {
                    Balance = model.Balance
                };
                if (user.CheckingAccounts == null)
                {
                    user.CheckingAccounts = new List<CheckingAccount>();
                }
               
                user.CheckingAccounts.Add(account);
                await _repo.Add(account);
                return RedirectToAction(nameof(GetAccounts));
                
            }
            return View(model);
        }
       // [HttpGet]
        public async Task<IActionResult> GetAccounts()
        {
            var accounts = await _repo.GetAllAccounts(await GetCurrentUserAsync());
            return View(accounts);
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _repo.GetAccount(id);
            if (account == null)
            {
                return NotFound();
            }
            return View(account);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Balance")] CheckingAccount account)
        {
            if (id != account.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _repo.Update(account);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountExists(account.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(account);
        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _repo.GetAccount(id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var account = await _repo.GetAccount(id);
            await _repo.Remove(account);
            return RedirectToAction(nameof(Index));
        }

        private bool AccountExists(int id)
        {
            return _repo.AccountExists(id);
        }
    }
}
