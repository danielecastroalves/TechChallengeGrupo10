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
        private readonly IRepository<ClienteEntity> _clientRepository;

        public BuyProductRequestHandler(ILogger<BuyProductRequestHandler> logger,
            IMessagePublisherService messagePublisherService,
            IOptions<RabbitMqConfig> options, IRepository<ClienteEntity> repository)
        {
            _logger = logger;
            _messagePublisherService = messagePublisherService;
            _rabbitMqConfig = options.Value;
            _clientRepository = repository;
        }

        public async Task<bool> Handle(BuyProductRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var client = await _clientRepository.GetByFilterAsync(x => x.Id == request.ClientId);
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
            var newPortfolio = new PortfolioEntity
            {
                UsuarioId = userId,
                Nome = "Meu Portfolio",
                Descricao = "Meu portfolio de investimentos pessoal",
                Ativos = new List<Wallet>()
            };
            
            client.Portfolio = newPortfolio;

            await _clientRepository.UpdateAsync(x=> x.Id == client.Id, client, cancellation);
        }
    }
}
