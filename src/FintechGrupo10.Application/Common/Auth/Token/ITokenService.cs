using FintechGrupo10.Domain.Entities;

namespace FintechGrupo10.Application.Common.Auth.Token;

public interface ITokenService
{
    string GetUserToken(User usuario);
}
