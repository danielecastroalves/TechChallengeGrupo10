using FintechGrupo10.Domain.Entities;

namespace FintechGrupo10.Application.Comum.Repositorios;

public interface IUsuarioRepositorio : IRepositorio<Usuario>
{
    Task<Usuario> ObterPorLoginESenhaAsync(
        string login,
        string senha,
        CancellationToken cancellationToken = default);
}