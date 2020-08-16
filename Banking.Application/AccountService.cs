using System;
using System.Collections.Generic;
using System.Linq;
using Banking.Domain.Interfaces;
using Banking.Domain.Models;

namespace Banking.Application
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _repository;

        public AccountService(IAccountRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public IQueryable<Account> GetAccounts()
        {
            return _repository.GetAccounts();
        }
    }
}