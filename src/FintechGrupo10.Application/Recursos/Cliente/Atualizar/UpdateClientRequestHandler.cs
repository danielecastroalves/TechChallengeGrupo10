using FintechGrupo10.Application.Comum.Repositorios;
using FintechGrupo10.Domain.Entities;
using Mapster;
using MediatR;

namespace FintechGrupo10.Application.Recursos.Cliente.Atualizar
{
    public class UpdateClientRequestHandler : IRequestHandler<UpdateClientRequest, ClienteEntity?>
    {
        private readonly IRepositorio<ClienteEntity> _repositorio;
        public UpdateClientRequestHandler(IRepositorio<ClienteEntity> repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<ClienteEntity?> Handle(UpdateClientRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var entity = await _repositorio.ObterPorFiltroAsync(x =>
                x.Id == request.Id,
                cancellationToken);

            entity.Ativo = true;

            entity = request.Adapt<ClienteEntity>();

            await _repositorio.AtualizarAsync(x => x.Id == entity.Id, entity, cancellationToken);

            return entity;
        }
    }
}
