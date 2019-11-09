using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankingApp.Models
{
    public class BankingAppDbContext : IdentityDbContext<ApplicationUser>
    {
        
        public DbSet<CheckingAccount> CheckingAccounts { get; set; }
        public DbSet<CheckingTransaction> CheckingAccountTransactions { get; set; }

        public DbSet<BusinessTransaction> BusinessAccountTransactions { get; set; }
        public DbSet<BusinessAccount> BusinessAccounts { get; set; }

        public DbSet<LoanTransaction> LoanTransactions { get; set; }
        public DbSet<Loan> Loans { get; set; }

        public BankingAppDbContext(DbContextOptions<BankingAppDbContext> context) : base(context){}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"data source=.\SQLEXPRESS;initial catalog=BankingAppDB;integrated security=True;MultipleActiveResultSets=True;");
            }
        }
    }
}
