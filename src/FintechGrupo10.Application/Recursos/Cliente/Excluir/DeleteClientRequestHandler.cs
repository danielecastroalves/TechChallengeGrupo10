using FintechGrupo10.Application.Comum.Repositories;
using FintechGrupo10.Domain.Entities;
using MediatR;

namespace FintechGrupo10.Application.Recursos.Cliente.Excluir
{
    public class DeleteClientRequestHandler : IRequestHandler<DeleteClientRequest>
    {
        private readonly IRepository<ClienteEntity> _repositorio;

        public DeleteClientRequestHandler(IRepository<ClienteEntity> repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<Unit> Handle(DeleteClientRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var entity = await _repositorio.GetByFilterAsync(x=> x.Id == request.ClientID, cancellationToken);

            entity.Ativo = false;

            await _repositorio.UpdateAsync(x=> x.Id == entity.Id, entity, cancellationToken);

            return Unit.Value;
        }
    }
}
