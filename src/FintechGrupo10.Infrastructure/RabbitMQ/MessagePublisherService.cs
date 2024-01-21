using System.Text;
using FintechGrupo10.Application.Comum.Services;
using RabbitMQ.Client;

namespace FintechGrupo10.Infrastructure.RabbitMQ
{
    public class MessagePublisherService : IMessagePublisherService
    {
        private readonly IConnection _connection;

        public MessagePublisherService(IConnection connection)
        {
            _connection = connection;
        }

        public void PublishMessage(string message, string queueName)
        {
            using var channel = _connection.CreateModel();

            channel.QueueDeclare(queue: queueName,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: "",
                                 routingKey: queueName,
                                 basicProperties: null,
                                 body: body);

        }
    }
}
