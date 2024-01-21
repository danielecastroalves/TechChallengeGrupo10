using FintechGrupo10.Application.Comum.Repositories;
using FintechGrupo10.Domain.Entities;
using Mapster;
using MediatR;

namespace FintechGrupo10.Application.Recursos.Cliente.Buscar
{
    public class GetClientRequestHandler : IRequestHandler<GetClientRequest, GetClientResponse>
    {
        private readonly IRepository<ClienteEntity> _repositorio;
        public GetClientRequestHandler(IRepository<ClienteEntity> repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<GetClientResponse> Handle(GetClientRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var entity = await _repositorio.GetByFilterAsync(x =>
                x.Documento.Equals(request.Documento) && x.Ativo,
                cancellationToken);            

            var response = entity.Adapt<GetClientResponse>();

            return response;
        }
    }
}
