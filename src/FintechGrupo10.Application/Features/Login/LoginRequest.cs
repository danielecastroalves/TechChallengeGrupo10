using MediatR;

namespace FintechGrupo10.Application.Features.Login;

public class LoginRequest : IRequest<string>
{
    public string Login { get; set; } = null!;
    public string Password { get; set; } = null!;
}
