using System.Threading.Tasks;
using Domain.Core.Bus;
using Transfer.Domain.Events;

namespace Transfer.Domain.EventHandlers
{
    public class TransferCreatedEventHandler : IEventHandler<TransferCreatedEvent>
    {

        public Task Handle(TransferCreatedEvent @event)
        {
            return Task.CompletedTask;
        }
    }
}