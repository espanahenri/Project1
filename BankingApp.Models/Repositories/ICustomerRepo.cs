using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp.Models.Repositories
{
    public interface ICustomerRepo
    {
        Task<Customer> GetCustomer(int? Id);
        Task<List<Customer>> GetAllCustomers();
        Task<bool> Add(Customer customer);
        Task<bool> Update(Customer customer);
        Task<bool> Remove(Customer customer);
        
    }
}
