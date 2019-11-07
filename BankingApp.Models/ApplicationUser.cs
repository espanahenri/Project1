﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankingApp.Models
{
    public class ApplicationUser: IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<CheckingAccount> CheckingAccounts { get; set; }
        public List<BusinessAccount> BusinessAccounts { get; set; }
        public List<Loan> Loans { get; set; }
    }
}
