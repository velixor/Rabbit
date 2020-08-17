// using System;
// using System.Collections.Generic;
// using System.Reflection;
// using System.Threading.Tasks;
// using Domain.Core.Bus;
//
// namespace Infra.Bus
// {
//     public sealed partial class RabbitMqBus
//     {
//         private class Subscriber
//         {
//             private readonly MethodInfo _handle;
//             private readonly Dictionary<Type, object> _handlers;
//
//             public Subscriber(Type eventType)
//             {
//                 EventType = eventType;
//                 _handlers = new Dictionary<Type, object>();
//                 _handle = GetHandlerMethod(eventType) ?? throw new ArgumentException($"Cannot get method 'Handle' for Event {eventType.Name}");
//             }
//
//             public Type EventType { get; }
//             public string EventName => EventType.Name;
//
//             private MethodInfo? GetHandlerMethod(Type eventType)
//             {
//                 var eventHandlerType = typeof(IEventHandler<>).MakeGenericType(eventType);
//                 return eventHandlerType.GetMethod("Handle");
//             }
//
//             public bool AddHandler(Type handlerType)
//             {
//                 if (_handlers.ContainsKey(handlerType)) return false;
//
//                 var handler = Activator.CreateInstance(handlerType);
//                 if (handler == null) return false;
//
//                 _handlers.Add(handlerType, handler);
//                 return true;
//             }
//
//             public async Task Handle(object @event)
//             {
//                 foreach (var (_, handler) in _handlers) await ((Task) _handle.Invoke(handler, new[] {@event}))!;
//             }
//         }
//     }
// }