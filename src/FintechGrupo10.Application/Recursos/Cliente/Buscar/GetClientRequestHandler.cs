using System.Text.Json;
using FintechGrupo10.Application.Comum.Repositories;
using FintechGrupo10.Domain.Entities;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FintechGrupo10.Application.Recursos.Cliente.Buscar
{
    public class GetClientRequestHandler : IRequestHandler<GetClientRequest, GetClientResponse>
    {
        private readonly IRepository<ClienteEntity> _repositorio;
        private readonly ILogger<GetClientRequestHandler> _logger;

        public GetClientRequestHandler
        (
            IRepository<ClienteEntity> repositorio,
            ILogger<GetClientRequestHandler> logger
        )
        {
            _repositorio = repositorio;
            _logger = logger;
        }

        public async Task<GetClientResponse> Handle(GetClientRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var entity = await _repositorio.GetByFilterAsync(x =>
                x.Documento.Equals(request.Documento) && x.Ativo,
                cancellationToken);

            var response = entity.Adapt<GetClientResponse>();

            _logger.LogInformation(
               "[GetClient] " +
               "[GetClient has been executed successfully] " +
               "[Payload: {entity}]",
               JsonSerializer.Serialize(entity));

            return response;
        }
    }
}
