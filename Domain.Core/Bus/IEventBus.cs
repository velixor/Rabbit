using System.Threading.Tasks;
using Domain.Core.Commands;
using Domain.Core.Events;

namespace Domain.Core.Bus
{
    public interface IEventBus
    {
        Task SendCommand<T>(T command) where T : Command;

        void Publish<T>(T @event) where T : Event;

        void Subscribe<T, THandler>()
            where T : Event
            where THandler : IEventHandler<T>;
    }
}