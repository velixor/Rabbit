using System.Linq;
using Banking.Domain.Models;

namespace Banking.Domain.Interfaces
{
    public interface IAccountRepository
    {
        IQueryable<Account> GetAccounts();
    }
}