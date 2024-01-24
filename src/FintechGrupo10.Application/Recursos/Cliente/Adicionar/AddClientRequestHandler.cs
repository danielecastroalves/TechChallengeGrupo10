using System.Text.Json;
using FintechGrupo10.Application.Comum.Repositories;
using FintechGrupo10.Domain.Entities;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FintechGrupo10.Application.Recursos.Cliente.Adicionar
{
    public class AddClientRequestHandler : IRequestHandler<AddClientRequest>
    {
        private readonly IRepository<ClienteEntity> _repositorio;
        private readonly ILogger<AddClientRequestHandler> _logger;

        public AddClientRequestHandler
        (
            IRepository<ClienteEntity> repositorio,
            ILogger<AddClientRequestHandler> logger
        )
        {
            _repositorio = repositorio;
            _logger = logger;
        }

        public async Task<Unit> Handle(AddClientRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var entity = request.Adapt<ClienteEntity>();

            await _repositorio.AddAsync(entity, cancellationToken);

            _logger.LogInformation(
                "[AddClient] " +
                "[Client has been added successfully] " +
                "[Payload: {entity}]",
                JsonSerializer.Serialize(entity));

            return Unit.Value;
        }
    }
}
