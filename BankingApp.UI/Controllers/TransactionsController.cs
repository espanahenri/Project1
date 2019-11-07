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
    
    public class TransactionsController : Controller
    {
        private readonly TransactionRepo _repo;
        //private readonly UserManager<ApplicationUser> _userManager;
        //private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
        public TransactionsController(TransactionRepo rep)
        {
            _repo = rep;
            //_userManager = userManager;

        }
        // GET: Transactions
        public ActionResult Index()
        {
            return View();
        }

        // GET: Transactions/Details/5
        public ActionResult Transactions()
        {
            
            return View();
        }
        public async Task<ActionResult> GetTransactions(TransactionViewModel model, int id)
        {
            var transactions = await _repo.GetAllTransactions(id);
            var datedtransactions = transactions.Where(x => x.DateStamp > model.StartDate && x.DateStamp < model.EndDate);
            TempData["data"] = id;
            return View(datedtransactions);
        }
        public async Task<ActionResult> GetAllTransactions(int id)
        {

            var transactions = await _repo.GetAllTransactions(id);
            TempData["data"] = id;

            return View(transactions);
        }

        // GET: Transactions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Transactions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Transactions/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Transactions/Edit/5
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

        // GET: Transactions/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Transactions/Delete/5
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