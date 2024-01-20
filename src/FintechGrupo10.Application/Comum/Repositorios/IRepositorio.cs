using FintechGrupo10.Domain.Entities;
using System.Linq.Expressions;

namespace FintechGrupo10.Application.Comum.Repositorios
{
    public interface IRepositorio <TEntity> where TEntity : Entity
    {
        Task<Guid> AdicionarAsync
        (
            TEntity entity,
            CancellationToken cancellationToken = default
        );

        Task<TEntity> ObterPorFiltroAsync
        (
            Expression<Func<TEntity, bool>> filter,
            CancellationToken cancellationToken = default
        );

        Task<TEntity> ObterPorIdAsync
        (
           Guid id,
           CancellationToken cancellationToken = default
        );

        Task<IEnumerable<TEntity>> ObterListaPorFiltroAsync
        (
           Expression<Func<TEntity, bool>> filter,
           CancellationToken cancellationToken = default
        );

        Task AtualizarAsync
        (
            Expression<Func<TEntity, bool>> filter,
            TEntity entity,
            CancellationToken cancellationToken = default
        );

        Task<bool> DeletarPorIdAsync
        (
           Guid id,
           CancellationToken cancellationToken = default
        );
    }
}
