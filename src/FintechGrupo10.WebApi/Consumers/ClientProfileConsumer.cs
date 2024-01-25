using System.Text;
using FintechGrupo10.Application.Common.Configurations;
using FintechGrupo10.Application.Features.ClientProfile.SetClientProfile;
using MediatR;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace FintechGrupo10.WebApi.Consumers
{
    public class ClientProfileConsumer : BackgroundService
    {
        private readonly IConnection _rabbitConnection;
        private readonly IServiceProvider _serviceProvider;
        private readonly RabbitMqConfig _rabbitMqConfig;

        public ClientProfileConsumer
        (
            IConnection rabbitConnection,
            IServiceProvider serviceProvider,
            IOptions<RabbitMqConfig> options
        )
        {
            _rabbitConnection = rabbitConnection;
            _serviceProvider = serviceProvider;
            _rabbitMqConfig = options.Value;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            using var channel = _rabbitConnection.CreateModel();

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();

                var clientProfileEvent = JsonConvert.DeserializeObject<SetClientProfileEvent>(Encoding.UTF8.GetString(body));

                using var scope = _serviceProvider.CreateScope();
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

                // Enviar um comando para o MediatR
                await mediator.Send(clientProfileEvent!);

                channel.BasicAck(ea.DeliveryTag, false);
            };

            channel.BasicConsume(queue: _rabbitMqConfig.ClientProfileQueue,
                                 autoAck: false,
                                 consumer: consumer);

            await Task.Delay(Timeout.Infinite, cancellationToken);
        }
    }
}
