using System.Linq;
using Banking.Application.Models;
using Banking.Domain.Models;

namespace Banking.Application.Services
{
    public interface IAccountService
    {
        IQueryable<Account> GetAccounts();
        void Transfer(AccountTransfer accountTransfer);
    }
}