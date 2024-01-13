using FintechGrupo10.Domain.Entidades;
using System.Linq.Expressions;

namespace FintechGrupo10.Application.Comum.Repositorios
{
    public interface IRepositorio <TEntidade> where TEntidade : EntidadeBase
    {
        Task<Guid> AdicionarAsync
        (
            TEntidade entity,
            CancellationToken cancellationToken = default
        );

        Task<TEntidade> ObterPorFiltroAsync
        (
            Expression<Func<TEntidade, bool>> filter,
            CancellationToken cancellationToken = default
        );

        Task<TEntidade> ObterPorIdAsync
        (
           Guid id,
           CancellationToken cancellationToken = default
        );

        Task<IEnumerable<TEntidade>> ObterListaPorFiltroAsync
        (
           Expression<Func<TEntidade, bool>> filter,
           CancellationToken cancellationToken = default
        );

        Task AtualizarAsync
        (
            Expression<Func<TEntidade, bool>> filter,
            TEntidade entity,
            CancellationToken cancellationToken = default
        );
    }
}
