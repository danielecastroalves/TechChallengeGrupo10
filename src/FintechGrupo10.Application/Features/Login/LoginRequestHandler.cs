using FintechGrupo10.Application.Common.Auth.Token;
using FintechGrupo10.Application.Common.Repositories;
using MediatR;

namespace FintechGrupo10.Application.Features.Login;

public class LoginRequestHandler : IRequestHandler<LoginRequest, string>
{
    private readonly IUserRepository _usuarioRepositorio;
    private readonly ITokenService _tokenService;

    public LoginRequestHandler(
        IUserRepository usuarioRepositorio,
        ITokenService tokenService)
    {
        _usuarioRepositorio = usuarioRepositorio;
        _tokenService = tokenService;
    }

    public async Task<string> Handle(LoginRequest request, CancellationToken cancellationToken)
    {
        var usuario = await _usuarioRepositorio.ObterPorLoginESenhaAsync(
            request.Login,
            request.Senha,
            cancellationToken);

        if (usuario is null)
            return string.Empty;

        return _tokenService.GetUserToken(usuario);
    }
}
