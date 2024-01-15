using FintechGrupo10.Application.Comum.Repositorios;
using FintechGrupo10.Domain.Entidades;
using FintechGrupo10.Infrastructure.Mongo.Contextos.Interfaces;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace FintechGrupo10.Infrastructure.Mongo.Repositorios
{
    public class RepositorioBase<TEntidade> : IRepositorio<TEntidade> where TEntidade : EntidadeBase
    {
        protected readonly IMongoContext _context;

        public RepositorioBase(IMongoContext context)
        {
            _context = context;
        }

        public virtual async Task<Guid> AdicionarAsync
        (
            TEntidade entity,
            CancellationToken cancellationToken = default
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            entity.SetDataInsercao();

            await _context.GetCollection<TEntidade>()
                .InsertOneAsync(entity, cancellationToken: cancellationToken);

            return entity.Id;
        }

        public virtual async Task<TEntidade> ObterPorFiltroAsync
        (
            Expression<Func<TEntidade, bool>> filter,
            CancellationToken cancellationToken = default
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            var queryResut = await _context.GetCollection<TEntidade>()
                .FindAsync(filter, cancellationToken: cancellationToken);

            return await queryResut.FirstOrDefaultAsync(cancellationToken);
        }

        public virtual async Task<TEntidade> ObterPorIdAsync
        (
            Guid id,
            CancellationToken cancellationToken = default
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            var queryResut = await _context.GetCollection<TEntidade>()
                .FindAsync(f => f.Id == id, cancellationToken: cancellationToken);

            return await queryResut.FirstOrDefaultAsync(cancellationToken);
        }

        public virtual async Task<IEnumerable<TEntidade>> ObterListaPorFiltroAsync
        (
            Expression<Func<TEntidade, bool>> filter,
            CancellationToken cancellationToken = default
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            var result = await _context.GetCollection<TEntidade>()
                .FindAsync(filter, cancellationToken: cancellationToken);

            return await result.ToListAsync(cancellationToken);
        }

        public virtual async Task AtualizarAsync
        (
            Expression<Func<TEntidade, bool>> filter,
            TEntidade entity,
            CancellationToken cancellationToken = default
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            entity.SetDataAtualizacao();

            await _context.GetCollection<TEntidade>()
                .ReplaceOneAsync(filter, entity, cancellationToken: cancellationToken);
        }
    }
}