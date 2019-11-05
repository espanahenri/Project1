using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankingApp.Models
{
    public class BankingAppDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CheckingAccount> CheckingAccounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public BankingAppDbContext(DbContextOptions<BankingAppDbContext> context) : base(context)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"data source=.\SQLEXPRESS;initial catalog=BankingAppDB;integrated security=True;MultipleActiveResultSets=True;");
            }
        }
    }
}
