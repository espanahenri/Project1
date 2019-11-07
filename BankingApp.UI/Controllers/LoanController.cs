using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankingApp.Models;
using BankingApp.Models.Repositories;
using BankingApp.UI.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BankingApp.UI.Controllers
{
    public class LoanController : Controller
    {
        private readonly LoanRepo _repo;
        private readonly UserManager<ApplicationUser> _userManager;
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
        // GET: Loan
        public LoanController(LoanRepo rep, UserManager<ApplicationUser> userManager)
        {
            _repo = rep;
            _userManager = userManager;

        }
        public async Task<IActionResult> GetAccounts()
        {
            var accounts = await _repo.GetAllAccounts(await GetCurrentUserAsync());
            return View(accounts);
        }

        // GET: Loan/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        public IActionResult PayInstallment()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> PayInstallment(PayInstallmentViewModel model, int? id)
        {
            if (ModelState.IsValid)
            {
                var trans = new Transaction()
                {
                    AccountNumber = id,
                    DateStamp = DateTime.Now,
                    Amount = model.Amount,
                    TransactionType = "Payment"
                };
                ApplicationUser user = await GetCurrentUserAsync();
                var account = await _repo.GetAccount(user, id);
                if (account.Transactions == null)
                {
                    account.Transactions = new List<Transaction>();
                }
                if (account.TotalBalance < model.Amount)
                {
                    ModelState.AddModelError(string.Empty, "You cannot pay more than what you owe!");
                    return View(model);
                }
                if (account.TotalBalance == model.Amount)
                {
                    account.isPayed = true;
                }
                account.TotalBalance -= model.Amount;
                account.Transactions.Add(trans);
                await _repo.Update(account);
                return RedirectToAction(nameof(GetAccounts));
            }
            
            return View(model);
        }
            
    

        // GET: Loan/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Loan/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Loan loan)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                ApplicationUser user = await GetCurrentUserAsync();
                var account = new Loan()
                {
                    Balance = loan.Balance,
                    Term = loan.Term,
                    TotalBalance = loan.Balance + ((decimal)0.2 * loan.Balance),
                    InterestRate = 0.2F
                };
                account.PaymentInstallment = account.TotalBalance / account.Term;
                if (user.Loans == null)
                {
                    user.Loans = new List<Loan>();
                }

                user.Loans.Add(account);
                await _repo.Add(account);
                return RedirectToAction(nameof(GetAccounts));

            }
            
            return View(loan);
        }

        // GET: Loan/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Loan/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Loan/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Loan/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}