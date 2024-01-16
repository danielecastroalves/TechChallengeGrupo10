using FintechGrupo10.Domain.Enums;

namespace FintechGrupo10.Application.Comum.Repositorios
{
    public interface IClienteRepositorio
    {
        Task<bool> AtualizaPerfilInvestimento(string documento, PerfilInvestimento perfil);
    }
}
