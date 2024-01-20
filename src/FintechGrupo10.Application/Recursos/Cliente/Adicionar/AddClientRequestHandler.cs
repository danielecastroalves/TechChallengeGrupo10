using FintechGrupo10.Application.Comum.Repositorios;
using FintechGrupo10.Domain.Entidades;
using Mapster;
using MediatR;

namespace FintechGrupo10.Application.Recursos.Cliente.Adicionar
{
    public class AddClientRequestHandler : IRequestHandler<AddClientRequest>
    {
        private readonly IRepositorio<ClienteEntity> _repositorio;

        public AddClientRequestHandler(IRepositorio<ClienteEntity> repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<Unit> Handle(AddClientRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var entity = request.Adapt<ClienteEntity>();

            await _repositorio.AdicionarAsync(entity, cancellationToken);

            return Unit.Value;
        }
    }
}
