using System;
using System.Collections.Generic;

namespace Infra.Bus
{
    public sealed partial class RabbitMqBus
    {
        private class Subscriber
        {
            public Type EventType { get; }
            public string EventName => EventType.Name;
            public HashSet<Type> Handlers { get; }

            public Subscriber(Type eventType)
            {
                EventType = eventType;
                Handlers = new HashSet<Type>();
            }
        }
    }
}