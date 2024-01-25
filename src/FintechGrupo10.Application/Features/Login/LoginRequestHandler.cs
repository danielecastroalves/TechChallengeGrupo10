using FintechGrupo10.Application.Common.Auth.Token;
using FintechGrupo10.Application.Common.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FintechGrupo10.Application.Features.Login;

public class LoginRequestHandler : IRequestHandler<LoginRequest, string>
{
    private readonly IUserRepository _repository;
    private readonly ILogger<LoginRequestHandler> _logger;
    private readonly ITokenService _tokenService;

    public LoginRequestHandler
    (
        IUserRepository repository,
        ILogger<LoginRequestHandler> logger,
        ITokenService tokenService
    )
    {
        _repository = repository;
        _logger = logger;
        _tokenService = tokenService;
    }

    public async Task<string> Handle(LoginRequest request, CancellationToken cancellationToken)
    {
        var usuario = await _repository.GetAuthByLoginAndPassword(
            request.Login,
            request.Password,
            cancellationToken);

        if (usuario is null)
            return string.Empty;

        var response = _tokenService.GetUserToken(usuario);

        _logger.LogInformation(
                "[Authenticate] " +
                "[UserToken has been generated successfully] " +
                "[Login: {login}]",
                request.Login);

        return response;
    }
}
