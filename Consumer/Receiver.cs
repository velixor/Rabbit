using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Consumer
{
    internal static class Receiver
    {
        private const string Queue = "BasicQueue";

        private static void Main()
        {
            var connectionFactory = new ConnectionFactory {HostName = "localhost"};

            using var connection = connectionFactory.CreateConnection();
            using var channel = connection.CreateModel();

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += ConsumerOnReceived;

            channel.BasicConsume(Queue, true, consumer);

            Console.WriteLine("End of receiving");
        }

        private static void ConsumerOnReceived(object? sender, BasicDeliverEventArgs e)
        {
            Console.WriteLine($"Message received: {GetString(e.Body)}");
        }

        private static string GetString(in ReadOnlyMemory<byte> body)
        {
            return Encoding.UTF8.GetString(body.ToArray());
        }
    }
}