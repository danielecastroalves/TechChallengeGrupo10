using System.Text.Json;
using FintechGrupo10.Application.Comum.Configurations;
using FintechGrupo10.Application.Comum.Services;
using FintechGrupo10.Application.Recursos.ClientProfile.SendClientProfile;
using MediatR;
using Microsoft.Extensions.Options;

namespace FintechGrupo10.Application.Recursos.ClientProfile.SendClientProfileCommand
{
    public class SendClientProfileRequestHandler : IRequestHandler<SendClientProfileRequest>
    {
        private readonly IMessagePublisherService _messagePublisherService;
        private readonly RabbitMqConfig _rabbitMqConfig;

        public SendClientProfileRequestHandler
        (
            IMessagePublisherService messagePublisherService,
            IOptions<RabbitMqConfig> options
        )
        {
            _messagePublisherService = messagePublisherService;
            _rabbitMqConfig = options.Value;
        }

        public Task<Unit> Handle(SendClientProfileRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var message = JsonSerializer.Serialize(request);

            _messagePublisherService.PublishMessage(message, _rabbitMqConfig.ClientProfileQueue);

            return Task.FromResult(Unit.Value);
        }
    }
}
