using System.Text.Json;
using FintechGrupo10.Application.Common.Configurations;
using FintechGrupo10.Application.Common.Repositories;
using FintechGrupo10.Application.Common.Services;
using FintechGrupo10.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace FintechGrupo10.Application.Features.InvestmentProduct.BuyProduct
{
    public class BuyProductRequestHandler : IRequestHandler<BuyProductRequest, bool>
    {
        private readonly ILogger<BuyProductRequestHandler> _logger;
        private readonly IMessagePublisherService _messagePublisherService;
        private readonly RabbitMqConfig _rabbitMqConfig;
        private readonly IRepository<ClienteEntity> _repository;

        public BuyProductRequestHandler(ILogger<BuyProductRequestHandler> logger,
            IMessagePublisherService messagePublisherService,
            IOptions<RabbitMqConfig> options, IRepository<ClienteEntity> repository)
        {
            _logger = logger;
            _messagePublisherService = messagePublisherService;
            _rabbitMqConfig = options.Value;
            _repository = repository;
        }

        public async Task<bool> Handle(BuyProductRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var entity = await _repository.GetByFilterAsync(x => x.Id == request.ClientId);
            if (entity == null)
                return false;

            var message = JsonSerializer.Serialize(request);

            _messagePublisherService.PublishMessage(message, _rabbitMqConfig.BuyProductQueue, true);

            _logger.LogInformation(
                "[SendOrderForProduct] " +
                "[Message was sent successfully] " +
                "[Payload: {message}]",
                message);

            return await Task.FromResult(true);
        }
    }
}
