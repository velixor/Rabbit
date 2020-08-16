using System.Collections.Generic;
using Banking.Domain.Models;

namespace Banking.Application
{
    public interface IAccountService
    {
        IEnumerable<Account> GetAccounts();
    }
}