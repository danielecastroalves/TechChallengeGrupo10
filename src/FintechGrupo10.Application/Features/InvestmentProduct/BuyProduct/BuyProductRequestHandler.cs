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
    public class BuyProductRequestHandler
    (
        ILogger<BuyProductRequestHandler> logger,
        IMessagePublisherService messagePublisherService,
        IOptions<RabbitMqConfig> options,
        IRepository<ClienteEntity> repository
    ) : IRequestHandler<BuyProductRequest, bool>
    {
        private readonly ILogger<BuyProductRequestHandler> _logger = logger;
        private readonly IMessagePublisherService _messagePublisherService = messagePublisherService;
        private readonly RabbitMqConfig _rabbitMqConfig = options.Value;
        private readonly IRepository<ClienteEntity> _clientRepository = repository;

        public async Task<bool> Handle(BuyProductRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var client = await _clientRepository.GetByFilterAsync(x => x.Id == request.ClientId, cancellationToken);

            if (client == null)
                return false;

            if (client.Portfolio == null)
                await CreatePortfolio(request.ClientId, client, cancellationToken);

            var message = JsonSerializer.Serialize(request);

            _messagePublisherService.PublishMessage(message, _rabbitMqConfig.BuyProductQueue, true);

            _logger.LogInformation(
                "[SendOrderForProduct] " +
                "[Message was sent successfully] " +
                "[Payload: {message}]",
                message);

            return await Task.FromResult(true);
        }

        private async Task CreatePortfolio(Guid userId, ClienteEntity client, CancellationToken cancellation)
        {
            client.Portfolio = new PortfolioEntity
            {
                UsuarioId = userId,
                Nome = "Meu Portfolio",
                Descricao = "Meu portfolio de investimentos pessoal",
                Ativos = []
            };

            await _clientRepository.UpdateAsync(x => x.Id == client.Id, client, cancellation);
        }
    }
}
