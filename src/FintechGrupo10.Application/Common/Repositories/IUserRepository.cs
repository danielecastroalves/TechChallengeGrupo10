using FintechGrupo10.Domain.Entities;

namespace FintechGrupo10.Application.Common.Repositories;

public interface IUserRepository : IRepository<Usuario>
{
    Task<Usuario> ObterPorLoginESenhaAsync(
        string login,
        string senha,
        CancellationToken cancellationToken = default);
}
