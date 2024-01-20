using FintechGrupo10.Application.Comum.Repositorios;
using FintechGrupo10.Domain.Entidades;
using MediatR;

namespace FintechGrupo10.Application.Recursos.Cliente.Excluir
{
    public class DeleteClientRequestHandler : IRequestHandler<DeleteClientRequest>
    {
        private readonly IRepositorio<ClienteEntity> _repositorio;

        public DeleteClientRequestHandler(IRepositorio<ClienteEntity> repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<Unit> Handle(DeleteClientRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var entity = await _repositorio.ObterPorFiltroAsync(x=> x.Id == request.ClientID, cancellationToken);

            entity.Ativo = false;

            await _repositorio.AtualizarAsync(x=> x.Id == entity.Id, entity, cancellationToken);

            return Unit.Value;
        }
    }
}
