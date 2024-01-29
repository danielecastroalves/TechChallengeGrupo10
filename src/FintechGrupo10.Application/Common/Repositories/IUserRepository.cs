using FintechGrupo10.Domain.Entities;

namespace FintechGrupo10.Application.Common.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<User> GetAuthByLoginAndPassword(
        string login,
        string password,
        CancellationToken cancellationToken = default);
}
