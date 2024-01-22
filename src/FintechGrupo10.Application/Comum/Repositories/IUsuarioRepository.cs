using FintechGrupo10.Domain.Entities;

namespace FintechGrupo10.Application.Comum.Repositories;

public interface IUsuarioRepository : IRepository<Usuario>
{
    Task<Usuario> ObterPorLoginESenhaAsync(
        string login,
        string senha,
        CancellationToken cancellationToken = default);
}
