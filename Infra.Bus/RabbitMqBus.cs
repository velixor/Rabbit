using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Core.Bus;
using Domain.Core.Commands;
using Domain.Core.Events;
using MediatR;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Infra.Bus
{
    public sealed partial class RabbitMqBus : IEventBus
    {
        private const string HostName = "localhost";
        private readonly IMediator _mediator;
        private readonly List<Subscriber> _subscribers;

        public RabbitMqBus(IMediator mediator)
        {
            _mediator = mediator;
            _subscribers = new List<Subscriber>();
        }

        public Task SendCommand<T>(T command) where T : Command
        {
            return _mediator.Send(command);
        }

        public void Publish<T>(T @event) where T : Event
        {
            var connectionFactory = new ConnectionFactory {HostName = HostName};
            using var connection = connectionFactory.CreateConnection();
            using var channel = connection.CreateModel();

            var eventName = @event.GetType().Name;
            channel.QueueDeclare(eventName, exclusive: false, autoDelete: false);

            var message = JsonSerializer.Serialize(@event);
            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(string.Empty, eventName, true, null, body);
        }

        public void Subscribe<TEvent, THandler>() where TEvent : Event where THandler : IEventHandler<TEvent>
        {
            var subscriber = GetOrCreateSubscriber<TEvent>();
            var handlerType = typeof(THandler);

            var isHandlerAdded = subscriber.AddHandler(handlerType);

            if (!isHandlerAdded) throw new ArgumentException($"Handler Type {handlerType.Name} already registered for '{subscriber.EventName}' or something gone wrong");

            StartBasicConsume<TEvent>();
        }

        private Subscriber GetOrCreateSubscriber<TEvent>() where TEvent : Event
        {
            var eventType = typeof(TEvent);

            var subscriber = _subscribers.SingleOrDefault(x => x.EventType == eventType);
            if (subscriber != null) return subscriber;

            subscriber = new Subscriber(typeof(TEvent));
            _subscribers.Add(subscriber);

            return subscriber;
        }

        private void StartBasicConsume<TEvent>() where TEvent : Event
        {
            var connectionFactory = new ConnectionFactory
            {
                HostName = HostName,
                DispatchConsumersAsync = true
            };
            using var connection = connectionFactory.CreateConnection();
            using var channel = connection.CreateModel();

            var eventName = typeof(TEvent).Name;
            channel.QueueDeclare(eventName, false, false, false, null);

            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.Received += ConsumerOnReceived;

            channel.BasicConsume(eventName, true, consumer);
        }

        private async Task ConsumerOnReceived(object sender, BasicDeliverEventArgs e)
        {
            var eventName = e.RoutingKey;
            var message = Encoding.UTF8.GetString(e.Body.ToArray());

            try
            {
                await ProcessEvent(eventName, message).ConfigureAwait(false);
            }
            catch (Exception)
            {
                // ignored
            }
        }

        private async Task ProcessEvent(string eventName, string message)
        {
            var subscriber = _subscribers.SingleOrDefault(x => x.EventName == eventName);
            if (subscriber == null) return;

            var @event = JsonSerializer.Deserialize(message, subscriber.EventType);
            await subscriber.Handle(@event);
        }
    }
}