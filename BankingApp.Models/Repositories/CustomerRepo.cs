using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp.Models.Repositories
{
    public class CustomerRepo : ICustomerRepo
    {
        private BankingAppDbContext _context;
        public CustomerRepo(BankingAppDbContext ctx)
        {
            _context = ctx;
        }
        public async Task<Customer> GetCustomer(int? id)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(m => m.Id == id);
            return customer;
        }
        public async Task<List<Customer>> GetAllCustomers()
        {
            var customers = await _context.Customers.ToListAsync();
            return customers;
        }
        public async Task<bool> Add(Customer customer)
        {
            _context.Add(customer);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Update(Customer customer)
        {
            _context.Update(customer);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Remove(Customer customer)
        {
            _context.Remove(customer);
            await _context.SaveChangesAsync();
            return true;
        }
        public bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.Id == id);
        }
    }
}
