using System;
using System.Threading.Tasks;
using Domain.Core.Bus;
using Transfer.Data.Repository;
using Transfer.Domain.Events;
using Transfer.Domain.Models;

namespace Transfer.Domain.EventHandlers
{
    public class TransferCreatedEventHandler : IEventHandler<TransferCreatedEvent>
    {
        private readonly ITransferRepository _transferRepository;

        public TransferCreatedEventHandler(ITransferRepository transferRepository)
        {
            _transferRepository = transferRepository ?? throw new ArgumentNullException(nameof(transferRepository));
        }

        public async Task Handle(TransferCreatedEvent @event)
        {
            await _transferRepository.AddTransferLog(new TransferLog
            {
                FromAccount = @event.From,
                ToAccount = @event.To,
                TransferAmount = @event.Amount
            });
        }
    }
}