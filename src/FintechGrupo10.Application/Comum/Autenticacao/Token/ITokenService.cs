using FintechGrupo10.Domain.Entities;

namespace FintechGrupo10.Infrastructure.Autenticacao.Token.Interface;

public interface ITokenService
{
    string GerarToken(Usuario usuario);
}