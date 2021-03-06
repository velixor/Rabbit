﻿using System;
using System.Linq;
using Banking.Data.Context;
using Banking.Domain.Interfaces;
using Banking.Domain.Models;

namespace Banking.Data.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly BankingDbContext _context;

        public AccountRepository(BankingDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IQueryable<Account> GetAccounts()
        {
            return _context.Accounts;
        }
    }
}