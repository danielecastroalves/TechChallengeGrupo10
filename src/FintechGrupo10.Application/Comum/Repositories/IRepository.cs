using FintechGrupo10.Domain.Entities;
using System.Linq.Expressions;

namespace FintechGrupo10.Application.Comum.Repositories
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        Task<Guid> AddAsync
        (
            TEntity entity,
            CancellationToken cancellationToken = default
        );

        Task<TEntity> GetByFilterAsync
        (
            Expression<Func<TEntity, bool>> filter,
            CancellationToken cancellationToken = default
        );

        Task<TEntity> GetByIdAsync
        (
           Guid id,
           CancellationToken cancellationToken = default
        );

        Task<IEnumerable<TEntity>> GetListByFilterAsync
        (
           Expression<Func<TEntity, bool>> filter,
           CancellationToken cancellationToken = default
        );

        Task UpdateAsync
        (
            Expression<Func<TEntity, bool>> filter,
            TEntity entity,
            CancellationToken cancellationToken = default
        );

        Task<bool> DeleteByIdAsync
        (
           Guid id,
           CancellationToken cancellationToken = default
        );
    }
}
