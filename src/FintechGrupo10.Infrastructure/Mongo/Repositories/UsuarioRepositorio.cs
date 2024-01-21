using System.Linq.Expressions;
using FintechGrupo10.Application.Comum.Repositories;
using FintechGrupo10.Domain.Entities;
using FintechGrupo10.Infrastructure.Mongo.Contexts.Interfaces;
using MongoDB.Driver;

namespace FintechGrupo10.Infrastructure.Mongo.Repositories;

public class UsuarioRepositorio : GenericRepository<Usuario>, IUsuarioRepository
{
    public UsuarioRepositorio(IMongoContext context) : base(context) { }

    public async Task<Usuario> ObterPorLoginESenhaAsync(
        string login,
        string senha,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        Expression<Func<Usuario, bool>> filter =
            x => x.Senha == senha && x.Login == login && x.Ativo;

        var queryResut = await _context.GetCollection<Usuario>()
            .FindAsync<Usuario>(filter, cancellationToken: cancellationToken);

        return await queryResut.FirstOrDefaultAsync(cancellationToken);
    }
}
