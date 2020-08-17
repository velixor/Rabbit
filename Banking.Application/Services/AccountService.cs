using System;
using System.Linq;
using Banking.Application.Models;
using Banking.Domain.Commands;
using Banking.Domain.Interfaces;
using Banking.Domain.Models;
using Domain.Core.Bus;

namespace Banking.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IEventBus _bus;
        private readonly IAccountRepository _repository;

        public AccountService(IAccountRepository repository, IEventBus bus)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _bus = bus ?? throw new ArgumentNullException(nameof(bus));
        }

        public IQueryable<Account> GetAccounts()
        {
            return _repository.GetAccounts();
        }

        public void Transfer(AccountTransfer accountTransfer)
        {
            var createTransferCommand = new CreateTransferCommand(accountTransfer.FromAccount, accountTransfer.ToAccount, accountTransfer.TransferAmount);
            _bus.SendCommand(createTransferCommand);
        }
    }
}