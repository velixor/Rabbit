using Domain.Core.Bus;
using Microsoft.AspNetCore.Builder;
using Transfer.Domain.EventHandlers;
using Transfer.Domain.Events;

namespace Transfer.Domain.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void SubscribeTransferCreatedEventHandler(this IApplicationBuilder app)
        {
            var eventBus = (IEventBus) app.ApplicationServices.GetService(typeof(IEventBus));
            eventBus.Subscribe<TransferCreatedEvent, TransferCreatedEventHandler>();
        }
    }
}