using System.Linq.Expressions;
using FintechGrupo10.Application.Common.Repositories;
using FintechGrupo10.Domain.Entities;
using FintechGrupo10.Infrastructure.Mongo.Contexts.Interfaces;
using MongoDB.Driver;

namespace FintechGrupo10.Infrastructure.Mongo.Repositories;

public class UsuarioRepositorio : GenericRepository<Usuario>, IUserRepository
{
    public UsuarioRepositorio(IMongoContext context) : base(context)
    {
    }

    public async Task<Usuario> ObterPorLoginESenhaAsync(
        string login,
        string senha,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        Expression<Func<ClienteEntity, bool>> filter =
            x => x.Senha == senha && x.Login == login && x.Ativo;

        var queryResut = await _context.GetCollection<ClienteEntity>()
            .FindAsync(filter, cancellationToken: cancellationToken);

        return await queryResut.FirstOrDefaultAsync(cancellationToken);
    }
}
