using MediatR;

namespace FintechGrupo10.Application.Recursos.Login;

public class LoginRequest : IRequest<string> // TODO: mudar o tipo de retorno para um objeto de retorno
{
    public string Login { get; set; } = null!;
    public string Senha { get; set; } = null!;
}