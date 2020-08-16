using System.Collections.Generic;
using System.Linq;
using Banking.Domain.Models;

namespace Banking.Application
{
    public interface IAccountService
    {
        IQueryable<Account> GetAccounts();
    }
}