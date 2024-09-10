using System.Text.Json;
using FintechGrupo10.Application.Common.Configurations;
using FintechGrupo10.Application.Common.Repositories;
using FintechGrupo10.Application.Common.Services;
using FintechGrupo10.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace FintechGrupo10.Application.Features.InvestmentProduct.SellProduct
{
    public class SellProductRequestHandler
    (
        ILogger<SellProductRequestHandler> logger,
        IMessagePublisherService messagePublisherService,
        IOptions<RabbitMqConfig> options,
        IRepository<ClienteEntity> repository
    ) : IRequestHandler<SellProductRequest, bool>
    {
        private readonly ILogger<SellProductRequestHandler> _logger = logger;
        private readonly IMessagePublisherService _messagePublisherService = messagePublisherService;
        private readonly RabbitMqConfig _rabbitMqConfig = options.Value;
        private readonly IRepository<ClienteEntity> _clientRepository = repository;

        public async Task<bool> Handle(SellProductRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var client = await _clientRepository.GetByFilterAsync(x => x.Id == request.IdCliente, cancellationToken);

            if (client == null)
                return false;

            var message = JsonSerializer.Serialize(request);

            _messagePublisherService.PublishMessage(message, _rabbitMqConfig.SellProductQueue, true);

            _logger.LogInformation(
                "[SendOrderForProduct] " +
                "[Message was sent successfully] " +
                "[Payload: {message}]",
                message);

            return true;
        }
    }
}
