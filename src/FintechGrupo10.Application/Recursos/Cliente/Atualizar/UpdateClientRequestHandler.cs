using FintechGrupo10.Application.Comum.Repositories;
using FintechGrupo10.Domain.Entities;
using Mapster;
using MediatR;

namespace FintechGrupo10.Application.Recursos.Cliente.Atualizar
{
    public class UpdateClientRequestHandler : IRequestHandler<UpdateClientRequest, ClienteEntity?>
    {
        private readonly IRepository<ClienteEntity> _repositorio;
        public UpdateClientRequestHandler(IRepository<ClienteEntity> repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<ClienteEntity?> Handle(UpdateClientRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var entity = await _repositorio.GetByFilterAsync(x =>
                x.Id == request.Id,
                cancellationToken);

            entity.Ativo = true;

            entity = request.Adapt<ClienteEntity>();

            await _repositorio.UpdateAsync(x => x.Id == entity.Id, entity, cancellationToken);

            return entity;
        }
    }
}
