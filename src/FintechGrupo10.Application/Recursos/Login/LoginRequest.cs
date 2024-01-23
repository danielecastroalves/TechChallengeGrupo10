using MediatR;

namespace FintechGrupo10.Application.Recursos.Login;

public class LoginRequest : IRequest<string>
{
    public string Login { get; set; } = null!;
    public string Senha { get; set; } = null!;
}
