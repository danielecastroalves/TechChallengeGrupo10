using System.Text.Json;
using FintechGrupo10.Application.Common.Repositories;
using FintechGrupo10.Domain.Entities;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FintechGrupo10.Application.Features.InvestmentProduct.AddInvestmentProduct
{
    internal class AddInvestmentProductRequestHandler : IRequestHandler<AddInvestmentProductRequest, Guid>
    {
        private readonly IRepository<ProductEntity> _repositorio;
        private readonly ILogger<AddInvestmentProductRequestHandler> _logger;

        public AddInvestmentProductRequestHandler
        (
            IRepository<ProductEntity> repositorio,
            ILogger<AddInvestmentProductRequestHandler> logger
        )
        {
            _repositorio = repositorio;
            _logger = logger;
        }

        public async Task<Guid> Handle(AddInvestmentProductRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var entity = request.Adapt<ProductEntity>();

            var response = await _repositorio.AddAsync(entity, cancellationToken);

            _logger.LogInformation(
                "[AddInvestmentProduct] " +
                "[Product has been added successfully] " +
                "[ProductId: {productId}] " +
                "[Payload: {entity}]",
                response.ToString(),
                JsonSerializer.Serialize(entity));

            return response;
        }
    }
}
