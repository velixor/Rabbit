using System;
using System.Text;
using RabbitMQ.Client;

namespace Producer
{
    internal static class Sender
    {
        private const string RoutingKey = "BasicQueue";

        private static void Main()
        {
            var connectionFactory = new ConnectionFactory {HostName = "localhost"};

            using var connection = connectionFactory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.BasicPublish("", RoutingKey, false, null, MessageBody());

            Console.WriteLine("Message sent");
        }

        private static ReadOnlyMemory<byte> MessageBody()
        {
            return Encoding.UTF8.GetBytes("Hello world");
        }
    }
}