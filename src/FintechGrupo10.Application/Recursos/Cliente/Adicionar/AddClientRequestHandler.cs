using FintechGrupo10.Application.Comum.Repositories;
using FintechGrupo10.Domain.Entities;
using Mapster;
using MediatR;

namespace FintechGrupo10.Application.Recursos.Cliente.Adicionar
{
    public class AddClientRequestHandler : IRequestHandler<AddClientRequest>
    {
        private readonly IRepository<ClienteEntity> _repositorio;

        public AddClientRequestHandler(IRepository<ClienteEntity> repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<Unit> Handle(AddClientRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var entity = request.Adapt<ClienteEntity>();

            await _repositorio.AddAsync(entity, cancellationToken);

            return Unit.Value;
        }
    }
}
