using System.Text;
using FintechGrupo10.Application.Comum.Services;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace FintechGrupo10.Infrastructure.RabbitMQ
{
    public class MessageConsumerService : IMessageConsumerService
    {
        private readonly IConnection _connection;

        public MessageConsumerService(IConnection connection)
        {
            _connection = connection;
        }

        public void ConsumeMessage()
        {
            using var channel = _connection.CreateModel();

            channel.QueueDeclare(queue: "sua_fila",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (sender, eventArgs) =>
            {
                var body = eventArgs.Body.ToArray();
                var mensagem = Encoding.UTF8.GetString(body);

                Console.WriteLine($"Mensagem recebida: {mensagem}");
            };

            channel.BasicConsume(queue: "sua_fila", autoAck: true, consumer: consumer);
        }
    }
}
