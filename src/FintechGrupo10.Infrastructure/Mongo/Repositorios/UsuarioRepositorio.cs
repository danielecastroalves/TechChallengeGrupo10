using FintechGrupo10.Application.Comum.Repositorios;
using FintechGrupo10.Domain.Entidades;
using FintechGrupo10.Infrastructure.Mongo.Contextos.Interfaces;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace FintechGrupo10.Infrastructure.Mongo.Repositorios;

public class UsuarioRepositorio : RepositorioBase<Usuario>, IUsuarioRepositorio
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

        Expression<Func<Usuario, bool>> filter =
            x => x.Senha == senha && x.Login == login && x.Ativo;

        var queryResut = await _context.GetCollection<Usuario>()
            .FindAsync<Usuario>(filter, cancellationToken: cancellationToken);

        return await queryResut.FirstOrDefaultAsync(cancellationToken);
    }
}